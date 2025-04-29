using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Azure.Core;
using Microsoft.Graph;
using System.Threading;

namespace fb_reg
{
    public static class HotmailOtpFetcher
    {
        // Lấy access_token từ refresh_token (gọi thẳng Microsoft token endpoint)
        public static async Task<string> GetAccessTokenFromRefreshToken(string clientId, string refreshToken)
        {
            var http = new HttpClient();

            var content = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("client_id", clientId),
            new KeyValuePair<string, string>("refresh_token", refreshToken),
            new KeyValuePair<string, string>("grant_type", "refresh_token"),
            new KeyValuePair<string, string>("scope", "https://graph.microsoft.com/.default")
        });

            var response = await http.PostAsync("https://login.microsoftonline.com/common/oauth2/v2.0/token", content);
            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("❌ Token request failed: " + body);
                throw new Exception("Token failed.");
            }

            var json = JsonDocument.Parse(body);
            return json.RootElement.GetProperty("access_token").GetString();
        }

        // Trích mã xác minh từ thư gần nhất trong hộp thư Hotmail
        public static async Task<string> GetFacebookOtp(string accessToken)
        {

            var credential = new AccessTokenCredential(accessToken);
            var graphClient = new GraphServiceClient(credential);

            try
            {
                var messages = await graphClient.Me.Messages
                    .GetAsync(request =>
                    {
                        request.QueryParameters.Top = 10;
                        request.QueryParameters.Select = new[] { "subject", "from", "body" };
                        request.QueryParameters.Orderby = new[] { "receivedDateTime desc" };
                    });

                foreach (var msg in messages.Value)
                {
                    string from = msg.From?.EmailAddress?.Address ?? "";
                    var subject = msg.Subject ?? "";
                    var body = msg.Body?.Content ?? "";

                    if (from != null && from.ToLower().Contains("facebook") ||
                            subject != null && subject.ToLower().Contains("facebook"))
                    {
                        var match = Regex.Match(subject + " " + body, @"\b\d{5,6}\b");
                        if (match.Success)
                            return match.Value;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Graph API error: " + ex.Message);
                return null;
            }
        }

        // TokenCredential adapter cho access_token thủ công
        private class AccessTokenCredential : TokenCredential
        {
            private readonly string _token;

            public AccessTokenCredential(string token)
            {
                _token = token;
            }

            public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
            {
                return new AccessToken(_token, DateTimeOffset.UtcNow.AddHours(1));
            }

            public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken)
            {
                return new ValueTask<AccessToken>(new AccessToken(_token, DateTimeOffset.UtcNow.AddHours(1)));
            }
        }
    }
}