using ActiveUp.Net.Security.OpenPGP.Packets;
using fb_reg.RequestApi;
using Newtonsoft.Json;
using OpenQA.Selenium;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using static fb_reg.Utility;

namespace fb_reg
{
    public class OutsideServer
    {

        public static bool CheckInternet()
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = ping.Send("8.8.8.8", 1000);
                    return reply.Status == IPStatus.Success;
                }
            }
            catch { return false; }
        }

        public static bool CheckDNS()
        {
            try
            {
                var entry = Dns.GetHostEntry("facebook.com");
                return entry.AddressList.Length > 0;
            }
            catch { return false; }
        }

        public static bool CheckFacebookAccessible()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(3);
                    var response = client.GetAsync("https://m.facebook.com").Result;
                    return response.IsSuccessStatusCode;
                }
            }
            catch { return false; }
        }

        public static void WaitUntilNetworkStable(int maxRetries = 60, int delayMs = 2000)
        {
            return;
            Console.WriteLine("[i] Kiểm tra mạng...");

            for (int i = 0; i < maxRetries; i++)
            {
                if (CheckInternet() && CheckDNS() && CheckFacebookAccessible())
                {
                    Console.WriteLine("[✓] Mạng ổn định. Bắt đầu reg clone.");
                    return;
                }

                Console.WriteLine($"[!] Mạng chưa ổn định (thử {i + 1}/{maxRetries})...");
                Thread.Sleep(delayMs);
            }

            throw new Exception("❌ Mạng không ổn định sau nhiều lần thử.");
        }
        public static void WaitUntilProxyStable(Proxy proxy, int maxRetries = 60, int delayMs = 2000)
        {
            //return;
            Console.WriteLine("[i] Kiểm tra mạng...");

            for (int i = 0; i < maxRetries; i++)
            {
                if (TestProxy(proxy.host, Int32.Parse( proxy.port), proxy.username, proxy.pass))
                {
                    Console.WriteLine("[✓] Proxy ổn định. Bắt đầu reg clone.");
                    return;
                }

                Console.WriteLine($"[!] Proxy chưa ổn định (thử {i + 1}/{maxRetries})...");
                Thread.Sleep(delayMs);
            }

            throw new Exception("❌ Proxy không ổn định sau nhiều lần thử.");
        }
        public static bool TestProxy(string host, int port, string username, string password)
        {
            try
            {
                var proxy = new WebProxy(host, port)
                {
                    Credentials = new NetworkCredential(username, password)
                };

                var request = (HttpWebRequest)WebRequest.Create("https://m.facebook.com/");
                request.Proxy = proxy;
                request.Timeout = 5000;
                request.Method = "GET";

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Proxy lỗi: " + ex.Message);
                return false;
            }
        }



        public class HvlReq
        {
            [JsonProperty("service")]
            public string service { get; set; }


            [JsonProperty("gmail")]
            public string gmail { get; set; }
        }

        public class HvlResp
        {
            [JsonProperty("code")]
            public int code { get; set; }


            [JsonProperty("message")]
            public string message { get; set; }

            [JsonProperty("data")]
            public HvlRespData data { get; set; }

        }
        public class HvlRespData
        {
            [JsonProperty("orderId")]
            public string orderId { get; set; }


            [JsonProperty("email")]
            public string email { get; set; }

            [JsonProperty("service")]
            public string service { get; set; }

            [JsonProperty("price")]
            public int price { get; set; }

            [JsonProperty("balanceAfter")]
            public int balanceAfter { get; set; }
        }

        public class HvlOtpResp
        {
            [JsonProperty("code")]
            public int code { get; set; }


            [JsonProperty("message")]
            public string message { get; set; }

            [JsonProperty("data")]
            public HvlOtpRespData data { get; set; }

        }
        public class HvlOtpRespData
        {
            [JsonProperty("orderId")]
            public string orderId { get; set; }


            [JsonProperty("email")]
            public string email { get; set; }

            [JsonProperty("status")]
            public string status { get; set; }

            [JsonProperty("otpReceivedTime")]
            public int otpReceivedTime { get; set; }

            [JsonProperty("otpSender")]
            public string otpSender { get; set; }
            [JsonProperty("otpContent")]
            public string otpContent { get; set; }
            [JsonProperty("otpCode")]
            public string otpCode { get; set; }
        }


        public static MailObject GetMailHvl()
        {
            try
            {
                if (!PublicData.GetHvlMaillocal)
                {
                    return null;
                }
                string key = PublicData.AccessTokengmailHvl;
                HvlReq mail = new HvlReq();
                mail.gmail = "";
                mail.service = "FACEBOOK";
                string server = "https://gm.haivl.wtf/";
                string apiGetSellGmail = "api/mail/otp-rent";
                var client = new RestClient(server);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetSellGmail);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("ZGM-API-TOKEN", key);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(mail);

                var response = client.Post(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("get mail:" + content);
                string decode = Utility.Decode_UTF8(content);
                HvlResp data = JsonConvert.DeserializeObject<HvlResp>(decode);

                if (data != null && data.data != null && !string.IsNullOrEmpty(data.data.email))
                {
                    MailObject respmail = new MailObject();
                    respmail.email = data.data.email;
                    respmail.orderId = data.data.orderId;
                    respmail.password = Constant.GMAIL_SELL_GMAIL;
                    respmail.key = key;
                    respmail.source = "hvl";
                    respmail.balanceAfter = data.data.balanceAfter;
                    return respmail;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public static string GetOtpHvl(MailObject mail)
        {
            string otp = "";
            try
            {
                string key = mail.key;
                var client = new RestClient("https://gm.haivl.wtf/");
                var request = new RestRequest("api/mail/otp-get", Method.GET);


                request.AddHeader("ZGM-API-TOKEN", key);
                request.AddParameter("orderId", mail.orderId);
                request.Timeout = 20000;
                var response = client.Execute(request);
                var content = response.Content; // Raw content as string

                try
                {
                    HvlOtpResp data = JsonConvert.DeserializeObject<HvlOtpResp>(content);

                    if (data != null && data.data != null && !string.IsNullOrEmpty(data.data.otpCode))
                    {
                        return data.data.otpCode;
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
                Console.WriteLine("GetGmailSuperTeam:" + eee.Message);
                return null;
            }


            return otp;
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

        public static MailObject GetGmailDichVuGmail(string key, string serviceId = "Facebook")
        {
            
            if (!PublicData.GetMailDvgm && ! PublicData.GetMailDvgmNormal)
            {
                return null;
            }
            MailObject mail = new MailObject();
            try
            {
                string apiGetHotMail = string.Format("{0}/{1}",
                key,
                serviceId);
                var client = new RestClient("https://boxreceive.com/DataMail/Mail/");
                //client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetHotMail);

                request.Timeout = 20000;
                var response = client.Get(request);
                var content = response.Content; // Raw content as string
                //if (content.Contains("500"))
                //{
                //    Console.WriteLine("Loi het mail:" + content);
                //    return mail;
                //}
                Console.WriteLine("get mail:" + content);
                string decode = Decode_UTF8(content);
                DichVuGmailEmail data = JsonConvert.DeserializeObject<DichVuGmailEmail>(decode);

                if (data != null && data.status == 200 && !string.IsNullOrEmpty(data.orders.gmail) && data.orders.status == "Wait")
                {
                    mail.email = data.orders.gmail;
                    mail.password = Constant.GMAIL_SELL_GMAIL;
                    mail.orderId = data.orders.order_id;
                    mail.source = "dvgm";
                    mail.key = "PtcRfCJe0UjBk4iJ2umU98ZnE7rzp0sJ";
                    mail.accessToken = key;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetGmailDichVuGmail:" + ex.Message);
            }



            return mail;
        }
        public class GmailSuperTeamDatarequest
        {
            [JsonProperty("gmail")]
            public string gmail { get; set; }
            [JsonProperty("success")]
            public bool success { get; set; }
        }

        
        public static MailObject GetGmailSuperTeam(string key)
        {
            try
            {
             
                if (!PublicData.GetMailSptLocal)
                {
                    return null;
                }
                var client = new RestClient("https://api.sptmail.com/api/otp-services/mail-otp-rental/");
                //client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("", Method.GET);


                request.AddParameter("apiKey", key);
                request.AddParameter("otpServiceCode", "facebook");
                request.Timeout = 20000;
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
                        mail.source = "super_gmail";
                        mail.accessToken = key;
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
                Console.WriteLine("GetGmailSuperTeam:" + eee.Message);
                return null;
            }
        }

        public class GmailThueSimDatarequest
        {
            [JsonProperty("id")]
            public string id { get; set; }
            
            [JsonProperty("mail")]
            public string mail { get; set; }

            [JsonProperty("serviceName")]
            public string serviceName;
            [JsonProperty("status")]
            public string status { get; set; }
            [JsonProperty("createdAt")]
            public string createdAt { get; set; }

        }
        public static MailObject GetGmailThueSimTeam()
        {
            try
            {
                if (! PublicData.GetMailThuesim)
                {
                    return null;
                }
                var client = new RestClient("https://api.thuesim.app/rent/email/facebook/" + PublicData.AccessTokenThueSimGmail);
                //client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("", Method.GET);
                request.Timeout = 2000;
                var response = client.Execute(request);
                var content = response.Content; // Raw content as string

                try
                {
                    Console.WriteLine("thuesimgmail:" + content);
                    if (content == null || content.Contains("Error"))
                    {
                        return null;
                    }

                    GmailThueSimDatarequest data = JsonConvert.DeserializeObject<GmailThueSimDatarequest>(content);

                    if (data != null && !string.IsNullOrEmpty(data.mail))
                    {
                        MailObject mail = new MailObject();
                        mail.email = data.mail;
                        mail.password = Constant.GMAIL_SELL_GMAIL;
                        mail.clientId = data.id;
                        mail.source = "thuesim_gmail";
                        mail.token = PublicData.AccessTokenThueSimGmail;
                        mail.accessToken = PublicData.AccessTokenThueSimGmail;
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
                Console.WriteLine("thuesim:" + eee.Message);
                return null;
            }
        }

        public static MailObject GetGmailThueSimVipTeam()
        {
            try
            {
                if (!PublicData.GetMailThuesimVip)
                {
                    return null;
                }
                var client = new RestClient("https://api.thuesim.app/rent/email/facebookvip/" + PublicData.AccessTokenThueSimGmail);
                //client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("", Method.GET);
                request.Timeout = 2000;
                var response = client.Execute(request);
                var content = response.Content; // Raw content as string

                try
                {
                    Console.WriteLine("thuesimgmail:" + content);
                    if (content == null || content.Contains("Error"))
                    {
                        return null;
                    }

                    GmailThueSimDatarequest data = JsonConvert.DeserializeObject<GmailThueSimDatarequest>(content);

                    if (data != null && !string.IsNullOrEmpty(data.mail))
                    {
                        MailObject mail = new MailObject();
                        mail.email = data.mail;
                        mail.password = Constant.GMAIL_SELL_GMAIL;
                        mail.clientId = data.id;
                        mail.source = "thuesim_gmail_vip";
                        mail.token = PublicData.AccessTokenThueSimGmail;
                        mail.accessToken = PublicData.AccessTokenThueSimGmail;
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
                Console.WriteLine("thuesim:" + eee.Message);
                return null;
            }
        }
        public class GmailOtpCheapDatarequest
        {
            [JsonProperty("success")]
            public bool success { get; set; }

            [JsonProperty("quid")]
            public string quid { get; set; }

           

        }
        public class GmailOtpCheapDetailDatarequest
        {
            [JsonProperty("quid")]
            public string quid { get; set; }

            [JsonProperty("email")]
            public string email { get; set; }


            [JsonProperty("code")]
            public string code { get; set; }

            [JsonProperty("status")]
            public string status { get; set; }
        }
        public static MailObject GetGmailOtpCheap()
        {
            try
            {
                var client = new RestClient("https://api.otp.cheap/stubs/action/neworder");
                //client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("", Method.GET);
                request.AddQueryParameter("api_key", PublicData.AccessTokenOtpCheap);
                request.AddQueryParameter("service_id", "facebook");
                request.AddQueryParameter("priority", "cheap");
                request.AddQueryParameter("amount", "1");
                

                request.Timeout = 2000;
                var response = client.Execute(request);
                var content = response.Content; // Raw content as string

                try
                {
                    Console.WriteLine("Otpcheap:" + content);
                    if (content == null || content.Contains("Error"))
                    {
                        return null;
                    }
                    for (int i = 0; i < 3; i ++)
                    {
                        GmailOtpCheapDatarequest data = JsonConvert.DeserializeObject<GmailOtpCheapDatarequest>(content);

                        if (data != null && !string.IsNullOrEmpty(data.quid))
                        {

                            client = new RestClient("https://api.otp.cheap/stubs/action/getorder");
                            //client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                            request = new RestRequest("", Method.GET);
                            request.AddQueryParameter("api_key", PublicData.AccessTokenOtpCheap);
                            request.AddQueryParameter("quid", data.quid);



                            request.Timeout = 2000;
                            response = client.Execute(request);
                            content = response.Content; // Raw content as string

                            try
                            {
                                Console.WriteLine("otpcheal detail:" + content);
                                if (content == null || content.Contains("Error"))
                                {
                                    return null;
                                }

                                GmailOtpCheapDetailDatarequest dataDetail = JsonConvert.DeserializeObject<GmailOtpCheapDetailDatarequest>(content);

                                if (dataDetail != null && !string.IsNullOrEmpty(dataDetail.email))
                                {
                                    MailObject mail = new MailObject();
                                    mail.email = dataDetail.email;
                                    mail.password = Constant.GMAIL_SELL_GMAIL;
                                    mail.orderId = data.quid;
                                    mail.source = "otpcheap_gmail";
                                    mail.token = PublicData.AccessTokenOtpCheap;
                                    mail.accessToken = PublicData.AccessTokenOtpCheap;
                                    return mail;
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        
                        
                    }
                }
                catch
                {
                    return null;
                }
            }
            catch (Exception eee)
            {
                Console.WriteLine("thuesim:" + eee.Message);
                return null;
            }
            return null;
        }


        public class GmailShopgmailDatarequest
        {
            [JsonProperty("status")]
            public string status { get; set; }

            [JsonProperty("username")]
            public string username { get; set; }

            [JsonProperty("availableEmail")]
            public string availableEmail;

            [JsonProperty("orderNumber")]
            public string orderNumber { get; set; }


        }
        public static MailObject GetGmailShopGmailTeam(string key)
        {
            try
            {
                if (!PublicData.GetShopgmailLocal)
                {
                    return null;
                }
                string user = "farmer20";
                if (PublicData.AccessTokenShopMail9999Current == PublicData.AccessTokenShopMail9999Vip)
                {
                    user = "bkukuniver";
                }
                var client = new RestClient(string.Format("https://api.shopgmail9999.com/api/ApiUsers/createorder?username={0}&apikey={1}&service={2}", user, key, "facebook"));
                //client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("", Method.GET);
                request.Timeout = 2000;

                var response = client.Execute(request);
                var content = response.Content; // Raw content as string

                try
                {

                    if (content == null || content.ToLower().Contains("error"))
                    {
                        Console.WriteLine("shopgmail:" + content);
                        return null;
                    }

                    GmailShopgmailDatarequest data = JsonConvert.DeserializeObject<GmailShopgmailDatarequest>(content);

                    if (data != null && !string.IsNullOrEmpty(data.availableEmail))
                    {
                        MailObject mail = new MailObject();
                        mail.email = data.availableEmail;
                        mail.password = Constant.GMAIL_SELL_GMAIL;
                        mail.clientId = data.orderNumber;
                        mail.source = "shopgmail_gmail";
                        mail.accessToken = key;
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
                Console.WriteLine("shopgmail:" + eee.Message);
                return null;
            }
        }
        public static MailObject GetGmailAllSite(bool vip)
        {
            MailObject gmail = new MailObject();

            gmail = GetGmailUnlimited();
            if (gmail != null && !string.IsNullOrEmpty(gmail.email))
            {
                return gmail;
            }

            gmail = GetGmailSuperTeam(PublicData.AccessTokenSuperGmailNormal);
            if (gmail != null && !string.IsNullOrEmpty(gmail.email))
            {
                return gmail;
            }
            
            gmail = GetGmailThueSimTeam();
            if (gmail != null && !string.IsNullOrEmpty(gmail.email))
            {
                return gmail;
            }
            gmail = GetGmailThueSimVipTeam();
            if (gmail != null && !string.IsNullOrEmpty(gmail.email))
            {
                return gmail;
            }
            gmail = GetGmailSuperTeam(PublicData.AccessTokenSuperGmailVip);
            if (gmail != null && !string.IsNullOrEmpty(gmail.email))
            {

                return gmail;
            }
            gmail = GetGmailDichVuGmail(PublicData.AccessTokenDvgmNormal);
            if (gmail != null && !string.IsNullOrEmpty(gmail.email))
            {
                return gmail;
            }
            gmail = GetMailHvl();
            if (gmail != null && !string.IsNullOrEmpty(gmail.email))
            {

                return gmail;
            }

            if (vip)
            {
                gmail = GetGmailShopGmailTeam(PublicData.AccessTokenShopMail9999Current);
                if (gmail != null && !string.IsNullOrEmpty(gmail.email))
                {
                    return gmail;
                }



                gmail = GetGmailSuperTeam(PublicData.AccessTokenSuperGmailCurrent);
                if (gmail != null && !string.IsNullOrEmpty(gmail.email))
                {

                    return gmail;
                }

                gmail = GetGmailDichVuGmail(PublicData.AccessTokenDvgmCurrent);
                if (gmail != null && !string.IsNullOrEmpty(gmail.email))
                {
                    return gmail;
                }
            }

           
            return gmail;
        }

        public static bool ResetIpMproxy (string key)
        {

            try
            {
                string url = string.Format("https://mproxy.vn/capi/Ztb4_kX5c06eUnxZfO1AOTc1XS39_2kNp6uqP5_Hc8M/key/{0}/resetIp", key);
                var client = new RestClient(url);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("");

                var response = client.Get(request);
                var content = response.Content; // Raw content as string
                Console.WriteLine("content mproxy:" + content);
              
                if (content.Contains("Thành công"))
                {
                    return true;
                }
            }
            catch (Exception eee)
            {
                return false;
            }
            return false;
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
                        mail.orderId = data.orders[0].id + "";
                        
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
                        mail.orderId = data.data.getCodeID + "";
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
            [JsonProperty("message")]
            public string message { get; set; }

            [JsonProperty("text_body")]
            public string text_body { get; set; }
            
        }


        public static List<string> GetOtpByOAuth2(MailObject inMmail)
        {
            

            List<string> otps = new List<string>();
            //string accessToken = HotmailOtpFetcher.GetAccessTokenFromRefreshToken(inMmail.clientId, inMmail.refreshToken).GetAwaiter().GetResult();
            //otp = HotmailOtpFetcher.GetFacebookOtp(accessToken).GetAwaiter().GetResult();

            //if (!string.IsNullOrEmpty(otp))
            //{
            //    return otp;
            //}
            otps = GetHotmailOtpByOAuth2Vandong(inMmail);
            if (otps != null && otps.Count > 0)
            {
                return otps;
            }
            //if (inMmail.otpVandong)
            //{
            otps = GetHotmailOtpByOAuth2Unlimit(inMmail);
            //}
            if (otps != null && otps.Count > 0)
            {
                return otps;
            }
            //string code = "";
            //string accessToken = inMmail.accessToken;
            //if (string.IsNullOrEmpty(accessToken))
            //{
            //    accessToken = GetAccessToken(inMmail.clientId, inMmail.refreshToken);
            //}
            //List<string> subjects = ReceiveMailByOauth(inMmail.email, accessToken);

            //////truy xuất nội dung từng mail
            //foreach (string mail in subjects)
            //{
            //    Console.WriteLine("subject:" + mail);
            //    code = FindCodeInSubject(mail);
            //    if (!string.IsNullOrEmpty(code) && code != Constant.FAIL)
            //    {
            //        return code;
            //    }
            //}
            return otps;
        }

        public static string GetOtp2faByOAuth2(MailObject inMmail)
        {

            string otp = GetHotmailOtp2faByOAuth2Unlimit(inMmail);
            if (!string.IsNullOrEmpty(otp))
            {
                return otp;
            }
            if (inMmail.otpVandong)
            {
                return GetHotmailOtp2faByOAuth2Vandong(inMmail);
            }
            string code = "";
            string accessToken = inMmail.accessToken;
            if (string.IsNullOrEmpty(accessToken))
            {
                accessToken = GetAccessToken(inMmail.clientId, inMmail.refreshToken);
            }
            List<string> subjects = ReceiveMailByOauth(inMmail.email, accessToken);

            ////truy xuất nội dung từng mail
            foreach (string mail in subjects)
            {
                Console.WriteLine("subject:" + mail);
                code = FindCode2fa(mail);
                if (!string.IsNullOrEmpty(code) && code != Constant.FAIL)
                {
                    return code;
                }
            }
            return code;
        }

        private static string GetHotmailOtp2faByOAuth2Vandong(MailObject inMmail)
        {
            throw new NotImplementedException();
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
                    else if (data != null && !string.IsNullOrEmpty(data.errorMessage) && data.errorMessage.Contains("Get access token"))
                    {
                        mail.status = Constant.FAIL;
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

        public static List<string> GetHotmailOtpByOAuth2Vandong(MailObject mail)
        {
            try
            {
                List<string> otps = new List<string>();

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
                        for (int i = 0; i < data.messages.Length; i++)
                        {
                            Console.WriteLine("subject:" + data.messages[i].subject);
                            string code = FindCodeInSubject(data.messages[i].subject);
                            if (!string.IsNullOrEmpty(code) && code != Constant.FAIL)
                            {
                                otps.Add( code);
                            }
                        }
                        return otps;
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
        public static List<string> GetHotmailOtpByOAuth2Unlimit(MailObject mail)
        {
            try
            {
                List<string> otps = new List<string>();
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
                        for (int i = 0; i < data.messages.Length; i++)
                        {
                            Console.WriteLine("subject:" + data.messages[i].subject);
                            string code = FindCodeInSubject(data.messages[i].subject);
                            if (!string.IsNullOrEmpty(code) && code != Constant.FAIL)
                            {
                                otps.Add( code);
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
        public static string GetHotmailOtp2faByOAuth2Unlimit(MailObject mail)
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
                        //for (int i = 0; i < data.messages.Length; i++)
                        //{
                        //    Console.WriteLine("subject:" + data.messages[i].);
                            string code = FindCode2fa(content);
                            if (!string.IsNullOrEmpty(code) && code != Constant.FAIL)
                            {
                                return code;
                            }
                        //}
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

        public class HotmailUnlimitedItem
        {

            [JsonProperty("email")]
            public string email { get; set; }
            [JsonProperty("password")]
            public string password { get; set; }
            [JsonProperty("access_token")]
            public string access_token { get; set; }
            [JsonProperty("refresh_token")]
            public string refresh_token { get; set; }
            [JsonProperty("client_id")]
            public string client_id { get; set; }
        }
        public class GmailUnlimitedItem
        {
            [JsonProperty("status")]
            public string status { get; set; }

            [JsonProperty("mail")]
            public string mail { get; set; }
         
            [JsonProperty("price")]
            public int price { get; set; }
        }
        public class OtpGmailUnlimitedItem
        {
            [JsonProperty("otp")]
            public string otp { get; set; }
        }
        public class HotmailUnlimitedResponse
        {
            [JsonProperty("status")]
            public bool status { get; set; }
            [JsonProperty("messsage")]
            public string message { get; set; }
            [JsonProperty("data")]
            public HotmailUnlimitedItem[] data { get; set; }
        }
        public class GmailUnlimitedResponse
        {
            [JsonProperty("status")]
            public bool status { get; set; }
            [JsonProperty("messsage")]
            public string message { get; set; }
            [JsonProperty("data")]
            public string data { get; set; }
        }
        public class OtpGmailUnlimitedResponse
        {
            [JsonProperty("status")]
            public bool status { get; set; }
            [JsonProperty("messsage")]
            public string message { get; set; }
            [JsonProperty("data")]
            public OtpGmailUnlimitedItem data { get; set; }
        }
        public class HotmailUnlimitedBodyRequest
        {
            [JsonProperty("quantity")]
            public int quantity { get; set; }
            [JsonProperty("token")]
            public string token { get; set; }
            [JsonProperty("product_id")]
            public string product_id { get; set; }
        }
        public class GmailUnlimitedBodyRequest
        {
            [JsonProperty("token")]
            public string token { get; set; }
            [JsonProperty("id")]
            public string id { get; set; }
        }
        public class OtpGmailUnlimitedBodyRequest
        {
            [JsonProperty("token")]
            public string token { get; set; }
            [JsonProperty("mail")]
            public string mail { get; set; }
        }
        public static HotmailUnlimitedResponse GetHotMailUnlimited(int amount, string type)
        {
            try
            {
                try
                {
                    HotmailUnlimitedBodyRequest requestBody = new HotmailUnlimitedBodyRequest();
                    requestBody.quantity = amount;
                    requestBody.token = PublicData.TokenUnlimit;
                    requestBody.product_id = type;
                    var client = new RestClient("https://web.unlimitmail.com/");
                    //client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                    var request = new RestRequest("api/buy-product", Method.POST);
                    request.AddHeader("Content-Type", "application/json");
                    request.RequestFormat = DataFormat.Json;
                    request.AddJsonBody(requestBody);
                    var response = client.Execute(request);
                    var content = response.Content; // Raw content as string    
                    try
                    {
                        if (content != null)
                        {
                            HotmailUnlimitedResponse data = JsonConvert.DeserializeObject<HotmailUnlimitedResponse>(content);
                            return data;
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static MailObject GetGmailUnlimited()
        {
            try
            {   
                MailObject mail = new MailObject();

                GmailUnlimitedBodyRequest requestBody = new GmailUnlimitedBodyRequest();
                    
                requestBody.token = PublicData.TokenUnlimit;
                requestBody.id = "1";
                var client = new RestClient("https://web.unlimitmail.com/");
                    
                var request = new RestRequest("api/otp-services/rent-otp", Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(requestBody);
                var response = client.Execute(request);
                var content = response.Content; // Raw content as string    
                if (content != null)
                {
                    GmailUnlimitedResponse data = JsonConvert.DeserializeObject<GmailUnlimitedResponse>(content);
                    mail.email = data.data;
                    mail.password = Constant.GMAIL_SELL_GMAIL;
                    mail.source = "unlimit_gmail";
                    mail.accessToken = PublicData.TokenUnlimit;
                    return mail;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
        public static string GetOtpGmailUnlimited(MailObject inMail)
        {
            try
            {
                OtpGmailUnlimitedBodyRequest requestBody = new OtpGmailUnlimitedBodyRequest();

                requestBody.token = PublicData.TokenUnlimit;
                requestBody.mail = inMail.email;
                var client = new RestClient("https://web.unlimitmail.com/");

                var request = new RestRequest("api/otp-services/get-otp", Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(requestBody);
                var response = client.Execute(request);
                var content = response.Content; // Raw content as string    
                if (content != null)
                {
                    OtpGmailUnlimitedResponse data = JsonConvert.DeserializeObject<OtpGmailUnlimitedResponse>(content);
                    if (data != null && data.data != null && !string.IsNullOrEmpty(data.data.otp))
                    {
                        return data.data.otp;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return "";
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
                        mail.orderId = data.id + "";
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

        public static string GetOtpGmail48h(string orderId)
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

       

        public static string GetOtpGmailSuperTeam(MailObject gmail)
        {
            try
            {

                var client = new RestClient("https://api.sptmail.com/api/otp-services/mail-otp-lookup/");
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("", Method.GET);


                request.AddParameter("apiKey", gmail.accessToken);
                request.AddParameter("otpServiceCode", "facebook");
                request.AddParameter("gmail", gmail.email);


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
        //public static string GetOtpOtpCheap(MailObject gmail)
        //{
        //    try
        //    {

        //        var client = new RestClient("https://api.otp.cheap/stubs/action/getorder");
        //        client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
        //        var request = new RestRequest("", Method.GET);


        //        request.AddParameter("apiKey", gmail.accessToken);
        //        request.AddParameter("otpServiceCode", "facebook");
        //        request.AddParameter("gmail", gmail.email);


        //        var response = client.Execute(request);
        //        var content = response.Content; // Raw content as string

        //        try
        //        {
        //            OtpGmailSuperTeamDatarequest data = JsonConvert.DeserializeObject<OtpGmailSuperTeamDatarequest>(content);

        //            if (data != null && !string.IsNullOrEmpty(data.status) && data.status == "SUCCESS" && !string.IsNullOrEmpty(data.otp))
        //            {

        //                return data.otp;
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
        //}

        public class OtpGmailThueSimTeamDatarequest
        {
            [JsonProperty("code")]
            public string code { get; set; }
            [JsonProperty("status")]
            public string status { get; set; }

        }
        public static string GetOtpGmailThuesim(MailObject gmail)
        {
            try
            {

                var client = new RestClient(string.Format("https://api.thuesim.app/order/email/{0}/{1}", gmail.clientId, gmail.accessToken));
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("", Method.GET);

                var response = client.Execute(request);
                var content = response.Content; // Raw content as string

                try
                {
                    OtpGmailThueSimTeamDatarequest data = JsonConvert.DeserializeObject<OtpGmailThueSimTeamDatarequest>(content);

                    if (data != null && !string.IsNullOrEmpty(data.status) && data.status == "Successed" && !string.IsNullOrEmpty(data.code))
                    {

                        return data.code;
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
        public class OtpGmailShopgmailTeamDatarequest
        {
            [JsonProperty("otp")]
            public string otp { get; set; }
            [JsonProperty("status")]
            public string status { get; set; }

        }
        public static string GetOtpGmailShopgmail(MailObject gmail)
        {
            try
            {

                
                var client = new RestClient(string.Format("https://api.shopgmail9999.com/api/ApiUsers/checkotp?ordernumber={0}", gmail.clientId));
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("", Method.GET);

                var response = client.Execute(request);
                var content = response.Content; // Raw content as string

                try
                {
                    OtpGmailShopgmailTeamDatarequest data = JsonConvert.DeserializeObject<OtpGmailShopgmailTeamDatarequest>(content);

                    if (data != null && !string.IsNullOrEmpty(data.status) && data.status == "Success" && !string.IsNullOrEmpty(data.otp))
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

        public static string GetOtpGmailOtp(string codeID)
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
        public static string GetCodeMailOtp(string orderId)
        {
            try
            {
                
                var client = new RestClient("https://api.otp.cheap/stubs/action/getorder");
                //client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("", Method.GET);
                request.AddQueryParameter("api_key", PublicData.AccessTokenOtpCheap);
                request.AddQueryParameter("quid", orderId);



                request.Timeout = 2000;
                var response = client.Execute(request);
                string content = response.Content; // Raw content as string

                try
                {
                    Console.WriteLine("otpcheal detail:" + content);
                    if (content == null || content.Contains("Error"))
                    {
                        return null;
                    }

                    GmailOtpCheapDetailDatarequest dataDetail = JsonConvert.DeserializeObject<GmailOtpCheapDetailDatarequest>(content);

                    if (dataDetail != null && !string.IsNullOrEmpty(dataDetail.code))
                    {
                       
                        return dataDetail.code;
                    }
                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception eee)
            {
                return null;
            }
            return null;
        }

        

        
        public static string GetGmailDichVuGmailOtp(MailObject mail)
        {
            string otp = "";
            
            try
            {
                string apiGetDvgmOtp = string.Format("{0}/{1}",
                mail.accessToken,
                mail.orderId);
                var client = new RestClient("https://boxreceive.com/DataMail/Mail/");
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
            }
            catch (Exception ex)
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
            }
            catch (Exception ex)
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
                }
                else
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
                }
                else
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
                }
                catch (Exception ex)
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

        public static string GetCodeFromCodeTextNow(string phone, string cookie = "PHPSESSID=l4bc763rmf6ttidn8s9dv944ib")
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
                    }
                    else
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

        public static List<String> GetPhoneFromCodeTextNow(int start, int length, string cookie = "PHPSESSID=l4bc763rmf6ttidn8s9dv944ib")
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
                        for (int i = 0; i < data.data.Length; i++)
                        {
                            phones.Add(data.data[i].sdt);
                            var json = new JavaScriptSerializer().Serialize(data.data[i]);
                            line = line + json + "\r\n";
                            if (i != data.data.Length - 1)
                            {
                                line = line + json + "\r\n";
                            }
                            else
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
                    string temp = content.Replace("[\"", "").Replace("\"", "");
                    string[] teemm = temp.Split(',');
                    for (int i = 0; i < teemm.Length; i++)
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
            for (int i = 2023; i >= 1960; i--)
            {



                for (int k = 0; k < states.Length; k++)
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
                            }
                            else
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

            for (int i = 0; i < maleName.Count; i++)
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
