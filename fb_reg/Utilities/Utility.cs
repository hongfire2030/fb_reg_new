using AE.Net.Mail;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;

using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using OpenPop.Mime;
using OpenPop.Pop3;

using static fb_reg.ServerApi;
using Pop3Client = OpenPop.Pop3.Pop3Client;
using Message = OpenPop.Mime.Message;
using System.Net.NetworkInformation;
using EAGetMail;
using static fb_reg.CacheServer;
using System.Globalization;
using System.IO.MemoryMappedFiles;

namespace fb_reg
{

    public static class CanFetchState
    {
        private const string Name = "Global\\MailQueueFlag";

        public static void Set(bool value)
        {
            using (var mmf = MemoryMappedFile.CreateOrOpen(Name, 1))
            using (var acc = mmf.CreateViewAccessor())
            {
                acc.Write(0, value);
            }
        }

        public static bool Get()
        {
            using (var mmf = MemoryMappedFile.OpenExisting(Name))
            using (var acc = mmf.CreateViewAccessor())
            {
                return acc.ReadBoolean(0);
            }
        }
    }


    [Serializable]
    public class POPClientEmail//Create a popmail class for getting mail details and going to store in list.
    {
        public POPClientEmail()
        {
            this.Attachments = new List<Attachment> ();
        }
        public int MessageNumber { get; set; }
        public string From { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
        public DateTime DateSent { get; set; }

        public List< Attachment> Attachments { get; set; }
    }
    [Serializable]
    public class Attachment
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
class Utility
    {


        public static List<string> listRoms = new List<string> { "android9","android10", "android11", "android13"};
        public static List<string> Ten_Nam;
        public static List<string> Ten_Nu;
        public static List<string> FemaleName;
        public static List<string> MaleName;
        public static List<string> FemaleNameRaw;
        public static List<string> MaleNameRaw;
        public static List<string> LastName;
        public static List<string> AllName;
        public static bool forceStopGetOtp = false;
        public static bool isAvailableSellGmail = true;
        public static bool unsignText;
        public static bool labanKeyboard;
        public static bool adbKeyboard;
        public static bool inputString;
        public static bool ghiChuTrenAvatar;

        public static List<string> bestwishList;
        public static Bitmap checkFacebookOpenImage;

        //public static Bitmap PROXY_CHECK = (Bitmap)Image.FromFile("img/proxy_check.png");


        public static Bitmap TAO_TAI_KHOAN_MOI = (Bitmap)Image.FromFile("img/tao_tai_khoan_moi.png");
        public static Bitmap TAO_TAI_KHOAN_MOI_2 = (Bitmap)Image.FromFile("img/tao_tai_khoan_moi_2.png");
        public static Bitmap FBLITE_DANG_NHAP_IMG = (Bitmap)Image.FromFile("img/fblite_dangnhap.png");
        public static Bitmap FBLITE_2FA = (Bitmap)Image.FromFile("img/fblite_2fa.png");
        public static Bitmap FB_2FA = (Bitmap)Image.FromFile("img/2fa.png");
        public static Bitmap FB_2FA1 = (Bitmap)Image.FromFile("img/2fa1.png");
        public static Bitmap FB_2FA1_1 = (Bitmap)Image.FromFile("img/2fa1_1.png");
        public static Bitmap FB_2FA3 = (Bitmap)Image.FromFile("img/2fa3.png");
        public static Bitmap FB_2FA2 = (Bitmap)Image.FromFile("img/2fa2.png");
        public static Bitmap NEXT_CHECK_FB = (Bitmap)Image.FromFile("img/next.png");
        public static Bitmap KHONG_CO_LOI_MOI_NAO = (Bitmap)Image.FromFile("img/khong_co_loi_moi.png");
        public static Bitmap LUU_THONG_TIN_DANG_NHAP = (Bitmap)Image.FromFile("img/luu_thong_tin_dang_nhap.png");
        public static Bitmap CHO_PHEP_TRUY_CAP = (Bitmap)Image.FromFile("img/cho_phep_truy_cap.png");
        public static Bitmap BAT_DANH_BA = (Bitmap)Image.FromFile("img/bat_danh_ba.png");
        public static Bitmap THU_VIEN_ANH = (Bitmap)Image.FromFile("img/thu_vien_anh.png");
        public static Bitmap CHOOSE_FB = (Bitmap)Image.FromFile("img/lua_chon_fb.png");
        public static Bitmap CHECK_AVATAR_PROFILE = (Bitmap)Image.FromFile("img/check_avatar_profile.png");
        //public static Bitmap NOTHANKS = (Bitmap)Image.FromFile("img/nothanks.png");
        //public static Bitmap DANGNHAP_HOTMAIL = (Bitmap)Image.FromFile("img/dangnhap_hotmail.png");
        public static Bitmap TAI_DANH_BA_LEN = (Bitmap)Image.FromFile("img/taidanhbalen.png");
        public static Bitmap FA_OK = (Bitmap)Image.FromFile("img/2fa_ok.png");
        public static Bitmap LUC_KHAC = (Bitmap)Image.FromFile("img/luc_khac.png");
        public static Bitmap DANG_CHO_SDT = (Bitmap)Image.FromFile("img/dang_cho_sdt.png");
        

        public static Bitmap THEM_5BB = (Bitmap)Image.FromFile("img/them_5_bb.png");
        //public static Bitmap MAIL_GO = (Bitmap)Image.FromFile("img/mail_go.png");
        //public static Bitmap MAIL_FB_CHECK = (Bitmap)Image.FromFile("img/mail_facebook_register.png");
        //public static Bitmap MAIL_RELOAD = (Bitmap)Image.FromFile("img/mail_reload.png");

        public static string[] AvatarFilesMale = Directory.GetFiles(@"img\avatar_vn\Nam");
        public static string[] AvatarFilesFemale = Directory.GetFiles(@"img\avatar_vn\Nu");
        public static string[] CARRY_CODE;
        
        public static Dictionary<String, String> dictAvatarMalePath = new Dictionary<string, string>();
        public static Dictionary<String, String> dictAvatarFemalePath = new Dictionary<string, string>();

        public static string SERVER_LOCAL = "";



        public static bool IsLogcatCrashDetected(string deviceId, string package)
        {
            Console.WriteLine($"📋 Đang kiểm tra logcat của thiết bị {deviceId}...");

            string logs = RunAdb(deviceId, "logcat -d -t 100");

            // Kiểm tra các lỗi nghiêm trọng liên quan app
            if (logs.Contains("FATAL EXCEPTION") && logs.Contains(package))
            {
                Console.WriteLine($"❌ FATAL EXCEPTION liên quan tới {package}");
                return true;
            }

            if (logs.Contains("ANR in") && logs.Contains(package))
            {
                Console.WriteLine($"⚠️ ANR (Application Not Responding) liên quan tới {package}");
                return true;
            }

            if (logs.Contains("Zygote") && logs.Contains("crash"))
            {
                Console.WriteLine("🔥 Zygote crash – khả năng hệ thống bị ảnh hưởng");
                return true;
            }

            if (logs.Contains("SystemServer") && logs.Contains("Exception"))
            {
                Console.WriteLine("🧨 SystemServer lỗi nghiêm trọng");
                return true;
            }

            return false;
        }

        public static string RunAdb(string deviceId, string args)
        {
            try
            {
                var p = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "adb",
                        Arguments = $"-s {deviceId} {args}",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                p.Start();
                string output = p.StandardOutput.ReadToEnd();
                string error = p.StandardError.ReadToEnd();
                p.WaitForExit();

                if (!string.IsNullOrWhiteSpace(error))
                    Console.WriteLine($"⚠️ ADB error: {error.Trim()}");

                return output.Trim();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ADB exception: {ex.Message}");
                return "";
            }
        }

        public static bool IsDeviceResponsive(string deviceID)
        {
            string result = RunAdb(deviceID, "shell echo ok");
            return result.Trim() == "ok";
        }
        public static void PrepareDeviceForClone1(string deviceId, string brandCode = "25", string modelCode = "3")
        {
            Console.WriteLine($"[*] Preparing device {deviceId} for Facebook clone...");

            // 1. Xoá dữ liệu Facebook
            //RunAdb(deviceId, "shell su -c 'pm clear com.facebook.katana'");

            // 2. Fake device fingerprint (MagiskHide Props)
     //       string propsScript =
     //$"shell su -c \"echo -e '1\\nf\\n{brandCode}\\n{modelCode}\\ny\\ny' | props\"";

     //       propsScript = propsScript.Replace("\n", "; ").Replace("\r", "");
     //       string cmd = string.Format(Device.CONSOLE_ADB , deviceId) + propsScript;
     //       Device.ExecuteCMD(cmd);

            // 3. Android ID random
            string androidId = Guid.NewGuid().ToString("N").Substring(0, 16);
            RunAdb(deviceId, $"shell su -c \"settings put secure android_id {androidId}\"");

            // 4. Tạo file proxychains.conf tạm thời
            //var parts = proxy.Split(':');
            //if (parts.Length < 2)
            //{
            //    Console.WriteLine("[!] Proxy format invalid. Use ip:port");
            //    return;
            //}

            //string proxyConf = $"strict_chain\nproxy_dns\n[ProxyList]\nsocks5 {parts[0]} {parts[1]}";
            //File.WriteAllText("proxychains.conf", proxyConf);
            //RunAdb(deviceId, "push proxychains.conf /data/local/tmp/proxychains.conf");

            // 5. Tăng animation scale
            //RunAdb(deviceId, "shell settings put global window_animation_scale 10");
            //RunAdb(deviceId, "shell settings put global transition_animation_scale 10");
            //RunAdb(deviceId, "shell settings put global animator_duration_scale 10");

            //// 6. Hiển thị toast (yêu cầu Termux + termux-api)
            ////RunAdb(deviceId, $"shell su -c \"termux-toast '✅ Device ready for FB clone ({androidId})'\"");

            //Console.WriteLine($"[✓] Device {deviceId} is ready. Android ID: {androidId}");
        }
        public static void PrepareDeviceForClone2(string deviceId, string brandCode = "25", string modelCode = "3")
        {
            //Console.WriteLine($"[*] Preparing device {deviceId} for Facebook clone...");

            //// 1. Xoá dữ liệu Facebook
            ////RunAdb(deviceId, "shell su -c 'pm clear com.facebook.katana'");

            //// 2. Fake device fingerprint (MagiskHide Props)
            //string propsScript = $@"
            //    su -c 'props <<EOF
            //    1
            //    f
            //    {brandCode}
            //    {modelCode}
            //    y
            //    y
            //    EOF'";
            //RunAdb(deviceId, $"shell \"{propsScript.Replace("\n", "; ").Replace("\r", "")}\"");

            //// 3. Android ID random
            //string androidId = Guid.NewGuid().ToString("N").Substring(0, 16);
            //RunAdb(deviceId, $"shell su -c \"settings put secure android_id {androidId}\"");

            // 4. Tạo file proxychains.conf tạm thời
            //var parts = proxy.Split(':');
            //if (parts.Length < 2)
            //{
            //    Console.WriteLine("[!] Proxy format invalid. Use ip:port");
            //    return;
            //}

            //string proxyConf = $"strict_chain\nproxy_dns\n[ProxyList]\nsocks5 {parts[0]} {parts[1]}";
            //File.WriteAllText("proxychains.conf", proxyConf);
            //RunAdb(deviceId, "push proxychains.conf /data/local/tmp/proxychains.conf");

            // 5. Tăng animation scale
            //RunAdb(deviceId, "shell settings put global window_animation_scale 5");
            //RunAdb(deviceId, "shell settings put global transition_animation_scale 5");
            //RunAdb(deviceId, "shell settings put global animator_duration_scale 5");

            // 6. Hiển thị toast (yêu cầu Termux + termux-api)
            //RunAdb(deviceId, $"shell su -c \"termux-toast '✅ Device ready for FB clone ({androidId})'\"");

            //Console.WriteLine($"[✓] Device {deviceId} is ready. Android ID: {androidId}");
        }


