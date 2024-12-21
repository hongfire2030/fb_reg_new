using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace fb_reg
{
    public class WwProxy
    {
        public class ProxyWwListKeyResponse
        {
            [JsonProperty("status")]
            public string status { get; set; }

            [JsonProperty("errorCode")]
            public int errorCode { get; set; }

            [JsonProperty("message")]
            public string message { get; set; }

            [JsonProperty("data")]
            public ProxyWwListKeyDataResponse[] data { get; set; }
        }

        public class ProxyWwListKeyDataResponse
        {
            [JsonProperty("uuid")]
            public string uuid { get; set; }

            [JsonProperty("vip")]
            public int vip { get; set; }

            [JsonProperty("expiredTime")]
            public string expiredTime { get; set; }

            [JsonProperty("expiredFlag")]
            public bool expiredFlag { get; set; }
        }

        public class ProxyWwResponse
        {
            [JsonProperty("status")]
            public string status { get; set; }

            [JsonProperty("errorCode")]
            public int errorCode { get; set; }

            [JsonProperty("message")]
            public string message { get; set; }

            [JsonProperty("data")]
            public ProxyWwDataResponse data { get; set; }
        }
        public class ProxyWwDataResponse
        {
            [JsonProperty("ipAddress")]
            public string ipAddress { get; set; }
            [JsonProperty("port")]
            public int port { get; set; }

            [JsonProperty("proxy")]
            public string proxy { get; set; }

            [JsonProperty("vip")]
            public int vip { get; set; }

            [JsonProperty("expiredTime")]
            public string expiredTime { get; set; }

            [JsonProperty("status")]
            public string status { get; set; }

            [JsonProperty("provinceId")]
            public int provinceId { get; set; }

            [JsonProperty("nextChange")]
            public string nextChange { get; set; }

        }
        public static Proxy GetProxyWwProxy(string key, string provinceId = "-1")
        {
            Proxy proxy = new Proxy();
            try
            {
                

                var client = new RestClient("https://wwproxy.com/api/client/proxy/available/");
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("");

                request.AddParameter("key", key);

                request.AddParameter("provinceId", provinceId);



                var response = client.Get(request);
                var content = response.Content; // Raw content as string

                ProxyWwResponse data = JsonConvert.DeserializeObject<ProxyWwResponse>(content);

              if (data != null && data.status == "OK" && data.errorCode == 0 && !string.IsNullOrEmpty(data.data.proxy))
                {
                    proxy.host = data.data.ipAddress;
                    proxy.port = "" + data.data.port;
                    proxy.hasProxy = true;
                    proxy.proxyType = "HTTP";
                    proxy.key = key;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return proxy;
        }

        public static string RemoveIpProxy(string key)
        {
            try
            {
                var client = new RestClient("https://wwproxy.com/api/client/proxy/remove/");
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("");

                request.AddParameter("key", key);

                var response = client.Post(request);
                var content = response.Content; // Raw content as string

                ProxyWwResponse reponseData = JsonConvert.DeserializeObject<ProxyWwResponse>(content);

                if (reponseData != null)
                {
                    return reponseData.message;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Constant.FAIL;
        }

        public static List<string> GetListKey(string userId)
        {

            List<string> keys = new List<string>();

            try
            {
                var client = new RestClient("https://wwproxy.com/api/client/key/list");
                client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                var request = new RestRequest("");

                request.AddParameter("user_api_key", userId);

                var response = client.Get(request);
                var content = response.Content; // Raw content as string

                ProxyWwListKeyResponse reponseData = JsonConvert.DeserializeObject<ProxyWwListKeyResponse>(content);

                if (reponseData != null && reponseData.data != null && reponseData.data.Length > 0)
                {
                    for (int i = 0; i < reponseData.data.Length; i ++)
                    {
                        if (!reponseData.data[i].expiredFlag && !string.IsNullOrEmpty(reponseData.data[i].uuid))
                        {
                            keys.Add(reponseData.data[i].uuid);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return keys;
        }
        public static void UpdateKeyUsed(string key)
        {

        }
    }
}
