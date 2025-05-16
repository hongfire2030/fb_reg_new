using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace fb_reg.Utilities
{
    public static class ProxyHelper
    {
        public static string GetCurrentIp(string proxyAddress, string proxyUsername, string proxyPassword)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://api.ipify.org?format=json");
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0";

                var proxy = new WebProxy(proxyAddress)
                {
                    Credentials = new NetworkCredential(proxyUsername, proxyPassword)
                };
                request.Proxy = proxy;

                using (var response = (HttpWebResponse)request.GetResponse())
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    var doc = JsonDocument.Parse(json);
                    return doc.RootElement.GetProperty("ip").GetString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy địa chỉ IP: " + ex.Message);
                return null;
            }
        }
    }
}
