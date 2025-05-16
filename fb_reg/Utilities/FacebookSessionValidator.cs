using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace fb_reg.Utilities
{
    public class FacebookSessionValidator
    {
        public class FacebookApiError
        {
            public FacebookError error { get; set; }
        }

        public class FacebookError
        {
            public string message { get; set; }
            public string type { get; set; }
            public int code { get; set; }
        }

        public async Task<string> CheckTokenStatusAsync(string accessToken)
        {
            try
            {
                using var client = new HttpClient();
                var url = $"https://graph.facebook.com/me?access_token={accessToken}";
                var response = await client.GetStringAsync(url);

                // Nếu không có lỗi, token còn sống
                return "✅ Token hợp lệ (login thành công)";
            }
            catch (HttpRequestException ex)
            {
                // Không dùng được response body trực tiếp từ HttpClient ở đây,
                // nên cần bắt nội dung lỗi theo thông thường hoặc giả lập phản hồi lỗi
                var errorJson = ex.Message;

                try
                {
                    var fb = JsonConvert.DeserializeObject<FacebookApiError>(errorJson);
                    return fb?.error?.code switch
                    {
                        459 => "🚫 Token bị checkpoint (code 459)",
                        190 => "❌ Token đã hết hạn hoặc bị xóa (code 190)",
                        102 => "❌ Token không còn hợp lệ (code 102)",
                        10 => "🚫 Không đủ quyền gọi API (code 10)",
                        _ => $"⚠️ Lỗi khác: {fb?.error?.message}"
                    };
                }
                catch
                {
                    return "❌ Không thể phân tích phản hồi Facebook.";
                }
            }
        }
    }
}
