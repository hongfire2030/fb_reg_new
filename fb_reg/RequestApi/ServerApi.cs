using System;
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
using static fb_reg.OutsideServer;

namespace fb_reg
{

    public class ServerApi
    {

        public static string SERVER_HOST = "http://192.168.1.32";


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
                
                var request = new RestRequest("accounts/resource/" + uid);

                byte [] response = client.DownloadData(request);
                if (response != null && response.Length > 0)
                {
                    File.WriteAllBytes("Authentication/" + uid + ".tar.gz", response);
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

    }
}
