using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using static fb_reg.CacheServer;
using System.Net;
using System.ComponentModel;

namespace fb_reg.RequestApi
{
    public class VerifyApiClient
    {
        private static readonly HttpClient _http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(2) // timeout cứng
        };

        public static string topCountryCode()
        {
            string url = PublicData.LogProxyCountry + "/export/high-confidence";

            string json = new WebClient
            {
                Encoding = Encoding.UTF8
            }.DownloadString(url);

            dynamic obj = JsonConvert.DeserializeObject(json);
            return obj.codes + "," + topCountryCodeTop();
            
            // codes = "th,id,us,ph,mx"

        }
        public static string topCountryCodeTop()
        {
            string url = PublicData.LogProxyCountry + "/export/top-good";

            string json = new WebClient
            {
                Encoding = Encoding.UTF8
            }.DownloadString(url);

            dynamic obj = JsonConvert.DeserializeObject(json);
            return obj.codes;

            // codes = "th,id,us,ph,mx"

        }

        public static string topException()
        {
            string url = PublicData.LogProxyCountry + "/export/top-die";

            string json = new WebClient
            {
                Encoding = Encoding.UTF8
            }.DownloadString(url);

            dynamic obj = JsonConvert.DeserializeObject(json);
            return obj.codes;

            // codes = "th,id,us,ph,mx"

        }
        public static void SendVerify(
            string baseUrl,
            string countryCode,
            string countryName,
            bool success, string mode)
        {
            try
            {
                var payload = new
                {
                    country_code = countryCode,
                    country_name = countryName,
                    success = success,
                    mode = mode
                };

                string json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // SYNC call – KHÔNG await
                var resp = _http
                    .PostAsync($"{baseUrl.TrimEnd('/')}/log", content)
                    .GetAwaiter()
                    .GetResult();

                // không cần đọc body
            }
            catch (Exception ex)
            {
                // CHỦ ĐÍCH NUỐT LỖI – KHÔNG ĐƯỢC THROW
                // vì đây chỉ là log phụ
            }
        }
    }
}
