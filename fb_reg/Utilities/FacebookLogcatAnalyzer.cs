using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace fb_reg.Utilities
{
    public class FacebookLogcatAnalyzer
    {
        private readonly string deviceId;

        public FacebookLogcatAnalyzer(string deviceId)
        {
            this.deviceId = deviceId;
        }

        public List<string> Analyze()
        {
            var results = new List<string>();

            string log = GetLogcatDump();

            if (log.Contains("checkpoint_required") || log.Contains("checkpoint"))
                results.Add("🔒 Checkpoint yêu cầu xác minh");

            if (log.Contains("BLOCKED_BY_HIGHRISK_IP"))
                results.Add("🚫 IP proxy bị Facebook đánh giá rủi ro");

            if (log.Contains("SecureVerify"))
                results.Add("🔐 Đang chạy xác minh bảo mật");

            if (Regex.IsMatch(log, "access_token=\\w+"))
                results.Add("✅ Có token đăng nhập đang sinh");

            if (log.Contains("app_errorreporting"))
                results.Add("⚠️ App Facebook ghi nhận lỗi (có thể do clone/restore)");

            if (log.Contains("sms_code") || log.Contains("otp"))
                results.Add("📩 Facebook đang xử lý hoặc nhận mã xác minh");

            if (log.Contains("AutoLoginManager") || log.Contains("LoginActivity"))
                results.Add("🔁 Đang thực hiện login");

            if (log.Contains("registration_start") || log.Contains("RegistrationActivity"))
                results.Add("🆕 Đang đăng ký tài khoản mới");

            if (results.Count == 0)
                results.Add("ℹ️ Không phát hiện trạng thái đặc biệt");

            return results;
        }

        private string GetLogcatDump()
        {
            var psi = new ProcessStartInfo
            {
                FileName = "adb",
                Arguments = $"-s {deviceId} logcat -d",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var proc = Process.Start(psi);
            string output = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();
            return output;
        }
    }
}