        public static void SendSMSCheckData(string deviceID)
        {
            string realSim = Device.GetRealSim(deviceID);
            if (realSim.Contains("Viettel"))
            {
                Device.SendSMS(deviceID, "191");
                Thread.Sleep(2000);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 38.1, 95.0);
                Device.InputText(deviceID, "kttk");
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 91.5, 50.5); // send sms

            }
            else if (realSim.Contains("VINAPHONE"))
            {
                Device.SendSMS(deviceID, "888");
                Thread.Sleep(2000);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 38.1, 95.0);
                Device.InputText(deviceID, "data");
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 91.5, 50.5); // send sms

            }
            else if (realSim.Contains("Mobifone"))
            {
                Device.SendSMS(deviceID, "999");
                Thread.Sleep(2000);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 38.1, 95.0);
                Device.InputText(deviceID, "kt all");
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 91.5, 50.5); // send sms

            }
            else
            {
                Device.SendSMS(deviceID, "789");
                Thread.Sleep(2000);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 38.1, 95.0);
                Device.InputText(deviceID, "kt all");
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 91.5, 50.5); // send sms
            }

            Thread.Sleep(1000);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 15.6, 54.1); // cho phep
            Thread.Sleep(1000);
            WaitAndTapXML(deviceID, 2, "luôn cho phép");
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 83.3, 62.8); // gửi
        }



        public static bool IsMailEmpty(MailObject mail)
        {
            if (mail == null || string.IsNullOrEmpty(mail.email))
            {
                return true;
            }
            return false;
        }
        public static string GetDuoiMailFromServer()
        {
            string duoimail = "";

            List<String> temp = ServerApi.GetListDuoiMail();

            if (temp != null && temp.Count > 0)
            {
                var random = new Random();
                int index = random.Next(temp.Count);
                duoimail = temp[index];
            }
            return duoimail;
        }

        public static bool AddDuoiMailToServer(string duoimail)
        {
            
            return ServerApi.UpdateDuoiMail(duoimail);
        }
        public static Account getNvrOutsite()
        {
            string[] lineTemp = FileUtil.GetAndDeleteLine("Data/nvr.txt").Split('|'); // Get mail form local first
            if (lineTemp != null || lineTemp.Length >= 2)
            {

                Account acc = new Account();
                acc.uid = lineTemp[0];
                acc.pass = lineTemp[1];
                return acc;
            }
            return null;
        }
        public static string buildStatus(string turnOnSim, string turnOnEmu)
        {
            string result = "status: turnOnSim:" + turnOnSim + "  turnOnEmu:" + turnOnEmu;
            return result;
        }
        public static void ResetData()
        {
            try
            {
                System.IO.DirectoryInfo di = new DirectoryInfo("Authentication");

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ex:" + ex.Message);
            }
        }

        public static bool InputConfirmCode(string deviceID, string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return false;
            }

            char[] textChar = code.ToCharArray();
            Random ran = new Random();

            foreach (char c in textChar)
            {
                if (!"0123456789".Contains(c))
                {
                    return false;
                }
                InputNumberByTouch(deviceID, c);
                Thread.Sleep(ran.Next(100, 200));
            }
            return true;
        }
        public static void InputNumberByTouch(string deviceID, char number)
        {
            switch(number)
            {
                case '0':
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 42.4, 95.1);
                    break;
                case '1':
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 16.4, 72.9);
                    break;
                case '2':
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 39.7, 72.1);
                    break;
                case '3':
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 69.2, 73.0);
                    break;
                case '4':
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 13.8, 80.2);
                    break;
                case '5':
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 41.2, 80.3);
                    break;
                case '6':
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 69.2, 79.5);
                    break;
                case '7':
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 15.2, 87.2);
                    break;
                case '8':
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 44.2, 88.2);
                    break;
                case '9':
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 66.1, 88.2);
                    break;
                    
            }
        }
        public static bool CheckNetwork(string deviceID)
        {
            string temp = Device.CurlGoogle(deviceID);
            if (temp.ToLower().Contains("could not resolve host"))
            {
                return false;
            }
            return true;
        }
        public static string GetPublicIp(DeviceObject device)
        {
            if (!string.IsNullOrEmpty(device.realSim))
            {
                if (device.realSim.ToLower().Contains("itel"))
                {
                    Thread.Sleep(3000);
                }
            }

            string ip;
            ip = GetIpCuatoi(device.deviceId);
            //if (!string.IsNullOrEmpty(device.realSim)
            //    && device.realSim.ToLower().Contains("mobi") && !device.realSim.ToLower().Contains("vietnam"))
            //{
            //    ip = GetSeeIp(device.deviceId);

            //    if (string.IsNullOrEmpty(ip))
            //    {
            //        ip = GetSeeIp(device.deviceId);
            //        if (string.IsNullOrEmpty(ip))
            //        {
            //            ip = GetIpCuatoi(device.deviceId);
            //        }
            //    }
            //}
            //else
            //{
            //    ip = GetIpApi(device.deviceId);

            //    if (string.IsNullOrEmpty(ip))
            //    {
            //        ip = GetSeeIp(device.deviceId);

            //        if (string.IsNullOrEmpty(ip))
            //        {
            //            ip = GetSeeIp(device.deviceId);
            //            if (string.IsNullOrEmpty(ip))
            //            {
            //                ip = GetIpCuatoi(device.deviceId);
            //            }
            //        }
            //    }
            //}

            return ip;
        }

        public static string GetIpApi(string deviceID)
        {
            string temp = Device.GetPublicIPAPI(deviceID);
            
            if (temp.Contains("query"))
            {
                Match aaa = Regex.Match(temp, "query\":\"([0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3})\"|\"(([a-f0-9:]+:+)+[a-f0-9]+)\"");
                if (aaa.Success)
                {
                    string ip = aaa.Groups[1].ToString();

                    if (Regex.Match(ip, "([0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3})|(([a-f0-9:]+:+)+[a-f0-9]+)").Success)
                    {
                        return ip;
                    }
                    else
                    {
                        return "";
                    }
                }

            }
            return "";
        }

        public static string GetSeeIp(string deviceID)
        {
            Device.OpenWeb(deviceID, "https://ip.seeip.org/geoip");
            Thread.Sleep(4000);
            string temp = Device.ExecuteCMD("adb -s " + deviceID + " shell uiautomator dump /dev/stdout");
            //string temp = Device.GetPublicIpSeeIp(deviceID);
            if (temp.Contains("ip"))
            {
                Match aaa = Regex.Match(temp, "ip\":\"([0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3})\"|\"(([a-f0-9:]+:+)+[a-f0-9]+)\"");
                if (aaa.Success)
                {
                    string ip = aaa.Groups[1].ToString();
                    if (aaa.Value.Length > 30)
                    {
                        ip = aaa.Groups[2].ToString();
                    }
                    

                    if (Regex.Match(ip, "([0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3})|(([a-f0-9:]+:+)+[a-f0-9]+)").Success)
                    {
                        return ip;
                    }
                    else
                    {
                        return "";
                    }
                }
                
            }
            return "";
        }
        public static string GetIpCuatoi(string deviceID)
        {
            Device.OpenWeb(deviceID, "https://ipcuatoi.com");
            Thread.Sleep(4000);
            string temp = Device.ExecuteCMD("adb -s " + deviceID + " shell uiautomator dump /dev/stdout");

            if (Regex.Match(temp, "\"([0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3})\"|\"(([a-f0-9:]+:+)+[a-f0-9]+)\"").Success)
            {
                string aaa = Regex.Match(temp, "\"([0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3})\"|\"(([a-f0-9:]+:+)+[a-f0-9]+)\"").ToString();
                string ip = aaa.Replace("\"", "");
                Device.KillApp(deviceID, Constant.BROWSER_PACKAGE);
                return ip;
            } else
            {
                // try again
                Device.OpenWeb(deviceID, "https://ipcuatoi.com");
                Thread.Sleep(7000);
                temp = Device.ExecuteCMD("adb -s " + deviceID + " shell uiautomator dump /dev/stdout");

                if (Regex.Match(temp, "\"([0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3})\"|\"(([a-f0-9:]+:+)+[a-f0-9]+)\"").Success)
                {
                    string aaa = Regex.Match(temp, "\"([0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3})\"|\"(([a-f0-9:]+:+)+[a-f0-9]+)\"").ToString();
                    string ip = aaa.Replace("\"", "");
                    Device.KillApp(deviceID, Constant.BROWSER_PACKAGE);
                    return ip;
                }
            }

            Device.KillApp(deviceID, Constant.BROWSER_PACKAGE);
            return "";
        }
        public static string GetIpPublic(string deviceID)
        {
            string publicIp = "";


            return publicIp;
        }
        public static string CheckIP()
        {
            string result = "";
            try
            {
                RequestXNet requestXNet = new RequestXNet("", "", "", 0);
                result = JObject.Parse(requestXNet.RequestGet("http://lumtest.com/myip.json"))["ip"].ToString();
            }
            catch
            {
            }
            return result;
        }
        public static bool IsDigitsOnly(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        public static string GetPhoneOtpSim()
        {

            string apiGetPhoneOtp = string.Format("http://otpsim.com/api/phones/request?token={0}&&service=7", Constant.OTPSIM);
            var request = (HttpWebRequest)WebRequest.Create(apiGetPhoneOtp);
            request.Method = "GET";
            request.Accept = "application/json";
            request.ContentType = "application/json; charset=utf-8";

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            ResponseObjectOTPSim data = JsonConvert.DeserializeObject<ResponseObjectOTPSim>(responseString);

            Console.WriteLine(data.message);


            if (data.status_code == 200)
            {
                return data.data.phone_number + "|" + data.data.session;
            }
            return Constant.FAIL;

        }

        public static string GeCodeOtpSim(string session)
        {
            bool waitSms = true;
            int timeout = 20;
            while(waitSms)
            {
                string apiGetCodeOtpSim = string.Format("http://otpsim.com/api/sessions/{0}?token={1}", session, Constant.OTPSIM);
                var request = (HttpWebRequest)WebRequest.Create(apiGetCodeOtpSim);
                request.Method = "GET";
                request.Accept = "application/json";
                request.ContentType = "application/json; charset=utf-8";

                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                string status = Regex.Match(responseString, "status\":(.*?),").Groups[1].ToString();
                if (status == "0")
                {
                    return Regex.Match(responseString, "otp\":\"(.*?)\"").Groups[1].ToString();
                }
                Thread.Sleep(2000);
                timeout--;
                if (timeout < 1 || status == "2")
                {
                    waitSms = false;
                }

            }

            CancelWaitSms(session);
            return Constant.FAIL;
        }
        public static void CancelWaitSms(string session)
        {
            string apiGetCodeOtpSim = string.Format("http://otpsim.com/api/sessions/cancel?session={0}&token={1}", session, Constant.OTPSIM);
            var request = (HttpWebRequest)WebRequest.Create(apiGetCodeOtpSim);
            request.Method = "GET";
            request.Accept = "application/json";
            request.ContentType = "application/json; charset=utf-8";

            var response = (HttpWebResponse)request.GetResponse();
        }
        public static string RandomSim(string currentSim)
        {
            Random ran = new Random();

            while (true)
            {
                int nex = ran.Next(1, 200);
                string sim;

                if (nex < 100)
                {

                    sim = Constant.VIETTEL;
                }
                else if (nex >= 100 && nex < 150)
                {

                    sim = Constant.VINAPHONE;
                }
                else //if (nex >= 140 && nex < 210)
                {
                    sim = Constant.VN_VINAPHONE;
                } 
                //else if (nex >= 210 && nex < 240)
                //{
                //    sim = Constant.VIETNAM_MOBILE;
                //} 
                //else
                //{
                //    sim = Constant.VN_MOBIPHONE;
                //}

                if (sim != currentSim)
                {
                    return sim;
                }
            }
        }

        public static string RandomSim2(string currentSim)
        {
            Random ran = new Random();

            while (true)
            {
                int nex = ran.Next(1, 150);
                string sim;

                if (nex < 50)
                {

                    sim = Constant.VN_MOBIPHONE;
                }
                else if (nex >= 50 && nex < 100)
                {

                    sim = Constant.VIETNAM_MOBILE;
                } else
                {
                    sim = Constant.MOBI;
                }
                //else //if (nex >= 140 && nex < 210)
                //{
                //    sim = Constant.VIETTEL_TELECOM;
                //}
                //else if (nex >= 210 && nex < 240)
                //{
                //    sim = Constant.VIETNAM_MOBILE;
                //} 
                //else
                //{
                //    sim = Constant.VN_MOBIPHONE;
                //}

                if (sim != currentSim)
                {
                    return sim;
                }
            }
        }
        public static bool ChangeHMAPC()
        {
            
            // Turn on hma
            var hmaHandle = IntPtr.Zero;
            hmaHandle = KAutoHelper.AutoControl.FindWindowHandle(null, "HMA VPN");
            var pointClick = KAutoHelper.AutoControl.GetGlobalPoint(hmaHandle, 455, 375);

            KAutoHelper.AutoControl.MouseClick(pointClick, KAutoHelper.EMouseKey.LEFT);
            Console.WriteLine(hmaHandle.ToString());
            Thread.Sleep(10000);
            return true;
        }
        public static bool isScreenLock(string deviceID)
        {
            if (Device.GetScreenStatus(deviceID) != Constant.SCREEN_OPEN_STATUS)
            {
                return true;
            }
            return false;
        }
        public static string GetIP(string deviceID)
        {
            string ip = "";
            ip = Device.GetIpLocal(deviceID);
            if (ip == "")
            {
                ip = Device.GetIPV6(deviceID);
            }
            return ip;
        }

        public static void InputVietVNIText(string deviceID, string text)
        {
            if (!unsignText)
            {
                if (labanKeyboard)
                {
                    text = ConvertToVNI(text);
                }
                
            } else
            {
                text = ConvertToUnsign(text);
            }
            
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            //if (inputString)
            //{
            //    if (adbKeyboard)
            //    {
            //        Device.InputStringAdbKeyboard(deviceID, text);
            //    } else
            //    {
            //        Device.InputText(deviceID, text);
            //    }
            //} else
            //{
                char[] textChar = text.ToCharArray();
                Random ran = new Random();

                foreach (char c in textChar)
                {
                    if (adbKeyboard)
                    {
                        Device.InputStringAdbKeyboard(deviceID, Convert.ToString(c));
                    } else
                    {
                        Device.InputText(deviceID, Convert.ToString(c));
                    }
                    Thread.Sleep(ran.Next(100, 200));
                }
            //}

            Thread.Sleep(700);
        }

        public static void InputText(string deviceID, string text, bool inputString)
        {
            
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            //Device.SelectAdbKeyboard(deviceID);

            if (inputString)
            {
               
                    Device.InputText(deviceID, text);
                

            }
            else
            {
                char[] textChar = text.ToCharArray();
                Random ran = new Random();

                foreach (char c in textChar)
                {

                        Device.InputText(deviceID, Convert.ToString(c));
                   
                    Thread.Sleep(ran.Next(150, 300));
                }
            }
            //Device.StopAdbKeyboard(deviceID);
            Thread.Sleep(700);
        }

        public static void InputMail(string deviceID, string text, bool inputString = true)
        {
            inputString = true;
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            Device.SelectAdbKeyboard(deviceID);

            if (inputString)
            {
                //Device.InputText(deviceID, text);

                GboardTyper.TypeText(deviceID, text);
            }
            else
            {
                char[] textChar = text.ToCharArray();
                Random ran = new Random();

                foreach (char c in textChar)
                {

                    Device.InputText(deviceID, Convert.ToString(c));

                    Thread.Sleep(ran.Next(200, 400));
                }
            }
            Device.StopAdbKeyboard(deviceID);
            Device.SelectLabanKeyboard(deviceID);
            Thread.Sleep(700);
        }

        public static void InputVietTelexText(string deviceID, string text, bool inputString)
        {
            if (!unsignText)
            {
                text = ConvertToTelex(text);
            }

            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            //Device.SelectAdbKeyboard(deviceID);

            if (inputString)
            {
               
                    Device.InputText(deviceID, text);
                

            }
            else
            {
                char[] textChar = text.ToCharArray();
                Random ran = new Random();

                foreach (char c in textChar)
                {

                        Device.InputText(deviceID, Convert.ToString(c));
                    


                    Thread.Sleep(ran.Next(100, 200));
                }
            }
            //Device.StopAdbKeyboard(deviceID);
            Thread.Sleep(700);
        }

        public static void InputUsText(string deviceID, string text, bool inputString)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            //Device.SelectAdbKeyboard(deviceID);

            if (inputString)
            {
                //Device.InputStringAdbKeyboard(deviceID, text);
                Device.InputText(deviceID, text);
            }
            else
            {
                char[] textChar = text.ToCharArray();
                Random ran = new Random();

                foreach (char c in textChar)
                {
                    //Device.InputStringAdbKeyboard(deviceID, Convert.ToString(c));
                    Device.InputText(deviceID, Convert.ToString(c));
                    Thread.Sleep(ran.Next(100, 200));
                }
            }
            //Device.StopAdbKeyboard(deviceID);
            Thread.Sleep(700);
        }
        public static string DownloadAvatar(Label status)
        {
            for (int i = 0; i < 5000; i++)
            {
                string fileName = "download/avatar/" + DateTime.Now.ToString("MM-dd-yyyy-hh-mm-ss-fff-tt") + ".png";
                string url = $"https://thispersondoesnotexist.com/image";

                using (var client = new WebClient())
                {
                    client.DownloadFile(url, fileName);
                }
                Thread.Sleep(2000);

                status.Invoke(new MethodInvoker(() =>
                {
                    status.Text = "Downloading iamge:" + (i + 1);
                }));

            }

            return "";
        }

        public static string DownloadRandomCover(string folder = "data/random_image/")
        {
            if (!Directory.Exists("data/random_image/"))
            {
                Directory.CreateDirectory("data/random_image/");
            }
            string fileName = folder + DateTime.Now.ToString("MM-dd-yyyy-hh-mm-ss-fff-tt") + ".png";
            string url = $"https://source.unsplash.com/user/c_v_r/100x100";

            using (var client = new WebClient())
            {
                client.DownloadFile(url, fileName);
            }
            Thread.Sleep(500);
            

            return fileName;
        }
        public static DeviceObject GetAvailableDevice()
        {
            DeviceObject device = new DeviceObject();
            IList<IList<object>> values = GoogleSheet.GetValues(Constant.MANAGEMENT_SHEET, "M2:O");

            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    string rowString = "";
                    for (int i = 0; i < row.Count; i++)
                    {
                        rowString = rowString + (string)row[i] + "|";
                    }
                    string[] rowArr = rowString.Split('|');
                    if (rowArr[2] == "")
                    {
                        int index = Convert.ToInt32(rowArr[0]) + 1;
                        // Write data 
                        string[] check = rowArr[1].Split(' ');
                        device.index = index;
                        device.deviceId = check[0].Trim();
                        device.status = rowArr[2];
                        return device;
                    }
                }
            }
            return null;
        }

        public static void UpdateDeviceStatus(DeviceObject device, string status)
        {
            string statusCell = "O" + device.index;
            GoogleSheet.UpdateEntry(Constant.MANAGEMENT_SHEET, statusCell, status);
        }
        public static OrderObject GetAvailableOrder()
        {
            ArrayList orders = Order.GetOrders();

            bool parallel = GoogleSheet.GetValue(Constant.MANAGEMENT_SHEET, "C2") == "x";
            Random ran = new Random();
            int indexOrder = 0;
            if (parallel)
            {
                indexOrder = ran.Next(1, orders.Count) - 1;
            }

            OrderObject order = (OrderObject)orders[indexOrder];
            if (order.status == "Running")
            {
                return order;
            } else if (order.status == "New")
            {
                GoogleSheet.AddSheet(order.code);
                // Update to running
                string range = "J" + order.index;
                string rangeCurrentAmount = "K" + order.index;
                string rangeCount = "D1";
                GoogleSheet.UpdateEntry(Constant.MANAGEMENT_SHEET, range, "Running");
                GoogleSheet.UpdateEntry(Constant.MANAGEMENT_SHEET, rangeCurrentAmount, "='" + order.code + "'!D1");
                GoogleSheet.UpdateEntry(order.code, rangeCount, "=COUNTA(B1:B)-1");
                GoogleSheet.UpdateEntry(order.code, "A1", "x");
                GoogleSheet.UpdateEntry(order.code, "B1", "x");
                return order;
            } else if (order.status == "Checking")
            {
                string cookie = GoogleSheet.GetValue(Constant.MANAGEMENT_SHEET, "P2");
                ArrayList accs = GetListUidInSheet(order);
                foreach (Account acc in accs)
                {

                    acc.hasAvatar = CheckAvatar(cookie, acc.uid);
                    GoogleSheet.UpdateEntry(order.code, "D" + acc.index, "" + acc.hasAvatar);
                    acc.status = CheckUid(acc.uid);
                    GoogleSheet.UpdateEntry(order.code, "E" + acc.index, "" + acc.status);
                }
                GoogleSheet.UpdateEntry(Constant.MANAGEMENT_SHEET, "J" + order.index, "Done");
            }

            return new OrderObject();
        }
        public static string GetLiveToken(string[] cookies)
        {
            string token = "";
            if (cookies == null || cookies.Length < 1)
            {
                return token;
            }
            for (int i = 0; i < cookies.Length; i++)
            {
                string cookie = cookies[i];
                if (!string.IsNullOrWhiteSpace(cookie))
                {
                    token = CommonRequest.GetTokenEAAG(cookies[i], "", "", 0);
                    if (CommonRequest.CheckLiveToken(token, "", "", 0))
                    {
                        return token;
                    }
                }
            }
            return token;
        }

        public static bool CheckAvatar(string cookiesString, string username)
        {
            try
            {
                string[] cookies = cookiesString.Split('\n');
                string token = GetLiveToken(cookies);
                bool checkAvata = true;

                ///////// downlaod hình avata xuong
                string url = $"https://graph.facebook.com/{username}/picture?type=square&redirect=true&width=50&height=50&access_token={token}";
                string avatarFile = "avatar.png";
                using (var client = new WebClient())
                {
                    client.DownloadFile(url, avatarFile);
                }
                Bitmap avatar = (Bitmap)Image.FromFile(avatarFile);
                string imageFolder = "img/";

                Bitmap avatacheck = (Bitmap)Image.FromFile(imageFolder + "Nam.png");
                Bitmap avatacheck1 = (Bitmap)Image.FromFile(imageFolder + "Nu.png");

                if (Device.FindOutPoint(avatar, avatacheck) != null)
                {
                    return false;
                }

                if (Device.FindOutPoint(avatar, avatacheck1) != null)
                {
                    return false;
                }
                return checkAvata;
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public static string CheckUid(string uid)
        {
            HttpRequest.RequestHTTP request_api = new HttpRequest.RequestHTTP();
            xNet.HttpRequest httpRequest = new xNet.HttpRequest();
            request_api.SetSSL(SecurityProtocolType.Tls12);
            request_api.SetKeepAlive(true);
            request_api.SetDefaultHeaders(new string[]
            {
                    "content-type: Text/html,application/xhtml+xml,application/xml;q=0.9,*;q=0.8",
                    "user-agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.131 Safari/537.36"
            });
            string Response = request_api.Request("POST", "https://www.facebook.com/api/graphql", null, Encoding.UTF8.GetBytes(string.Concat(new string[]
            {
                    "fb_dtsg=",
                    "AQHJakFqRFnd:AQGOqAoZRse",
                    "&q=node(",
                    uid,
                    "){name}"
            })), true, null, 60000);
            if (!Response.Contains("null"))
            {
                string namefb = Regex.Match(Response, "name\":\"(.*?)\"").Groups[1].Value;
                namefb = Regex.Unescape(namefb);
                return namefb;
            }
            else
            {
                return "Die";
            }
        }
        public static ArrayList GetListUidInSheet(OrderObject order)
        {
            IList<IList<object>> values = GoogleSheet.GetValues(order.code, "A2:C");
            ArrayList acc = new ArrayList();
            if (values != null && values.Count > 0)
            {

                foreach (var row in values)
                {
                    string rowString = "";
                    for (int i = 0; i < row.Count; i++)
                    {
                        rowString = rowString + (string)row[i] + ",";
                    }
                    Account account = ConvertString2Account(rowString);
                    if (account != null)
                    {

                        acc.Add(account);
                    }
                }
            }
            return acc;
        }

        public static Account ConvertString2Account(string accountString)
        {
            Account acc = new Account();
            string[] accAr = accountString.Split(',');
            acc.index = Convert.ToInt32(accAr[0]);
            acc.uid = accAr[1];
            acc.data = accAr[2];
            return acc;
        }
        public static void Log(string content, Label status)
        {
            Console.Out.WriteLine(content);
            status.Invoke(new MethodInvoker(() =>
            {

                status.Text = content;
            }));
        }

        public static string ConvertToUnsign(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
                                        "đ", "ð",
                                        "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
                                        "í","ì","ỉ","ĩ","ị",
                                        "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
                                        "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
                                        "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                                        "d", "d",
                                        "e","e","e","e","e","e","e","e","e","e","e",
                                        "i","i","i","i","i",
                                        "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
                                        "u","u","u","u","u","u","u","u","u","u","u",
                                        "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }

        public static string ConvertToTelex(string text)
        {
            string[] arr1 = new string[] { "á",  "à",  "ả",  "ã",  "ạ",  "â",  "ấ",   "ầ",    "ẩ",   "ẫ",   "ậ",  "ă",  "ắ",   "ằ",   "ẳ",  "ẵ",   "ặ",
                                        "đ",
                                        "é","è", "ẻ",  "ẽ", "ẹ", "ê", "ế",  "ề",  "ể",  "ễ",  "ệ",
                                        "í", "ì", "ỉ", "ĩ", "ị",
                                        "ó", "ò", "ỏ", "õ", "ọ", "ô", "ố",  "ồ",  "ổ",  "ỗ",  "ộ",  "ơ", "ớ",  "ờ",  "ở",  "ỡ",  "ợ",
                                        "ú", "ù", "ủ", "ũ", "ụ", "ư", "ứ",  "ừ",  "ử",  "ữ", "ự",
                                        "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "as", "af", "ar", "ax", "aj", "aa", "aas", "aaf", "aar", "aax", "aaj", "aw", "aws", "awf", "awr", "awx", "awj",
                                        "dd",
                                        "es","ef","er","ex","ej","ee","ees","eef","eer","eex","eej",
                                        "is","if","ir","ix","if",
                                        "os","of","or","ox","oj","oo","oos","oof","oor","oox","ooj","ow","ows","owf","owr","owx","owj",
                                        "us","uf","ur","ux","uj","uw","uws","uwf","uwr","uwx","uwj",
                                        "ys","yf","yr","yw","yj",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }

        public static string ConvertToVNI(string text)
        {
            string[] arr1 = new string[] { "á",  "à",  "ả",  "ã",  "ạ",  "â",  "ấ",   "ầ",    "ẩ",   "ẫ",   "ậ",  "ă",  "ắ",   "ằ",   "ẳ",  "ẵ",   "ặ",
                                        "đ",
                                        "é","è", "ẻ",  "ẽ", "ẹ", "ê", "ế",  "ề",  "ể",  "ễ",  "ệ",
                                        "í", "ì", "ỉ", "ĩ", "ị",
                                        "ó", "ò", "ỏ", "õ", "ọ", "ô", "ố",  "ồ",  "ổ",  "ỗ",  "ộ",  "ơ", "ớ",  "ờ",  "ở",  "ỡ",  "ợ",
                                        "ú", "ù", "ủ", "ũ", "ụ", "ư", "ứ",  "ừ",  "ử",  "ữ", "ự",
                                        "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a1", "a2", "a3", "a8", "a5", "a6", "a61", "a62", "a63", "a64", "a65", "a8", "a81", "a82", "a83", "a84", "a85",
                                        "d9",
                                        "e1","e2","e3","e4","e5","e6","e61","e62","e63","e64","e65",
                                        "i1","i2","i3","i4","i5",
                                        "o1","o2","o3","o4","o5","o6","o61","o62","o63","o64","o65","o7","o71","o72","o73","o74","o75",
                                        "u1","u2","u3","u4","u5","u7","u71","u72","u73","u74","u75",
                                        "y1","y2","y3","y4","y5",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }

        public static string GetHotMailApi()
        {
            string apiGetHotMail = string.Format("http://api.maxclone.vn/api/portal/buyaccount?key={0}&type=HOTMAIL&quantity=1", Constant.MAXCLONE_KEY);
            var request = (HttpWebRequest)WebRequest.Create(apiGetHotMail);
            request.Method = "GET";
            request.Accept = "application/json";
            request.ContentType = "application/json; charset=utf-8";

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            ResponseObject data = JsonConvert.DeserializeObject<ResponseObject>(responseString);

            Console.WriteLine(data.Message);


            if (data.Code == 0)
            {
                return data.Data[0].Username + "|" + data.Data[0].Password;
            }
            return Constant.FAIL;
        }

        public static string GetResetProxy()
        {
            string apiChangeIp = string.Format("http://longfb.ddns.net:10000/reset?proxy=longfb.ddns.net:6001");
            var request = (HttpWebRequest)WebRequest.Create(apiChangeIp);
            request.Method = "GET";
            request.Accept = "application/json";
            request.ContentType = "application/json; charset=utf-8";

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            ResponseProxyObject data = JsonConvert.DeserializeObject<ResponseProxyObject>(responseString);

            Console.WriteLine(data.status);


            if (data.status)
            {
                return "true";
            }
            return Constant.FAIL;
        }

        public static ResponseDongVanObject GetHotMailDongVanApi(int count, int mailType)
        {
            try
            {
                string apiGetHotMail = string.Format("https://api.dongvanfb.net/user/buy?apikey={0}&account_type={1}&quality={2}&type=null", "b4e2f1a9dc40f4aad654c570ee6418f5", mailType, count);
                var request = (HttpWebRequest)WebRequest.Create(apiGetHotMail);
                request.Method = "GET";
                request.Accept = "application/json";
                request.ContentType = "application/json; charset=utf-8";

                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return JsonConvert.DeserializeObject<ResponseDongVanObject>(responseString);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool checkScreen(string deviceID, string screenName)
        {
            return CheckTextExist(deviceID, screenName);
        }



        public static List<string> GetAllSubjectMail(MailObject mail)
        {
            List<string> subjects = new List<string>();
            try
            {

                Pop3Client pop3Client = new Pop3Client();//create an object for pop3client

                var username = mail.email;
                var Password = mail.password;

                pop3Client.Connect("outlook.office365.com", 995, true);
                pop3Client.Authenticate(username, Password, AuthenticationMethod.UsernameAndPassword);

                int count = pop3Client.GetMessageCount(); //total count of email in MessageBox  ie our inbox
                var Emails = new List<POPClientEmail>(); //object for list POPClientEmail class which we already created. 

                int counter = 0;
                for (int i = count; i >= 1; i--)//going to read mails from last till total number of mails received
                {
                    Message message = pop3Client.GetMessage(i);//assigning messagenumber to get detailed mail.//each mail having messagenumber
                    subjects.Add(message.Headers.Subject);

                }

                return subjects;
            }
            catch (Exception ex)
            {
                Console.WriteLine(" check live mail lan 1:" + ex.Message);
                mail.status = Constant.FAIL;
                mail.message = ex.Message;
                return subjects;
            }
            return subjects;
        }

        public static MailObject CheckLiveHotmailByOAuth2(MailObject mail)
        {
           // return mail;
            mail = OutsideServer.CheckHotmailByUnlimit(mail);
            if (mail.status != Constant.FAIL)
            {
                return mail;
            }

            mail = OutsideServer.CheckHotmailByVandong(mail);
            if (mail.status != Constant.FAIL)
            {
                return mail;
            }

            // Check again
            mail = OutsideServer.CheckHotmailByVandong(mail);
            if (mail.status != Constant.FAIL)
            {
                return mail;
            }

            //if (string.IsNullOrEmpty(mail.accessToken))
            //{
            //    mail.accessToken = GetAccessToken(mail.clientId, mail.refreshToken);

            //    if (string.IsNullOrEmpty(mail.accessToken))
            //    {
            //        mail.accessToken = GetAccessToken(mail.clientId, mail.refreshToken);
            //    }
            //    if (!string.IsNullOrEmpty(mail.accessToken))
            //    {
            //        mail = CheckLiveHotmailWithAccessToken(mail, mail.accessToken);
            //    }
            //    else
            //    {
            //        mail.status = Constant.FAIL;
            //    }
            //}
            //else
            //{
            //    mail = CheckLiveHotmailWithAccessToken(mail, mail.accessToken);
            //    if (mail.status != "OK")
            //    {
            //        // 
            //        mail.accessToken = GetAccessToken(mail.clientId, mail.refreshToken);
            //        mail = CheckLiveHotmailWithAccessToken(mail, mail.accessToken);
            //    }
            //}
        
            
            return mail;
        }

        public static MailObject CheckLiveHotmailWithAccessToken(MailObject mail, string accessToken)
        {
            mail.status = "OK";
            try
            {
                
                // use SSL IMAP + XOAUTH2
                MailServer oServer = new MailServer("outlook.office365.com", mail.email, accessToken, true,
                    ServerAuthType.AuthXOAUTH2, ServerProtocol.Pop3);
                // Set IMAP OAUTH 2.0
                oServer.AuthType = ServerAuthType.AuthXOAUTH2;
                // Enable SSL/TLS connection, most modern email server require SSL/TLS by default
                oServer.SSLConnection = true;
                oServer.Port = 995;

                Console.WriteLine("Connecting server ...");

                MailClient oClient = new MailClient("TryIt");
                oClient.Connect(oServer);

                Console.WriteLine("Retreiving email list ...");
                MailInfo[] infos = oClient.GetMailInfos();
                Console.WriteLine("Total {0} email(s)", infos.Length);

                Console.WriteLine("Disconnecting ...");

                // Delete method just mark the email as deleted, 
                // Quit method expunge the emails from server permanently.
                oClient.Quit();

                Console.WriteLine("Completed!");
            }
            catch (Exception ep)
            {
                Console.WriteLine("Error: {0}", ep.Message);
                mail.status = Constant.FAIL;
                return mail;
            }
            return mail;
        }

        public static MailObject CheckLiveHotmailPop3(MailObject mail)
        {
            if (mail == null)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(mail.refreshToken))
            {
                return mail;
            }
            try
            {

                Pop3Client pop3Client = new Pop3Client();//create an object for pop3client

                var username = mail.email;
                var Password = mail.password;

                pop3Client.Connect("outlook.office365.com", 995, true);
                pop3Client.Authenticate(username, Password, AuthenticationMethod.UsernameAndPassword);

                int count = pop3Client.GetMessageCount(); //total count of email in MessageBox  ie our inbox
                var Emails = new List<POPClientEmail>(); //object for list POPClientEmail class which we already created. 

                int counter = 0;
                for (int i = count; i >= 1; i--)//going to read mails from last till total number of mails received
                {
                    Message message = pop3Client.GetMessage(i);//assigning messagenumber to get detailed mail.//each mail having messagenumber

                    POPClientEmail email = new POPClientEmail()
                    {
                        MessageNumber = i,
                        Subject = message.Headers.Subject,
                        DateSent = message.Headers.DateSent,
                        From = message.Headers.From.Address,
                    };
                    mail.status = "OK";
                    return mail;
                    MessagePart body = message.FindFirstHtmlVersion();
                    if (body != null)
                    {
                        email.Body = body.GetBodyAsText();
                    }
                    else
                    {
                        body = message.FindFirstPlainTextVersion();
                        if (body != null)
                        {
                            email.Body = body.GetBodyAsText();
                        }
                    }
                    //List< MessagePart> attachments = message.FindAllAttachments();

                    //foreach (MessagePart attachment in attachments)
                    //{
                    //    email.Attachments.Add(new Attachment
                    //    {
                    //        FileName = attachment.FileName,
                    //        ContentType = attachment.ContentType.MediaType,
                    //        Content = attachment.Body
                    //    });
                    //}
                    Emails.Add(email);
                    counter++;

                }
                var emails = Emails;//You can filter mails by date from this list
                mail.status = "OK";
                return mail;
            }
            catch (Exception ex)
            {
                Console.WriteLine(" check live mail lan 1:" + ex.Message);
                mail.status = Constant.FAIL;
                mail.message = ex.Message;
                return mail;
            }
        }
        public static MailObject CheckLiveHotmail(MailObject mail)
        {
            if (mail == null)
            {
                return null;
            }
            try
            {

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;

                var mailRepo = new MailRepository("imap-mail.outlook.com", 993, true, mail.email, mail.password);
                mailRepo.GetAllMailSubjects();
                mail.mailRepository = mailRepo;
                mail.status = "OK";
                return mail;
            }
            catch (Exception ex)
            {
                Console.WriteLine(" check live mail lan 1:" + ex.Message);
                mail.status = Constant.FAIL;
                mail.message = ex.Message;
                return mail;
            }
        }
        public enum ProxyDomain
        {
            Tinsoft,
            Softlike,
            TinProxy,
            TmProxy,
            dtProxy,
            fastProxy,
            zuesProxy,
            zuesProxy4G,
            impulseProxy,
            tunProxy,
            wwProxy
        }
        public static Proxy GetProxy(string domain, string apiKey, string allowIp, string location)
        {

            if (domain == ProxyDomain.Softlike.ToString())
            {
                return OutsideServer.getProxyShoplike(apiKey, location);
            }
            else if (domain == ProxyDomain.Tinsoft.ToString())
            {
                return OutsideServer.getProxyTinsoft(apiKey);
            }
            else if (domain == ProxyDomain.TinProxy.ToString())
            {
                if (string.IsNullOrEmpty(allowIp))
                {
                    return null;
                }
                return OutsideServer.getProxyTinProxy(apiKey, allowIp);

            }
            else if (domain == ProxyDomain.TmProxy.ToString())
            {
                return OutsideServer.getProxyTmProxy(apiKey);
            }
            else if (domain == ProxyDomain.dtProxy.ToString())
            {
                Proxy p = OutsideServer.getProxyDtProxy(apiKey, allowIp);

                return p;
            }
            else if (domain == ProxyDomain.fastProxy.ToString())
            {
                Proxy p = new Proxy();
                string[] kk = apiKey.Split(':');
                if (kk == null || kk.Length != 4)
                {
                    return p;
                }
                p.host = kk[0];
                p.port = kk[1];
                p.username = kk[2];
                p.pass = kk[3];
                p.hasProxy = true;
                return p;
            } else if (domain == ProxyDomain.wwProxy.ToString())
            {
                return WwProxy.GetProxyWwProxy(apiKey);
            }

            return null;
        }

        public static string CheckLive(string userName, string password)
        {
            try
            {
                using (ImapClient ic = new ImapClient())
                {
                    ic.Connect("pop-mail.outlook.com", 993, true, false);
                    try
                    {
                        ic.Login(userName, password);
                        ic.Dispose();
                        return userName + "|" + password;
                    }
                    catch
                    {
                        using (StreamWriter HDD = new StreamWriter("FileCLone/HotmailChết.txt", true))
                        {
                            HDD.WriteLine(userName + "|" + password);
                            HDD.Close();
                        }
                        ic.Dispose();
                        return Constant.FAIL;
                    }
                }
            }
            catch (Exception e)
            {
                using (StreamWriter HDD = new StreamWriter("FileCLone/HotmailChết.txt", true))
                {
                    HDD.WriteLine(userName + "|" + password);
                    HDD.Close();
                }

                return Constant.FAIL;
            }
        }

        public static void WriteFileLog(string line, string filePath)
        {
            try
            {
                using (StreamWriter HDD = new StreamWriter(filePath, true))
                {
                    DateTime time = DateTime.Now;
                    HDD.WriteLine(time.ToString() + "|" + line);
                    HDD.Close();
                }
            }
            catch (Exception ex)
            {

            }

        }
        public static void WriteFileLine(string line, string filePath)
        {
            try
            {
                using (StreamWriter HDD = new StreamWriter(filePath, true))
                {

                    HDD.WriteLine(line);
                    HDD.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("write file error:" + ex.Message);
            }

        }
        public class Data
        {

            public string Username { get; set; }
            [JsonProperty("Password")]
            public string Password { get; set; }
            [JsonProperty("Status")]
            public string Status { get; set; }
        }
        public class ResponseObject
        {
            [JsonProperty("Code")]
            public int Code { get; set; }
            [JsonProperty("Message")]
            public string Message { get; set; }

            [JsonProperty("Data")]
            public Data[] Data { get; set; }
        }
        public class ResponseObjectOTPSim
        {
            [JsonProperty("status_code")]
            public int status_code { get; set; }
            [JsonProperty("message")]
            public string message { get; set; }

            [JsonProperty("data")]
            public DataOtpSim data { get; set; }
        }
        public class DataOtpSim
        {
            [JsonProperty("phone_number")]
            public string phone_number { get; set; }
            [JsonProperty("network")]
            public string network { get; set; }
            [JsonProperty("session")]
            public string session { get; set; }
        }
        public class ResponseProxyObject
        {
            [JsonProperty("msg")]
            public int msg { get; set; }
            [JsonProperty("status")]
            public bool status { get; set; }


        }

        public class ResponseDongVanObject
        {
            [JsonProperty("success")]
            public int success { get; set; }
            [JsonProperty("accounts")]
            public string accounts { get; set; }
        }
        public static string DownloadUsAvatar(string gender)
        {

            string url = string.Format("https://fakeface.rest/face/view/anythingcangohere_theapidoesntdoanythingwithit?gender={0}&maximum_age=40&minimum_age=18", gender);

            string fileName = "download/avatar/" + DateTime.Now.ToString("MM-dd-yyyy-hh-mm-ss-fff-tt") + ".png";

            using (var client = new WebClient())
            {
                client.DownloadFile(url, fileName);
            }
            Thread.Sleep(1000);
            return fileName;
        }
        public static string RandomAvatar(string deviceID, string gender, string language, bool forceUs = false)
        {
            Random n = new Random();
            string[] AvatarFiles;
            string path = "";
            string path2 = "";
            string path3 = @"F:\project\fb_reg\fb_reg\bin\Debug\img\avatar_vn\Nu\test.jpg";
            if (forceUs)
            {
                path = DownloadUsAvatar(gender);
            }
            else
            {
                Random random = new Random();
                AvatarObject cacheName = CacheServer.GetAvatarLocalCache(SERVER_LOCAL, gender, deviceID);
                string filePath = "";
                if (!string.IsNullOrEmpty(cacheName.localPath))
                {
                    filePath = cacheName.localPath;
                }
                string filePathSmall = "";

                //if (gender == Constant.MALE)
                //{

                //    if (cacheName != null && !string.IsNullOrEmpty(cacheName.avatarName) && dictAvatarMalePath.ContainsKey(cacheName.avatarName))
                //    {

                //        if (!dictAvatarMalePath.TryGetValue(cacheName.avatarName, out filePath))
                //        {
                //            int index = random.Next(dictAvatarMalePath.Count);
                //            KeyValuePair<string, string> pair = dictAvatarMalePath.ElementAt(index);
                //            filePath = pair.Value;
                //        }
                //    }
                //    else
                //    {
                //        int index = random.Next(dictAvatarMalePath.Count);
                //        KeyValuePair<string, string> pair = dictAvatarMalePath.ElementAt(index);
                //        filePath = pair.Value;
                //    }
                //}
                //else
                //{
                //    if (cacheName != null && !string.IsNullOrEmpty(cacheName.avatarName) && dictAvatarFemalePath.ContainsKey(cacheName.avatarName))
                //    {

                //        if (!dictAvatarFemalePath.TryGetValue(cacheName.avatarName, out filePath))
                //        {
                //            int index = random.Next(dictAvatarFemalePath.Count);
                //            KeyValuePair<string, string> pair = dictAvatarFemalePath.ElementAt(index);
                //            filePath = pair.Value;
                //        }
                //    }
                //    else
                //    {
                //        int index = random.Next(dictAvatarFemalePath.Count);
                //        KeyValuePair<string, string> pair = dictAvatarFemalePath.ElementAt(index);
                //        filePath = pair.Value;
                //    }
                //}


                if (string.IsNullOrEmpty(filePath))
                {
                    int index = random.Next(dictAvatarFemalePath.Count);
                    KeyValuePair<string, string> pair = dictAvatarFemalePath.ElementAt(index);
                    filePath = pair.Value;
                }
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);

                return path;

                if (string.IsNullOrEmpty(filePathSmall))
                {
                    int index = random.Next(dictAvatarFemalePath.Count);
                    KeyValuePair<string, string> pair = dictAvatarFemalePath.ElementAt(index);
                    filePathSmall = pair.Value;
                }

                path2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePathSmall);
                path3 = path2.Insert(path2.Length - 5, "temp");
                join2Image(path2, path, path3);
            }



            //path = @"C:\Users\2014\Documents\project\fb\fb_reg_2\fb_reg\fb_reg\bin\Debug\img\avatar_vn\Nu\2\FB_IMG_1610238698299.jpg";

            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                return "";
            }
            string ppp = changeImage(path3);
            //if (forceUs || language == Constant.LANGUAGE_US)
            //{
            //    File.Delete(path);
            //}
            File.Delete(path3);
            return ppp;
        }
        public static string RandomCover()
        {
            Random n = new Random();
            string[] coverFiles = Directory.GetFiles(@"img\CoVer\");


            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, coverFiles[n.Next(0, coverFiles.Length)]);
            return changeImage(path);

        }
        public static string DownloadRandomImage()
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFile("https://source.unsplash.com/user/c_v_r/100x100", "image.png");
            }
            return "";
        }

        public static byte[] GetBytesFromImage(Image img)
        {
            if (img == null) return null;
            byte[] result;
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, img.RawFormat);
                result = stream.GetBuffer();
            }
            return result;
        }

        public static Image GetImageFromBytes(byte[] bytes)
        {
            if ((bytes == null) || (bytes.Length == 0)) return null;
            Image result;
            using (MemoryStream stream = new MemoryStream(bytes))
                result = Image.FromStream(stream);
            return result;
        }
        public static string changeImage(string stPhotoPath)
        {
            Random random = new Random();
            int rannnnn = random.Next(-40, 40);
            Console.WriteLine("randdd =" + rannnnn);
            float value1 = rannnnn * 0.01f;
            rannnnn = random.Next(-30, 30);
            rannnnn = rannnnn + random.Next(1, 50);
            float value2 = rannnnn * 0.001f;
            rannnnn = random.Next(-35, 35);
            rannnnn = rannnnn + random.Next(1, 5);
            float value3 = rannnnn * 0.01f;

            float[][] colorMatrixElements = {
            new float[] {1,0,0,0,0},
            new float[] {0,1,0,0,0},
            new float[] {0,0,1,0,0},
            new float[] {0,0,0,1,0},
            new float[] {0,value2,0,0,1}
            };
            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

            byte[] imageBytes = File.ReadAllBytes(stPhotoPath);

            using (var ms = new MemoryStream(imageBytes))
            {
                var imgPhoto = Image.FromStream(ms);
                ImageAttributes imageAttributes = new ImageAttributes();
                //imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                Graphics _g = default(Graphics);
                Bitmap bm_dest = new Bitmap(Convert.ToInt32(imgPhoto.Width), Convert.ToInt32(imgPhoto.Height));
                _g = Graphics.FromImage(bm_dest);
                _g.DrawImage(imgPhoto, new Rectangle(0, 0, bm_dest.Width + 1, bm_dest.Height + 1), 0, 0, bm_dest.Width + 1, bm_dest.Height + 1, GraphicsUnit.Pixel, imageAttributes);
                //_g.DrawImage(imgPhoto, new Rectangle(0, 0, bm_dest.Width + 1, bm_dest.Height + 1), 0, 0, bm_dest.Width + 1, bm_dest.Height + 1, GraphicsUnit.Pixel);
                imgPhoto = (Image)bm_dest;
                int sourceWidth = imgPhoto.Width;
                int sourceHeight = imgPhoto.Height;

                Image image = imgPhoto;

                string path = stPhotoPath.Insert(stPhotoPath.Length - 5, "temp");

                Bitmap bmPhoto = (Bitmap)imgPhoto;
                string watermarkText = bestwishList.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                //Read the File into a Bitmap.
                using (Graphics grp = Graphics.FromImage(bmPhoto))
                {
                    //Set the Color of the Watermark text.
                    Random rnd = new Random();
                    System.Drawing.Color randomColor = System.Drawing.Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                    Brush brush = new SolidBrush(randomColor);
                    int randomHeigh = rnd.Next(4, 10);
                    int size = (int)((bmPhoto.Height * randomHeigh) / 100);
                    //Set the Font and its size.
                    System.Drawing.Font font = new System.Drawing.Font(FontNames.ToList().OrderBy(x => Guid.NewGuid()).FirstOrDefault(), size, System.Drawing.FontStyle.Bold, GraphicsUnit.Pixel);

                    //Determine the size of the Watermark text.
                    SizeF textSize = new SizeF();

                    for (int i = 0; i < 15; i++)
                    {
                        textSize = grp.MeasureString(watermarkText, font);

                        if (textSize.Width > bmPhoto.Width)
                        {
                            watermarkText = bestwishList.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                        }
                        else
                        {
                            break;
                        }
                    }


                    //Position the text and draw it on the image.
                    int space = 0;
                    if (bmPhoto.Width < bmPhoto.Height)
                    {
                        space = (int)((bmPhoto.Height - bmPhoto.Width) / 2);
                    }
                    int xText = (int)((bmPhoto.Width - textSize.Width) / 2);

                    int yText = bmPhoto.Height - space - (2 * (int)textSize.Height);
                    Point position = new Point(xText, yText);


                    if (!ghiChuTrenAvatar)
                    {
                        watermarkText = "";
                    }


                    grp.DrawString(watermarkText, font, brush, position);
                    watermarkText = bestwishList.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                    if (!ghiChuTrenAvatar)
                    {
                        watermarkText = "";
                    }
                    for (int i = 0; i < 15; i++)
                    {
                        textSize = grp.MeasureString(watermarkText, font);

                        if (textSize.Width > bmPhoto.Width)
                        {
                            watermarkText = bestwishList.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                        }
                        else
                        {
                            break;
                        }
                    }
                    xText = (int)((bmPhoto.Width - textSize.Width) / 2);
                    position = new Point(xText, yText + font.Height);
                    grp.DrawString(watermarkText, font, brush, position);
                    bmPhoto.Save(path);
                }

                //using (Watermarker watermarker = new Watermarker(path))
                //{
                //    // Đặt Phông chữ Văn bản và Hình chìm mờ
                //    GroupDocs.Watermark.Watermarks.Font font = new GroupDocs.Watermark.Watermarks.Font("Arial", 30, GroupDocs.Watermark.Watermarks.FontStyle.Bold | GroupDocs.Watermark.Watermarks.FontStyle.Italic);
                //    TextWatermark watermark = new TextWatermark("GroupDocs", font);

                //    // Đặt thuộc tính hình mờ
                //    watermark.ForegroundColor = GroupDocs.Watermark.Watermarks.Color.Black;
                //    watermark.TextAlignment = TextAlignment.Right;
                //    watermark.X = 70;
                //    watermark.Y = 70;
                //    watermark.RotateAngle = -30;
                //    watermark.Opacity = 0.4;
                //    // hình mờ.BackgroundColor = Color.Blue;

                //    // Thêm hình mờ được định cấu hình vào Hình ảnh JPG
                //    watermarker.Add(watermark);
                //    path = path.Insert(path.Length - 5, "temp");
                //    watermarker.Save(path);
                //}
                return path;
            }
        }

        public static void join2Image(string pathImageAvatar, string pathImageSmall, string pathOut)
        {
            Image playbutton;
            try
            {
                playbutton = Image.FromFile(pathImageAvatar);
            }
            catch (Exception ex)
            {
                return;
            }

            Image frame;
            try
            {
                frame = Image.FromFile(pathImageSmall);
            }
            catch (Exception ex)
            {
                return;
            }
            int x = (int)(frame.Width / 10);
            int y = (int)(frame.Height / 10);
            int hei = (int)(frame.Height / 2);
            using (frame)
            {
                using (var bitmap = new Bitmap(frame.Width, frame.Height))
                {
                    using (var canvas = Graphics.FromImage(bitmap))
                    {
                        canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        canvas.DrawImage(frame,
                                         new Rectangle(0,
                                                       0,
                                                       frame.Width,
                                                       frame.Height),
                                         new Rectangle(0,
                                                       0,
                                                       frame.Width,
                                                       frame.Height),
                                         GraphicsUnit.Pixel);
                        canvas.DrawImage(playbutton,
                                         new Rectangle(0,
                                                       hei,
                                                       x,
                                                       y),
                                         new Rectangle(0,
                                                       0,
                                                       playbutton.Width,
                                                       playbutton.Height),
                                         GraphicsUnit.Pixel);
                        canvas.Save();
                    }
                    try
                    {
                        bitmap.Save(pathOut);
                    }
                    catch (Exception ex) { }
                }
            }
        }

        public static void join2Image1_1(string pathImageAvatar, string pathImageSmall, string pathOut)
        {
            Image playbutton;
            try
            {
                playbutton = Image.FromFile(pathImageAvatar);
            }
            catch (Exception ex)
            {
                return;
            }

            Image frame;
            try
            {
                frame = Image.FromFile(pathImageSmall);
            }
            catch (Exception ex)
            {
                return;
            }
            int x = (int)(frame.Width);
            int y = (int)(frame.Height);
            int hei = (int)(frame.Height);
            using (frame)
            {
                using (var bitmap = new Bitmap(frame.Width * 2, frame.Height))
                {
                    using (var canvas = Graphics.FromImage(bitmap))
                    {
                        canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        canvas.DrawImage(frame,
                                         new Rectangle(0,
                                                       0,
                                                       frame.Width,
                                                       frame.Height),
                                         new Rectangle(0,
                                                       0,
                                                       frame.Width,
                                                       frame.Height),
                                         GraphicsUnit.Pixel);


                        canvas.DrawImage(playbutton,
                             frame.Width,
                             0);



                        //canvas.DrawImage(playbutton,
                        //                 new Rectangle(frame.Width,
                        //                               0,
                        //                               x,
                        //                               y),
                        //                 new Rectangle(0,
                        //                               0,
                        //                               playbutton.Width,
                        //                               playbutton.Height),
                        //                 GraphicsUnit.Pixel);
                        canvas.Save();
                    }
                    try
                    {
                        bitmap.Save(pathOut);
                    }
                    catch (Exception ex) { }
                }
            }
        }

        public static string[] FontNames =
         {
            "Times New Roman",
            "Courier New",
            "Ink Free",
            "Microsoft Yi Baiti",
            "MS Gothic",
            "MV Boli",
            "MingLiU_HKSCS-ExtB",
            "Nirmala UI"
        };
        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static void InputTextMicer(string deviceID, string text)
        {
            string ss = Device.StartMicerService(deviceID);
            Console.WriteLine("ss:" + ss);
            ss = Device.CopyClipboardDevice(deviceID, text);

            if (ss.Contains("extras") && !ss.Contains("Error"))
            {
                Console.WriteLine("ssss:" + ss);
                Device.Paste(deviceID);
            }
            else
            {
                Utility.InputText(deviceID, text, false);
            }
        }
        public static bool StoreInfo(bool isServer, OrderObject order, DeviceObject device, string password, string Hotmail, string qrCode,
            string gender, int yearOld, string isVerified, string status = "checking")
        {
            //var watch = Stopwatch.StartNew();
            order.isSuccess = true;
            string deviceID = device.deviceId;
            string cookies = FbUtil.GetCookieFromPhone(deviceID);
            if ((order.isReverify || order.reupFullInfoAcc) && cookies.Length < 178)
            {
                cookies = order.account.cookie + "|" + order.account.token;
            }
            string uid;
            bool needBackup = true;
            if (!string.IsNullOrEmpty(order.uid))
            {
                uid = order.uid;
            }
            else
            {
                uid = FbUtil.GetUidFromCookie(cookies);
            }

            if (string.IsNullOrEmpty(uid))
            {
                return false;
            }
            if (string.IsNullOrEmpty(Hotmail) || Hotmail == Constant.FAIL)
            {
                Hotmail = Constant.FAIL + "|" + Constant.FAIL;
            }

            if (order.hasAddFriend)
            {
                if (!string.IsNullOrEmpty(order.accType))
                {
                    order.accType = order.accType.Replace("_friend", "");
                }
                order.accType = order.accType + "_friend";
            }

            string rawData = uid + "|" + password
                + "|" + cookies + "|" + qrCode + "|" + Hotmail + "|" + gender + "|" + yearOld;

            string hasAvatar;
            string has2Fa;
            string emailType = "";

            string fileName = "FileCLone/";
            if (order.checkAccHasAvatar)
            {
                fileName = fileName + "avatar";
                hasAvatar = Constant.TRUE;
            }
            else
            {
                if (isVerified != Constant.TRUE && !order.nvrUpAvatar)
                {
                    fileName = fileName + "NoAvatar";
                    hasAvatar = Constant.FALSE;
                }
                else
                {
                    // Recheck avatar
                    if (order.hasAvatar)
                    {
                        Device.GotoFbProfileEdit(deviceID);
                        Thread.Sleep(3000);
                        if (CheckTextExist(deviceID, "nodeindex0textchỉnhsửaresourceidclassandroidwidgettextviewpackagecomfacebookkatanacontentdesccheckablefalsecheckedfalseclickablefalseenabledtruefocusablefalsefocusedfalsescrollablefalselongclickablefalsepasswordfalseselectedfalseboundsxxx1115,3", 2)) // đã có avatar rồi
                        {
                            fileName = fileName + "avatar";
                            hasAvatar = Constant.TRUE;
                        }
                        else
                        {
                            fileName = fileName + "NoAvatar";
                            hasAvatar = Constant.FALSE;
                        }
                    }
                    else
                    {
                        fileName = fileName + "NoAvatar";
                        hasAvatar = Constant.FALSE;
                    }
                }
            }

            fileName = fileName + "_" + order.language;

            if (!string.IsNullOrWhiteSpace(qrCode))
            {
                fileName = fileName + "_2fa";
                has2Fa = Constant.TRUE;
            }
            else
            {
                fileName = fileName + "_no2fa";
                has2Fa = Constant.FALSE;
            }
            try
            {
                string mailPass = Hotmail.Split('|')[1];

                if (mailPass == Constant.TEMPMAIL)
                {
                    emailType = Constant.TEMPMAIL;
                }
                else if (order.currentMail.password == Constant.VERI_BY_PHONE)
                {
                    emailType = Constant.VERI_BY_PHONE;
                }
                else if (mailPass == Constant.GMAIL_SELL_GMAIL)
                {
                    emailType = Constant.GMAIL_SELL_GMAIL;
                }
                else
                {
                    emailType = Constant.HOTMAIL;
                }

                fileName = fileName + "_" + emailType;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            bool verified;
            if (isVerified == Constant.TRUE)
            {
                fileName = fileName + "_veri";
                verified = true;
            }
            else
            {
                verified = false;
                fileName = fileName + "_Noveri";
            }


            if (order.checkAccHasCover)
            {
                status = status + "|cover";
            }
            string data = rawData + "|" + hasAvatar + "|" + has2Fa
                + "|" + order.language + "|" + emailType + "|" + isVerified
                + "|" + Environment.MachineName + "|" + deviceID;

            if (hasAvatar == Constant.TRUE && has2Fa == Constant.TRUE || order.doitenAcc || isVerified != Constant.TRUE)
            {
                needBackup = false;
            }
            needBackup = true;
            if (needBackup)
            {
                FbUtil.PullBackupFbNew(uid, deviceID);
                //Thread.Sleep(1000);
                //ZipFile.CreateFromDirectory("Authentication/" + uid, "Authentication/" + uid + ".zip");
                //Thread.Sleep(3000);

                //// push to server
                //if (order.NAM_SERVER)
                //{
                //    NamServer.UploadFileAuth("Authentication/" + uid + ".zip", uid);
                //} else
                //{
                //    ServerApi.UploadAuthAcc("Authentication/" + uid + ".zip", uid);
                //}

                //Thread.Sleep(3000);

                Thread.Sleep(1000);
              
                // push to server
                if (order.NAM_SERVER)
                {
                    NamServer.UploadFileAuth("Authentication/" + uid + ".zip", uid);
                }
                else
                {
                    ServerApi.UploadAuthAcc(Application.StartupPath + "\\Authentication\\" + uid + ".tar.gz", uid);
                }

                Thread.Sleep(3000);
                try
                {
                    
                    File.Delete("Authentication/" + uid + ".tar.gz");
                }
                catch (IOException ex)
                {
                    Console.WriteLine("ex:" + ex.Message);
                }
            }

            if (isServer)
            {
                if (order.veriByPhone || order.veriDirectByPhone)
                {
                    if (!string.IsNullOrEmpty(order.phoneT.source))
                    {
                        status = order.phoneT.phone;
                    }
                }

                if (!order.NAM_SERVER)
                {
                    if (!ServerApi.PostData(isServer, data, status, order.accType))
                    {
                        bool checkOk = GoogleSheet.WriteAccount(data, fileName.Substring(10));
                        if (!checkOk)
                        {
                            DateTime dateTime = DateTime.UtcNow.Date;
                            using (StreamWriter HDD = new StreamWriter(fileName + "_Missing_" + dateTime.ToString("dd/MM/yyyy") + ".txt", true))
                            {
                                HDD.WriteLine(data);
                                HDD.Close();
                            }
                        }
                        return false;
                    }
                    ServerApi.DeleteAccWait2Veri(uid);
                }
                
                
            }
            

            try
            {
                
                Account acc = new Account();
                acc.uid = uid;
               
                acc.data = data;
                acc.pass = password;
                if (hasAvatar == Constant.TRUE)
                {
                    acc.hasAvatar = true;
                }
                order.source = "HUNG";
                if (!order.NAM_SERVER)
                {
                    acc.pass = "";
                    
                    acc.data = acc.data.Replace(password, "");
                } else
                {
                    order.source = "NAM";
                }
                acc.qrCode = qrCode;
                
                acc.email = Hotmail.Split('|')[0];

                acc.emailPass = Hotmail.Split('|')[1];
                if (order.currentMail != null)
                {
                    acc.emailType = order.currentMail.type;
                }
                
                acc.gender = gender;
                acc.language = order.language;
                acc.pcName = Environment.MachineName;
                acc.verified = verified;

                string fbVersion = Device.GetVersionFB(deviceID);
                string fbLiteVersion = Device.GetVersionFBLite(deviceID);
                string fbBusinessVersion = Device.GetVersionFBBusiness(deviceID);
                order.versionFb = fbVersion + "|" + fbLiteVersion + "|" + fbBusinessVersion;
                //NamServer.PostData(acc, order, device);
            } catch(Exception eee)
            {

            }
            
            
            

            //watch.Stop();
            //long second = watch.ElapsedMilliseconds / 1000;
            //Console.WriteLine($"---------------------Store Time: {second} s");
            return true;
        }
        public static bool storeAccWithThread(bool isServer, OrderObject order, DeviceObject device, string password, string Hotmail, string qrCode,
            string gender, int yearOld, string isVerified, string status)
        {
            if (!order.set2FaSuccess)
            {
                qrCode = "";
            }
            return StoreInfo(isServer, order, device, password, Hotmail, qrCode,
                 gender, yearOld, isVerified, status);
        }

        public static string getPublicIPThread(string deviceID)
        {
            string ip = "";
            try
            {
                var op = new MyOperation();
                var handler = new OperationHandler(op);

                Console.WriteLine("Starting with timeout of 10 seconds, 3 times");
                //ip = handler.StartWithTimeoutIP(35 * 1000, deviceID);
            }
            catch (Exception ex)
            {
                Console.WriteLine("store exception:" + ex.Message);

            }
            Console.WriteLine("currentip -----" + ip);
            return ip;
        }
        public static void StoreErrorName(string name)
        {

            //using (StreamWriter HDD = new StreamWriter("FileClone/ErrorName.txt", true))
            //{
            //    HDD.WriteLine(name);
            //    HDD.Close();
            //}
        }

        public static void StorePhoneUsed(string phone)
        {

            using (StreamWriter HDD = new StreamWriter("FileClone/PhoneInUsed.txt", true))
            {
                HDD.WriteLine(phone);
                HDD.Close();
            }
        }

        public static bool CheckFacebookOpenByImage(string deviceID)
        {
            for (int i = 0; i < 20; i++)
            {
                if (CheckImageExist(deviceID, checkFacebookOpenImage))
                {
                    return true;
                }
                if (i == 4)
                {
                    if (CheckTextExist(deviceID, "phản hồi", 1))
                    {
                        return false;
                    }
                }
            }
            return false;
        }
        public static bool CheckVietnamese(string deviceID)
        {
            for (int i = 0; i < 3; i++)
            {
                Device.TapByPercent(deviceID, 51.2, 87.4);
                Device.TapByPercent(deviceID, 51.2, 90.9);
                Device.TapByPercent(deviceID, 48.5, 89.6);
                Device.TapByPercent(deviceID, 50.7, 80.6);
                Device.TapByPercent(deviceID, 49.4, 87.3);
                string uiXml = GetUIXml(deviceID);

                if (CheckTextExist(deviceID, "phản hồi", 1, uiXml))
                {
                    return false;
                }
                if (CheckTextExist(deviceID, "thiếtlậptiếngviệt", 1, uiXml))
                {
                    // Wait
                    for (int k = 1; k < 10; i++)
                    {
                        string xml = GetUIXml(deviceID);
                        if (!CheckTextExist(deviceID, "thiếtlậptiếngviệt", 1, xml))
                        {
                            if (CheckTextExist(deviceID, Language.Next(), 1, xml))
                            {
                                return true;
                            }
                            if (CheckTextExist(deviceID, "next", 1, xml)) // tiếng anh
                            {
                                return false;
                            }
                            break;
                        }
                    }
                }
                if (CheckTextExist(deviceID, Language.Next(), 1, uiXml))
                {
                    return true;
                }
                if (CheckTextExist(deviceID, "next", 1, uiXml)) // tiếng anh
                {
                    return false;
                }
            }
            return false;
        }
        public static bool CheckFacebookOpen(string deviceID, bool regNormal)
        {

            for (int i = 0; i < 9; i++)
            {
                Device.TapByPercent(deviceID, 71.2, 87.4);
                Device.TapByPercent(deviceID, 71.2, 90.9);
                Device.TapByPercent(deviceID, 78.5, 89.6);
                Device.TapByPercent(deviceID, 70.7, 80.6);
                Device.TapByPercent(deviceID, 79.4, 87.3);
                string uiXml = GetUIXml(deviceID);
                if (WaitAndTapXML(deviceID, 1, "bắtđầucheckable", uiXml)
                    || WaitAndTapXML(deviceID, 1, "đồngý&amp;tiếptụccheckable", uiXml))
                {

                    Device.ClearCache(deviceID, Constant.FACEBOOK_PACKAGE);
                    if (regNormal)
                    {
                        return false;
                    }
                    return false;
                }
                if (CheckTextExist(deviceID, "phản hồi", 1, uiXml))
                {
                    return false;
                }
                if (CheckTextExist(deviceID, "thiếtlậptiếngviệt", 1, uiXml))
                {
                    // Wait
                    for (int k = 1; k < 10; i++)
                    {
                        if (!CheckTextExist(deviceID, "thiếtlậptiếngviệt"))
                        {
                            break;
                        }
                    }
                }
                if (CheckTextExist(deviceID, Language.Next(), 1, uiXml))
                {
                    if (!CheckImageExist(deviceID, NEXT_CHECK_FB))
                    {
                        return false;
                    }
                    return true;
                }
                if (CheckTextExist(deviceID, "next", 1, uiXml)) // tiếng anh
                {
                    return false;
                }
                if (i >= 2)
                {
                    if (WaitAndTapXML(deviceID, 1, "bắtđầucheckable", uiXml))
                    {
                        Device.ClearCache(deviceID, Constant.FACEBOOK_PACKAGE);
                        if (regNormal)
                        {
                            return false;
                        }
                        return false;
                    }
                    if (CheckImageExist(deviceID, TAO_TAI_KHOAN_MOI_2))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 58.6, 65.9);
                        return true;
                    }
                    if (CheckImageExist(deviceID, TAO_TAI_KHOAN_MOI))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 56.5, 55.8);
                        return true;
                    }
                }
            }
            if (CheckImageExist(deviceID, TAO_TAI_KHOAN_MOI_2))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 58.6, 65.9);
                Thread.Sleep(2000);
                if (!CheckImageExist(deviceID, NEXT_CHECK_FB))
                {
                    return false;
                }
                return true;
            }
            if (CheckImageExist(deviceID, TAO_TAI_KHOAN_MOI))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 56.5, 55.8);

                Thread.Sleep(2000);
                if (!CheckImageExist(deviceID, NEXT_CHECK_FB))
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public static bool CheckFacebookNormalOpen(string deviceID, bool regNormal)
        {

            for (int i = 0; i < 9; i++)
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 68.3, 72.6);
                Device.TapByPercent(deviceID, 71.2, 87.4);
                Device.TapByPercent(deviceID, 71.2, 90.9);
                Device.TapByPercent(deviceID, 78.5, 89.6);
                Device.TapByPercent(deviceID, 70.7, 80.6);
                Device.TapByPercent(deviceID, 79.4, 87.3);
                string uiXml = GetUIXml(deviceID);
                if (WaitAndTapXML(deviceID, 1, "bắtđầucheckable", uiXml)
                    || WaitAndTapXML(deviceID, 1, "đồngý&amp;tiếptụccheckable", uiXml))
                {
                    return true;
                    Device.ClearCache(deviceID, Constant.FACEBOOK_PACKAGE);
                    if (regNormal)
                    {
                        return false;
                    }
                    return false;
                }
                if (CheckTextExist(deviceID, "phản hồi", 1, uiXml))
                {
                    return false;
                }
                if (CheckTextExist(deviceID, "thiếtlậptiếngviệt", 1, uiXml))
                {
                    // Wait
                    for (int k = 1; k < 10; i++)
                    {
                        if (!CheckTextExist(deviceID, "thiếtlậptiếngviệt"))
                        {
                            break;
                        }
                    }
                }
                if (CheckTextExist(deviceID, Language.Next(), 1, uiXml))
                {
                    if (!CheckImageExist(deviceID, NEXT_CHECK_FB))
                    {
                        return false;
                    }
                    return true;
                }
                if (CheckTextExist(deviceID, "next", 1, uiXml)) // tiếng anh
                {
                    return false;
                }
                if (i >= 2)
                {
                    if (WaitAndTapXML(deviceID, 1, "bắtđầucheckable", uiXml))
                    {
                        return true;
                        Device.ClearCache(deviceID, Constant.FACEBOOK_PACKAGE);
                        if (regNormal)
                        {
                            return false;
                        }
                        return false;
                    }
                    if (CheckImageExist(deviceID, TAO_TAI_KHOAN_MOI_2))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 58.6, 65.9);
                        return true;
                    }
                    if (CheckImageExist(deviceID, TAO_TAI_KHOAN_MOI))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 56.5, 55.8);
                        return true;
                    }
                }
            }
            if (CheckImageExist(deviceID, TAO_TAI_KHOAN_MOI_2))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 58.6, 65.9);
                Thread.Sleep(2000);
                if (!CheckImageExist(deviceID, NEXT_CHECK_FB))
                {
                    return false;
                }
                return true;
            }
            if (CheckImageExist(deviceID, TAO_TAI_KHOAN_MOI))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 56.5, 55.8);

                Thread.Sleep(2000);
                if (!CheckImageExist(deviceID, NEXT_CHECK_FB))
                {
                    return false;
                }
                return true;
            }
            return false;
        }
        public static Image Base64ToImage(string base64String)
        {
            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                Image image = Image.FromStream(ms, true);
                return image;
            }
        }
        public static Bitmap GetImageFromUid(string uid, string cookies)
        {
            if (string.IsNullOrEmpty(uid))
            {
                return null;
            }

            //var client = new RestClient("https://graph.facebook.com/" + uid + "/picture?access_token=6628568379|c1e620fa708a1d5696fb991c1bde5662");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("authority", "graph.facebook.com");
            //request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            //request.AddHeader("accept-language", "en-US;q=0.7");
            //request.AddHeader("cookie", "c_user=100004151984079");
            //client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.5060.66 Safari/537.36";
            //IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);

            //Image temp = Base64ToImage(response.Content);

            byte[] bytes = new RequestXNet("", "", "", 0).GetBytes("https://graph.facebook.com/" + uid + "/picture?access_token=6628568379|c1e620fa708a1d5696fb991c1bde5662");
            MemoryStream memoryStream = new MemoryStream();
            memoryStream.Write(bytes, 0, Convert.ToInt32(bytes.Length));
            Bitmap result = new Bitmap(memoryStream, false);
            memoryStream.Dispose();




            

            return result;
        }

        public static string CheckAvatarFromUid(string uid, string cookies)
        {
            try
            {
                if (string.IsNullOrEmpty(uid))
                {
                    return "error";
                }
                string checkAvata = "true";
                
                Bitmap avatar = GetImageFromUid(uid, cookies);
                if (avatar == null)
                {
                    return "error";
                }
                string imageFolder = "img/";
                
                Bitmap avatacheck = (Bitmap)Image.FromFile(imageFolder + "Nam.png");
                Bitmap avatacheck3 = (Bitmap)Image.FromFile(imageFolder + "Nam2.png");
                Bitmap avatacheck1 = (Bitmap)Image.FromFile(imageFolder + "Nu.png");
                Bitmap avatacheck2 = (Bitmap)Image.FromFile(imageFolder + "Nu2.png");

                if (Device.FindOutPoint(avatar, avatacheck) != null)
                {
                    return "false";
                }

                if (Device.FindOutPoint(avatar, avatacheck1) != null)
                {
                    return "false";
                }
                if (Device.FindOutPoint(avatar, avatacheck2) != null)
                {
                    return "false";
                }
                if (Device.FindOutPoint(avatar, avatacheck3) != null)
                {
                    return "false";
                }

                return checkAvata;
            }
            catch (Exception ex)
            {
                Console.WriteLine("check avatar:" + ex.Message);
                return "error";
            }
        }

        
        public static Bitmap LoadBitmapUnlocked(string file_name)
        {
            using (Bitmap bm = new Bitmap(file_name))
            {
                return new Bitmap(bm);
            }
        }
        
        public static string RandomNumberString(int length)
        {
            Random random = new Random();
            string number = "";
            int i;
            for (i = 0; i < length; i++)
            {
                number += random.Next(0, 9).ToString();
            }
            return number;
        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomStringNum(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string GeneratePhoneNumber(
            bool isServer, string prefixText, bool isRandom, bool usePhoneLocal, bool isUsPhone, string network = "")
        {
            string[] listPre = prefixText.Split(',');
            Random ran = new Random();
            int index = ran.Next(0, listPre.Length - 1);
            string prefix = listPre[index];
            if (usePhoneLocal)
            {
                string phoneNumber = FileUtil.GetAndDeleteLine("phonefull.txt");
                if (!string.IsNullOrEmpty(phoneNumber))
                {
                    return  phoneNumber.Replace(" ", "");
                }
            }
            
           
            // Get phone from server
            string phone = ServerApi.GetPhones(isServer, network);
         

            if (string.IsNullOrEmpty(phone) || phone == Constant.FAIL)
            {
                
                string standardNetwork = StandardNetwork(network);
                return GetRandomPrefix(standardNetwork) + RandomNumberString(7);
            }

            return "+84" + phone.Replace("+84", "0").Remove(0, 1);
        }

        public static string GeneratePhoneNumberDauso( string prefixText)
        {
            string[] listPre = prefixText.Split(',');
            Random ran = new Random();
            int index = ran.Next(0, listPre.Length - 1);
            string prefix = listPre[index];
           

            
            string phone = prefix + RandomNumberString(7);
            

            return "+84" + phone.Replace("+84", "0").Remove(0, 1);
        }

        public static string GeneratePhoneAmerica()
        {
            Random random = new Random();
            int ind = random.Next(0, Constant.PhonePrefixUS.Length - 1);
            string prefix = Constant.PhonePrefixUS[ind];
            return prefix + RandomNumberString(7);
        }
        public static string GeneratePhone12Prefix(string[] prefixes)
        {
            Random random = new Random();
            int ind = random.Next(0, prefixes.Length - 1);
            string prefix = prefixes[ind];
            return prefix + RandomNumberString(12 - prefix.Length);
        }

        public static string GeneratePhonePrefix(string[] prefixes)
        {
            Random random = new Random();
            int ind = random.Next(0, prefixes.Length - 1);
            string prefix = prefixes[ind];
            return prefix + RandomNumberString(10 - prefix.Length);
        }

        public static string GeneratePhoneCarryCode()
        {
            Random random = new Random();
            int ind = random.Next(0, CARRY_CODE.Length - 1);
            string prefix = CARRY_CODE[ind];
            if (string.IsNullOrEmpty(prefix))
            {
                ind = random.Next(0, CARRY_CODE.Length - 1);
                prefix = CARRY_CODE[ind];
            }
            return prefix + RandomNumberString(7);
        }
        public static string checkNetwork(string phone)
        {

            string result = "";
            string phoneStandard = standardPhone(phone);

            string prefix = phoneStandard.Substring(0, 3);
            if (string.IsNullOrEmpty(prefix))
            {
                return result;
            }
            if (Constant.PhonePrefixVETTEL.Contains(prefix))
            {
                result = Constant.VIETTEL;
            } else if (Constant.PhonePrefixVINA.Contains(prefix))
            {
                result = Constant.VINAPHONE;
            } else if (Constant.PhonePrefixMOBI.Contains(prefix))
            {
                result = Constant.MOBI;
            } else if (Constant.PhonePrefixVIETNAM.Contains(prefix))
            {
                result = Constant.VIETNAM_MOBILE;
            }

            return result;
        }
        public static string standardPhone(string phone)
        {
            string result = phone;
            if (string.IsNullOrEmpty(phone))
            {
                return result;
            }
            if (phone.StartsWith("+84"))
            {
                result = "0" + phone.Substring(3);
            }
            return result;
        }
        public static string GetRandomPrefix(string network)
        {
            Random random = new Random();
            if (network == Constant.VIETTEL)
            {
                int ind = random.Next(0, Constant.PhonePrefixVETTEL.Length - 1);
                return Constant.PhonePrefixVETTEL[ind];
            }
            else if (network == Constant.VINAPHONE)
            {
                int ind = random.Next(0, Constant.PhonePrefixVINA.Length - 1);
                return Constant.PhonePrefixVINA[ind];
            } else if (network == Constant.MOBI)
            {
                int ind = random.Next(0, Constant.PhonePrefixMOBI.Length - 1);
                return Constant.PhonePrefixMOBI[ind];
            } else if (network == Constant.VIETNAM_MOBILE)
            {
                int ind = random.Next(0, Constant.PhonePrefixVIETNAM.Length - 1);
                return Constant.PhonePrefixVIETNAM[ind];
            } else
            {
                int ind = random.Next(0, Constant.PhonePrefix.Length - 1);

                return Constant.PhonePrefix[ind];
            }
        }
        public static string StandardNetwork(string currentNetwork)
        {
            string network = "";
            if (currentNetwork == Constant.VN_MOBIPHONE
                || currentNetwork == Constant.MOBI)
            {
                network = Constant.MOBI;
            } else
            if (currentNetwork == Constant.VN_VINAPHONE
                || currentNetwork == Constant.VINAPHONE)
            {
                network = Constant.VINAPHONE;
            } else
            if (currentNetwork == Constant.VIETTEL_TELECOM
                || currentNetwork == Constant.VIETTEL)
            {
                network = Constant.VIETTEL;
            } else if (currentNetwork == Constant.VIETNAM_MOBILE)
            {
                network = Constant.VIETNAM_MOBILE;
            }
            return network;
        }
        public static void GetCookieTempMail()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string uri = "https://generator.email";
            //string uri = "https://forum.donanimhaber.com/service/v1/session/set?version=-1&securekey=123213&projectType=Forum&forumId=12";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = WebRequestMethods.Http.Get;
            request.AllowAutoRedirect = false;
            request.CookieContainer = new CookieContainer();
            request.KeepAlive = true;
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.0.5) Gecko/2008120122 Firefox/3.0.5";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            string oku = readStream.ReadToEnd();


            foreach (Cookie cook in response.Cookies)
            {
                Console.WriteLine("Domain: {0}, Name: {1}, value: {2}", cook.Domain, cook.Name, cook.Value);

            }
        }

        public static string GetPhoneCode(PhoneTextNow phoneT)
        {
            string code = Constant.FAIL;
            for (int i = 0; i < 50; i++)
            {
                code = Phone.GetConfirmCodeTextNow(phoneT);

                if (!string.IsNullOrEmpty(code))
                {
                    break;
                }
                
                Thread.Sleep(5000);
            }
            return code;
        }
        public static List<String> ReceiveMailByOauth(string user, string accessToken)
        {
            List<String> subjects = new List<string>();
            try
            {

                // use SSL IMAP + XOAUTH2
                MailServer oServer = new MailServer("outlook.office365.com", user, accessToken, true,
                    ServerAuthType.AuthXOAUTH2, ServerProtocol.Pop3);
                oServer.AuthType = ServerAuthType.AuthXOAUTH2;
                // Enable SSL/TLS connection, most modern email server require SSL/TLS by default
                oServer.SSLConnection = true;
                oServer.Port = 995;

                Console.WriteLine("Connecting server ...");

                MailClient oClient = new MailClient("TryIt");
                oClient.Connect(oServer);

                Console.WriteLine("Retreiving email list ...");
                MailInfo[] infos = oClient.GetMailInfos();
                Console.WriteLine("Total {0} email(s)", infos.Length);

                for (int i = 0; i < infos.Length; i++)
                {
                    MailInfo info = infos[i];

                    EAGetMail.Mail oMail = oClient.GetMail(info);

                    subjects.Add(oMail.Subject);

                    // Mark the email as deleted on server.
                    Console.WriteLine("Deleting ... {0}/{1}", i + 1, infos.Length);
                    oClient.Delete(info);
                }

                Console.WriteLine("Disconnecting ...");

                // Delete method just mark the email as deleted, 
                // Quit method expunge the emails from server permanently.
                oClient.Quit();

                Console.WriteLine("Completed!");
            }
            catch (Exception ep)
            {
                Console.WriteLine("Error: {0}", ep.Message);
            }
            return subjects;
        }
        public static string GetOtp2fa(string deviceID, OrderObject order, int time)
        {
            string code = "";

            try
            {
                for (int i = 0; i < time; i++)
                {
                    if (!string.IsNullOrEmpty(order.currentMail.refreshToken))
                    {
                        code = OutsideServer.GetOtp2faByOAuth2(order.currentMail);
                    }
                    else
                    {

                        List<string> subjects = GetAllSubjectMail(order.currentMail);

                        ////truy xuất nội dung từng mail
                        foreach (string mail in subjects)
                        {
                            Console.WriteLine("subject:" + mail);
                            code = FindCode2fa(mail);
                            if (code != Constant.FAIL)
                            {
                                break;
                            }
                        }
                    }


                    if (!string.IsNullOrEmpty(code) && code != Constant.FAIL)
                    {
                        break;
                    }
                    Thread.Sleep(3000);
                    if (forceStopGetOtp && i > 3)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Get code mail:" + order.currentMail.email + "|" + order.currentMail.password + " - " + ex.Message);
                return Constant.FAIL;
            }

            return code;
        }
        public static string GetOtp(string deviceID, string tempmailType, MailObject inMail, int time)
        {
            string code = Constant.FAIL;


            if (inMail.password == Constant.TEMP_MAIL || inMail.password == Constant.GMAIL_SELL_GMAIL)
            {
                if (tempmailType == Constant.TEMP_GENERATOR_EMAIL)
                {
                    return Mail.GetOTPGenerator(inMail.email, time);
                } else if (tempmailType == Constant.TEMP_TEMPMAIL_LOL)
                {
                    return Mail.GetCodeTempmailLol(inMail.token, time);
                } else if (tempmailType == Constant.GMAIL_SELL_GMAIL)
                {
                    return Mail.GetOtpSellGmail(inMail, time);
                }
                else if (tempmailType == Constant.GMAIL_SELL_GMAIL_SERVER)
                {
                    return Mail.GetOtpSellGmail(inMail, time);
                }
                else if (tempmailType == Constant.GMAIL_DICH_VU_GMAIL)
                {
                    return Mail.GetOtpDichVuGmail(inMail, time);
                }
                else if (tempmailType == Constant.GMAIL_DICH_VU_GMAIL2)
                {
                    return Mail.GetOtpDichVuGmail2(inMail, time);
                }
                else if (tempmailType == Constant.FAKE_MAIL)
                {
                    return Mail.GetOTPfakemailgenerator(inMail.email, time);
                } else if (tempmailType == Constant.TEMP_FAKE_EMAIL)
                {
                    return Mail.GetOTPEmailFake(inMail.email, time);
                } else if (tempmailType == Constant.MAIL_OTP)
                {
                    return Mail.GetOtpMailOtp(inMail.orderId);
                } else if (tempmailType == Constant.GMAIL_30_MIN)
                {
                    return Mail.GetOtpGmail30(inMail, time);
                }else if (tempmailType == Constant.GMAIL_OTP_GMAIL)
                {
                    return Mail.GetOtpGmailOtpGmail(inMail, time);
                }else if (tempmailType == Constant.GMAIL_SUPERTEAM)
                {
                    return Mail.GetOtpGmailSuperTeam(inMail, time);
                }
                else if (tempmailType == Constant.GMAIL_48h)
                {
                    return Mail.GetOtpGmail48h(inMail, time);
                }
                else if (tempmailType == Constant.TEMP_GENERATOR_1_SEC_EMAIL)
                {
                    return Mail.GetOtp1Sec(inMail);
                }
            }
            try
            {
                for (int i = 0; i < time; i++)
                {
                    if (!string.IsNullOrEmpty(inMail.refreshToken))
                    {
                        code = OutsideServer.GetOtpByOAuth2(inMail);
                    } else
                    {

                        List<string> subjects = GetAllSubjectMail(inMail);

                        ////truy xuất nội dung từng mail
                        foreach (string mail in subjects)
                        {
                            Console.WriteLine("subject:" + mail);
                            code = FindCodeInSubject(mail);
                            if (code != Constant.FAIL)
                            {
                                break;
                            }
                        }
                    }
                    

                    if (!string.IsNullOrEmpty(code) && code != Constant.FAIL)
                    {
                        break;
                    }
                    Thread.Sleep(3000);
                    if (forceStopGetOtp && i > 3)
                    {
                        break;
                    }
                }
                

                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;

                //inMail.mailRepository = new MailRepository("imap-mail.outlook.com", 993, true, inMail.email, inMail.password); // Re login

                //for (int i = 0; i < time; i ++)
                //{
                //    List<string> mmm = inMail.mailRepository. GetAllMailSubjects();
                //    ////truy xuất nội dung từng mail
                //    foreach (string mail in mmm)
                //    {
                //        Console.WriteLine("subject:" + mail);
                //        code = FindCodeInSubject(mail);
                //        if (code != Constant.FAIL)
                //        {
                //            break;
                //        }
                //    }

                //    if (!string.IsNullOrEmpty(code) && code != Constant.FAIL)
                //    {
                //        break;
                //    }
                //    Thread.Sleep(3000);
                //    if (forceStopGetOtp && i > 3)
                //    {
                //        break;
                //    }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine("Get code mail:" + inMail.email + "|" + inMail.password + " - " + ex.Message);
                return Constant.FAIL;
            }
            if (code != Constant.FAIL)
            {
                return code;
            }

            return code;
        }

        public static string GetCode2faInMail(MailObject inMail, int time)
        {
            string code = Constant.FAIL;

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;

                inMail.mailRepository = new MailRepository("imap-mail.outlook.com", 993, true, inMail.email, inMail.password); // Re login

                for (int i = 0; i < time; i++)
                {
                    List<string> mmm = inMail.mailRepository.GetAllMailSubjects();
                    ////truy xuất nội dung từng mail
                    foreach (string mail in mmm)
                    {
                        Console.WriteLine("subject:" + mail);
                        code = FindCode2fa(mail);
                        if (code != Constant.FAIL)
                        {
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(code) && code != Constant.FAIL)
                    {
                        break;
                    }
                    Thread.Sleep(5000);
                    if (forceStopGetOtp && i > 3)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Get code mail:" + inMail.email + "|" + inMail.password + " - " + ex.Message);
                return Constant.FAIL;
            }
            if (code != Constant.FAIL)
            {
                return code;
            }

            return code;
        }

        public static string FindCodeInSubject(string subject)
        {
            string code = Constant.FAIL;
            
            if (subject.Contains("mã xác nhận"))
            {
                var Item = subject.Split(' ');
                code = Item[0];
                if (string.IsNullOrEmpty(code))
                {
                    return Constant.FAIL;
                }
                return code;
            }
            return code;
        }

        public static string FindCode2fa(string subject)
        {
            string otp = Constant.FAIL;

            if (subject.Contains("Mã bảo mật"))
            {
                otp = Regex.Match(subject, ">\\d{8}<").Value.Replace(" ", "").Replace(">", "").Replace("<", "");
                if (otp != "")
                {
                    return otp;
                }
                otp = Regex.Match(subject, ">\\d{8} is your Facebook confirmation").Value.Replace(" ", "").Replace(">", "").Replace("<", "");
                if (otp != "")
                {
                    return otp.Substring(0, 8);
                }
                otp = Regex.Match(subject, "d{8}\\s").Value.Replace(" ", "");
                if (otp != "")
                {
                    return otp;
                }
            }
            return otp;
        }

        public static string GetLocationText(string deviceID, string Tên, string XML = "")
        {
            if (string.IsNullOrEmpty(XML))
            {
                XML = GetUIXml(deviceID);
            }
            
            Tên = Tên.Replace(" ", "").ToLower().Replace("-","");

            string HaHa = Regex.Match(XML, Tên + "[a-z,A-Z,0-9,:,.,_]{0,}").ToString();
            string HaHaHa = Regex.Match(HaHa, "xxx[a-z,A-Z,0-9]{0,}").ToString();
            HaHaHa = HaHaHa.Replace("xxx", "");
            HaHaHa = HaHaHa.Replace("yyy", ",");
            return HaHaHa;
        }
        public static string GetLocationTextNew(string deviceID, string Tên, string XML = "")
        {
            if (string.IsNullOrEmpty(XML))
            {
                XML = GetUIxmlNew(deviceID);
            }

            string pattern = string.Format(@"text=\""[^\""]*{0}[^\""]*\""[^>]*bounds=\""\[(\d+),(\d+)]\[(\d+),(\d+)]", Tên);
            var match = Regex.Match(XML, pattern);

            if (match.Success)
            {
                return int.Parse(match.Groups[1].Value) + "," + int.Parse(match.Groups[2].Value) + "," + int.Parse(match.Groups[3].Value) + "," + int.Parse(match.Groups[4].Value);
            }
            return "";
        }

        public static string GetLocationTextUnsign(string deviceID, string text, string XML = "")
        {
            if (string.IsNullOrEmpty(XML))
            {
                XML = GetUIXml(deviceID);
                XML = ConvertToUnsign(XML);
            }

            text = text.Replace(" ", "").ToLower().Replace("-", "");
            text = ConvertToUnsign(text);
            string HaHa = Regex.Match(XML, text + "[a-z,A-Z,0-9,:,.,_]{0,}").ToString();
            string HaHaHa = Regex.Match(HaHa, "xxx[a-z,A-Z,0-9]{0,}").ToString();
            HaHaHa = HaHaHa.Replace("xxx", "");
            HaHaHa = HaHaHa.Replace("yyy", ",");
            return HaHaHa;
        }

        public static void WriteLog(string form, string mess)
        {
            
            using (FileStream fileStream = new FileStream(Application.StartupPath + "\\data\\log.txt", FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine(string.Concat(new string[]
                    {
                        DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                        "  ",
                        form,
                        ": ",
                        mess
                    }));
                }
            }
            
        }
        public static string GetUIXml(string deviceID)
        {
            //string XML = Device.ExecuteCMD("adb -s " + deviceID + " shell uiautomator dump /dev/stdout");
            //string XML = Device.ExecuteCMD("adb -s " + deviceID + " shell uiautomator dump /dev/stdout");
           
            string XML = Device.ExecuteCMD("adb -s " + deviceID + " exec-out uiautomator dump /dev/tty");
            //Device.ExecuteCMD("adb -s " + deviceID + " shell uiautomator dump ");
            //string XML = Device.ExecuteCMD("adb -s " + deviceID + " shell cat /sdcard/window_dump.xml");
            XML = Decode_UTF8(XML);
            XML = XML.Replace(" ", "");
            XML = XML.Replace("=", "");
            XML = XML.Replace("\"", "");
            XML = XML.Replace("-", "");
            XML = XML.Replace("(", "");
            XML = XML.Replace(")", "");
            XML = XML.Replace("/", "");
            XML = XML.Replace("[", "xxx");
            XML = XML.Replace("]", "yyy");
            XML = XML.Replace(":", "");
            XML = XML.Replace(".", "");
            XML = XML.Replace("_", "").ToLower();

            return XML;
        }

        public static string GetUIxmlNew(string deviceID)
        {
            string XML = Device.ExecuteCMD("adb -s " + deviceID + " exec-out uiautomator dump /dev/tty");
            //Device.ExecuteCMD("adb -s " + deviceID + " shell uiautomator dump ");
            //string XML = Device.ExecuteCMD("adb -s " + deviceID + " shell cat /sdcard/window_dump.xml");
            XML = Decode_UTF8(XML);

            return XML.ToLower();
        }

        public static string GetRawUIXml(string deviceID)
        {
            string XML = Device.ExecuteCMD("adb -s " + deviceID + " shell uiautomator dump /dev/stdout");
            //Device.ExecuteCMD("adb -s " + deviceID + " shell uiautomator dump /sdcard/uidump.xml");
            //string XML = Device.ExecuteCMD("adb -s " + deviceID + " shell cat /sdcard/uidump.xml");
            XML = Decode_UTF8(XML);
            

            return XML;
        }
        public static string GetTextExist(string deviceID, string Tên)
        {
            string XML = GetUIXml(deviceID);
            Tên = Tên.Replace(" ", "").ToLower();

            MatchCollection matchList = Regex.Matches(XML, Tên + "[a-z,A-Z,0-9,:,.,_]{0,}");
            var list = matchList.Cast<Match>().Select(match => match.Value).ToList();
            string HaHa = Regex.Match(XML, Tên + "[a-z,A-Z,0-9,:,.,_]{0,}").ToString();
            
            return HaHa;
        }
        public static string GetLocationText2(string deviceID, string Tên)
        {
            
            string XML = GetUIXml(deviceID);
            Tên = Tên.Replace(" ", "").ToLower();


            MatchCollection matchList = Regex.Matches(XML, Tên + "[a-z,A-Z,0-9,:,.,_]{0,}");
            var list = matchList.Cast<Match>().Select(match => match.Value).ToList();
            string HaHa = "";
            if (list.Count > 1)
            {
                HaHa = list[1];
            }
            string HaHaHa = Regex.Match(HaHa, "xxx[a-z,A-Z,0-9]{0,}").ToString();
            HaHaHa = HaHaHa.Replace("xxx", "");
            HaHaHa = HaHaHa.Replace("yyy", ",");
            return HaHaHa;
        }
        public static string GetLocationText3(string deviceID, string Tên)
        {
            string XML = GetUIXml(deviceID);
            Tên = Tên.Replace(" ", "").ToLower();


            MatchCollection matchList = Regex.Matches(XML, Tên + "[a-z,A-Z,0-9,:,.,_]{0,}");
            var list = matchList.Cast<Match>().Select(match => match.Value).ToList();
            string HaHa = "";
            if (list.Count > 2)
            {
                Random r = new Random();
                int index = r.Next(2, 4);

                HaHa = list[index];
            }
            string HaHaHa = Regex.Match(HaHa, "xxx[a-z,A-Z,0-9]{0,}").ToString();
            HaHaHa = HaHaHa.Replace("xxx", "");
            HaHaHa = HaHaHa.Replace("yyy", ",");
            return HaHaHa;
        }

        
        public static string[] GetCordText(string deviceID, string text)
        {
            string location = GetLocationText(deviceID, text);

            if (string.IsNullOrWhiteSpace(location))
            {
                return null;
            }

            string[] ll = location.Split(',');
            if (ll.Length < 4)
            {
                return null;
            }
            
            return ll;
        }

        public static string[] GetCordTextUnsign(string deviceID, string text)
        {
            string location = GetLocationText(deviceID, text);

            if (string.IsNullOrWhiteSpace(location))
            {
                return null;
            }

            string[] ll = location.Split(',');
            if (ll.Length < 4)
            {
                return null;
            }

            return ll;
        }

        public static string CheckText(string deviceID, string text, string xml = "")
        {
            if (string.IsNullOrEmpty(xml))
            {
                xml = GetUIXml(deviceID);
            }
            
            text = text.Replace(" ", "").ToLower();

            //return Regex.Match(xml, Tên + "[a-z,A-Z,0-9,:,.,_]{0,}").ToString();
            if (xml.Contains(text))
            {
                return "true";
            }
            return "";
        }
        public static bool CheckTextExist(string deviceId, string[] text , string uixml = "")
        {
            if (text != null && text.Length > 0)
            {
                string xml;
                if (string.IsNullOrEmpty(uixml))
                {
                    xml = GetUIXml(deviceId);
                } else 
                {
                    xml = uixml;
                } 
                
                for (int i = 0; i < text.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(CheckText(deviceId, text[i], xml)))
                    {
                        return true;
                    }
                }
            }
            

            return false;
        }

        public static bool CheckTextExistTime(string deviceId, string[] text, int time, string uixml = "")
        {
            if (text != null && text.Length > 0)
            {
                for (int k = 0; k < time; k ++)
                {
                    string xml;
                    if (string.IsNullOrEmpty(uixml))
                    {
                        xml = GetUIXml(deviceId);
                    }
                    else
                    {
                        xml = uixml;
                    }
                    for (int i = 0; i < text.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(CheckText(deviceId, text[i], xml)))
                        {
                            return true;
                        }
                    }
                }
            }


            return false;

        }
        public static bool CheckTextExist(string deviceId, string text, int time = 1, string xml = "")
        {
            if (!string.IsNullOrEmpty(xml))
            {
                time = 1;
            }
            for (int i = 0; i < time; i ++)
            {
                if (!string.IsNullOrWhiteSpace(CheckText(deviceId, text, xml)))
                {
                    return true;
                }
            }
            
            return false;
        }

        
        public static bool DeleteFile(string path)
        {
            try
            {
                File.Delete(path);
                return true;
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public static bool CheckAndTap(string deviceId, string buttonText, string xml = "")
        {
            bool check = true;
            string location = GetLocationText(deviceId, buttonText, xml);

            if (string.IsNullOrWhiteSpace(location))
            {
                return false;
            }

            string[] ll = location.Split(',');
            if (ll.Length < 4)
            {
                return false;
            }
            double width = Int32.Parse(ll[2]) - Int32.Parse(ll[0]);
            double heigh = Int32.Parse(ll[3]) - Int32.Parse(ll[1]);
            Random ran = new Random();
            int ranX = ran.Next(5, 95);
            int ranY = ran.Next(5, 95);
            int x = Int32.Parse(ll[0]) + (int)(width * ranX/100) ;
            int y = Int32.Parse(ll[1]) + (int)(heigh * ranY/100);
  
            Device.TapRoot(deviceId, x, y);
            return check;
        }
        public static bool CheckAndTapNew(string deviceId, string buttonText, string xml = "")
        {
            bool check = true;
            string location = GetLocationTextNew(deviceId, buttonText, xml);

            if (string.IsNullOrWhiteSpace(location))
            {
                return false;
            }

            string[] ll = location.Split(',');
            if (ll.Length < 4)
            {
                return false;
            }
            double width = Int32.Parse(ll[2]) - Int32.Parse(ll[0]);
            double heigh = Int32.Parse(ll[3]) - Int32.Parse(ll[1]);
            Random ran = new Random();
            int ranX = ran.Next(5, 95);
            int ranY = ran.Next(5, 95);
            int x = Int32.Parse(ll[0]) + (int)(width * ranX / 100);
            int y = Int32.Parse(ll[1]) + (int)(heigh * ranY / 100);

            Device.TapRoot(deviceId, x, y);
            return check;
        }

        public static bool CheckAndTapUnsign(string deviceId, string buttonText, string xml = "")
        {
            bool check = true;
            string location = GetLocationTextUnsign(deviceId, buttonText, xml);

            if (string.IsNullOrWhiteSpace(location))
            {
                return false;
            }

            string[] ll = location.Split(',');
            if (ll.Length < 4)
            {
                return false;
            }
            double width = Int32.Parse(ll[2]) - Int32.Parse(ll[0]);
            double heigh = Int32.Parse(ll[3]) - Int32.Parse(ll[1]);
            int x = Int32.Parse(ll[0]) + (int)(width / 2);
            int y = Int32.Parse(ll[1]) + (int)(heigh / 2);
            Device.Tap(deviceId, x, y);
            return check;
        }

        public static bool CheckPointValid(Point p)
        {
            if (p.X == 0 && p.Y == 0)
            {
                return false;
            }
            return true;
        } 
        public static Point GetTapPoint(string deviceId, string buttonText, string xml)
        {
            Point p = new Point(0,0);
            string location = GetLocationTextUnsign(deviceId, buttonText, xml);

            if (string.IsNullOrWhiteSpace(location))
            {
                return p;
            }

            string[] ll = location.Split(',');
            if (ll.Length < 4)
            {
                return p;
            }
            double width = Int32.Parse(ll[2]) - Int32.Parse(ll[0]);
            double heigh = Int32.Parse(ll[3]) - Int32.Parse(ll[1]);
            int x = Int32.Parse(ll[0]) + (int)(width / 2);
            int y = Int32.Parse(ll[1]) + (int)(heigh / 2);
            p = new Point(x, y);
            return p;
        }
        public static bool Tap(string deviceId, string[] ll)
        {
            bool check = true;
           
            if (ll.Length < 4)
            {
                return false;
            }
            double width = Int32.Parse(ll[2]) - Int32.Parse(ll[0]);
            double heigh = Int32.Parse(ll[3]) - Int32.Parse(ll[1]);
            int x = Int32.Parse(ll[0]) + (int)(width / 2);
            int y = Int32.Parse(ll[1]) + (int)(heigh / 2);
            Device.Tap(deviceId, x, y);
            return check;
        }

        public static bool CheckAndTap2(string deviceId, string buttonText)
        {
            bool check = true;
            string location = GetLocationText2(deviceId, buttonText);

            if (string.IsNullOrWhiteSpace(location))
            {
                return false;
            }

            string[] ll = location.Split(',');
            if (ll.Length < 4)
            {
                return false;
            }
            double width = Int32.Parse(ll[2]) - Int32.Parse(ll[0]);
            double heigh = Int32.Parse(ll[3]) - Int32.Parse(ll[1]);
            int x = Int32.Parse(ll[0]) + (int)(width / 2);
            int y = Int32.Parse(ll[1]) + (int)(heigh / 2);
            Device.Tap(deviceId, x, y);
            return check;
        }
        public static bool CheckAndTap3(string deviceId, string buttonText)
        {
            bool check = true;
            string location = GetLocationText3(deviceId, buttonText);

            if (string.IsNullOrWhiteSpace(location))
            {
                return false;
            }

            string[] ll = location.Split(',');
            if (ll.Length < 4)
            {
                return false;
            }
            double width = Int32.Parse(ll[2]) - Int32.Parse(ll[0]);
            double heigh = Int32.Parse(ll[3]) - Int32.Parse(ll[1]);
            int x = Int32.Parse(ll[0]) + (int)(width / 2);
            int y = Int32.Parse(ll[1]) + (int)(heigh / 2);
            Device.Tap(deviceId, x, y);
            return check;
        }

        public static string GetFirtName(string language, string gender, bool option = false)
        {
            string firstname = "";
            
            
            if (language == Constant.LANGUAGE_US || option)
            {
                if (gender == Constant.MALE)
                {
                    string firstname1 = MaleName.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                    //string firstname2 = MaleName.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                    //firstname = firstname1 + " " + firstname2 ;
                   firstname =  firstname1;
                } else
                {
                    string firstname1 = FemaleName.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                    
                    firstname = firstname1;
                }
            } else
            {
                if (gender == Constant.MALE)
                {
                    firstname = Ten_Nam.OrderBy(x => Guid.NewGuid()).FirstOrDefault() ;
                }
                else
                {
                    firstname =Ten_Nu.OrderBy(x => Guid.NewGuid()).FirstOrDefault() ;
                }
            }
            return firstname;
        }

        public static string GetLastName(string language, string gender, bool name3word)
        {
            string middleName = GetMidleName(language, gender);
            if (language == Constant.LANGUAGE_US)
            {
                if (name3word)
                {
                    return Constant.lastNameUSArr.OrderBy(x => Guid.NewGuid()).FirstOrDefault() + " " + middleName;
                }
                else
                {
                    return Constant.lastNameUSArr.OrderBy(x => Guid.NewGuid()).FirstOrDefault();// + " " + middleName;
                }
                
            }
            else
            {
                if (name3word)
                {
                    return Constant.lastNameVietArr.OrderBy(x => Guid.NewGuid()).FirstOrDefault() + " " + middleName;
                }
                else
                {
                    return Constant.lastNameVietArr.OrderBy(x => Guid.NewGuid()).FirstOrDefault();// + " " + middleName;
                }
                
            }
        }

        public static string GetMidleName(string language, string gender, bool option = false)
        {
            Random r = new Random();

            List<string> midleName;

            if (language == Constant.LANGUAGE_US || option)
            {
                if (gender == Constant.MALE)
                {
                    midleName = Constant.middleNameMaleUSArr;
                } else
                {
                    midleName = Constant.middleNameFemaleUSArr;
                }
                 
            }
            else
            {
                midleName = Constant.lastNameVietArr;
            }

            string temp = midleName.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

            return Regex.Match(temp, "[\\w]+").ToString();
        }
       
        public static string GeneratePassword()
        {
            Random r = new Random();
            
            string lastname = Constant.lastNameVietArr.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

            string firstname = AllName.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

            string prePass = lastname  + firstname;
            int num = r.Next(2, 4);
            int num2 = r.Next(1, 2);
            string password =  prePass + RandomNumberString(num)+ RandomStringNum(num2);
            //string password = RandomString(8) + RandomNumberString(2);
         
            password = password.ToLower();
            password = ConvertToUnsign(password);
            //password = password.ToLower()
            //    .Replace("s", "")
            //    .Replace("f", "")
            //    .Replace("r", "")
            //    .Replace("x", "")
            //    .Replace("j", "")
            //    .Replace("aa", "")
            //    .Replace("ow", "")
            //    .Replace("aw", "")
            //    .Replace("dd", "");
            if (password.Length < 8)
            {
                password = password + RandomStringNum(3);
            }
            return password.Replace(" ","").Trim();
        }

        public static bool checkAvatar(string deviceID, Bitmap emptyAvatarMale, Bitmap emptyAvatarFemale)
        {
            if (FindImage(deviceID, emptyAvatarFemale, 2))
            {
                return false;
            }
            if (FindImage(deviceID, emptyAvatarMale, 2))
            {
                return false;
            }
            return true;
        }
        public static bool FindImage(string deviceID, Bitmap image, int time)
        {
            for (int i = 0; i < time; i++)
            {
                var screen = Device.ScreenShoot(deviceID); // chup man hinh
                var next = Device.FindOutPoint(screen, image);// so sanh hinh anh
                if (next != null)// kiem tra
                {
                     return true;
                }
            }
            return false;
        }
        public static bool FindImageAndTap(string deviceID, Bitmap image, int time)
        {
            for (int i = 0; i < time; i++)
            {
                var screen = Device.ScreenShoot(deviceID); // chup man hinh
                var next = Device.FindOutPoint(screen, image);// so sanh hinh anh
                if (next != null)// kiem tra
                {
                    try
                    {
                        Device.Tap(deviceID, next.Value.X + image.Width / 2, next.Value.Y + image.Height / 2);// click vào hinh anh
                        return true;
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(250);
                    }
                }
            }
            return false;
        }

        public static bool WaitXML(string deviceID, int time, string text)
        {
            for (int i = 0; i < time; i++)
            {

                if (Utility.CheckTextExist(deviceID, text))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CheckImageExist(string deviceID, Bitmap imageCheck, double percent = 0.9)
        {
            var screen = Device.ScreenShoot(deviceID); // chup man hinh
            var hasLock = Device.FindOutPoint(screen, imageCheck, percent);
            if (hasLock != null)// kiem tra
            {

                return true;
            }
            return false;
        }

        public static bool WaitAndTapXML(string deviceID, string[] text)
        {

            if (text != null && text.Length > 0)
            {
                string xml = GetUIXml(deviceID);
                for (int i = 0; i < text.Length; i++)
                {
                    if (Utility.CheckAndTap(deviceID, text[i], xml))
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }
        public static bool WaitAndTapXML(string deviceID, string[] text, string xmlIn = "")
        {
            if (text != null && text.Length > 0)
            {
                string xml = xmlIn;
                if (string.IsNullOrEmpty(xmlIn))
                {
                    xml = GetUIXml(deviceID);
                } 
                
                for (int i = 0; i < text.Length; i++)
                {
                    if (Utility.CheckAndTap(deviceID, text[i], xml))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public static bool WaitAndTapXML(string deviceID, int time, string text, string xml = "")
        {
            if (!string.IsNullOrEmpty(xml))
            {
                time = 1;
            }
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }
            for (int i = 0; i < time; i++)
            {
                if (Utility.CheckAndTap(deviceID, text, xml))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool WaitAndTapXMLNew(string deviceID, int time, string text, string xml = "")
        {
            text = text.ToLower();
            if (!string.IsNullOrEmpty(xml))
            {
                time = 1;
            }
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }
            for (int i = 0; i < time; i++)
            {
                if (Utility.CheckAndTapNew(deviceID, text, xml))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool WaitAndTapXMLUnsign(string deviceID, int time, string text, string xml = "")
        {
            if (!string.IsNullOrEmpty(xml))
            {
                time = 1;
            }
            for (int i = 0; i < time; i++)
            {
                if (Utility.CheckAndTapUnsign(deviceID, text, xml))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool WaitAndTapXML2(string deviceID, int time, string text)
        {
            for (int i = 0; i < time; i++)
            {

                if (Utility.CheckAndTap2(deviceID, text))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool WaitAndTapXML3(string deviceID, int time, string text)
        {
            for (int i = 0; i < time; i++)
            {

                if (Utility.CheckAndTap3(deviceID, text))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool TurnOnAirPlane(string deviceID)
        {
            //return true;
            if (Device.IsAirPlaneMode(deviceID))
            {
                return true;
            }
            string openAirplaneModeCmd = string.Format("adb -s {0} shell am start -a android.settings.AIRPLANE_MODE_SETTINGS", deviceID);
            Device.ExecuteCMD(openAirplaneModeCmd);
            //Thread.Sleep(1000);
            for (int i = 0; i < 3; i++)
            {
                if (Device.IsAirPlaneMode(deviceID))
                {
                    return true;
                }

                //Utility.WaitAndTapXML(deviceID, 2, Language.Airplane());
                //Thread.Sleep(1000);
                //if (Device.IsAirPlaneMode(deviceID))
                //{
                //    return true;
                //}

                KAutoHelper.ADBHelper.TapByPercent(deviceID, 56.9, 33.8);
                Thread.Sleep(1000);
                if (Device.IsAirPlaneMode(deviceID))
                {
                    return true;
                }

                Device.AirplaneOn(deviceID);
                Thread.Sleep(1000);
                if (Device.IsAirPlaneMode(deviceID))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool TurnOffAirPlane(string deviceID, bool needOpenSetting = true)
        {
           
            if (needOpenSetting)
            {
                string openAirplaneModeCmd = string.Format("adb -s {0} shell am start -a android.settings.AIRPLANE_MODE_SETTINGS", deviceID);
                Device.ExecuteCMD(openAirplaneModeCmd);
            }
            
            //Thread.Sleep(1000);
            for (int i = 0; i < 3; i++)
            {
                if (!Device.IsAirPlaneMode(deviceID))
                {
                    return true;
                }

                //Utility.WaitAndTapXML(deviceID, 2, Language.Airplane());
                //Thread.Sleep(1000);
                //if (!Device.IsAirPlaneMode(deviceID))
                //{
                //    return true;
                //}

                KAutoHelper.ADBHelper.TapByPercent(deviceID, 56.9, 33.8);
                //Thread.Sleep(1000);
                if (!Device.IsAirPlaneMode(deviceID))
                {
                    return true;
                }

                Device.AirplaneOff(deviceID);
                //Thread.Sleep(1000);
                if (!Device.IsAirPlaneMode(deviceID))
                {
                    return true;
                }
            }
            return false;
        }
      
        public static string RandomAndroidID()
        {

            Random rnd = new Random();
            
            string id = "";
            
            for (int i = 0; i < 16; i ++)
            {
                id += Constant.HEX[rnd.Next(0, 15)];
                
            }
            return id;
        }


        public static string GetXproxyStatus(string proxy)
        {
            string url = proxy.Split(':')[0];
            if (!url.Contains("http://)"))
            {
                url = "http://" + url;
            }
            string port = proxy.Split(':')[1];
            string apiGetXproxyStatus = string.Format("{0}/status?proxy={1}", url, proxy);
            var request = (HttpWebRequest)WebRequest.Create(apiGetXproxyStatus);
            request.Method = "GET";
            request.Accept = "application/json";
            request.ContentType = "application/json; charset=utf-8";

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            
            if (!string.IsNullOrEmpty(responseString) && responseString.Contains("MODEM_READY"))
            {
                return Regex.Match(responseString, "public_ip\":\"(.*?)\"").Groups[1].ToString();
            }
            Console.WriteLine(responseString);

            return Constant.FAIL;
        }

        public static string ResetXproxy(string proxy)
        {
            string url = proxy.Split(':')[0];
            if (!url.Contains("http://)"))
            {
                url = "http://" + url;
            }
            string port = proxy.Split(':')[1];
            string apiGetXproxyStatus = string.Format("{0}/reset?proxy={1}", url, proxy);
            var request = (HttpWebRequest)WebRequest.Create(apiGetXproxyStatus);
            request.Method = "GET";
            request.Accept = "application/json";
            request.ContentType = "application/json; charset=utf-8";

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            if (!string.IsNullOrEmpty(responseString) && responseString.Contains("true"))
            {
                return responseString;
            }
            Console.WriteLine(responseString);

            return Constant.FAIL;
        }

        public static string Decode_UTF8(string s)
        {
            string text = "";
            byte[] bytes = Encoding.Default.GetBytes(s);
            text = Encoding.UTF8.GetString(bytes);

            return text;
        }
        public static List<String> LoadData(string filename)
        {
            List<String> name = new List<string>();

            List<string> line = (List<string>)File.ReadAllLines(filename).Shuffle();
            for (int i = 0; i < line.Count; i ++)
            {
                if (!string.IsNullOrEmpty(line[i].Trim()))
                {
                    name.Add(line[i].Trim());
                }
            }
            return name;
        }

        private static Process listeningProc;

        public static void someClass()
        {
            listeningProc = new Process();
            listeningProc.StartInfo.FileName = "cmd.exe";
            listeningProc.StartInfo.UseShellExecute = false;
            listeningProc.StartInfo.RedirectStandardOutput = true;
            listeningProc.StartInfo.RedirectStandardInput = true;
            listeningProc.Start();

            listeningProc.OutputDataReceived += new DataReceivedEventHandler(
                (s, e) => {
                    if (e.Data == "SPECIFIC_COMMAND")
                    {
                        return;
               // do something
           }
                }
            );
            listeningProc.BeginOutputReadLine();
        }

        public static string GetIpType(string ip)
        {
            string ipType = "";
            if (string.IsNullOrEmpty(ip)) return "";

            if (ip.Length > 20)
            {
                ipType = Constant.ACTION_CHANGE2IP6;
            }
            else if (ip.Length > 0 && ip.Length < 20)
            {
                ipType = Constant.ACTION_CHANGE2IP4;
            }
            else
            {
                ipType = "";
            }

            return ipType;
        }

        
        public static string GetWifiName(string deviceID)
        {
            string wifiName = "";

            wifiName = Device.GetWifiStatus(deviceID);

            return wifiName;
        }

        public static bool ExtractZipAuth(string uid)
        {

            if (!File.Exists("Authentication/" + uid + ".tar.gz"))
            {
                return false;
            }

           
            //if (Directory.Exists("Authentication/ " + uid))
            //{
            //    Thread.Sleep(5000);
            //    return false;
            //}
            //try
            //{
            //    ZipFile.ExtractToDirectory("Authentication/" + uid + ".zip", "Authentication/" + uid);
            //}
            //catch (Exception exx)
            //{
            //    File.Delete("Authentication/" + uid + ".zip");
            //    return false;
            //}
            return true;
        }

        public static void generateName()
        {
            List<string> femaleName = LoadData("data/female_name.txt");
            List<string> maleName = LoadData("data/male_name.txt");
            List<string> lastName = LoadData("data/lastname.txt");

            
            string maleFile = "data/male.txt";
            string femaleFile = "data/female.txt";
            try
            {
                File.Delete(maleFile);
                File.Delete(femaleFile);
            }
            catch (IOException ex)
            {
                Console.WriteLine("ex:" + ex.Message);
            }

            for (int i = 0; i < lastName.Count; i ++ )
            {
                for (int k = 0;k < femaleName.Count; k++)
                {
                    File.AppendAllText(femaleFile, lastName[i] + " " + femaleName[k] + "\n");
                }
            }
            
        }

        public static string ConvertList2String(List<String> dd)
        {
            string result = "";

            if (dd != null)
            {
                if (dd.Count == 1)
                {
                    result = dd[0];
                }
                else
                {
                    for (int i = 0; i < dd.Count - 1; i++)
                    {
                        result = result + dd[i] + "|";
                    }
                    result = result + dd[dd.Count - 1];
                }
            }
            return result;
        }
        static string _generateFileName(int sequence)
        {
            DateTime currentDateTime = DateTime.Now;
            return string.Format("{0}-{1:000}-{2:000}.eml",
                currentDateTime.ToString("yyyyMMddHHmmss", new CultureInfo("en-US")),
                currentDateTime.Millisecond,
                sequence);
        }
    }
}
