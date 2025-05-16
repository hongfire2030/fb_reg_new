using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fb_reg.Fbreg
{
    public class FacebookScreenDetector
    {
        public enum FacebookScreen
        {
            Unknown,
            RegisterForm,
            VerifyCheckpoint,
            LoggedIn,
        }

        public FacebookScreen Detect(string deviceId)
        {
            string activity = GetCurrentActivity(deviceId);
            if (activity.Contains("Registration")) return FacebookScreen.RegisterForm;
            if (activity.Contains("checkpoint")) return FacebookScreen.VerifyCheckpoint;
            if (activity.Contains("home")) return FacebookScreen.LoggedIn;
            return FacebookScreen.Unknown;
        }

        public string GetCurrentActivity(string deviceId)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "adb",
                Arguments = $"-s {deviceId} shell dumpsys window windows",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = Process.Start(psi))
            {
                string output = process.StandardOutput.ReadToEnd();
                return output;
            }
        }
    }
}
