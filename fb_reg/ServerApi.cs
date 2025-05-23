﻿using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;
using System.IO;

using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Net;
using HttpRequest;
using RestSharp;
using static fb_reg.Utility;
using System.Net.Cache;

namespace fb_reg
{

    public class ServerApi
    {

        //public static string SERVER_HOST = "https://fbtool.tech-dev.work";
        //public static string SERVER_HOST = "https://fbtool.tech-dev.work";
        //public static string SERVER_HOST = "https://fbtool.tech-dev.me";
        //public static string SERVER_HOST = "http://192.168.1.32";
        public static string SERVER_HOST = "http://192.168.1.32";


        public class ResponseMaxcloneMail
        {
            [JsonProperty("Code")]
            public int Code { get; set; }
            [JsonProperty("Message")]
            public string Message { get; set; }

            [JsonProperty("Data")]
            public DataMaxcloneMail Data { get; set; }
        }

        public class DataMaxcloneMail
        {
            [JsonProperty("Emails")]
            public DataMaxcloneMailDetail[] Emails { get; set; }
        }
        public class DataMaxcloneMailDetail
        {
            [JsonProperty("Email")]
            public string Email { get; set; }
            [JsonProperty("Password")]
            public string Password { get; set; }
            public string toString()
            {
                return Email + "|" + Password;
            }
        }
        public static ResponseMaxcloneMail GetMaxcloneMail(string key, string mailType, int count)
        {
            try
            {
                string apiGetHotMail = string.Format("https://api.hotmailbox.me/mail/buy?apikey={0}&mailcode={1}&quantity={2}", key, mailType, count);
                var request = (HttpWebRequest)WebRequest.Create(apiGetHotMail);
                request.Method = "GET";
                request.Accept = "application/json";
                request.ContentType = "application/json; charset=utf-8";

                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return JsonConvert.DeserializeObject<ResponseMaxcloneMail>(responseString);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return null;
        }
        public class Gmail30MinRes
        {
            [JsonProperty("success")]
            public Gmail30MinResData success { get; set; }
        }

        public class Gmail30MinResData
        {
            [JsonProperty("status")]
            public string status { get; set; }

            [JsonProperty("products")]
            public string[] products { get; set; }

        }

        public class MailOtp
        {
            [JsonProperty("code")]
            public int code { get; set; }


            [JsonProperty("message")]
            public string message { get; set; }

            [JsonProperty("orders")]
            public MailOtpOrder[] orders { get; set; }
        }
        public class MailOtpCode
        {
            [JsonProperty("code")]
            public int code { get; set; }


            [JsonProperty("message")]
            public string message { get; set; }

            [JsonProperty("order")]
            public MailOtpOrder order { get; set; }
        }

        public class MailOtpOrder
        {
            [JsonProperty("id")]
            public int id { get; set; }

            [JsonProperty("service_id")]
            public int service_id { get; set; }


            [JsonProperty("email")]
            public string email { get; set; }

            [JsonProperty("otp")]
            public string otp { get; set; }
        }

        public static MailObject GetMailOtp()
        {
            try
            {
                var client = new RestClient("https://mailotp.com/api/");
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("v1/create-order");
                
                request.AddParameter("apikey", "714a44e4-dd1e-46f8-a392-580eab8af4bf");
                request.AddParameter("service_id", 6);
                request.AddParameter("count", 1);


                var response = client.Get(request);
                var content = response.Content; // Raw content as string

                try
                {
                    MailOtp data = JsonConvert.DeserializeObject<MailOtp>(content);

                    if (data != null && data.code == 200 && data.orders != null && data.orders.Length > 0 && !string.IsNullOrEmpty(data.orders[0].email))
                    {
                        MailObject mail = new MailObject();
                        mail.email = data.orders[0].email;
                        mail.password = Constant.GMAIL_SELL_GMAIL;
                        mail.orderId = data.orders[0].id;
                        return mail;
                    } else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
            }
            catch (Exception eee)
            {
                return null;
            }
            return null;
        }

        public class GmailOtpDatarequest
        {
            [JsonProperty("serviceId")]
            public int serviceId { get; set; }
        }
        public class GmailOtpResponse
        {
            [JsonProperty("code")]
            public int code { get; set; }

            [JsonProperty("message")]
            public string message { get; set; }

            [JsonProperty("data")]
            public GmailOtpDataResponse data { get; set; }
        }
        public class GmailOtpDataResponse
        {
            [JsonProperty("email")]
            public string email { get; set; }
            [JsonProperty("service")]
            public string service { get; set; }
            [JsonProperty("getCodeID")]
            public int getCodeID { get; set; }
        }

        public static MailObject GetGmailOtp()
        {
            try
            {
                GmailOtpDatarequest dataRequest = new GmailOtpDatarequest();
                dataRequest.serviceId = 1;
                var client = new RestClient("https://api.gmailotp.com/api/");
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("v1/mail-rent-services/ex-forward/rent-mail", Method.POST);
                request.AddHeader("X-API-TOKEN", "WVkyQkhBR0lQVzRJMElYOE5QUVM=");
                    
                request.AddJsonBody(dataRequest);

                var response = client.Execute(request);
                var content = response.Content; // Raw content as string

                try
                {
                    GmailOtpResponse data = JsonConvert.DeserializeObject<GmailOtpResponse>(content);

                    if (data != null && data.code == 0 && data.data != null && !string.IsNullOrEmpty(data.data.email))
                    {
                        MailObject mail = new MailObject();
                        mail.email = data.data.email;
                        mail.password = Constant.GMAIL_SELL_GMAIL;
                        mail.orderId = data.data.getCodeID;
                        return mail;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
            }
            catch (Exception eee)
            {
                return null;
            }
            return null;


        }

        public class HotmailDataRequest
        {
            [JsonProperty("email")]
            public string email { get; set; }

            [JsonProperty("refresh_token")]
            public string refresh_token { get; set; }

            [JsonProperty("client_id")]
            public string client_id { get; set; }

            //[JsonProperty("type")]
            //public string type { get; set; }
        }


        public class HotmailDataResponse
        {
            [JsonProperty("email")]
            public string email { get; set; }

            [JsonProperty("code")]
            public string code { get; set; }
            [JsonProperty("error_message")]
            public string errorMessage { get; set; }

            [JsonProperty("status")]
            public bool status { get; set; }

            [JsonProperty("messages")]
            public Message[] messages { get; set; }
        }

        public class Message
        {
            [JsonProperty("subject")]
            public string subject { get; set; }

            
        }
        //public static string GetHotmailOtpByOAuth2(string server, MailObject mail)
        //{
        //    try
        //    {
        //        HotmailDataRequest dataRequest = new HotmailDataRequest();
        //        dataRequest.email = mail.email;
        //        dataRequest.refresh_token = mail.refreshToken;
        //        dataRequest.client_id = mail.clientId;
        //        dataRequest.type = "facebook";
        //        var client = new RestClient("https://tools.dongvanfb.net/api/");
        //        client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
        //        var request = new RestRequest("get_code_oauth2", Method.POST);
        //        request.AddHeader("X-API-TOKEN", "WVkyQkhBR0lQVzRJMElYOE5QUVM=");

        //        request.AddJsonBody(dataRequest);

        //        var response = client.Execute(request);
        //        var content = response.Content; // Raw content as string

        //        try
        //        {
        //            HotmailDataResponse data = JsonConvert.DeserializeObject<HotmailDataResponse>(content);
        //            if (data!= null)
        //            {
        //                Console.WriteLine(data.content);
        //            }

        //            if (data != null && data.status && !string.IsNullOrEmpty(data.code))
        //            {

        //                return data.code;
        //            }
        //            else
        //            {
        //                return null;
        //            }
        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception eee)
        //    {
        //        return null;
        //    }
        //    return null;


        //}

        public static string GetOtpByOAuth2(MailObject inMmail)
        {

            string otp = GetHotmailOtpByOAuth2Unlimit(inMmail);
            if (!string.IsNullOrEmpty(otp))
            {
                return otp;
            }
            if (inMmail.otpVandong)
            {
                return GetHotmailOtpByOAuth2Vandong(inMmail);
            }
            string code = "";
            string accessToken = inMmail.accessToken;
            if (string.IsNullOrEmpty(accessToken))
            {
                accessToken = ServerApi.GetAccessToken(inMmail.clientId, inMmail.refreshToken);
            }
            List<string> subjects = ReceiveMailByOauth(inMmail.email, accessToken);
            
            ////truy xuất nội dung từng mail
            foreach (string mail in subjects)
            {
                Console.WriteLine("subject:" + mail);
                code = FindCodeInSubject(mail);
                if (!string.IsNullOrEmpty(code) && code != Constant.FAIL)
                {
                    return code;
                }
            }
            return code;
        }

        public static MailObject CheckHotmailByVandong(MailObject mail)
        {
            try
            {
                HotmailDataRequest dataRequest = new HotmailDataRequest();
                dataRequest.email = mail.email;
                dataRequest.refresh_token = mail.refreshToken;
                dataRequest.client_id = mail.clientId;
                //dataRequest.type = "facebook";
                var client = new RestClient("https://tools.dongvanfb.net/api/");
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("get_messages_oauth2", Method.POST);
                //request.AddHeader("X-API-TOKEN", "WVkyQkhBR0lQVzRJMElYOE5QUVM=");

                request.AddJsonBody(dataRequest);

                var response = client.Execute(request);
                var content = response.Content; // Raw content as string
                Console.WriteLine("all mail:" + content);
                try
                {
                    HotmailDataResponse data = JsonConvert.DeserializeObject<HotmailDataResponse>(content);


                    if (data != null && data.status && data.messages != null && data.messages.Length > 0)
                    {
                        mail.status = "";
                        return mail;
                    }
                    else if (data != null && !string.IsNullOrEmpty(data.errorMessage) && data.errorMessage.Contains("không có email"))
                    {
                        mail.status = "";
                        return mail;
                    }
                    else
                    {
                        mail.status = Constant.FAIL;
                        return mail;
                    }
                }
                catch
                {
                    mail.status = Constant.FAIL;
                    return mail;
                }
            }
            catch (Exception eee)
            {
                mail.status = Constant.FAIL;
                return mail;
            }
            mail.status = Constant.FAIL;
            return mail;
        }
        public static MailObject CheckHotmailByUnlimit(MailObject mail)
        {
            try
            {
                HotmailDataRequest dataRequest = new HotmailDataRequest();
                dataRequest.email = mail.email;
                dataRequest.refresh_token = mail.refreshToken;
                dataRequest.client_id = mail.clientId;
                //dataRequest.type = "facebook";
                var client = new RestClient("https://tool.unlimitmail.com/api/");
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("get_messages_oauth2", Method.POST);
                //request.AddHeader("X-API-TOKEN", "WVkyQkhBR0lQVzRJMElYOE5QUVM=");

                request.AddJsonBody(dataRequest);

                var response = client.Execute(request);
                var content = response.Content; // Raw content as string
                Console.WriteLine("all mail:" + content);
                try
                {
                    HotmailDataResponse data = JsonConvert.DeserializeObject<HotmailDataResponse>(content);


                    if (data != null && data.status && data.messages != null && data.messages.Length > 0)
                    {
                        mail.status = "";
                        return mail;
                    }
                    else if (data != null && !string.IsNullOrEmpty(data.errorMessage) && data.errorMessage.Contains("không có email"))
                    {
                        mail.status = "";
                        return mail;
                    }
                    else
                    {
                        mail.status = Constant.FAIL;
                        return mail;
                    }
                }
                catch
                {
                    mail.status = Constant.FAIL;
                    return mail;
                }
            }
            catch (Exception eee)
            {
                mail.status = Constant.FAIL;
                return mail;
            }
            mail.status = Constant.FAIL;
            return mail;
        }

        public static string GetHotmailOtpByOAuth2Vandong(MailObject mail)
        {
            try
            {
                HotmailDataRequest dataRequest = new HotmailDataRequest();
                dataRequest.email = mail.email;
                dataRequest.refresh_token = mail.refreshToken;
                dataRequest.client_id = mail.clientId;
                //dataRequest.type = "facebook";
                var client = new RestClient("https://tool.unlimitmail.com/api/");
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("get_messages_oauth2", Method.POST);
                //request.AddHeader("X-API-TOKEN", "WVkyQkhBR0lQVzRJMElYOE5QUVM=");

                request.AddJsonBody(dataRequest);

                var response = client.Execute(request);
                var content = response.Content; // Raw content as string
                Console.WriteLine("all mail:" + content);
                try
                {
                    HotmailDataResponse data = JsonConvert.DeserializeObject<HotmailDataResponse>(content);
                    

                    if (data != null && data.status && data.messages != null && data.messages.Length > 0)
                    {
                        for (int i = 0; i < data.messages.Length; i ++)
                        {
                            Console.WriteLine("subject:" + data.messages[i].subject);
                            string code = FindCodeInSubject(data.messages[i].subject);
                            if (!string.IsNullOrEmpty(code) && code != Constant.FAIL)
                            {
                                return code;
                            }
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
            }
            catch (Exception eee)
            {
                return null;
            }
            return null;
        }
        public static string GetHotmailOtpByOAuth2Unlimit(MailObject mail)
        {
            try
            {
                HotmailDataRequest dataRequest = new HotmailDataRequest();
                dataRequest.email = mail.email;
                dataRequest.refresh_token = mail.refreshToken;
                dataRequest.client_id = mail.clientId;
                //dataRequest.type = "facebook";
                var client = new RestClient("https://tools.dongvanfb.net/api/");
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("get_messages_oauth2", Method.POST);
                //request.AddHeader("X-API-TOKEN", "WVkyQkhBR0lQVzRJMElYOE5QUVM=");

                request.AddJsonBody(dataRequest);

                var response = client.Execute(request);
                var content = response.Content; // Raw content as string
                Console.WriteLine("all mail:" + content);
                try
                {
                    HotmailDataResponse data = JsonConvert.DeserializeObject<HotmailDataResponse>(content);


                    if (data != null && data.status && data.messages != null && data.messages.Length > 0)
                    {
                        for (int i = 0; i < data.messages.Length; i++)
                        {
                            Console.WriteLine("subject:" + data.messages[i].subject);
                            string code = FindCodeInSubject(data.messages[i].subject);
                            if (!string.IsNullOrEmpty(code) && code != Constant.FAIL)
                            {
                                return code;
                            }
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
            }
            catch (Exception eee)
            {
                return null;
            }
            return null;
        }

        public class Gmail48hDataresponse
        {
            [JsonProperty("status")]
            public string status { get; set; }
            [JsonProperty("id")]
            public int id { get; set; }
            [JsonProperty("email")]
            public string email { get; set; }
            [JsonProperty("service")]
            public string service { get; set; }
        }
        public class Gmail48hOtpDataresponse
        {
            [JsonProperty("status")]
            public string status { get; set; }
           
            [JsonProperty("message")]
            public string message { get; set; }
        }
        public static MailObject GetGmail48h()
        {
            try
            {
                var client = new RestClient("https://gmail48h.com/api/gmailotpget/token=591bcce493f0f8cf753143bcc8f6fa42&service=facebook");
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("", Method.GET);

                var response = client.Execute(request);
                var content = response.Content; // Raw content as string

                try
                {
                    Gmail48hDataresponse data = JsonConvert.DeserializeObject<Gmail48hDataresponse>(content);

                    if (data != null && !string.IsNullOrEmpty(data.email))
                    {
                        MailObject mail = new MailObject();
                        mail.email = data.email;
                        mail.password = Constant.GMAIL_SELL_GMAIL;
                        mail.orderId = data.id;
                        return mail;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
            }
            catch (Exception eee)
            {
                return null;
            }
            return null;
        }

        public static string GetOtpGmail48h(int orderId)
        {
            try
            {
                var client = new RestClient("https://gmail48h.com/api/gmailotpcheck/token=591bcce493f0f8cf753143bcc8f6fa42&id_order=" + orderId);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("", Method.GET);

                var response = client.Execute(request);
                var content = response.Content; // Raw content as string

                try
                {
                    Gmail48hOtpDataresponse data = JsonConvert.DeserializeObject<Gmail48hOtpDataresponse>(content);

                    if (data != null && !string.IsNullOrEmpty(data.status) && data.status == "success")
                    {
                        return data.message;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
            }
            catch (Exception eee)
            {
                return null;
            }
            return null;
        }
        public class GmailSuperTeamDatarequest
        {
            [JsonProperty("gmail")]
            public string gmail { get; set; }
            [JsonProperty("success")]
            public bool success { get; set; }
        }
        public class OtpGmailSuperTeamDatarequest
        {
            [JsonProperty("gmail")]
            public string gmail { get; set; }
            [JsonProperty("status")]
            public string status { get; set; }

            [JsonProperty("otp")]
            public string otp { get; set; }

            [JsonProperty("price")]
            public int price { get; set; }
        }

        public static MailObject GetGmailSuperTeam()
        {
            try
            {

                var client = new RestClient("https://api.sptmail.com/api/otp-services/mail-otp-rental/");
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("", Method.GET);
                
                
                request.AddParameter("apiKey", "Y10UF406JFC27BEV");
                request.AddParameter("otpServiceCode", "facebook");
                var response = client.Execute(request);
                var content = response.Content; // Raw content as string

                try
                {
                    GmailSuperTeamDatarequest data = JsonConvert.DeserializeObject<GmailSuperTeamDatarequest>(content);

                    if (data != null && data.success && !string.IsNullOrEmpty(data.gmail))
                    {
                        MailObject mail = new MailObject();
                        mail.email = data.gmail;
                        mail.password = Constant.GMAIL_SELL_GMAIL;
                        
                        return mail;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
            }
            catch (Exception eee)
            {
                return null;
            }
        }

        public static string GetOtpGmailSuperTeam(string gmail)
        {
            try
            {

                var client = new RestClient("https://api.sptmail.com/api/otp-services/mail-otp-lookup/");
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("", Method.GET);


                request.AddParameter("apiKey", "Y10UF406JFC27BEV");
                request.AddParameter("otpServiceCode", "facebook");
                request.AddParameter("gmail", gmail);


                var response = client.Execute(request);
                var content = response.Content; // Raw content as string

                try
                {
                    OtpGmailSuperTeamDatarequest data = JsonConvert.DeserializeObject<OtpGmailSuperTeamDatarequest>(content);

                    if (data != null && !string.IsNullOrEmpty(data.status) && data.status == "SUCCESS" && !string.IsNullOrEmpty(data.otp))
                    {

                        return data.otp;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
            }
            catch (Exception eee)
            {
                return null;
            }
        }

        public class OtpGmailOtpResponse
        {
            [JsonProperty("code")]
            public int code { get; set; }

            [JsonProperty("message")]
            public string message { get; set; }

            [JsonProperty("data")]
            public OtpGmailOtpDataResponse data { get; set; }
        }
        public class OtpGmailOtpDataResponse
        {
            [JsonProperty("code")]
            public string code { get; set; }

        }

        public static string GetOtpGmailOtp(int codeID)
        {
            try
            {
                GmailOtpDatarequest dataRequest = new GmailOtpDatarequest();
                dataRequest.serviceId = 1;
                var client = new RestClient("https://api.gmailotp.com/api/");
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                string url = string.Format("v1/mail-rent-services/external/{0}/get-ex-code", codeID);
                var request = new RestRequest(url);
                request.AddHeader("X-API-TOKEN", "WVkyQkhBR0lQVzRJMElYOE5QUVM=");

                var response = client.Get(request);
                var content = response.Content; // Raw content as string

                try
                {
                    OtpGmailOtpResponse data = JsonConvert.DeserializeObject<OtpGmailOtpResponse>(content);

                    if (data != null && data.code == 0 && data.data != null && !string.IsNullOrEmpty(data.data.code))
                    {
                        return data.data.code;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
            }
            catch (Exception eee)
            {
                return null;
            }
            return null;


        }
        public static MailObject GetGmail30Min()
        {
            try
            {
                var client = new RestClient("https://gmail30min.com/api/");
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("san-pham/mua2");

                request.AddParameter("token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiNGRlNWJiNDUzNjk1NjdlODNlM2EzYjU3N2ViMjM5ZThiMTY3ODY3YTBlNWFhYjk1OTMyNWMwNDI5ZGRlN2MwOWVlNDUxMDRiNTk4ZTg5NzkiLCJpYXQiOjE2NzU3Nzg4NzguMjkxMDc0LCJuYmYiOjE2NzU3Nzg4NzguMjkxMDg5LCJleHAiOjE3MDczMTQ4NzguMjg1MzEyLCJzdWIiOiI2MzMiLCJzY29wZXMiOltdfQ.DTw_18NzU1bJN-gdDbjy-3VnPhtSO6VOQtdVqGKk4sv38N_ts2QZoa6Jq0FkIgBw3KAuHKJf_GkwpoEkdImwjULKNkkqM8tEiGCkvFHNZJgtdRsBS06Etprtwrn15grrmvIIHvj7O2YBpkHRiIOUYSfL9nvMMckUrf6-GPrlgHR6ng6TeMZwUsVtEqrLj2HoOs85_i_ou9Qr-90I_8aCii_Gd26IUuPeFMED6pPap6GwmLXLccfOtc8BLsOzo7OtOO0_XM1kJexxVYF3NieZeJQDVJIDlvdNzJRdotjp5rfN0FlfHObD9y0m6vaF7s-6WOCwdjXBo4nIHYLujsvzugBAtGJGwZbqRautu6e0sMF_uiGBsjVABmn8aL1gY8NE23hQUltiZjLUeLhkCRreZAfZILYPVgC2AB1gWVjHEqvtZxOG5yrXBGvBuK_GZfhcSl3dbg1WGxNqcGHYmBbdVUKoS6BFeaS6cN2pSN_jZb0rRJZRuH5NzKAYjsWjrdaAApEQfG8T3d622Sh8fqX4u98q6zGnwFI4oaMrWyJkRDP88JOmdALg5lkpC1WcW6-OXGUZsJ4bp6MrYMlPs6jGEhtVD-Nm9hEeARbrOafgH_kQWngeY5AJz-Q7LmCutlh9AqXrq9STUffKCqCDQq2s7bwcFyzlwnLpbxGddu3BnDM");
                request.AddParameter("category", 4);
                request.AddParameter("quantity", 1);


                var response = client.Get(request);
                var content = response.Content; // Raw content as string

                try
                {
                    Gmail30MinRes data = JsonConvert.DeserializeObject<Gmail30MinRes>(content);

                    if (data != null && data.success != null && data.success.status == "completed" && data.success.products != null && data.success.products.Length > 0)
                    {
                        MailObject mail = new MailObject();
                        string[] temp = data.success.products[0].Split('|');
                        mail.email = temp[0];
                        mail.password = Constant.GMAIL_SELL_GMAIL;
                        mail.passwordTemp = temp[1];
                        return mail;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
            }
            catch (Exception eee)
            {
                return null;
            }
            return null;
        }
        public static string GetCodeMailOtp(int orderId)
        {
            try
            {
                var client = new RestClient("https://mailotp.com/api/");
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("v1/get-order-detail");

                request.AddParameter("apikey", "714a44e4-dd1e-46f8-a392-580eab8af4bf");
                request.AddParameter("order_id", orderId);


                var response = client.Get(request);
                var content = response.Content; // Raw content as string

                try
                {
                    MailOtpCode data = JsonConvert.DeserializeObject<MailOtpCode>(content);

                    if (data != null && data.code == 200 && data.order != null && !string.IsNullOrEmpty(data.order.email))
                    {
                        return data.order.otp;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
            }
            catch (Exception eee)
            {
                return null;
            }
            return null;
        }


        public class DataSettings
        {
            [JsonProperty("code")]
            public string code { get; set; }


            [JsonProperty("value")]
            public string value { get; set; }

            //[JsonProperty("description")]
            //public string description { get; set; }
        }

        public class Settings
        {
            [JsonProperty("result")]
            public Boolean result { get; set; }


            [JsonProperty("message")]
            public string message { get; set; }

            [JsonProperty("data")]
            public DataSettings data { get; set; }
        }

        public static bool UpdateSettings(DataSettings data)
        {
            bool result = false;

            var client = new RestClient(SERVER_HOST);
            client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            var request = new RestRequest("settings/update", Method.POST);

            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(data);
            try
            {
                RestResponse response = (RestResponse)client.Execute(request);
                Console.WriteLine(response.Content);
            }
            catch (Exception error)
            {
                // Log
            }

            return result;
        }

        public class ServerInfoResponse
        {
            [JsonProperty("ip")]
            public string ip { get; set; }

        }

        public static string GetServerIp(string server)
        {
            try
            {
                string apiGetHotMail = "api/ipserver";
                var client = new RestClient(server);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetHotMail);


                var response = client.Get(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("get IP sever:" + content);
                try
                {
                    ServerInfoResponse data = JsonConvert.DeserializeObject<ServerInfoResponse>(content);
                    if (data != null)
                    {
                        return data.ip;
                    }
                    
                }
                catch
                {
                    return null;
                }
                return "";
            }
            catch (Exception ex)
            {

            }

            return "";
        }
        public static Settings GetSetting(string code)
        {
            try
            {
                var client = new RestClient(SERVER_HOST);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("settings/get");
                if (!string.IsNullOrEmpty(code))
                {
                    request.AddParameter("code", code);
                } else
                {
                    return null;
                }
               
                var response = client.Get(request);
                var content = response.Content; // Raw content as string

                try
                {
                    Settings data = JsonConvert.DeserializeObject<Settings>(content);
                    return data;
                }
                catch
                {
                    return null;
                }
            }
            catch (Exception eee)
            {
                return null;
            }
        }


        public static bool UpdateDuoiMail(string duoiMail)
        {
            DataSettings dd = new DataSettings();

            dd.code = "duoimail";

            List<String> duoimails = GetListDuoiMail();
            if (duoimails.Contains(duoiMail))
            {
                return true;
            }
            duoimails.Add(duoiMail);
            
            if (duoimails.Count < 10 )
            {
                // Do nothing
            } else
            {
                duoimails.RemoveAt(0);
            }

            dd.value = ConvertList2String(duoimails);
            return UpdateSettings(dd);
        }
        public static string ConvertList2String(List<String> dd)
        {
            string result = "";

            if (dd != null)
            {
                if (dd.Count == 1)
                {
                    result = dd[0];
                } else
                {
                    for (int i = 0; i < dd.Count - 1; i ++)
                    {
                        result = result + dd[i] + "|";
                    }
                    result = result + dd[dd.Count - 1];
                }
            }
            return result;
        }
        public static List<String> GetListDuoiMail()
        {
            List<String> duoiMails = new List<string>();

            Settings ss = GetSetting("duoimail");

            if (ss != null && ss.data != null && !string.IsNullOrEmpty(ss.data.value))
            {
                string[] temp = ss.data.value.Split('|');
                for (int i = 0; i < temp.Length; i ++)
                {
                    duoiMails.Add(temp[i]);
                }
            }
            
            return duoiMails;
        }

        public class DichVuGmailEmail
        {
            [JsonProperty("status")]
            public int status { get; set; }


            [JsonProperty("orders")]
            public DichVuGmailEmailOrder orders { get; set; }
        }
        public class DichVuGmailEmailOrder
        {
            [JsonProperty("gmail")]
            public string gmail { get; set; }
            [JsonProperty("order_id")]
            public string order_id { get; set; }

            [JsonProperty("otp")]
            public string otp { get; set; }
            [JsonProperty("status")]
            public string status { get; set; }
        }

        public static MailObject GetGmailDichVuGmail(string serviceId = "Facebook", string key = "PtcRfCJe0UjBk4iJ2umU98ZnE7rzp0sJ")
        {
            MailObject mail = new MailObject();
            try
            {
                string apiGetHotMail = string.Format("{0}/{1}",
                key,
                serviceId);
                var client = new RestClient("https://dichvugmail.com/DataMail/Mail/");
                //var client = new RestClient("https://dichvugmail.com/DataMail/Mail/");
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetHotMail);


                var response = client.Get(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("get mail:" + content);
                string decode = Utility.Decode_UTF8(content);
                DichVuGmailEmail data = JsonConvert.DeserializeObject<DichVuGmailEmail>(decode);

                if (data != null && data.status == 200 && !string.IsNullOrEmpty(data.orders.gmail) && data.orders.status == "Wait")
                {
                    mail.email = data.orders.gmail;
                    mail.password = Constant.GMAIL_SELL_GMAIL;
                    mail.token = data.orders.order_id;
                }
            } catch(Exception ex)
            {

            }
            
            

            return mail;
        }
        public static MailObject GetDichvuGmailLocalCache(string server)
        {
            MailObject mail = new MailObject();
            try
            {
                string apiGetHotMail = "api/gmail";
                var client = new RestClient(server);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetHotMail);


                var response = client.Get(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("get mail:" + content);
                string decode = Utility.Decode_UTF8(content);
                MailObject data = JsonConvert.DeserializeObject<MailObject>(decode);

                if (data != null && !string.IsNullOrEmpty(data.email))
                {
                    return data;
                }
            }
            catch (Exception ex)
            {

            }



            return mail;
        }

        public class AvatarObject
        {
            public string avatarName;
            public string base64;
            public string localPath;
        }
        public static AvatarObject GetAvatarLocalCache(string server, string gender, string deviceID)
        {
            AvatarObject avatarCache = new AvatarObject();
            try
            {
                string apiGetHotMail = string.Format("api/avatar?gender={0}",gender);
                var client = new RestClient(server);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetHotMail);


                var response = client.Get(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("get avatar:" + content);
                string decode = Utility.Decode_UTF8(content);
                AvatarObject data = JsonConvert.DeserializeObject<AvatarObject>(decode);

                if (data != null && !string.IsNullOrEmpty(data.avatarName))
                {
                    Byte[] bytes = Convert.FromBase64String(data.base64);
                    File.WriteAllBytes("img/avatar/" + deviceID + ".png", bytes);
                    data.localPath = "img/avatar/" + deviceID + ".png";
                    return data;
                }
            }
            catch (Exception ex)
            {

            }



            return avatarCache;
        }

        public class NameObject
        {
            public string name;
            public string lastname;
            public string gender;
            public bool isVn;
        }
        public static NameObject GetNameLocalCache(string server, string gender, string language)
        {
            NameObject nameCache = new NameObject();
            try
            {
                string apiGetHotMail = string.Format("api/name?gender={0}&language={1}", gender, language);
                var client = new RestClient(server);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetHotMail);


                var response = client.Get(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("get name:" + content);
                
                NameObject data = JsonConvert.DeserializeObject<NameObject>(content);

                if (data != null && !string.IsNullOrEmpty(data.name))
                {

                    return data;
                }
            }
            catch (Exception ex)
            {

            }



            return nameCache;
        }

        public static MailObject GetSellGmailLocalCache(string server)
        {
            MailObject mail = new MailObject();
            try
            {
                string apiGetSellGmail = "api/sellgmail";
                var client = new RestClient(server);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetSellGmail);


                var response = client.Get(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("get mail:" + content);
                string decode = Utility.Decode_UTF8(content);
                MailObject data = JsonConvert.DeserializeObject<MailObject>(decode);

                if (data != null && !string.IsNullOrEmpty(data.email))
                {
                    return data;
                }
            }
            catch (Exception ex)
            {

            }



            return mail;
        }
        public static MailObject GetSuperGmailLocalCache(string server)
        {
            MailObject mail = new MailObject();
            try
            {
                string apiGetSuperGmail = "api/supergmail";
                var client = new RestClient(server);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetSuperGmail);


                var response = client.Get(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("get mail:" + content);
                string decode = Utility.Decode_UTF8(content);
                MailObject data = JsonConvert.DeserializeObject<MailObject>(decode);

                if (data != null && !string.IsNullOrEmpty(data.email))
                {
                    return data;
                }
            }
            catch (Exception ex)
            {

            }



            return mail;
        }

        public static MailObject GetGmailOtpLocalCache(string server)
        {
            MailObject mail = new MailObject();
            try
            {
                string apiGetGmailOtp = "api/gmailotp";
                var client = new RestClient(server);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetGmailOtp);


                var response = client.Get(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("get mail:" + content);
                string decode = Utility.Decode_UTF8(content);
                MailObject data = JsonConvert.DeserializeObject<MailObject>(decode);

                if (data != null && !string.IsNullOrEmpty(data.email))
                {
                    return data;
                }
            }
            catch (Exception ex)
            {

            }



            return mail;
        }

        public static Account GetAccMoiLocalCache(string server)
        {
            Account accMoi = new Account();
            try
            {
                string apiGetAccMoi = "api/accmoi";
                var client = new RestClient(server);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetAccMoi);

                var response = client.Get(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("Get Acc Moi:" + content);
                string decode = Utility.Decode_UTF8(content);
                accMoi = JsonConvert.DeserializeObject<Account>(decode);

                if (accMoi != null && !string.IsNullOrEmpty(accMoi.uid))
                {
                    return accMoi;
                }
            }
            catch (Exception ex)
            {

            }

            return accMoi;
        }

        public static Proxy GetProxyFromServer(string server, bool p1, bool p3)
        {
            Proxy proxy = new Proxy();
            try
            {
                string type = "";
                if (p1)
                {
                    type = "1";
                } else
                {
                    type = "2";
                }
                if (p3)
                {
                    type = "3";
                }
                string apiGetProxy = string.Format("api/proxy?type={0}", type);
                var client = new RestClient(server);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetProxy);

                var response = client.Get(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("Get Proxy:" + content);
                string decode = Utility.Decode_UTF8(content);
                proxy = JsonConvert.DeserializeObject<Proxy>(decode);

                if (proxy != null && !string.IsNullOrEmpty(proxy.host))
                {
                    return proxy;
                }
            }
            catch (Exception ex)
            {

            }

            return proxy;
        }

        public static string GetGmailDichVuGmailOtp(string orderId, string key = "PtcRfCJe0UjBk4iJ2umU98ZnE7rzp0sJ")
        {
            string otp = "";

            try
            {
                string apiGetDvgmOtp = string.Format("{0}/{1}",
                key,
                orderId);
                var client = new RestClient("https://dichvugmail.com/DataMail/Mail/");
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetDvgmOtp);


                var response = client.Get(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("get mail:" + content);
                string decode = Utility.Decode_UTF8(content);
                DichVuGmailEmail data = JsonConvert.DeserializeObject<DichVuGmailEmail>(decode);

                if (data != null && data.status == 200 && data.orders != null && !string.IsNullOrEmpty(data.orders.otp))
                {
                    otp = data.orders.otp;
                }
                if (data == null || data.orders == null || data.orders.status == "Cancel" || data.orders.status == "cancel")
                {
                    return "";
                }
            }
            catch (Exception ex)
            {

            }

            return otp;
        }

        public class SellGmailEmail
        {
            [JsonProperty("state")]
            public bool state { get; set; }
            [JsonProperty("msg")]
            public string msg { get; set; }

            [JsonProperty("data")]
            public string data { get; set; }
        }

        public class SellGmailEmailServer
        {
            [JsonProperty("state")]
            public bool state { get; set; }
            [JsonProperty("msg")]
            public string msg { get; set; }

            [JsonProperty("data")]
            public string data { get; set; }
        }

        public static MailObject GetGmailSellGmail(string serviceId = "1", string key = "E0Q68-TWNV8-DXB6B-4HQ3M-5OOC2")
        {
            MailObject mail = new MailObject();

            //if (!Utility.isAvailableSellGmail)
            //{
             //   return mail;
           // }
            //Utility.isAvailableSellGmail = false;
            try
            {
                string apiGetHotMail = string.Format("http://sellgmail.com/api/mailselling/order-rent-mail?serviceId={0}&apiKey={1}",
                serviceId,
                key);
                var request = (HttpWebRequest)WebRequest.Create(apiGetHotMail);
                request.Method = "GET";
                request.Accept = "application/json";
                request.ContentType = "application/json; charset=utf-8";

                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                string decode = Utility.Decode_UTF8(responseString);
                SellGmailEmail data = JsonConvert.DeserializeObject<SellGmailEmail>(decode);

                if (data.state && !string.IsNullOrEmpty(data.data))
                {
                    mail.email = data.data;
                    mail.password = Constant.GMAIL_SELL_GMAIL;
                }
                //Utility.isAvailableSellGmail = true;
            } catch(Exception ex)
            {
                
            }
            return mail;
        }

        public static MailObject GetGmailSellGmailServer(string serviceId = "1", string key = "E0Q68-TWNV8-DXB6B-4HQ3M-5OOC2")
        {
            MailObject mail = new MailObject();

            try
            {
                //string apiGetHotMail = SERVER_HOST + "/sellgmail";
               
                //var request = (HttpWebRequest)WebRequest.Create(apiGetHotMail);
                //request.Method = "GET";
                //request.Accept = "application/json";
                //request.ContentType = "application/json; charset=utf-8";

                //var response = (HttpWebResponse)request.GetResponse();
                //var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                //string decode = Utility.Decode_UTF8(responseString);
                //SellGmailEmailServer data = JsonConvert.DeserializeObject<SellGmailEmailServer>(decode);

                //if (data != null && data.state && data.state && !string.IsNullOrEmpty(data.data))
                //{
                //    mail.email = data.data;
                //    mail.password = Constant.GMAIL_SELL_GMAIL;
                //}



                try
                {
                    var client = new RestClient(SERVER_HOST);
                    client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                    var request = new RestRequest("sellgmail");
                    
                    var response = client.Get(request);
                    var content = response.Content; // Raw content as string

                    try
                    {
                        SellGmailEmailServer data = JsonConvert.DeserializeObject<SellGmailEmailServer>(content);
                        if (data != null && data.state && data.state && !string.IsNullOrEmpty(data.data))
                        {
                            mail.email = data.data;
                            mail.password = Constant.GMAIL_SELL_GMAIL;
                        }
                    }
                    catch
                    {
                        
                    }
                }
                catch (Exception eee)
                {
                    
                }
            }
            catch (Exception ex)
            {

            }
            return mail;
        }

        public class SellGmailOtp
        {
            [JsonProperty("state")]
            public bool state { get; set; }
            [JsonProperty("msg")]
            public string msg { get; set; }

            [JsonProperty("data")]
            public SellGmailOtpData data { get; set; }
        }
        public class SellGmailOtpData
        {
            [JsonProperty("otp")]
            public string otp { get; set; }
            [JsonProperty("status")]
            public string status { get; set; }
        }


        public static bool ChangeIpFastProxy(string key, string port)
        {
            Proxy proxy = new Proxy();
            var client = new RestClient("https://api-socks.fastproxy.vip");

            var request = new RestRequest("/api/v1/proxies/EggMc92VLoqO7RPm/ip");
            request.AddParameter("port", port);

            request.AddParameter("api_key", key);

            var response = client.Get(request);
            var content = response.Content; // Raw content as string
            if (!string.IsNullOrEmpty(content) && content.Contains("true"))
            {
                return true;
            }
            return false;
        }

        public static bool ChangeIpZuesProxy(string apiKey, string proxyId, string rotate)
        {
            string url = string.Format("https://api.zeusproxy.com/proxies/rotate/{0}?proxy={1}&rotate={2}", apiKey, proxyId, rotate);
            var client = new RestClient(url);

            var request = new RestRequest("");

            var response = client.Put(request);
            var content = response.Content; // Raw content as string
            if (!string.IsNullOrEmpty(content) && content.Contains("true"))
            {
                return true;
            }
            return false;
        }

        public static bool ChangeGeoFastProxy(string key, string portIn)
        {
            Proxy proxy = new Proxy();
            var client = new RestClient("https://api-socks.fastproxy.vip/api/v1/proxies/EggMc92VLoqO7RPm/geo");

            var request = new RestRequest("");
            request.AddQueryParameter("api_key", "OEgSvhQD39KwxuQ2");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new { port = portIn, geo = "US" });
            
            try
            {
                var response = client.Post(request);
                var content = response.Content; // Raw content as string
                if (!string.IsNullOrEmpty(content) && content.Contains("true"))
                {
                    return true;
                }
                return false;
            } catch(Exception ex)
            {
                string gg = ex.Message;
            }


            return false;

        }
        public static string GetOtpSellGmail(string email, string key = "E0Q68-TWNV8-DXB6B-4HQ3M-5OOC2")
        {
            string otp = "";
            try
            {
                string apiGetHotMail = string.Format("http://sellgmail.com/api/mailselling/get-mail-otp?mail={0}&apiKey={1}",
                email,
                key);
                var request = (HttpWebRequest)WebRequest.Create(apiGetHotMail);
                request.Method = "GET";
                request.Accept = "application/json";
                request.ContentType = "application/json; charset=utf-8";

                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                string decode = Utility.Decode_UTF8(responseString);
                SellGmailOtp data = JsonConvert.DeserializeObject<SellGmailOtp>(decode);
                if (data != null && data.data != null && data.state)
                {
                    if (data.data.status == "REF")  // Hoàn lại
                    {
                        return "0";
                    }
                    if (!string.IsNullOrEmpty(data.data.otp))
                    {
                        otp = data.data.otp;
                    }
                }
            } catch(Exception ex)
            {

            }

            return otp;
        }

        public static string GetAccessToken(string clientId, string refreshToken)
        {
            
            var client = new RestClient("https://login.microsoftonline.com");
            var request = new RestRequest("/common/oauth2/v2.0/token?api-version=1.0", Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Cookie", "esctx=PAQABBwEAAADW6jl31mB3T7ugrWTT8pFew6njZsf7TuchTdow8FC5Y9v23GPkFKBW3pM76BsnIPJUg_Jte7YNUai0Ppz50LLjJdK6FnkIcxHr1FaWEAtxSBm5353rLpULh0tAjNkYLGZ9cqmWwpUJtQqNrNZBkyRrzEDxctiXYWwoxAbKX1eW0knAUa15N1bY_5DXUeq8ZLYgAA; fpc=Atnflir3fGtDjWYPQa7pGh38EslYAQAAABBwiN4OAAAA; stsservicecookie=estsfd; x-ms-gateway-slice=estsfd");
            request.AddParameter("client_id", clientId);
            request.AddParameter("grant_type", "refresh_token");
            request.AddParameter("refresh_token", refreshToken);
            RestResponse response = (RestResponse)client.Post(request);
            var content = response.Content; // Raw content as string
            if (!string.IsNullOrEmpty(content) && content.Contains("access_token"))
            {
                string check = Regex.Match(content, "access_token\":\"(.*?)\"").ToString();
                string accessToken = check.Replace("access_token\":\"", "").Replace("\"", "");
                if (!string.IsNullOrEmpty(accessToken))
                {
                    return accessToken;
                }
            }
            return "";
        }
        public static Proxy getProxyTmProxy(string key)
        {
            Proxy proxy = new Proxy();
            var client = new RestClient("https://tmproxy.com/api/proxy");

            var request = new RestRequest("get-new-proxy");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new { api_key = key, sign = "string", id_location = 0 });

            var response = client.Post(request);
            var content = response.Content; // Raw content as string
            if (!string.IsNullOrEmpty(content) && content.Contains("socks5"))
            {
                string aaa = Regex.Match(content, "socks5\":\"[0-9.:]{0,}").ToString();
                string ip = aaa.Replace("socks5\":\"", "");

                if (Regex.Match(ip, "[0-9]+.[0-9]+.[0-9]+.[0-9]+:[0-9]+").Success)
                {
                    proxy.host = ip.Split(':')[0];
                    proxy.port = ip.Split(':')[1];
                    proxy.hasProxy = true;
                }
            }
            return proxy;
        }
        public class ResponseTinProxy
        {
            [JsonProperty("status")]
            public string status { get; set; }
            [JsonProperty("message")]
            public string message { get; set; }

            [JsonProperty("data")]
            public ResponseTinProxyData data { get; set; }
        }
        public class ResponseDtProxy
        {
            [JsonProperty("code")]
            public int code { get; set; }
            [JsonProperty("status")]
            public string status { get; set; }
            [JsonProperty("message")]
            public string message { get; set; }

            [JsonProperty("data")]
            public ResponseDtProxyData data { get; set; }
        }

        public class ResponseTinProxyAuthen
        {
            [JsonProperty("username")]
            public string username { get; set; }


            [JsonProperty("password")]
            public string password { get; set; }
        }
        public class ResponseTinProxyData
        {
            [JsonProperty("http_ipv4")]
            public string http_ipv4 { get; set; }


            [JsonProperty("http_ipv6")]
            public string http_ipv6 { get; set; }

            [JsonProperty("socks_ipv4")]
            public string socks_ipv4 { get; set; }

            [JsonProperty("http_ipv6_ipv4")]
            public string http_ipv6_ipv4 { get; set; }

            [JsonProperty("public_ip")]
            public string public_ip { get; set; }

            [JsonProperty("public_ip_ipv6")]
            public string public_ip_ipv6 { get; set; }

            [JsonProperty("expired_at")]
            public string expired_at { get; set; }

            [JsonProperty("timeout")]
            public int timeout { get; set; }

            [JsonProperty("next_request")]
            public int next_request { get; set; }

            [JsonProperty("ip_allow")]
            public string[] ip_allow { get; set; }

            [JsonProperty("your_ip")]
            public string your_ip { get; set; }

            [JsonProperty("authentication")]
            public ResponseTinProxyAuthen authentication { get; set; }

        }

        public class ResponseDtProxyData
        {
            [JsonProperty("http_ipv4")]
            public string http_ipv4 { get; set; }


            [JsonProperty("http_ipv6")]
            public string http_ipv6 { get; set; }

            [JsonProperty("socks_ipv4")]
            public string socks_ipv4 { get; set; }

            [JsonProperty("http_ipv6_ipv4")]
            public string http_ipv6_ipv4 { get; set; }

            [JsonProperty("public_ip")]
            public string public_ip { get; set; }

            [JsonProperty("public_ip_ipv6")]
            public string public_ip_ipv6 { get; set; }

            [JsonProperty("expired_at")]
            public string expired_at { get; set; }

            [JsonProperty("timeout")]
            public int timeout { get; set; }

            [JsonProperty("next_request")]
            public int next_request { get; set; }

            [JsonProperty("ip_allow")]
            public string ip_allow { get; set; }

            [JsonProperty("your_ip")]
            public string your_ip { get; set; }

            [JsonProperty("authentication")]
            public ResponseTinProxyAuthen authentication { get; set; }

        }
        public static Proxy getProxyTinProxy(string key, string allowIp)
        {
            Proxy proxy = new Proxy();

            string apiGetHotMail = string.Format("https://tinproxy.com/api/proxy/get-new-proxy?api_key={0}&authen_ips={1}",
                key,
                allowIp);
            var request = (HttpWebRequest)WebRequest.Create(apiGetHotMail);
            request.Method = "GET";
            request.Accept = "application/json";
            request.ContentType = "application/json; charset=utf-8";

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            string decode = Utility.Decode_UTF8(responseString);
            ResponseTinProxy data = JsonConvert.DeserializeObject<ResponseTinProxy>(decode);
            
            if (data.status == "active")
            {
                if (!string.IsNullOrEmpty(data.message) && data.message.Contains("thất bại"))
                {
                    proxy.hasProxy = true;
                    proxy.isWait = true;
                    proxy.timeout = data.data.next_request;
                } else
                {
                    string proxyPort = data.data.http_ipv4;

                    proxy.host = proxyPort.Split(':')[0];
                    proxy.port = proxyPort.Split(':')[1];
                    proxy.hasProxy = true;
                    proxy.username = data.data.authentication.username;
                    proxy.pass = data.data.authentication.password;
                }
            }
            proxy.proxyDomain = ProxyDomain.TinProxy.ToString();
            return proxy;
        }

        public static Proxy getProxyDtProxy(string key, string allowIp)
        {
            Proxy proxy = new Proxy();
            try
            {
                string apiGetHotMail = string.Format("https://app.proxydt.com/api/public/proxy/get-new-proxy?license={0}&authen_ips={1}",
                key,
                allowIp);
                var request = (HttpWebRequest)WebRequest.Create(apiGetHotMail);
                request.Method = "GET";
                request.Accept = "application/json";
                request.ContentType = "application/json; charset=utf-8";

                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                //string decode = Utility.Decode_UTF8(responseString);
                ResponseDtProxy data = JsonConvert.DeserializeObject<ResponseDtProxy>(responseString);

                if (data.code == 1)
                {
                    if (!string.IsNullOrEmpty(data.message) && data.message.Contains("thất bại"))
                    {
                        proxy.hasProxy = true;
                        proxy.isWait = true;
                        proxy.timeout = data.data.next_request;
                    }
                    else
                    {

                        string proxyPort = data.data.http_ipv4;
                        if (!string.IsNullOrEmpty(proxyPort.Split(':')[0]))
                        {
                            proxy.hasProxy = true;
                            proxy.host = proxyPort.Split(':')[1];
                            if (!string.IsNullOrEmpty(proxy.host))
                            {
                                proxy.host = proxy.host.Replace("//", "");
                            }
                            proxy.port = proxyPort.Split(':')[2];
                            proxy.hasProxy = true;
                            proxy.username = data.data.authentication.username;
                            proxy.pass = data.data.authentication.password;
                        }

                    }
                } else
                {
                    proxy.timeout = 60;
                    proxy.isWait = true;
                    proxy.hasProxy = true;
                }

                proxy.proxyDomain = ProxyDomain.dtProxy.ToString();
                return proxy;
            }
            catch (Exception ex)
            {

            }

            return proxy;
        }

        public static Proxy getProxyTinsoft(string key)
        {
            Proxy proxy = new Proxy();
            Random ran = new Random();
            int location = ran.Next(0, 15);
            location = 0;
            string apiGetHotMail = string.Format("http://proxy.tinsoftsv.com/api/changeProxy.php?key={0}&location={1}",
                key,
                location);
            var request = (HttpWebRequest)WebRequest.Create(apiGetHotMail);
            request.Method = "GET";
            request.Accept = "application/json";
            request.ContentType = "application/json; charset=utf-8";

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            if (responseString.Contains("success") && responseString.Contains("wait"))
            {
                string temp = Regex.Match(responseString, "wait [0-9.:]{0,}").ToString();
                string timeout = temp.Trim().Replace("wait ", "");
                try
                {
                    proxy.timeout = Convert.ToInt32(timeout);
                    proxy.isWait = true;
                    proxy.hasProxy = true;
                } catch(Exception ex)
                {
                  
                    proxy.hasProxy = false;
                    return proxy;
                }
            }
            string temp2 = Regex.Match(responseString, "proxy\":\"[0-9.:]{0,}").ToString();
            temp2 = temp2.Replace("proxy\":\"", "");

            if (!string.IsNullOrEmpty(temp2))
            {
                proxy.host = temp2.Split(':')[0];
                proxy.port = temp2.Split(':')[1];
                proxy.hasProxy = true;
            }
            proxy.message = responseString;
            proxy.proxyDomain = ProxyDomain.Tinsoft.ToString();
            return proxy;
        }

        public class TempmailLolMail
        {
            [JsonProperty("address")]
            public string address { get; set; }

            [JsonProperty("token")]
            public string token { get; set; }
        }

        public class TempmailLolListMail
        {
            [JsonProperty("email")]
            public TempmailLolMailObject[] email { get; set; }

        }

        public class TempmailLolMailObject
        {
            [JsonProperty("subject")]
            public string subject { get; set; }

            [JsonProperty("body")]
            public string body { get; set; }
        }


        public static MailObject getTempmailLol()
        {
            MailObject mailToken = new MailObject();
            
            string apiGetHotMail = "https://api.tempmail.lol/generate";
            var request = (HttpWebRequest)WebRequest.Create(apiGetHotMail);
            request.Method = "GET";
            request.Accept = "application/json";
            request.ContentType = "application/json; charset=utf-8";

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            Console.WriteLine("mail tempmailLol:" + responseString);
            if (responseString.Contains("address"))
            {
                TempmailLolMail mail = JsonConvert.DeserializeObject<TempmailLolMail>(responseString);
                
                mailToken.email = mail.address;
                mailToken.token = mail.token;
            }
           
            return mailToken;
        }

        public static string getCodeTempmailLol(string token)
        {

            string apiGetHotMail = "https://api.tempmail.lol/auth/" + token;
            var request = (HttpWebRequest)WebRequest.Create(apiGetHotMail);
            request.Method = "GET";
            request.Accept = "application/json";
            request.ContentType = "application/json; charset=utf-8";

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            Console.WriteLine("mail tempmailLol:" + responseString);
            if (responseString.Contains("subject"))
            {
                TempmailLolListMail mail = JsonConvert.DeserializeObject<TempmailLolListMail>(responseString);
                if (mail.email != null && mail.email.Length > 0)
                {
                    string otp = Regex.Match(mail.email[0].subject, "\\d{5}").Value.Replace(" ", "");
                    if (!string.IsNullOrEmpty(otp))
                    {
                        return otp;
                    }
                }
                
            }

            return "";
        }

        public static Proxy getProxyShoplike(string key, string location)
        {
            Proxy proxy = new Proxy();
            Random ran = new Random();
            
            List<string> locations = new List<string>();
            
            if (string.IsNullOrEmpty(location))
            {
                //qn, hcm, hd, hue, bn
                locations.Add("tq");
                locations.Add("nd");
                locations.Add("hp");
                locations.Add("dn");
                locations.Add("hd");
                locations.Add("qn");
                locations.Add("hcm");
                locations.Add("hue");
                locations.Add("bn");
                location = locations.OrderBy(s => Guid.NewGuid()).First();
            }
            
            
            string apiGetHotMail = string.Format("https://proxy.shoplike.vn/Api/getNewProxy?access_token={0}",
                key
                );
            var request = (HttpWebRequest)WebRequest.Create(apiGetHotMail);
            request.Method = "GET";
            request.Accept = "application/json";
            request.ContentType = "application/json; charset=utf-8";

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            if (responseString.Contains("Con lai "))
            {
                string temp1 = Regex.Match(responseString, "Con lai [0-9.:]{0,}").ToString();
                string timeout = temp1.Trim().Replace("Con lai ", "");
                if (!string.IsNullOrEmpty(timeout))
                {
                    proxy.isWait = true;
                    proxy.timeout = Convert.ToInt32(timeout);
                    proxy.hasProxy = true;
                }
            }
            
            string temp = Regex.Match(responseString, "proxy\":\"[0-9.:]{0,}").ToString();
            temp = temp.Replace("proxy\":\"", "");
            if (!string.IsNullOrEmpty(temp))
            {
                proxy.host = temp.Split(':')[0];
                proxy.port = temp.Split(':')[1];
                proxy.hasProxy = true;
            }
            proxy.message = responseString;
            proxy.proxyDomain = ProxyDomain.Softlike.ToString();
            return proxy;
        }



        public static string GetPhoneCodeTextNow(string cookie)
        {
            try
            {
                using (var request = new xNet.HttpRequest("https://codetextnow.com/api/api.php"))
                {
                    string reqStr = "action=create-request&serviceId=1&count=1";
                    if (string.IsNullOrEmpty(cookie))
                    {
                        return "";
                    }
                    request.AddHeader("cookie", cookie);
                    string content = request.Post(
                        "https://codetextnow.com/api/api.php", reqStr,
                        "application/x-www-form-urlencoded").ToString();
                    Console.WriteLine("ddd:" + content);
                    return content;
                }
            }
            catch (xNet.HttpException ex)
            {
                Console.WriteLine(ex.HttpStatusCode);
                return "";
            }


            return "";
        }

        public static string GetCodeFromCodeTextNow(string phone, string cookie = "PHPSESSID=l4bc763rmf6ttidn8s9dv944ib" )
        {
            try
            {
                using (var request = new xNet.HttpRequest("https://codetextnow.com/api/api.php"))
                {
                    string reqStr = "start=0&length=200&action=data-request";
                    if (string.IsNullOrEmpty(cookie))
                    {
                        return "";
                    }
                    request.AddHeader("cookie", cookie);
                    string content = request.Post(
                        "https://codetextnow.com/api/api.php", reqStr,
                        "application/x-www-form-urlencoded").ToString();
                    Console.WriteLine("ddd:" + content);
                    if (!string.IsNullOrEmpty(content) && content.Contains("otp"))
                    {
                        string template = string.Format("{0}\",\"otp\":\"(.*?)\"", phone);
                        return Regex.Match(content, template).Groups[1].ToString();
                    } else
                    {
                        return "";
                    }
                    
                }
            }
            catch (xNet.HttpException ex)
            {
                Console.WriteLine(ex.HttpStatusCode);
                return "";
            }


            return "";
        }



        public class ResponsePhoneTextNow
        {
            [JsonProperty("status")]
            public string status { get; set; }


            [JsonProperty("data")]
            public ResponsePhoneData[] data { get; set; }
        }
        public class ResponsePhoneData
        {
            [JsonProperty("sdt")]
            public string sdt { get; set; }

            [JsonProperty("id")]
            public string id { get; set; }
            [JsonProperty("created_time")]
            public long created_time { get; set; }
            [JsonProperty("requestId")]
            public long requestId { get; set; }
        }

        public static List<String> GetPhoneFromCodeTextNow(int start, int length,  string cookie = "PHPSESSID=l4bc763rmf6ttidn8s9dv944ib")
        {
            List<String> phones = new List<string>();
            try
            {
                
                using (var request = new xNet.HttpRequest("https://codetextnow.com/api/api.php"))
                {
                    string reqStr = string.Format("start={0}&length={1}&action=data-request", start, length);
                    if (string.IsNullOrEmpty(cookie))
                    {
                        return phones;
                    }
                    request.AddHeader("cookie", cookie);
                    string content = request.Post(
                        "https://codetextnow.com/api/api.php", reqStr,
                        "application/x-www-form-urlencoded").ToString();
                    Console.WriteLine("ddd:" + content);

                    ResponsePhoneTextNow data = JsonConvert.DeserializeObject<ResponsePhoneTextNow>(content);
                    if (data != null && data.data != null && data.data.Length > 0)
                    {
                        string line = "";
                        for (int i = 0; i < data.data.Length; i ++)
                        {
                            phones.Add(data.data[i].sdt);
                            var json = new JavaScriptSerializer().Serialize(data.data[i]);
                            line = line + json + "\r\n";
                            if (i != data.data.Length - 1)
                            {
                                line = line + json + "\r\n";
                            } else
                            {
                                line = line + json;
                            }
                        }
                        Utility.WriteFileLine(line, "all_data_code_textnow.txt");
                    }
                    
                    else
                    {
                        return phones;
                    }

                }
            }
            catch (xNet.HttpException ex)
            {
                Console.WriteLine(ex.HttpStatusCode);
                return phones;
            }


            return phones;
        }
            public class ResponseMail
            {
                [JsonProperty("result")]
                public bool result { get; set; }
                [JsonProperty("message")]
                public string message { get; set; }

                [JsonProperty("data")]
                public MailData data { get; set; }
            }

        public class ResponsePhone
        {
            [JsonProperty("result")]
            public bool result { get; set; }
            [JsonProperty("message")]
            public string message { get; set; }

            [JsonProperty("data")]
            public PhoneData data { get; set; }
        }
        public class PhoneData
        {
            [JsonProperty("phoneNumber")]
            public string phoneNumber { get; set; }
            

        }
        public class MailData
        {
            [JsonProperty("username")]
            public string username { get; set; }
            [JsonProperty("password")]
            public string password { get; set; }
            
        }

         

        public static MailObject GetHotmailLocalCache(string server, string type)
        {
            MailObject mail = new MailObject();
            try
            {
                string apiGetHotmail = "api/hotmail";
                var client = new RestClient(server);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetHotmail);


                var response = client.Get(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("get mail:" + content);
                string decode = Utility.Decode_UTF8(content);
                MailObject data = JsonConvert.DeserializeObject<MailObject>(decode);

                if (data != null && !string.IsNullOrEmpty(data.email))
                {
                    return data;
                }
            }
            catch (Exception ex)
            {

            }
            return mail;
        }

        public static MailObject Get1SecMail()
        {
            MailObject mail = new MailObject();
            try
            {
                var client = new RestClient("https://www.1secmail.com/api/");
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("v1/");

                request.AddParameter("action", "genRandomMailbox");
               
                request.AddParameter("count", 10);


                
                var response = client.Get(request);
                var content = response.Content; // Raw content as string

                if (!string.IsNullOrEmpty(content))
                {
                    string temp = content.Replace("[\"", "").Replace("\"","");
                    string[] teemm = temp.Split(',');
                    for (int i = 0; i < teemm.Length; i ++)
                    {
                        if (teemm[i].Contains("rteet.com") || teemm[i].Contains("1sec"))
                        {
                            mail.email = teemm[i];
                            mail.type = Constant.TEMP_GENERATOR_1_SEC_EMAIL;
                            mail.password = Constant.TEMPMAIL;
                            return mail;
                        }
                    }
                    
                }

               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return mail;
        }

        public class Response1SecMail
        {
            [JsonProperty("msg")]
            public Mail1Sec[] msg { get; set; }
        }
        public class Mail1Sec
        {
            [JsonProperty("subject")]
            public string subject { get; set; }  
        }
        public static List<string> GetAllSubject1SecMail(MailObject inMail)
        {
            List<string> subjects = new List<string>();
            try
            {
                System.Net.Mail.MailAddress addr = new System.Net.Mail.MailAddress(inMail.email);
                string username = addr.User;
                string domain = addr.Host;
                var client = new RestClient("https://www.1secmail.com/api/");
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("v1/");

                request.AddParameter("action", "getMessages");

                request.AddParameter("login", username);

                request.AddParameter("domain", domain);

                var response = client.Get(request);
                var content = response.Content; // Raw content as string
                JavaScriptSerializer ser = new JavaScriptSerializer();
                List<Mail1Sec> records = ser.Deserialize<List<Mail1Sec>>(content);

                if (records != null && records.Count > 0)
                {
                    for (int i = 0; i < records.Count; i ++)
                    {
                        subjects.Add(records[i].subject);
                    }
                    return subjects;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return subjects;
        }
        public static MailObject GetMailServer(string type)
        {
            try
            {
                RequestHTTP requestHTTP = new RequestHTTP();
                //requestHTTP.SetSSL(SecurityProtocolType.Tls12);
                //requestHTTP.SetKeepAlive(true);
                MailObject mail = new MailObject();
                string resString = requestHTTP.Request("GET", string.Format("{0}/emails/get?type={1}&limit=1", SERVER_HOST, type));

                try
                {
                    ResponseMail data = JsonConvert.DeserializeObject<ResponseMail>(resString);
                    if (data != null && data.data != null)
                    {
                        mail.email = data.data.username;
                        mail.password = data.data.password;
                        return mail;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
            }
            catch (Exception eee)
            {
                return null;
            }

            return null;
        }
        public static string GetPhones(bool isServer, string network = "", string prefix = "")
        {

            if (!isServer)
            {
                return "";
            }

            try
            {
                var client = new RestClient(SERVER_HOST);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("phones/get");
                if (!string.IsNullOrEmpty(prefix))
                {
                    request.AddParameter("prefix", prefix);
                }
                if (!string.IsNullOrEmpty(network))
                {
                    request.AddParameter("network", Utility.StandardNetwork(network));
                }

                var response = client.Get(request);
                var content = response.Content; // Raw content as string
         
                try
                {
                    ResponsePhone data = JsonConvert.DeserializeObject<ResponsePhone>(content);
                    if (data != null && data.data != null)
                    {
                        return data.data.phoneNumber;
                    }
                    else
                    {
                        return Constant.FAIL;
                    }
                }
                catch
                {
                    return Constant.FAIL;
                }
            } catch (Exception eee )
            {
                return Constant.FAIL;
            }
        }
        public class ResponseProxyObject
        {
            [JsonProperty("msg")]
            public string msg { get; set; }
            [JsonProperty("status")]
            public bool status { get; set; }


        }
        public static string GetChangeIp(string host, string proxy)
        {
            try
            {
                using (var request = new xNet.HttpRequest(host))
                {
                    request.UserAgent = xNet.Http.ChromeUserAgent();

                    request
                    .AddUrlParam("proxy", proxy)
                    .AddHeader("X-Apocalypse", "21.12.12");
                    // These parameters are sent in this request.
                    string resString = request.Get("/reset").ToString();
                    try
                    {
                        ResponseProxyObject data = JsonConvert.DeserializeObject<ResponseProxyObject>(resString);
                        if (data.status)
                        {
                            return "true";
                        }
                        else
                        {
                            return Constant.FAIL;
                        }
                    } catch
                        {
                        return Constant.FAIL;
                    }

                    
                }
            }
            catch (xNet.HttpException ex)
            {
                Console.WriteLine(ex.HttpStatusCode);
                return Constant.FAIL;
            }
        }
        public static bool Log(bool isServer, string data)
        {
            return true;
        }
        public static bool PostData(bool isServer, string data, string status = "checking", string type = "normal")
        {
            try
            {
                if (!isServer)
                {
                    return true;
                }

                var client = new RestClient(SERVER_HOST);
               
                var request = new RestRequest("accounts/create");
                request.AddParameter("data", data);
                request.AddParameter("status", status);
                request.AddParameter("type", type);
                var response = client.Post(request);
                var resString = response.Content; // Raw content as string


                if (resString == "true")
                {
                    return true;
                }
            }
            catch (xNet.HttpException ex)
            {
                Console.WriteLine(ex.HttpStatusCode);
                return false;
            }
            

            return false;
        }

        public class ResponseObject
        {
            [JsonProperty("result")]
            public bool result { get; set; }
            [JsonProperty("Message")]
            public string Message { get; set; }
            [JsonProperty("Data")]
            public AccountData[] Data { get; set; }
        }
        public class ResponseObjectGetAcc
        {
            [JsonProperty("result")]
            public bool result { get; set; }
            [JsonProperty("message")]
            public string message { get; set; }
            [JsonProperty("data")]
            public AccountData data { get; set; }
        }
        public class AccountData
        {
            [JsonProperty("note")]
            public string note { get; set; }
            [JsonProperty("uid")]
            public string uid { get; set; }
            
            [JsonProperty("pass")]
            public string pass { get; set; }
            [JsonProperty("qrcode")]
            public string qrcode { get; set; }
            [JsonProperty("email")]
            public string email { get; set; }
            [JsonProperty("emailPass")]
            public string emailPass { get; set; }
            [JsonProperty("gender")]
            public string gender { get; set; }

            [JsonProperty("language")]
            public string language { get; set; }

            [JsonProperty("hasAvatar")]
            public bool hasAvatar { get; set; }
            [JsonProperty("has2fa")]
            public bool has2fa { get; set; }
            [JsonProperty("pcName")]
            public string pcName { get; set; }
            [JsonProperty("createdAt")]
            public string createdAt { get; set; }
            [JsonProperty("updatedAt")]
            
            public string updatedAt { get; set; }
            
            [JsonProperty("token")]
            public string token { get; set; }
            [JsonProperty("deviceId")]
            public string deviceId { get; set; }

            [JsonProperty("type")]
            public string type { get; set; }
        }
        public static int CountAccInHourData()
        {
            try
            {
                DateTime now = DateTime.Now;
                DateTime lastHour = now.AddMinutes(-20);
                string lastHourString = lastHour.ToString("yyyy-MM-dd HH:mm:ss");
                using (var request = new xNet.HttpRequest(SERVER_HOST))
                {
                    request.UserAgent = xNet.Http.ChromeUserAgent();
                    //request.Proxy = Socks5ProxyClient.Parse("127.0.0.1:1080");

                    request
                        //.AddUrlParam("fromDate", lastHourString)
                        .AddUrlParam("checkUsed", false)
                        .AddUrlParam("apiKey", "longphi@fb")

                        .AddHeader("X-Apocalypse", "21.12.12");

                    string resString = request.Get("/accounts/count-created").ToString();
                    try
                    {
                        ResponseObject data = JsonConvert.DeserializeObject<ResponseObject>(resString);
                        if (data != null && !string.IsNullOrEmpty(data.Message))
                        {
                            return Convert.ToInt32(data.Message) * 3;
                        }
                        return 0;
                    } catch
                    {
                        return 0;
                    }
                    
                }
            }
            catch (xNet.HttpException ex)
            {
                Console.WriteLine(ex.HttpStatusCode);
                return 0;
            }
        }

        public static Account TryToGetAccNoveri(string deviceID, string language, bool force)
        {
            Account acc = GetNvrForVeri(deviceID, language);
            //Account acc = GetNvrForVeri("", language);
            if (string.IsNullOrEmpty(deviceID))
            {
                acc.message = "Get Acc không theo đúng máy reg------";
            }
            if (acc == null || string.IsNullOrEmpty(acc.uid))
            {
                if (force)
                {
                    //Thread.Sleep(10000);
                    return acc;
                }
                //acc = GetNvrForVeri("", language);
                if (acc == null || string.IsNullOrEmpty(acc.uid))
                {
                    acc = GetNvrForVeri("", "");
                } else
                {
                    acc.message = "Get acc random máy";
                }
            } else
            {
                acc.message = "Get Acc theo đúng máy reg------";
                
            }
            if (!string.IsNullOrEmpty(deviceID) && acc != null && !string.IsNullOrEmpty(acc.deviceID) && deviceID != acc.deviceID)
            {
                acc.message = "Get Acc không theo đúng máy reg-> Hết acc cùng máy";
                //Thread.Sleep(4000);
            }

            return acc;
        }

        public static Account TryToGetAccWaitInfo(string deviceID, string language)
        {
            Account acc = GetAccWaitInfo(deviceID, language);
            if (acc == null || string.IsNullOrEmpty(acc.uid))
            {
                acc = GetAccWaitInfo("", language);
                if (acc == null || string.IsNullOrEmpty(acc.uid))
                {
                    acc = GetAccWaitInfo("", "");
                }
                else
                {
                    acc.message = "Get acc random máy";
                }
            }
            else
            {
                acc.message = "Get Acc theo đúng máy reg------";
            }

            return acc;
        }

       
        public static Account GetNvrForVeri( string deviceID, string language)
        {
            OrderObject order = new OrderObject();
            
            order.deviceID = deviceID;
            order.language = language;
            ResponseObjectGetAcc res = QueryAccNoVeri(order, true);
            if (res != null && res.data != null )
            {
                Account acc = new Account();
                acc.uid = res.data.uid;
                acc.pass = res.data.pass;
                acc.gender = res.data.gender;
                acc.language = res.data.language;
                acc.token = res.data.token;
                try
                {
                    acc.cookie = res.data.note.Split('|')[2];
                    if (string.IsNullOrEmpty(acc.token))
                    {
                        acc.token = res.data.note.Split('|')[3];
                    }
                } catch
                {

                }
                acc.email = res.data.email;
                acc.emailPass = res.data.emailPass;
                acc.hasAvatar = res.data.hasAvatar;
                acc.createdAt = res.data.createdAt;
                acc.qrCode = res.data.qrcode;
                acc.deviceID = res.data.deviceId;
                return acc;
            }
            return null;
        }

        public static Account GetAccWaitInfo(string deviceID, string language)
        {
            OrderObject order = new OrderObject();

            order.deviceID = deviceID;
            order.language = language;
            ResponseObjectGetAcc res = QueryAccWaitInfo(order, true);
            if (res != null && res.data != null)
            {
                Account acc = new Account();
                acc.uid = res.data.uid;
                acc.pass = res.data.pass;
                acc.gender = res.data.gender;
                acc.language = res.data.language;
                acc.token = res.data.token;
                try
                {
                    acc.cookie = res.data.note.Split('|')[2];
                    if (string.IsNullOrEmpty(acc.token))
                    {
                        acc.token = res.data.note.Split('|')[3];
                    }
                }
                catch
                {

                }
                
                acc.email = res.data.email;
                acc.emailPass = res.data.emailPass;
                acc.hasAvatar = res.data.hasAvatar;
                acc.has2fa = res.data.has2fa;
                acc.createdAt = res.data.createdAt;
                acc.qrCode = res.data.qrcode;
                acc.type = res.data.type;
                return acc;
            }
            return null;
        }

        public static ResponseObjectGetAcc QueryAccWaitInfo(OrderObject order, bool isDownload)
        {
            try
            {
                var client = new RestClient(SERVER_HOST);
                // client.Authenticator = new HttpBasicAuthenticator(username, password);
                var request = new RestRequest("accounts/get-wait-to-upload");
                request.AddParameter("language", order.language);
                request.AddParameter("deviceId", order.deviceID);
                var response = client.Get(request);
                var resString = response.Content; // Raw content as string



                //using (var request = new xNet.HttpRequest(SERVER_HOST))
                //{
                //    request.UserAgent = xNet.Http.ChromeUserAgent();
                   

                //    request

                //        .AddUrlParam("language", order.language)
           

                //        .AddUrlParam("deviceId", order.deviceID)
    
                //        .AddHeader("X-Apocalypse", "21.12.12");

                //    // These parameters are sent in this request.
                //    string resString = request.Get("/accounts/get-wait-to-upload").ToString();

                try
                {
                    ResponseObjectGetAcc data = JsonConvert.DeserializeObject<ResponseObjectGetAcc>(resString);

                    return data;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ex:" + ex.Message);
                    return null;
                }

      
            }
            catch (xNet.HttpException ex)
            {
                Console.WriteLine(ex.HttpStatusCode);
                return null;
            }


            return null;

        }
        public static ResponseObjectGetAcc QueryAccNoVeri(OrderObject order, bool isDownload)
        {
            try
            {
                
                var client = new RestClient(SERVER_HOST);
                // client.Authenticator = new HttpBasicAuthenticator(username, password);
                var request = new RestRequest("accounts/get-wait-to-veri");
                request.AddParameter("language", order.language);
                request.AddParameter("deviceId", order.deviceID);
                var response = client.Get(request);
                var resString = response.Content; // Raw content as string
                try
                    {
                        ResponseObjectGetAcc data = JsonConvert.DeserializeObject<ResponseObjectGetAcc>(resString);

                        return data;
                    } catch (Exception ex)
                    {
                        Console.WriteLine("ex:" + ex.Message);
                        return null;
                    }
                }
            catch (xNet.HttpException ex)
            {
                Console.WriteLine(ex.HttpStatusCode);
                return null;
            }


            return null;
        }



        public static bool UploadAuthAcc(string path, string uid)
        {
            try
            {
                var client = new RestClient(SERVER_HOST);
                // client.Authenticator = new HttpBasicAuthenticator(username, password);
                var request = new RestRequest("accounts/resource/" + uid);

                request.AddFile("file", path);
                var response = client.Post(request);
                var resString = response.Content; // Raw content as string
                if (!string.IsNullOrEmpty(resString))
                {
                    return true;
                }
            }
            catch (xNet.HttpException ex)
            {
                Console.WriteLine(ex.HttpStatusCode);
                return false;
            }


            return false;
        }

        public static bool GetBackupAcc(string uid)
        {
            try
            {
                var client = new RestClient(SERVER_HOST);
                // client.Authenticator = new HttpBasicAuthenticator(username, password);
                var request = new RestRequest("accounts/resource/" + uid);

                byte [] response = client.DownloadData(request);
                if (response != null && response.Length > 0)
                {
                    File.WriteAllBytes("Authentication/" + uid + ".zip", response);
                    return true;
                }
                else
                {
                    return false;
                }
                
               


                //using (var request = new xNet.HttpRequest(SERVER_HOST))
                //{
                //    request.UserAgent = xNet.Http.ChromeUserAgent();
                    
                //    request
                //        // HTTP-header.
                //        .AddHeader("X-Apocalypse", "21.12.12");

                //    // These parameters are sent in this request.
                //    var response = request.Get("accounts/resource/" + uid);
                    
                //    var stream = response.ToMemoryStream();
                //    if (stream != null && response.ContentLength > 0)
                //    {
                //        using (FileStream file = new FileStream("Authentication/" + uid + ".zip", FileMode.Create, FileAccess.Write))
                //        {
                //            stream.WriteTo(file);
                //        }
                //        return true;
                //    } else
                //    {
                //        return false;
                //    }
                    
                //}
            }
            catch (xNet.HttpException ex)
            {
                Console.WriteLine("ServerApi get Backup acc" + ex.HttpStatusCode);
                return false;
            }
        }

        public static bool DeleteAccWait2Veri(string uid)
        {
            try
            {
                var client = new RestClient(SERVER_HOST);

                var request = new RestRequest("accounts/delete-verify");
                request.AddParameter("uids", uid);
                
                var response = client.Post(request);
                var resString = response.Content; // Raw content as string


                if (resString == "")
                {
                    return true;
                }
            }
            catch (xNet.HttpException ex)
            {
                Console.WriteLine(ex.HttpStatusCode);
                return false;
            }

            return false;
        }

        public class ResponseDongVanObject
        {
            [JsonProperty("error_code")]
            public int error_code { get; set; }
            [JsonProperty("status")]
            public bool status { get; set; }
            [JsonProperty("message")]
            public string message { get; set; }


            [JsonProperty("data")]
            public DataDongVanObject data { get; set; }
        }
        public class DataDongVanObject
        {
            [JsonProperty("order_code")]
            public string order_code { get; set; }
            [JsonProperty("account_type")]
            public string account_type { get; set; }
            [JsonProperty("quality")]
            public int quality { get; set; }


            [JsonProperty("list_data")]
            public string[] list_data { get; set; }
        }
        public static ResponseDongVanObject GetHotMailDongVanApi(int count, int mailType)
        {
            try
            {
                string apiGetHotMail = string.Format("https://api.dongvanfb.net/user/buy?apikey={0}&account_type={1}&quality={2}&type=full", "b4e2f1a9dc40f4aad654c570ee6418f5", mailType, count);
                var request = (HttpWebRequest)WebRequest.Create(apiGetHotMail);
                request.Method = "GET";
                request.Accept = "application/json";
                request.ContentType = "application/json; charset=utf-8";

                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return JsonConvert.DeserializeObject<ResponseDongVanObject>(responseString);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public class NameDataRequest
        {
            [JsonProperty("state")]
            public string state { get; set; }

            [JsonProperty("year")]
            public string year { get; set; }
        }

        public static List<string> GetPopularName(string state, string year)
        {
            List<string> result = new List<string>();
            try
            {
                
                //dataRequest.type = "facebook";
                var client = new RestClient("https://www.ssa.gov/cgi-bin/namesbystate.cgi");
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("", Method.POST);
                //request.AddHeader("X-API-TOKEN", "WVkyQkhBR0lQVzRJMElYOE5QUVM=");

                var body = string.Format(@"state={0}&year={1}", state, year);
                request.AddParameter("application/x-www-form-urlencoded", body, ParameterType.RequestBody);

    
                var response = client.Execute(request);
                var content = response.Content; // Raw content as string

                string template = "    <td align=\"center\">(.*?)</td>";
                Regex.Match(content, template).Groups[1].ToString();

                Regex expression = new Regex(template);
                var results = expression.Matches(content);
                foreach (Match match in results)
                {
                    Console.WriteLine(match.Groups[1].Value);
                    result.Add(match.Groups[1].Value);
                }
                Console.WriteLine("all mail:" + content);
               
            }
            catch (Exception eee)
            {
                
            }
            return result;
        }

        public static void GetAllPopularName()
        {
            string[] states = new string[] { "AL", "AK" 
            , "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD", "MA", 
                "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY"};

            List<string> maleName = new List<string>();
            List<string> femaleName = new List<string>();
            for (int i = 2023; i >= 1960; i --)
            {



                for (int k = 0;k < states.Length; k ++)
                {
                    Console.WriteLine("state:" + states[k] + " year:" + i);
                    List<string> temp = GetPopularName(states[k], "" + i);
                    if (temp.Count == 200)
                    {
                        bool filter = true;
                        for (int m = 0; m < temp.Count; m++)
                        {
                            if (filter)
                            {
                                if (!maleName.Contains(temp[m]))
                                {
                                    maleName.Add(temp[m]);
                                }
                                filter = false;
                            } else
                            {
                                if (!femaleName.Contains(temp[m]))
                                {
                                    femaleName.Add(temp[m]);
                                }
                                filter = true;
                            }
                        }
                    }
                }
            }

            System.IO.Directory.CreateDirectory("data/name");
            string maleFile = "data/name/male_raw.txt";
            string femaleFile = "data/name/female_raw.txt";
            try
            {
                File.Delete(maleFile);
                File.Delete(femaleFile);
            }
            catch (IOException ex)
            {
                Console.WriteLine("ex:" + ex.Message);
            }
           
            for (int i = 0; i < maleName.Count; i ++)
            {
                File.AppendAllText(maleFile, maleName[i] + "\n");
            }
            for (int i = 0; i < femaleName.Count; i++)
            {
                File.AppendAllText(femaleFile, femaleName[i] + "\n");
            }
        }
    }
}
