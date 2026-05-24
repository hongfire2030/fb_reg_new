using Chilkat;
using EAGetMail;
using Emgu.CV.Ocl;
using fb_reg.Model;
using fb_reg.RequestApi;
using Microsoft.Graph.AuditLogs;
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

        
        public class VerifyLogRequest
        {
            public string country_code { get; set; }   // "US"
            public string country_name { get; set; }   // "United States"
            public bool success { get; set; }           // true / false
        }

        public class Decision
        {
            public  bool stop { get; set; } 
            public  string reason { get; set; }
            public  int remaining { get; set; }
        }
        public static async Task<string> LogCheckpoint(DeviceObject device, OrderObject order, string status)
        {
            try
            {
                string uri = PublicData.LogServerUri;

                if (order.isHotmail)
                {
                    uri = PublicData.LogHotmailServerUri;

                }
                LogEntryDevice log = new LogEntryDevice();
                log.DeviceId = device.deviceId;
                log.ProxyIp = order.currentIp;
                log.Status = status;
                log.Pcname = Environment.MachineName;
                log.AndroidVersion = Device.GetAndroidVersion(device.deviceId);

                string submitLog = string.Format("log/submit-log");
                var client = new RestClient(uri);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(submitLog);
                request.AddHeader("Content-Type", "application/json");
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(log);
                request.Timeout = 1000;
                var response = client.Post(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("submit log:" + content);

                string mode = "probe";
                if (!string.IsNullOrEmpty(PublicData.includeProxy))
                {
                    mode = "scale";
                }

                
                if (order.ipInfo == null || order.ipInfo.country == null)
                {
                    return "";
                }
                if (status == Constant.CHECKPOINT)
                {
                    VerifyApiClient.SendVerify(
                        PublicData.LogProxyCountry,
                        order.ipInfo.country.code,
                        order.ipInfo.country.name,
                        false,
                        mode
                    );
                }
                else
                {
                    VerifyApiClient.SendVerify(
                        PublicData.LogProxyCountry,
                        order.ipInfo.country.code,
                        order.ipInfo.country.name,
                        true,
                        mode
                    );
                }

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
            request.Timeout = 20000; // 20 seconds timeout
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
                request.Timeout = 20000; // 20 seconds timeout
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
                request.Timeout = 20000; // 20 seconds timeout

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


        public static string SetCacheMail2(string server, int dvgm, int sellgmail, int superGmail, int sptVip, int gmailOtp, int hotmail, int id, int ratecachehotmail, int runVeri)
        {
            try
            {
                string apiGetHotMail = string.Format("api/setting?dvgm={0}&sellgmail={1}&supergmail={2}&sptvip={3}&gmailotp={4}&hotmail={5}&hotmailtype={6}&ratecachehotmail={7}&runveri={8}", dvgm, sellgmail, superGmail, sptVip, gmailOtp, hotmail, id, ratecachehotmail, runVeri);
                var client = new RestClient(server);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetHotMail);

                request.Timeout = 200; // 20 seconds timeout
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
        public static int GetMailCacheCount()
        {
            int cacheMail = 0;
            MailObject mail = new MailObject();
            mail.isHotmail = true;

            MailObject resp = ForceAddMailServerCache(mail);
            
            if (resp != null)
            {
                cacheMail = resp.mailCount;
            }
            return cacheMail;
        }

        public class DeviceStat
        {
            public int total { get; set; }
            public int success { get; set; }
            public int checkpoint { get; set; }
            public double successRate { get; set; }
        }

        public static DeviceStat GetDeviceStats()
        {
            try
            {
                string apiGetHotMail = "log/stats-by-pc-recent";
                var client = new RestClient(PublicData.LogServerUri);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetHotMail);
                request.Timeout = 200; // 20 seconds timeout
                var response = client.Get(request);
                var content = response.Content; // Raw content as string
                string decode = Utility.Decode_UTF8(content);
                var data = JsonConvert.DeserializeObject<Dictionary<string, DeviceStat>>(decode);

                if (data != null && data.ContainsKey(Environment.MachineName))
                {
                    return data[Environment.MachineName];

                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }
        public static Setting SettingLogServer(int dvgm, int sellgmail, int superGmail, int sptVip, int gmailOtp, int hotmail, int id, int ratecachehotmail, int runVeri)
        {
            try
            {
                string apiGetHotMail = string.Format("api/setting?dvgm={0}&sellgmail={1}&supergmail={2}&sptvip={3}&gmailotp={4}&hotmail={5}&hotmailtype={6}&ratecachehotmail={7}&runveri={8}", dvgm, sellgmail, superGmail, sptVip, gmailOtp, hotmail, id, ratecachehotmail, runVeri);
                var client = new RestClient(PublicData.LogServerUri);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetHotMail);

                request.Timeout = 200; // 20 seconds timeout
                var response = client.Get(request);
                var content = response.Content; // Raw content as string
                string decode = Utility.Decode_UTF8(content);
                var root = JObject.Parse(decode);
                var dataDict = root["RecentStatsByPc"].ToObject<Dictionary<string, object>>();
                
                Setting data = JsonConvert.DeserializeObject<Setting>(decode);
                string pcName = Environment.MachineName;
                if (dataDict.TryGetValue(pcName, out var userObj))
                {
                    
                    data.recentRate = JsonConvert.SerializeObject(userObj, Formatting.None);
                    
                }
                
                
                Console.WriteLine("SetCacheMail2:" + content);
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }
        public static string SetRunVeri(string server, int runVeri)
        {
            PublicData.countSuccessVeribackup = 0;
            return SetCacheMail2(server, -1, -1, -1, -1, -1, -1, -1,-1, runVeri);
            return "0";
        }
        public static int GetRunVeri(string server)
        {
            string temp = SetCacheMail2(server, -1, -1, -1, -1, -1, -1, -1, -1, -1);
            temp = temp.Replace("\"", "");
            temp = temp.Replace("/", "");
            temp = temp.Replace("\\", "");
            string[] tempArray = temp.Split('|');
            int runveri = 0;
            try
            {
                runveri = Convert.ToInt32(tempArray[14]);
            }
            catch (Exception ex)
            {

            }
            //return 0;
            return runveri;
        }

        public class ServerInfoResponse
        {
            [JsonProperty("ip")]
            public string ip { get; set; }
            [JsonProperty("iplocal")]
            public string iplocal { get; set; }


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
                request.Timeout = 20000; // 20 seconds timeout

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

        public static ServerInfoResponse GetServerIp(string server, bool namServer)
        {
            try
            {
                return null;
                if (namServer)
                {
                    return new ServerInfoResponse();
                }
                string apiGetHotMail = "api/ipserver";
                var client = new RestClient(server);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetHotMail);
                request.Timeout = 200; // 20 seconds timeout

                var response = client.Get(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("get IP sever:" + content);
                try
                {
                    ServerInfoResponse data = JsonConvert.DeserializeObject<ServerInfoResponse>(content);
                    if (data != null)
                    {
                        if (!PublicData.global)
                        {
                            data.ip = data.iplocal;
                        }
                        return data;
                    }

                }
                catch
                {
                    return null;
                }
                return new ServerInfoResponse();
            }
            catch (Exception ex)
            {

            }

            return new ServerInfoResponse();
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
                request.Timeout = 20000; // 20 seconds timeout

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
                server = PublicData.AvatarServer;
                //string apiGetHotMail = string.Format("api/avatar?gender={0}", gender);
                string apiGetHotMail = string.Format("avatar?gender={0}", gender);
                var client = new RestClient(server);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetHotMail);

                request.Timeout = 20000; // 20 seconds timeout
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
            public string first;
            public string last;
            public string gender;
            public string full;
            public bool isVn;
        }
        public static NameObject GetNameLocalCache(string gender, string language)
        {
            NameObject nameCache = new NameObject();
            try
            {
                string server = PublicData.NameServer;
                
                string apiGetHotMail = string.Format("api/name?gender={0}&language={1}", gender, language);
                if (PublicData.nameUbuntu)
                {
                    server = PublicData.NameServerUbuntu;
                    apiGetHotMail = string.Format("name?gender={0}", gender);
                }

                var client = new RestClient(server);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetHotMail);

                request.Timeout = 20000; // 20 seconds timeout
                var response = client.Get(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("get name:" + content);

                NameObject data = JsonConvert.DeserializeObject<NameObject>(content);

                if (data != null && !string.IsNullOrEmpty(data.first))
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

                request.Timeout = 20000; // 20 seconds timeout
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
        public static MailObject ForceAddMailServerCache(MailObject mail)
        {
            
            try
            {
                mail.reusedCount++;

                string server = PublicData.CacheServerUri;



                string apiGetSellGmail = "api/supergmail";
                if (mail.isHotmail)
                {
                    apiGetSellGmail = "api/hotmail";
                }
                var client = new RestClient(server);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetSellGmail);
                request.AddHeader("Content-Type", "application/json");
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(mail);
                request.Timeout = 2000; // 20 seconds timeout
                var response = client.Post(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("AddMailServerCache:" + content);
                string decode = Utility.Decode_UTF8(content);
                MailObject data = JsonConvert.DeserializeObject<MailObject>(decode);

                if (data != null)
                {
                    return data;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public static MailObject AddMailServerCache(MailObject mail)
        {
            if ( !PublicData.needReuseMail)
            {
                return mail;
            }
            if (!mail.needReused || mail.reusedCount > 2)
            {
                return mail;
            }
            try
            {
                mail.reusedCount++;
                string server = PublicData.CacheServerUri;
                string apiGetSellGmail = "api/supergmail";
                var client = new RestClient(server);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetSellGmail);
                request.AddHeader("Content-Type", "application/json");
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(mail);
                request.Timeout = 20000; // 20 seconds timeout
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
        public static MailObject LogMailServerCache(MailObject mail, string server)
        {
            try
            {
                string apiGetSellGmail = "api/logmail";
                var client = new RestClient(server);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetSellGmail);
                request.AddHeader("Content-Type", "application/json");
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(mail);
                request.Timeout = 1000; // 20 seconds timeout
                var response = client.Post(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("AddMailServerCache:" + content);
                string decode = Utility.Decode_UTF8(content);
                MailObject data = JsonConvert.DeserializeObject<MailObject>(decode);

                if (data != null)
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
                request.Timeout = 20000; // 20 seconds timeout
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

                request.Timeout = 20000; // 20 seconds timeout
                var response = client.Get(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("GetSuperGmailLocalCache:" + content);
                string decode = Utility.Decode_UTF8(content);
                mail = JsonConvert.DeserializeObject<MailObject>(decode);

                if (mail != null && !string.IsNullOrEmpty(mail.email))
                {
                    
                    if (string.IsNullOrEmpty(mail.message))
                    {
                        mail.message = "gmail from server cache";
                    }
                    //mail.createdAt = DateTime.UtcNow;
                    return mail;
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

                request.Timeout = 20000; // 20 seconds timeout
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
                request.Timeout = 20000; // 20 seconds timeout
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

        public static Proxy GetProxyFromServerUbuntu(DeviceObject device, string server, OrderObject order)
        {
            Proxy proxy = new Proxy();
            try
            {
                //string type = order.proxyType;
                int type = Int32.Parse(order.proxyType);
                server = PublicData.ProxyServer;
                ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                //string apiGetProxy = string.Format("api/proxy?type={0}", type);
                string apiGetProxy = string.Format("proxy?type={0}", type);
                var client = new RestClient(server);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetProxy);
                request.Timeout = 2000; // 20 seconds timeout
                var response = client.Get(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("Get Proxy:" + content);
                string decode = Utility.Decode_UTF8(content);
                proxy = JsonConvert.DeserializeObject<Proxy>(decode);
                //if (type == "key")
                //{
                //    if (proxy != null && !string.IsNullOrEmpty(proxy.key))
                //    {
                //        return proxy;
                //    }
                //} else
                //{
                //    if (proxy != null && !string.IsNullOrEmpty(proxy.host))
                //    {
                //        return proxy;
                //    }
                //}

            }
            catch (Exception ex)
            {
                Utility.LogStatus(device, ex.Message);
            }

            return proxy;
        }

        public static Proxy GetProxyFromServer(DeviceObject device, string server, OrderObject order)
        {
            Proxy proxy = new Proxy();
            try
            {
                //string type = order.proxyType;
                int type = Int32.Parse( order.proxyType);
                //server = PublicData.ProxyServer;
                ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                string apiGetProxy = string.Format("api/proxy?type={0}", type);
                //string apiGetProxy = string.Format("proxy?type={0}", type);
                var client = new RestClient(server);
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest(apiGetProxy);
                request.Timeout = 2000; // 20 seconds timeout
                var response = client.Get(request);
                var content = response.Content; // Raw content as string

                Console.WriteLine("Get Proxy:" + content);
                string decode = Utility.Decode_UTF8(content);
                proxy = JsonConvert.DeserializeObject<Proxy>(decode);
                //if (type == "key")
                //{
                //    if (proxy != null && !string.IsNullOrEmpty(proxy.key))
                //    {
                //        return proxy;
                //    }
                //} else
                //{
                //    if (proxy != null && !string.IsNullOrEmpty(proxy.host))
                //    {
                //        return proxy;
                //    }
                //}
                
            }
            catch (Exception ex)
            {
                Utility.LogStatus(device, ex.Message);
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
                request.Timeout = 20000; // 20 seconds timeout
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

                request.Timeout = 20000; // 20 seconds timeout
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
