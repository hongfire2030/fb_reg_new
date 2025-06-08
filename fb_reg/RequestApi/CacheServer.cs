using Chilkat;
using EAGetMail;
using Emgu.CV.Ocl;
using fb_reg.Model;
using fb_reg.RequestApi;
using Microsoft.Graph.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace fb_reg
{
    public class CacheServer
    {

        public  class Decision
        {
            public  bool stop { get; set; } 
            public  string reason { get; set; }
            public  int remaining { get; set; }
        }
        public static string LogCheckpoint(DeviceObject device, OrderObject order, string status)
        {
            try
            {
                LogEntryDevice log = new LogEntryDevice();
                log.DeviceId = device.deviceId;
                log.ProxyIp = order.currentIp;
                log.Status = status;
                log.Pcname = Environment.MachineName;
                log.AndroidVersion = Device.GetAndroidVersion(device.deviceId);

                string submitLog = string.Format("log/submit-log");
                var client = new RestClient(PublicData.CacheServerUri.Replace("8081", "8082"));
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(submitLog);
                request.AddHeader("Content-Type", "application/json");
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(log);
                request.Timeout = 5000;
                var response = client.Post(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("submit log:" + content);
                return "";
            }
            catch (Exception ex)
            {

            }

            return "";
        }
        public static Decision CheckDecision(string deviceId)
        {
            Decision dd = new Decision();
            var client = new RestClient(PublicData.CacheServerUri);
            var request = new RestRequest("log/should-stop", Method.GET);
            request.AddParameter("device", deviceId);

            IRestResponse response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = JObject.Parse(response.Content);
                dd.stop = json["stop"].Value<bool>();
                dd.reason = json["reason"].Value<string>();
                dd.remaining = json["remainingSeconds"]?.Value<int>() ?? 0;
                return dd;
            }
            else
            {
                Console.WriteLine("Failed to connect: " + response.StatusCode);
            }
            return null;
        }

        public static bool ShouldStop(string deviceId)
        {
            try
            {
                if (string.IsNullOrEmpty(deviceId))
                {
                    return false;
                }
                var client = new RestClient(PublicData.CacheServerUri); // chỉnh lại URL nếu cần
                var request = new RestRequest("log/should-stop", Method.GET);
                request.AddParameter("device", deviceId);

                var response = client.Execute(request);
                
                var content = response.Content; // JSON string như: { "stop": true }
                return content.Contains("\"stop\":true");
                
            }
            catch (Exception ex)
            {

            }

            return false;
        }

        public static string UpdateCheckpointIp(string server, string checkpointIp)
        {
            try
            {
                if (string.IsNullOrEmpty(checkpointIp))
                {
                    return "";
                }
                string apiGetHotMail = string.Format("api/log?ipCheckpoint={0}", checkpointIp);
                var client = new RestClient(server);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetHotMail);


                var response = client.Put(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("checkpointIp:" + content);
                return "";
            }
            catch (Exception ex)
            {

            }

            return "";
        }
        public static string SetCacheMail(string server, int dvgm, int sellgmail, int superGmail, int gmailOtp, int hotmail, int id, int ratecachehotmail, int runveri)
        {
            try
            {
                string apiGetHotMail = string.Format("api/setting?dvgm={0}&sellgmail={1}&supergmail={2}&gmailotp={3}&hotmail={4}&hotmailtype={5}&ratecachehotmail={6}&runveri={7}", dvgm, sellgmail, superGmail, gmailOtp, hotmail, id, ratecachehotmail, runveri);
                var client = new RestClient(server);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetHotMail);


                var response = client.Get(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("get Setting:" + content);
                return content;
            }
            catch (Exception ex)
            {

            }

            return "";
        }

        public static string SetCacheMail2(string server, int dvgm, int sellgmail, int superGmail, int sptVip, int gmailOtp, int hotmail, int id, int ratecachehotmail)
        {
            try
            {
                string apiGetHotMail = string.Format("api/setting?dvgm={0}&sellgmail={1}&supergmail={2}&sptvip={3}&gmailotp={4}&hotmail={5}&hotmailtype={6}&ratecachehotmail={7}&runveri=-1", dvgm, sellgmail, superGmail, sptVip, gmailOtp, hotmail, id, ratecachehotmail, -1);
                var client = new RestClient(server);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetHotMail);


                var response = client.Get(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("SetCacheMail2:" + content);
                return content;
            }
            catch (Exception ex)
            {
                return "Server lỗi rồi --------------";
            }

            return "";
        }
        public static string SetRunVeri(string server, int runVeri)
        {
            return SetCacheMail(server, -1, -1, -1, -1, -1, -1, -1, runVeri);
        }
        public static int GetRunVeri(string server)
        {
            string temp = SetCacheMail(server, -1, -1, -1, -1, -1, -1, -1, -1);
            temp = temp.Replace("\"", "");
            temp = temp.Replace("/", "");
            temp = temp.Replace("\\", "");
            string[] tempArray = temp.Split('|');
            int runveri = 0;
            try
            {
                runveri = Convert.ToInt32(tempArray[14]);
            } catch(Exception ex)
            {

            }
            return runveri;
        }

        public class ServerInfoResponse
        {
            [JsonProperty("ip")]
            public string ip { get; set; }

        }
        public static string UpdateInvalidName(string server, string invalidName)
        {
            try
            {
                if (string.IsNullOrEmpty(invalidName))
                {
                    return "";
                }
                string apiGetHotMail = string.Format("api/invalidname?invalidName={0}", invalidName);
                var client = new RestClient(server);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetHotMail);


                var response = client.Put(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("UpdateInvalidName:" + content);
                return "";
            }
            catch (Exception ex)
            {

            }

            return "";
        }
        public static string GetServerIp(string server, bool namServer)
        {
            try
            {
                if (namServer)
                {
                    return "";
                }
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

                Console.WriteLine("GetDichvuGmailLocalCache:" + content);
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
                string apiGetHotMail = string.Format("api/avatar?gender={0}", gender);
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
                    //Byte[] bytes = Convert.FromBase64String(data.base64);
                    //File.WriteAllBytes("img/avatar/" + deviceID + ".png", bytes);
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

                Console.WriteLine("GetSellGmailLocalCache:" + content);
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
        
        public static MailObject AddMailServerCache(MailObject mail, string server)
        {
            try
            {
                string apiGetSellGmail = "api/supergmail";
                var client = new RestClient(server);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetSellGmail);
                request.AddHeader("Content-Type", "application/json");
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(mail);

                var response = client.Post(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("AddMailServerCache:" + content);
                string decode = Utility.Decode_UTF8(content);
                MailObject data = JsonConvert.DeserializeObject<MailObject>(decode);

                if (data != null )
                {
                    return data;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public static Proxy AddProxyShareServerCache(Proxy proxy, string server)
        {
            try
            {
                string apiGetSellGmail = "api/proxy";
                var client = new RestClient(server);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetSellGmail);
                request.AddHeader("Content-Type", "application/json");
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(proxy);

                var response = client.Post(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("get proxy:" + content);
                string decode = Utility.Decode_UTF8(content);
                Proxy data = JsonConvert.DeserializeObject<Proxy>(decode);

                if (data != null && !string.IsNullOrEmpty(data.proxyDomain))
                {
                    return data;
                }
            }
            catch (Exception ex)
            {

            }
            return proxy;
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

                Console.WriteLine("GetSuperGmailLocalCache:" + content);
                string decode = Utility.Decode_UTF8(content);
                MailObject data = JsonConvert.DeserializeObject<MailObject>(decode);

                if (data != null && !string.IsNullOrEmpty(data.email))
                {
                    if (data.source == "otpcheap_gmail")
                    {
                        if (string.IsNullOrEmpty(data.orderId))
                        {
                            mail.message = "otp cheap mất orderid";
                            return mail;
                        }
                    }
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

                Console.WriteLine("GetGmailOtpLocalCache:" + content);
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
                request.AddParameter("type", "");
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

        public static Proxy GetProxyFromServer(string server, OrderObject order)
        {
            Proxy proxy = new Proxy();
            try
            {
                string type = order.proxyType;
               
                
                ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                string apiGetProxy = string.Format("api/proxy?type={0}", type);
                var client = new RestClient(server);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetProxy);
               
                var response = client.Get(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("Get Proxy:" + content);
                string decode = Utility.Decode_UTF8(content);
                proxy = JsonConvert.DeserializeObject<Proxy>(decode);
                if (type == "key")
                {
                    if (proxy != null && !string.IsNullOrEmpty(proxy.key))
                    {
                        return proxy;
                    }
                } else
                {
                    if (proxy != null && !string.IsNullOrEmpty(proxy.host))
                    {
                        return proxy;
                    }
                }
                
            }
            catch (Exception ex)
            {

            }

            return proxy;
        }

        public static void deleteKeyProxy(string server, OrderObject order)
        {
            if (order.deleteKeyProxy || string.IsNullOrEmpty(order.proxy.key))
            {
                return;
            }
            order.deleteKeyProxy = true;
            Proxy proxy = new Proxy();
            try
            {
                
                string apiGetProxy = string.Format("api/proxy?key={0}&&version={1}", order.proxy.key, order.proxy.proxyVersion);
                var client = new RestClient(server);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetProxy);

                var response = client.Delete(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("Get Proxy:" + content);
                //WwProxy.RemoveIpProxy(key);
            }
            catch (Exception ex)
            {

            }
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

                Console.WriteLine("GetHotmailLocalCache:" + content);
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
        
    }
}
