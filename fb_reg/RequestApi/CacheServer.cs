using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace fb_reg
{
    public class CacheServer
    {
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

        public static Proxy GetProxyFromServer(string server, bool p1, bool p3, bool key)
        {
            Proxy proxy = new Proxy();
            try
            {
                string type = "";
                if (p1)
                {
                    type = "1";
                }
                else
                {
                    type = "2";
                }
                if (p3)
                {
                    type = "3";
                }
                if (key)
                {
                    type = "key";
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

        public static void deleteKeyProxy(string server, string key)
        {
            Proxy proxy = new Proxy();
            try
            {
                
                string apiGetProxy = string.Format("api/proxy?key={0}", key);
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
        
    }
}
