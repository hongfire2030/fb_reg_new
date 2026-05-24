using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Web.Script.Serialization;


namespace fb_reg.Utilities
{
    public class ProxyInfo
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public static class HotmailReader
    {
        private static RestClient CreateClient(string baseUrl, ProxyInfo proxy, int timeoutMs)
        {
            var client = new RestClient(baseUrl);
            client.Timeout = timeoutMs;

            if (proxy != null && !string.IsNullOrEmpty(proxy.Host))
            {
                var webProxy = new WebProxy(proxy.Host, proxy.Port);

                if (!string.IsNullOrEmpty(proxy.Username))
                {
                    webProxy.Credentials = new NetworkCredential(proxy.Username, proxy.Password);
                }

                client.Proxy = webProxy;
            }

            return client;
        }

        public static string GetAccessToken(
            string clientId,
            string refreshToken,
            string clientSecret,
            ProxyInfo proxy,
            int timeoutMs = 30000)
        {
            var client = CreateClient("https://login.microsoftonline.com", proxy, timeoutMs);

            var request = new RestRequest("/common/oauth2/v2.0/token", Method.POST);

            // Không cần tự ép Content-Type trước nếu dùng GetOrPost,
            // nhưng để rõ ràng vẫn có thể giữ:
            request.AddHeader("Accept", "application/json");

            request.AddParameter("client_id", clientId, ParameterType.GetOrPost);
            request.AddParameter("grant_type", "refresh_token", ParameterType.GetOrPost);
            request.AddParameter("refresh_token", refreshToken, ParameterType.GetOrPost);

            // Nên giữ đúng scope đọc mail
            request.AddParameter("scope", "https://graph.microsoft.com/.default offline_access", ParameterType.GetOrPost);

            if (!string.IsNullOrEmpty(clientSecret))
            {
                request.AddParameter("client_secret", clientSecret, ParameterType.GetOrPost);
            }

            var response = client.Execute(request);

            if (response == null)
                throw new Exception("Token response null");

            if (response.ResponseStatus != ResponseStatus.Completed)
                throw new Exception("Token request lỗi transport: " + response.ErrorMessage);

            if (string.IsNullOrEmpty(response.Content))
                throw new Exception("Token response rỗng");

            if ((int)response.StatusCode != 200)
                throw new Exception("Token HTTP lỗi: " + (int)response.StatusCode + " | " + response.Content);

            var serializer = new JavaScriptSerializer();
            var json = serializer.Deserialize<Dictionary<string, object>>(response.Content);

            if (json == null)
                throw new Exception("Không parse được JSON token");

            if (json.ContainsKey("error"))
                throw new Exception("Token error: " + response.Content);

            if (!json.ContainsKey("access_token"))
                throw new Exception("Không có access_token: " + response.Content);

            return Convert.ToString(json["access_token"]);
        }

        public static List<string> ReadInboxSubjects(
            string accessToken,
            ProxyInfo proxy,
            int top,
            int timeoutMs = 30000)
        {
            var client = CreateClient("https://graph.microsoft.com", proxy, timeoutMs);

            var request = new RestRequest("/v1.0/me/mailFolders/inbox/messages", Method.GET);
            request.AddHeader("Authorization", "Bearer " + accessToken);
            request.AddHeader("Accept", "application/json");

            request.AddParameter("$top", top);
            request.AddParameter("$orderby", "receivedDateTime desc");
            request.AddParameter("$select", "subject");

            var response = client.Execute(request);

            if (response == null)
                throw new Exception("Mail response null");

            if (response.ResponseStatus != ResponseStatus.Completed)
                throw new Exception("Mail request lỗi transport: " + response.ErrorMessage);

            if (string.IsNullOrEmpty(response.Content))
                throw new Exception("Mail response rỗng");

            if ((int)response.StatusCode != 200)
                throw new Exception("Mail HTTP lỗi: " + (int)response.StatusCode + " | " + response.Content);

            var serializer = new JavaScriptSerializer();
            var json = serializer.Deserialize<Dictionary<string, object>>(response.Content);

            var result = GetSubjects11(response.Content);

            return result;
        }

        public static List<string> GetSubjects11(string inboxJson)
        {
            var result = new List<string>();

            if (string.IsNullOrEmpty(inboxJson))
                return result;

            var serializer = new JavaScriptSerializer();
            var root = serializer.Deserialize<Dictionary<string, object>>(inboxJson);

            if (root == null || !root.ContainsKey("value") || root["value"] == null)
                return result;

            var list = root["value"] as ArrayList;
            if (list == null)
                return result;

            foreach (var item in list)
            {
                var mail = item as Dictionary<string, object>;
                if (mail == null) continue;

                object subjectObj;
                if (mail.TryGetValue("subject", out subjectObj) && subjectObj != null)
                {
                    result.Add(subjectObj.ToString());
                }
            }

            return result;
        }
        public static List<string> ReadInboxSubjectsByRefreshToken(
            string clientId,
            string refreshToken,
            string clientSecret,
            ProxyInfo proxyToken,
            ProxyInfo proxyMail,
            int top,
            int timeoutMs = 30000)
        {
            string accessToken = GetAccessToken(clientId, refreshToken, clientSecret, proxyToken, timeoutMs);
            return ReadInboxSubjects(accessToken, proxyMail, top, timeoutMs);
        }
        public static List<string> GetSubjects(string inboxJson)
        {
            var result = new List<string>();

            if (string.IsNullOrEmpty(inboxJson))
                return result;

            int index = 0;

            while (true)
            {
                // tìm "subject":
                int start = inboxJson.IndexOf("\"subject\":", index);
                if (start == -1) break;

                // tìm dấu " bắt đầu value
                start = inboxJson.IndexOf('"', start + 10);
                if (start == -1) break;

                // tìm dấu " kết thúc
                int end = inboxJson.IndexOf('"', start + 1);
                if (end == -1) break;

                string subject = inboxJson.Substring(start + 1, end - start - 1);

                result.Add(subject);

                index = end + 1;
            }

            return result;
        }
        
    }
}
