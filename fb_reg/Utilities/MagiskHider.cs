using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fb_reg.Utilities
{
    public static class MagiskHider
    {
        // ✅ Danh sách app cần thêm vào DenyList
        public static string[] apps = new string[]
        {
        "com.facebook.katana",
        "com.facebook.lite",
        "com.scottyab.rootbeer.sample",
        "com.google.android.gms",
        "com.zing.zalo"
        };

        public static void Run(string deviceId = "")
        {
            string adb = string.IsNullOrEmpty(deviceId) ? "adb" : $"adb -s {deviceId}";

            void RunAdb(string cmd)
            {
                Console.WriteLine($"$ {adb} {cmd}");
                var p = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = $"/c {adb} {cmd}",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                p.Start();
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                Console.WriteLine(output.Trim());
            }

            Console.WriteLine("🔧 Bật Zygisk & DenyList...");
            RunAdb(@"shell su -c ""settings put global magisk_zygisk 1""");
            RunAdb(@"shell su -c ""settings put global magisk_denylist 1""");

            Console.WriteLine("📦 Cài Shamiko module (Shamiko.zip trong cùng thư mục)...");
            RunAdb("push Shamiko-v1.2.1-383-release.zip /sdcard/");
            RunAdb(@"shell su -c ""magisk --install-module /sdcard/Shamiko-v1.2.1-383-release.zip""");

            Console.WriteLine("📱 Ẩn Magisk App...");
            RunAdb(@"shell su -c ""magisk --hide""");

            Console.WriteLine("📌 Thêm app vào DenyList...");
            foreach (var pkg in apps)
            {
                RunAdb($@"shell su -c ""magisk --denylist add {pkg}""");
            }

            Console.WriteLine("🧹 Xoá dấu vết `magisk*` trong /data/adb...");
            RunAdb(@"shell su -c ""cd /data/adb && rm -rf magisk*""");

            Console.WriteLine("✅ Xong! Hãy reboot thiết bị để hoàn tất.");
        }
    }
}
