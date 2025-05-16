using fb_reg.RequestApi;
using fb_reg.Utilities;
using HttpRequest;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.SessionState;
using System.Windows.Forms;
using static fb_reg.Utility;
namespace fb_reg
{
    class FbUtil
    {
        public static List<string> cityList;
        public static List<string> schoolList;
        public static List<string> universityList;
        public static List<string> companyList;
        public static List<string> descriptionList;

        public static bool ReinstallFblite(string deviceID)
        {
            string[] fileNames = Directory.GetFiles("data\\fblite", "*.apk");
            if (fileNames != null && fileNames.Length > 0)
            {
                string fileName = fileNames[0];
                Console.WriteLine("file fblite: " + fileName);
                Device.Uninstall(deviceID, Constant.FACEBOOK_LITE_PACKAGE);

                Thread.Sleep(1000);
                Device.InstallApp(deviceID, fileName);

                Thread.Sleep(1000);
                return true;
            }
            return false;
        }

        public static bool ReinstallBusinsess(string deviceID)
        {
            System.IO.Directory.CreateDirectory("data/business");
            string[] fileNames = Directory.GetFiles("data\\business", "*.apk");
            if (fileNames != null && fileNames.Length > 0)
            {
                string fileName = fileNames[0];
                Console.WriteLine("file Business: " + fileName);
                Device.Uninstall(deviceID, Constant.FACEBOOK_BUSINESS_PACKAGE);

                Thread.Sleep(1000);
                Device.InstallApp(deviceID, fileName);

                Thread.Sleep(10000);
                return true;
            }
            return false;
        }

        public static bool InstallMissingFb(string deviceID)
        {
            string[] fileNames = Directory.GetFiles("data\\fb", "*.apk");
            if (fileNames != null && fileNames.Length > 0)
            {
                string fileName = fileNames[fileNames.Length -1];
                Thread.Sleep(1000);
                Device.InstallApp(deviceID, fileName);

                Thread.Sleep(1000);
                return true;
            }
            return false;
        }
        public static bool InstallMissingFblite(string deviceID)
        {
            try
            {
                string[] fileNames = Directory.GetFiles("data\\fblite", "*.apk");
                if (fileNames != null && fileNames.Length > 0)
                {
                    string fileName = fileNames[0];
                    Thread.Sleep(1000);
                    Device.InstallApp(deviceID, fileName);

                    Thread.Sleep(1000);
                    return true;
                }
                return false;
            } catch(Exception ex)
            {
                return false;
            }
            
        }
        public static bool InstallMissingFbLite(string deviceID)
        {
            string[] fileNames = Directory.GetFiles("data\\fblite", "*.apk");
            if (fileNames != null && fileNames.Length > 0)
            {
                string fileName = fileNames[0];
                Thread.Sleep(1000);
                Device.InstallApp(deviceID, fileName);

                Thread.Sleep(1000);
                return true;
            }
            return false;
        }
        public static bool InstallMissingBusiness(string deviceID)
        {
            if (Device.CheckAppInstall(deviceID, Constant.FACEBOOK_BUSINESS_PACKAGE))
            {
                return true;
            }
            string[] fileNames = Directory.GetFiles("data\\business", "*.apk");
            if (fileNames != null && fileNames.Length > 0)
            {
                string fileName = fileNames[0];
                Thread.Sleep(1000);
                Device.InstallApp(deviceID, fileName);

                Thread.Sleep(1000);
                return true;
            }
            return false;
        }
        public static bool InstallMissingMessenger(string deviceID)
        {
            string[] fileNames = Directory.GetFiles("data\\messenger", "*.apk");
            if (fileNames != null && fileNames.Length > 0)
            {
                string fileName = fileNames[0];
                Thread.Sleep(1000);
                Device.InstallApp(deviceID, fileName);

                Thread.Sleep(1000);
                return true;
            }
            return false;
        }
        public static void InstallFb(string deviceID, string fileName)
        {
            
            Console.WriteLine("file fb: " + fileName);
            Device.Uninstall(deviceID, Constant.FACEBOOK_PACKAGE);

            Thread.Sleep(1000);
            Device.InstallApp(deviceID, fileName);

            Thread.Sleep(1000);
            
        }
        public static void ChangeHMA(string deviceID)
        {
            Thread.Sleep(1000);
            Device.OpenApp(deviceID, "com.hidemyass.hidemyassprovpn");
            Thread.Sleep(3000);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 87.8, 66.9);
            Device.TapByPercent(deviceID, 85.6, 60.6);
            Thread.Sleep(7000);

            
        }
        public static string GetCookieRawFromPhone(string deviceID)
        {
            string text = Device.ExecuteCMD("adb -s " + deviceID + " shell \"su -c cat data/data/com.facebook.katana/app_light_prefs/com.facebook.katana/authentication\"");
            return text;
        }
        public static string GetCookieFbLiteRawFromEmulator(string deviceID)
        {
            File.Delete("PropertiesStore_v02");
            Device.ExecuteCMD("adb -s " + deviceID + " shell  cp /data/data/com.facebook.lite/files/PropertiesStore_v02 /sdcard/PropertiesStore_v02");
            Device.ExecuteCMD("adb -s " + deviceID + " pull   /sdcard/PropertiesStore_v02  PropertiesStore_v02");
            
            string text = File.ReadAllText("PropertiesStore_v02").ToString();

            File.Delete("PropertiesStore_v02");
            Device.ExecuteCMD("adb -s " + deviceID + " shell \" rm -rf /sdcard/PropertiesStore_v02");
            
            return text;
        }
        public static string GetCookieRawFromEmulator(string deviceID)
        {
            Device.ExecuteCMD("adb -s " + deviceID + " shell  cp /data/data/com.facebook.katana/app_light_prefs/com.facebook.katana/authentication /sdcard/Cookie");
            string text = Device.ExecuteCMD("adb -s " + deviceID + " shell  cat /sdcard/Cookie");
            Device.ExecuteCMD("adb -s " + deviceID + " shell \" rm -rf /sdcard/Cookie");
            return text;
        }

        public static string CheckInfoUsingUid(string uid)
        {
            RequestXNet requestHttp = new RequestXNet("", "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:50.0) Gecko/20100101 Firefox/50.0", "", 0);
            try
            {
                string text = "";
                string text2 = "";
                string text3 = "";
                string text4 = "";
                string text5 = "";
                string text6 = "";
                string text7 = "";
                string text8 = requestHttp.RequestPost("https://www.facebook.com/api/graphql", "q=user(" + uid + "){friends{count},groups{count},id,name,gender,birthday,email_addresses,username}");
                if (!string.IsNullOrEmpty(text8))
                {
                    JObject jobject = JObject.Parse(text8);
                    if (string.IsNullOrEmpty(jobject[uid].ToString()))
                    {
                        return "0|";
                    }
                    if (jobject[uid]["name"] != null)
                    {
                        if (jobject[uid]["friends"]["count"] != null)
                        {
                            text = jobject[uid]["friends"]["count"].ToString();
                        }
                        if (jobject[uid]["groups"]["count"] != null)
                        {
                            text2 = jobject[uid]["groups"]["count"].ToString();
                        }
                        if (jobject[uid]["name"] != null)
                        {
                            text3 = jobject[uid]["name"].ToString();
                        }
                        if (jobject[uid]["gender"] != null)
                        {
                            text4 = jobject[uid]["gender"].ToString();
                        }
                        if (jobject[uid]["username"] != null)
                        {
                            text5 = jobject[uid]["username"].ToString();
                        }
                        if (jobject[uid]["birthday"] != null)
                        {
                            text6 = jobject[uid]["birthday"].ToString();
                        }
                        if (jobject[uid]["email_addresses"].ToString() != "[]")
                        {
                            text7 = jobject[uid]["email_addresses"].ToString();
                        }
                        return string.Concat(new string[]
                        {
                            "1|",
                            text5,
                            "|",
                            text3,
                            "|",
                            text4,
                            "|",
                            text6,
                            "|",
                            text,
                            "|",
                            text2,
                            "|",
                            text7
                        });
                    }
                }
            }
            catch (Exception)
            {
            }
            return "2|";
        }

        public static bool BackToFbHome(string deviceID)
        {
            for (int i = 0; i < 10; i ++ )
            {
                string xml = Utility.GetUIXml(deviceID);
                if (Utility.CheckTextExist(deviceID, Language.Message(), 1, xml))
                {
                    return true;
                }
                WaitAndTapXML(deviceID, 1, "dừng lại");
                if (CheckTextExist(deviceID, "truycậpvịtrí", 1))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 53.7, 65.1);
                    Thread.Sleep(2000);

                }
                Device.Back(deviceID);
                Console.WriteLine("back: " + i);
            }
            Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
            Thread.Sleep(1000);
            Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
            Thread.Sleep(2000);
            if (Utility.CheckTextExist(deviceID, Language.Message()))
            {
                return true;
            }
            return false;
        }
        public static string GetCookieFromFbLite(string deviceID)
        {
            try
            {
                string rawCookie = "";
                
                rawCookie = GetCookieFbLiteRawFromEmulator(deviceID);
                
                string input = rawCookie;

                string userRaw = Regex.Match(input, "c_user(.*?)}").Groups[1].ToString();
                if (string.IsNullOrEmpty(userRaw))
                {
                    return "";
                }
                string user = "c_user=" + Regex.Match(userRaw, "value\":\"(.*?)\"").Groups[1].ToString();
                string datRaw = Regex.Match(input, "datr(.*?)}").Groups[1].ToString();
                string datr = "datr=" + Regex.Match(datRaw, "value\":\"(.*?)\"").Groups[1].ToString();

                string xsRaw = Regex.Match(input, "xs(.*?)}").Groups[1].ToString();
                string xs = "xs=" + Regex.Match(xsRaw, "value\":\"(.*?)\"").Groups[1].ToString();

                string frRaw = Regex.Match(input, "fr(.*?)}").Groups[1].ToString();
                string fr = "fr=" + Regex.Match(frRaw, "value\":\"(.*?)\"").Groups[1].ToString();


                
                string str = Regex.Match(rawCookie, "EAA[a-z,A-Z,0-9]{0,}").ToString();
                string str2 = string.Concat(new string[]
                    {
                    user,
                    ";",
                    datr,
                    ";",
                    fr,
                    ";",
                    xs,";"
                    });
                File.Delete("PropertiesStore_v02");
                return str2 + "|" + str;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public static string GetUidFromCookie(string cookie)
        {
            if (string.IsNullOrEmpty(cookie))
            {
                return "";
            }
            string uid = Regex.Match(cookie, "c_user=[0-9]{0,}").ToString();
            uid = uid.Replace("c_user=", "");
            return uid;
        }
        public static string GetCookieFromPhone(string deviceID)
        {
            try
            {
                string rawCookie = "";
                if (string.IsNullOrEmpty(deviceID))
                {
                    return "|";
                }
                if (deviceID.StartsWith(Constant.EMULATOR))
                {
                    rawCookie = GetCookieRawFromEmulator(deviceID);
                } else
                {
                    rawCookie = GetCookieRawFromPhone(deviceID);
                }
                string input = rawCookie.Replace("\"", " ");

                string text3 = Regex.Match(input, "c_user , value : [0-9]{0,}").ToString();
                string text4 = text3.Replace(" , value : ", "=");

                if (text4.Length < 10)
                {
                    return "|";
                }
                string xsValue = "xs=" + Regex.Match(input, "xs , value : (.*?) ,").Groups[1].ToString();

                string frValue = "fr=" + Regex.Match(input, "fr , value : (.*?) ,").Groups[1].ToString();

                string datrValue = "datr=" + Regex.Match(input, "datr , value : (.*?) ,").Groups[1].ToString();


                string str = Regex.Match(rawCookie, "EAA[a-z,A-Z,0-9]{0,}").ToString();
                string str2 = string.Concat(new string[]
                    {
                    text4,
                    ";",
                    datrValue,
                    ";",
                    frValue,
                    ";",
                    xsValue, ";"
                    });
                Console.WriteLine("cookie:" + str2 + "|" + str);
                return str2 + "|" + str;
            } catch(Exception ex)
            {
                return "";
            }
        }

       
        public static bool FakerPlusChange(string deviceID, bool reboot)
        {
            try
            {
                
                //QLong.Phone.Về_Màn_Hình_Chính(deviceID);
                Thread.Sleep(100);

               
                Device.OpenApp(deviceID, "com.devicefaker.plus");

                Thread.Sleep(1000);
                WaitAndTapXML(deviceID, 1, "yesresource");
                Utility.WaitAndTapXML2(deviceID, 5, "spnTextView");
                Thread.Sleep(700);
                Utility.WaitAndTapXML3(deviceID, 5, "spnTextView");
                Thread.Sleep(700);
                Utility.WaitAndTapXML(deviceID, 5, "Apply");
                //Thread.Sleep(1000);
                if (reboot) Device.RebootDevice(deviceID);


                Thread.Sleep(1500);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void ClearCacheFbLite(string deviceID, bool clearAccInSetting)
        {
            Device.OpenAppDetail(deviceID, "com.facebook.lite");
            Thread.Sleep(3000);
            Utility.WaitAndTapXML(deviceID, 2, Language.Storage());
            Thread.Sleep(1000);
            Utility.WaitAndTapXML(deviceID, 2, Language.ManageStorage());
            Thread.Sleep(3000);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 7.4, 57.8); // Select account and setting
            Utility.WaitAndTapXML(deviceID, 2, "OKresource");
            Thread.Sleep(1000);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.5, 72.7);
            Thread.Sleep(1000);

            KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.5, 72.7);
            Device.ClearCache(deviceID, "com.facebook.lite");
            Device.Back(deviceID);
            Thread.Sleep(200);
            Device.Back(deviceID);
            Thread.Sleep(200);
            Device.Back(deviceID);
            Thread.Sleep(200);
            Device.Back(deviceID);
            if (clearAccInSetting)
            {
                ClearAccountFbInSetting(deviceID, true);
            }
        }
        public static void ClearAccountFbInSetting(string deviceID, bool clearAll)
        {
            OpenAccountsSetting(deviceID);
            Utility.WaitAndTapXML(deviceID, 2, "1textfacebookresourceid");
            //Utility.WaitAndTapXML(deviceID, 2, "RemoveAccount");
            Utility.WaitAndTapXML(deviceID, 2, "xóa tài khoảnresourceid");
            if (Utility.WaitAndTapXML(deviceID, 2, "xóa tài khoảnresourceid"))
            {
                Thread.Sleep(5000);
            }
            
            //if (clearAll)
            //{
            //    while (Utility.CheckTextExist(deviceID, "Facebook"))
            //    {
            //        RemoveAccountFb(deviceID);
            //        Thread.Sleep(1000);
            //    }
            //} else
            //{
                string input = GetUIXml(deviceID);
            //var result = Regex.Match(responseString, "text100(.*?)resource");
            Regex expression = new Regex(@"text615(.*?)resource");
            //Regex expression = new Regex(@"text615(.*?)resource");
            var results = expression.Matches(input);
                if (results != null)
                {
                    foreach (Match match in results)
                    {
                        Console.WriteLine(match.Groups[1].Value);
                        string uid = "615" + match.Groups[1].Value;
                        //if (CheckLiveWall(uid) == Constant.DIE)
                        //{
                            Utility.WaitAndTapXML(deviceID, 2, uid);
                            //Utility.WaitAndTapXML(deviceID, 2, "RemoveAccount");
                            Utility.WaitAndTapXML(deviceID, 2, "xóa tài khoảnresourceid");
                            Utility.WaitAndTapXML(deviceID, 2, "xóa tài khoảnresourceid");
                            Thread.Sleep(1000);
                        //}
                    }
                }
                
            //}
            
            //while (Utility.CheckTextExist(deviceID, "Messenger"))
            //{
            //    RemoveAccountMessenger(deviceID);
            //}
            Thread.Sleep(500);
            Device.Back(deviceID);
            Thread.Sleep(500);
            Device.Back(deviceID);
            Thread.Sleep(500);
            Device.Back(deviceID);
        }
        public static void OpenAccountsSetting(string deviceID)
        {
            //Device.OpenSetting(deviceID);
            //Thread.Sleep(1000);
            //Device.Swipe(deviceID, 200, 2000, 200, 100);
            //Thread.Sleep(1000);
            //string uixml = GetUIXml(deviceID);
            //if (!CheckTextExist(deviceID, "facebook",1, uixml) && !CheckTextExist(deviceID, "messenger", 1, uixml))
            //{
            //    return;
            //}
            //WaitAndTapXML(deviceID, 2, "Tài khoản resourceid", uixml);
            //Thread.Sleep(1000);
            Device.OpenSettingAccount(deviceID);


        }
        public static void RemoveAccountFb(string deviceID)
        {
            Utility.WaitAndTapXML(deviceID, 2, "Facebook"); 
            //Utility.WaitAndTapXML(deviceID, 2, "RemoveAccount");
            Utility.WaitAndTapXML(deviceID, 2, "xóa tài khoảnresourceid");
            Utility.WaitAndTapXML(deviceID, 2, "xóa tài khoảnresourceid");
        }
        public static void RemoveAccountMessenger(string deviceID)
        {
            Utility.WaitAndTapXML(deviceID, 2, "Messenger");
            Utility.WaitAndTapXML(deviceID, 2, "xóa tài khoảnresourceid");
            Utility.WaitAndTapXML(deviceID, 2, "xóa tài khoảnresourceid");
        }

        public static void ChangeDeviceInfoHook(string deviceID)
        {
            // open app
            Device.OpenApp(deviceID, "com.variable.apkhook");
            Thread.Sleep(3000);

            KAutoHelper.ADBHelper.TapByPercent(deviceID, 47.9, 14.3);
            Thread.Sleep(400);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 47.9, 14.3);
            Thread.Sleep(400);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 7.9, 27.4);
            Thread.Sleep(600);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 94.8, 8.2);
            Thread.Sleep(500);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 62.4, 21.2);
            Thread.Sleep(500);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 72.5, 7.7);
            Thread.Sleep(500);
            Device.Home(deviceID);
        }

        public static Proxy GetProxyFromServer(DeviceObject device, OrderObject order, bool p1, bool p2, bool p3)
        {
            Proxy proxy = new Proxy();
            for (int i = 0; i < 20; i++)
            {
                LogStatus(device, "Get proxy lần :" + (i + 1));
                proxy = CacheServer.GetProxyFromServer(PublicData.ServerCacheMail, order);
                if (proxy != null &&
                    (!string.IsNullOrEmpty(proxy.host)))
                {

                    bool proxyOk = OutsideServer.TestProxy(proxy.host, Convert.ToInt32(proxy.port), proxy.username, proxy.pass);
                    if (!proxyOk)
                    {
                        LogStatus(device, "❌ Proxy không ổn định, dừng reg clone.");
                        continue;
                    }
                    proxy.hasProxy = true;
                    return proxy;
                }
                Thread.Sleep(10000);
            }

            if (p1 || p2 || p3)
            {
                for (int i = 0; i < 2; i++)
                {
                    LogStatus(device, "Get Proxy Data ----------");
                    proxy = CacheServer.GetProxyFromServer(PublicData.ServerCacheMail, order);
                    if (proxy != null && !string.IsNullOrEmpty(proxy.host))
                    {
                        proxy.hasProxy = true;
                        return proxy;
                    }
                    Thread.Sleep(10000);
                }
            }

            return null;
        }
        public static string createProxyFile(Proxy proxy)
        {
            if (proxy == null || !proxy.hasProxy)
            {
                return "";
            }
            System.IO.Directory.CreateDirectory("data/proxy");

            string fileName = "data/proxy/" + proxy.port + ".txt";
            try
            {
                File.Delete(fileName);
            }
            catch (IOException ex)
            {
                Console.WriteLine("ex:" + ex.Message);
            }

            string _vcf = "socks5://" + proxy.username + ":" + proxy.pass + "@" + proxy.host + ":" + proxy.port + " \"" + proxy.port + "\" *";
            if (!string.IsNullOrEmpty(proxy.proxyType) && proxy.proxyType == "HTTP")
            {
                _vcf = "http://" + proxy.username + ":" + proxy.pass + "@" + proxy.host + ":" + proxy.port + " \"" + proxy.port + "\" *";
            }
            File.AppendAllText(fileName, "# superproxy:proxylist:v1\n");
            File.AppendAllText(fileName, _vcf);
            return fileName;
        }
        public static bool StopProxySuper(DeviceObject device, OrderObject order)
        {
            Device.RemoveProxy(device.deviceId);

           
            Device.ForceStop(device.deviceId, "com.scheler.superproxy");
            

            device.keyProxy = "";
            return true;
        }
        public static bool randomProxy(Proxy proxy, string deviceID)
        {
            Device.DeleteAllFileMusic(deviceID);
            string randomFilePath = createProxyFile(proxy);

            if (string.IsNullOrEmpty(randomFilePath)) return false;
            Device.PushFile2Sdcard(deviceID, randomFilePath, proxy.port + ".txt");
            File.Delete(randomFilePath);

            return true;
        }
        public static bool SetProxySuperProxy(OrderObject order, DeviceObject device)
        {
            try
            {
                string deviceID = device.deviceId;

                if (deviceID.Contains("line") || deviceID.Contains("rec"))
                {
                    return false;
                }
                if (order.proxy == null || !order.proxy.hasProxy)
                {
                    return false;
                }
                if (device.currentRom == "13")
                {
                    Device.OpenApp(deviceID, "com.estrongs.android.pop");
                    WaitAndTapXML(deviceID, 2, "cho phép re");
                    WaitAndTapXML(deviceID, 5, "internal");
                    if (CheckTextExist(deviceID, "đicấpquyềntruycậpresourceid", 3))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 64.0, 74.6);
                        WaitAndTapXML(deviceID, 5, "quản lý tất cả các tệp");
                    }
                }



                Device.DeleteTxtSdcard(deviceID);
                //Device.RebootDevice(deviceID);
                randomProxy(order.proxy, deviceID);
                LogStatus(device, "Bắt đầu Chạy set proxy ------------");
                Device.ForceStop(device.deviceId, "com.scheler.superproxy");
                Device.RemoveProxy(deviceID);
                Device.ClearCache(deviceID, "com.scheler.superproxy");
                Device.ClearCache(deviceID, "com.estrongs.android.pop");

                Device.OpenApp(deviceID, "com.scheler.superproxy");

                for (int i = 0; i < 16; i++)
                {
                    string xml = GetUIXml(deviceID);

                    if (WaitAndTapXML(deviceID, 1, "import proxies", xml))
                    {
                        break;
                    }
                    if (WaitAndTapXML(deviceID, 3, "proxies&#10;tab1of3checkable", xml))
                    {
                        Thread.Sleep(1000);
                        continue;
                    }

                }

                if (device.currentRom != "13")
                {
                    if (device.currentRom == "9")
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 72.9, 59.4);
                    }
                    else
                    {
                        if (!WaitAndTapXML(deviceID, 2, "cho phép resourceid"))
                        {

                            WaitAndTapXML(deviceID, 1, "allow resourceid");
                        }
                    }
                }

                if (Device.CheckAppInstall(deviceID, "com.estrongs.android.pop"))
                {
                    WaitAndTapXML(deviceID, 3, "duyệttậptincheckable");

                    if (!CheckTextExist(deviceID, "chọn đường dẫn", 5))
                    {
                        return false;
                    }
                    //Thread.Sleep(1000);
                    Device.Swipe(deviceID, 88, 1400, 99, 300);
                    //Thread.Sleep(1000);
                    Device.Swipe(deviceID, 88, 1400, 99, 300);
                    //Thread.Sleep(1000);
                    if (!WaitAndTapXML(deviceID, 3, order.proxy.port))
                    {
                        LogStatus(device, "Root xong- bật explorer", 1000);
                        Device.OpenApp(deviceID, "com.estrongs.android.pop");
                        WaitAndTapXML(deviceID, 2, "cho phép re");
                        WaitAndTapXML(deviceID, 5, "internal");
                        if (CheckTextExist(deviceID, "đicấpquyềntruycậpresourceid", 3))
                        {
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 64.0, 74.6);
                            WaitAndTapXML(deviceID, 5, "quản lý tất cả các tệp");
                        }
                        return false;
                    }
                    Thread.Sleep(1000);
                    if (!CheckTextExist(deviceID, "lấytậptindướidạng", 2))
                    {
                        return false;
                    }
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 51.0, 47.9);
                    Thread.Sleep(1000);
                }
                else
                {
                    Device.TapByPercent(deviceID, 8.7, 8.3, 2000); // click setting
                    if (!WaitAndTapXML(deviceID, new string[] { "nodeindex0textsmg935sresourceidandroididtitleclassandroidwidgettextviewpackagecomandroiddocumentsuicontentdesccheckablefalsecheckedfalseclickablefa", "còntrốngresource", "freeresource", "sm-g" }))
                    {
                        Device.TapByPercent(deviceID, 34.4, 33.9, 1000); ; // click to SM-G
                    }
                    Thread.Sleep(2000);
                    Device.Swipe(deviceID, 33, 1500, 44, 500);
                    Thread.Sleep(2000);
                    //Device.TapByPercent(deviceID, 22.9, 54.7);
                    if (device.currentRom == "9")
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 25.9, 79.7);
                    }
                    else
                    {
                        if (!WaitAndTapXMLNew(deviceID, 2, order.proxy.port))
                        {
                            return false;
                        }
                    }
                }

                Device.DeleteTxtSdcard(deviceID);
                if (!string.IsNullOrEmpty(order.proxy.proxyType) && order.proxy.proxyType == "HTTP")
                {
                    if (!WaitAndTapXML(deviceID, 2, "httpcheckable"))
                    {
                        return false;
                    }
                }
                else
                {
                    if (device.currentRom == "9")
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 38.6, 15.7);
                    }
                    else
                    {
                        if (!WaitAndTapXML(deviceID, 2, "socks5checkable"))
                        {
                            return false;
                        }
                    }
                }

                if (!WaitAndTapXML(deviceID, 2, "startcheckable"))
                {
                    LogStatus(device, "Không thấy nút start proxy - set proxy lỗi");
                    Thread.Sleep(6000);
                    return false;
                }

                Device.AdbConnect(deviceID);
                Thread.Sleep(1000);
                WaitAndTapXML(deviceID, 1, "okresourceid");
                LogStatus(device, "Set proxy ok");

                if (!CheckTextExist(deviceID, "stopcheckable", 5))
                {
                    LogStatus(device, "Không thấy nút stop - khong start proxy duoc proxy - set proxy lỗi", 6000);
                    return false;
                }
                Device.Home(deviceID);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool StartProxy(OrderObject order, DeviceObject device, bool proxy4g, bool forceChangeWifi, bool randomWifi)
        {
            string deviceID = device.deviceId;
            if (!order.hasproxy)
            {
                Device.DisableWifi(deviceID);
                return true;
            }
            if (order.proxyWfi)
            {
                return true;
            }
            if (proxy4g)
            {
                Device.DisableWifi(deviceID);
            }
            TurnOffAirPlane(deviceID, false);
            if (!proxy4g && !deviceID.Contains(":"))
            {   // Check wifi before
                Device.EnableWifi(deviceID);
                string ssid = "";
                if (Utility.isScreenLock(deviceID))
                {
                    Device.Unlockphone(deviceID);
                }

                ssid = GetWifiName(deviceID);
                LogStatus(device, "Wifi:" + ssid);
                if (string.IsNullOrEmpty(ssid) || ssid.Contains("unknown") || forceChangeWifi)
                {
                    string mainWifi = "";
                    if (PublicData.wifilist.Count > 0)
                    {
                        List<string> wifitemp = new List<string>();
                        if (randomWifi)
                        {
                            wifitemp = (List<string>)PublicData.wifilist.Shuffle();
                        }
                        else
                        {
                            wifitemp = PublicData.wifilist;
                        }

                        mainWifi = wifitemp[0];
                        LogStatus(device, "Set wifi: " + mainWifi);
                        string[] temp = mainWifi.Split('|');
                        Device.SetWifi(deviceID, temp[0].Trim(), temp[1].Trim());
                        Thread.Sleep(3000);

                    }

                }

                for (int i = 0; i < 4; i++)
                {
                    ssid = GetWifiName(deviceID);
                    LogStatus(device, "Wifi:" + ssid);
                    if (!string.IsNullOrEmpty(ssid) && !ssid.Contains("unknown"))
                    {
                        Utility.LogRegStatus(device, "Wifi:" + ssid);
                        break;
                    }
                    Thread.Sleep(10000);
                    ssid = GetWifiName(deviceID);
                    LogStatus(device, "Wifi:" + ssid);
                    if (!ssid.Contains("unknown"))
                    {
                        break;
                    }
                    else
                    {
                        Device.DisableWifi(deviceID);
                        Thread.Sleep(1000);
                        Device.EnableWifi(deviceID);
                        Device.OpenWifiSetting(deviceID);
                        LogStatus(device, "Bật wifi lại - chờ láy ip");
                        Thread.Sleep(20000);
                        if (Utility.isScreenLock(deviceID))
                        {
                            Device.Unlockphone(deviceID);
                        }
                        ssid = GetWifiName(deviceID);
                        LogStatus(device, "Wifi:" + ssid);
                        if (!ssid.Contains("unknown"))
                        {
                            break;
                        }
                    }
                    if (i == 3)
                    {
                        LogStatus(device, "Can not access wifi");
                        return false;
                    }
                    Thread.Sleep(10000);
                }
                ssid = GetWifiName(deviceID);
                if (ssid.Contains("unknown"))
                {
                    LogStatus(device, "Không Thể start proxy - Chạy lại");
                    return false;
                }
            }


            //if (proxyCMDcheckBox.Checked)
            //{
            //    string proxyPort = device.proxyDevice.host + ":" + device.proxyDevice.port;
            //    Device.SetProxyCmd(deviceID, proxyPort);
            //    return true;
            //}

            if (order.proxyFromServer)
            {
                LogStatus(device, "Chay set proxy mới   -tttttttttttttt-----------");

                if (!SetProxySuperProxy(order, device))
                {
                    LogStatus(device, "Can not set proxy ---------return ", 6000);
                    device.loadNewProxy = true;

                    string fbv = Device.GetVersionFB(deviceID);
                    LogStatus(device, "Không start dc proxy kiem tra fbversion: " + fbv);
                    if (string.IsNullOrEmpty(fbv))
                    {
                        //LogStatus(device, "Máy bị lỗi mất kết nồi - restart máy ------0000");
                        //Device.RebootByCmd(deviceID);
                    }

                    return false;
                }
                else
                {

                    device.loadNewProxy = false;
                    return true;
                }
            }
            if (order.proxyFromLocal)
            {
                if (!SetProxySuperProxy(order, device))
                {
                    LogStatus(device, "Can not set proxy ----ttttttttt-------return ", 6000);
                    device.loadNewProxy = true;
                    return false;
                }
                else
                {
                    return true;
                }
            }

            string domain = "";
            //if (tinsoftRadioButton.Checked)
            //{
            //    domain = ProxyDomain.Tinsoft.ToString();
            //}
            //else if (fastProxyRadioButton.Checked)
            //{
            //    domain = ProxyDomain.fastProxy.ToString();
            //}
            //else if (zuesProxyradioButton.Checked)
            //{
            //    domain = ProxyDomain.zuesProxy.ToString();
            //}
            //else if (zuesProxy4G.Checked)
            //{
            //    domain = ProxyDomain.zuesProxy4G.ToString();
            //}
            //else if (tunProxyradioButton.Checked)
            //{
            //    domain = ProxyDomain.tunProxy.ToString();
            //}
            //else if (impulseradioButton.Checked)
            //{
            //    domain = ProxyDomain.impulseProxy.ToString();
            //}
            //else if (shopLike1RadioButton.Checked)
            //{
            //    domain = ProxyDomain.Softlike.ToString();
            //}
            //else if (tinProxyRadioButton.Checked)
            //{
            //    device.currentPublicIp = GetPublicIp(device);
            //    dataGridView.Rows[device.index].Cells[8].Value = device.currentPublicIp;
            //    if (string.IsNullOrEmpty(device.currentPublicIp))
            //    {
            //        LogStatus(device, "Gỡ proxy khi không lấy được ip");

            //        RemoveAllProxy(deviceID);
            //    }
            //    domain = ProxyDomain.TinProxy.ToString();
            //}
            //else if (tmProxyRadioButton.Checked)
            //{
            //    domain = ProxyDomain.TmProxy.ToString();
            //}
            //else if (dtProxyRadioButton.Checked)
            //{
            //    device.currentPublicIp = GetPublicIp(device);
            //    dataGridView.Rows[device.index].Cells[8].Value = device.currentPublicIp;
            //    if (string.IsNullOrEmpty(device.currentPublicIp))
            //    {
            //        // TODO
            //    }
            //    domain = ProxyDomain.dtProxy.ToString();
            //}
            //else if (wwProxyradioButton.Checked)
            //{
            //    domain = ProxyDomain.wwProxy.ToString();

            //}
            //int proxyTime = 4;
            //if (getProyx20timecheckBox.Checked)
            //{
            //    proxyTime = 20;
            //}
            //for (int i = 0; i < 3; i++)
            //{
            //    LogStatus(device, "Getting proxy ... lần : " + (i + 1));
            //    if (device.proxyDevice != null && device.proxyDevice.hasProxy && !string.IsNullOrEmpty(device.proxyDevice.host))
            //    {
            //        break;
            //    }
            //    device.proxyDevice = Utility.GetProxy(domain, device.proxyDevice.key, device.currentPublicIp, locationProxyTextBox.Text);
            //    // Handle timeout
            //    if (device.proxyDevice != null && device.proxyDevice.isWait)
            //    {
            //        LogStatus(device, "Proxy timeout:" + device.proxyDevice.timeout);
            //        Thread.Sleep(device.proxyDevice.timeout * 1000 + 5000);
            //        device.proxyDevice = Utility.GetProxy(domain, device.proxyDevice.key, device.currentPublicIp, locationProxyTextBox.Text);
            //    }
            //    if (device.proxyDevice != null && device.proxyDevice.hasProxy)
            //    {
            //        string proxyPort = device.proxyDevice.host + ":" + device.proxyDevice.port;
            //        if (!SetProxySuperProxy(order, device))
            //        {
            //            LogStatus(device, "Can not set proxy ----ttttttttt-------return ", 6000);
            //            device.loadNewProxy = true;
            //            return false;
            //        }
            //        return true;
            //        //break;
            //    }
            //    Thread.Sleep(3000);
            //}

            if (device.proxyDevice != null && !device.proxyDevice.hasProxy)
            {
                LogStatus(device, "error:" + device.proxyDevice.message);


                PublicData.dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.DeepPink;
                Thread.Sleep(10000);
            }


            if (proxy4g)
            {
                Device.DisableWifi(deviceID);
            }
            return true;
        }
        public static bool ChangeIp(OrderObject order, DeviceObject device, bool giulaiport, bool proxyShare, bool randomProxyData, bool p1, bool p2, bool p3, bool proxy4g, bool forceChangeWifi, bool randomWifi)
        {
            Device.AirplaneOff(device.deviceId);
            if (order.proxyFromServer)
            {
                bool checkStartProxy = FbUtil.SetProxy(order, device, giulaiport, proxyShare, randomProxyData, p1, p2, p3, proxy4g, forceChangeWifi, randomWifi);

                if (!checkStartProxy)
                {
                    return false;
                }
            }
            else
            {
                LogStatus(device, "change ip by airplane - run sim 4g");
                FbUtil.ChangeIpByAirplane(device);
            }
            return true;
        }
        public static bool SetProxy(OrderObject order, DeviceObject device, bool giulaiport, bool proxyShare,bool randomProxyData, bool p1, bool p2, bool p3, bool proxy4g,bool forceChangeWifi,bool randomWifi)
        {
            string deviceID = device.deviceId;
            for (int i = 0; i < 2; i ++) 
            {
                if (order.proxyFromServer)
                {
                    Utility.LogStatus(device, "Get proxy from server");

                    Proxy pp;

                    if (giulaiport && !proxyShare
                        //&& device.currentProxyCount < 3 
                        && device.VeriOk && device.currentProxy != null && string.IsNullOrEmpty(device.currentProxy.key))
                    {
                        pp = device.currentProxy;
                        device.currentProxyCount++;
                    }
                    else
                    {
                        if (!device.chuyenProxy2P1 && randomProxyData)
                        {
                            Random rr = new Random();
                            int checkrRan = rr.Next(1, 100);

                            if (checkrRan > 50)
                            {
                                order.proxyType = "3";
                            }
                            else
                            {
                                order.proxyType = "2";
                            }
                        }
                        pp = GetProxyFromServer(device, order, p1, p2, p3);
                        device.currentProxyCount = 0;
                    }
                    if (pp != null)
                    {
                        if (!proxy4g)
                        {
                            Device.EnableWifi(deviceID);
                        }

                        device.keyProxy = pp.proxyDomain + "-" + pp.host + ":" + pp.port + ":" + pp.key;
                        order.hasproxy = true;
                        if (device.proxyDevice == null || device.proxyDevice.host != pp.host)
                        {
                            device.loadNewProxy = true;
                        }
                        order.proxy = pp;
                        device.currentProxy = pp;
                    }
                    else
                    {
                        break;
                    }

                }
                else if (order.proxyWfi)
                {
                    StopProxySuper(device, order);
                    Device.EnableWifi(deviceID);
                    Proxy pp = new Proxy();
                    pp.host = "1.1.1.1";
                    pp.port = "1111";
                    device.keyProxy = ":" + pp.host + ":" + pp.port;
                    order.hasproxy = true;
                    if (device.proxyDevice == null || device.proxyDevice.host != pp.host)
                    {
                        device.loadNewProxy = true;
                    }
                    order.proxy = pp;
                    break;
                }
                else if (order.proxyFromLocal)
                {
                    //LogStatus(device, "Get proxy from Local");

                    //Proxy pp = GetProxyFromLocal(device);
                    //if (pp != null)
                    //{

                    //    Device.EnableWifi(deviceID);
                    //    device.keyProxy = ":" + pp.host + ":" + pp.port + ":" + pp.key;
                    //    order.hasproxy = true;
                    //    if (device.proxyDevice == null || device.proxyDevice.host != pp.host)
                    //    {
                    //        device.loadNewProxy = true;
                    //    }
                    //    order.proxy = pp;
                    //}
                    //else
                    //{
                    //    device.keyProxy = "";
                    //    Device.DisableWifi(deviceID);
                    //}
                    //break;
                }
                else
                {
                    StopProxySuper(device, order);
                    FbUtil.ChangeIpByAirplane(device);
                    return true;
                    //device.keyProxy = "";
                    //if ((wwProxyradioButton.Checked || tunProxyradioButton.Checked || order.proxyFromServer) && device.proxyDevice != null && device.proxyDevice.hasProxy)
                    //{
                    //    order.hasproxy = true;
                    //}
                    //else
                    //{
                    //    Device.DisableWifi(deviceID);
                    //}
                    //break;
                }

                string ssid = Device.GetWifiStatus(deviceID);

                if (!ssid.Contains("unknown") && order != null && order.proxy == null)
                {
                    LogStatus(device, "Máy không có proxy mà lại chạy Wifi - chạy lại", 3000);
                    continue;
                }
                if (order != null && order.proxy != null && order.proxy.hasProxy)
                {
                    if (device.currentProxyCount > 0)
                    {
                        LogStatus(device, "Giữ lại port proxy - Check proxy running");

                        Device.OpenApp(deviceID, "com.scheler.superproxy");
                        if (!CheckTextExist(deviceID, "stopcheckable", 5))
                        {
                            LogStatus(device, "Không thấy nút stop - khong start proxy duoc proxy - set proxy lỗi", 6000);
                            if (!StartProxy(order, device, proxy4g, forceChangeWifi, randomWifi))
                            {
                                // Device.DisableWifi(deviceID);
                                CacheServer.deleteKeyProxy(PublicData.ServerCacheMail, order);
                                StopProxySuper(device, order);
                                device.keyProxy = "";
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        if (!StartProxy(order, device, proxy4g, forceChangeWifi, randomWifi))
                        {
                            // Device.DisableWifi(deviceID);
                            CacheServer.deleteKeyProxy(PublicData.ServerCacheMail, order);
                            StopProxySuper(device, order);
                            device.keyProxy = "";
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }


                //if (order != null && order.proxy != null && order.proxy.hasProxy && device.currentPublicIp.Length < 5)
                //{

                //    Device.DisableWifi(deviceID);
                //    CacheServer.deleteKeyProxy(serverCacheMailTextbox.Text, order);
                //    device.keyProxy = "";
                //    LogStatus(device, "Set proxy không có mạng --------", 2000);
                //    continue;
                //}
                if (order != null && order.proxy != null && !string.IsNullOrEmpty(order.proxy.key))
                {
                    break;
                }
            }
            return false;
        }
        public static void PrepareForClone(DeviceObject device)
        {
            LogStatus(device, "Bắt đầu chuẩn bị device - trước khi start proxy");
            string deviceId = device.deviceId;
            //Console.WriteLine($"🧼 Đang reset thiết bị {deviceId} trước khi reg clone...");
            Device.ClearCache(deviceId, Constant.FACEBOOK_PACKAGE);
            Device.ClearCache(deviceId, Constant.FACEBOOK_LITE_PACKAGE);
            //// 2. Clear GMS (Google Services) để reset Google Ad ID
            RunAdb(deviceId, "shell su -c \"pm clear com.google.android.gms\"");
            //Thread.Sleep(800); // Delay nhẹ cho thiết bị phản ứng
            // 3. Xóa Android ID (Android sẽ tự tạo lại sau reboot)
            RunAdb(deviceId, "shell su -c \"settings delete secure android_id\"");
            // RunAdb(deviceID, "shell settings delete secure android_id"); // delete android id
            Device.RandomAndroidID(deviceId);
            Thread.Sleep(800); // Delay nhẹ cho thiết bị phản ứng
            // 4. Xóa cache hệ thống
            RunAdb(deviceId, "shell su -c \"rm -rf /data/cache/* /data/local/tmp/* /data/system/dropbox/*\"");
            //Thread.Sleep(800); // Delay nhẹ cho thiết bị phản ứng
            //// 5. Xóa ảnh, video, avatar cũ
            RunAdb(deviceId, "shell su -c \"rm -rf /sdcard/DCIM/* /sdcard/Pictures/*\"");
            //Thread.Sleep(800); // Delay nhẹ cho thiết bị phản ứng
            //// 6. Reset Wi-Fi, proxy, DNS
            RunAdb(deviceId, "shell su -c \"svc wifi disable\"");
            //Thread.Sleep(800); // Delay nhẹ cho thiết bị phản ứng
            RunAdb(deviceId, "shell su -c \"rm -rf /data/misc/wifi\"");
            //Thread.Sleep(800); // Delay nhẹ cho thiết bị phản ứng
            RunAdb(deviceId, "shell su -c \"settings put global http_proxy :0\"");
            Thread.Sleep(800); // Delay nhẹ cho thiết bị phản ứng
            // 7. Gợi ý reboot sau chuẩn bị
            if (device.needRebootAfterClear)
            {
                //Console.WriteLine("🔁 Đang khởi động lại thiết bị...");
                Device.RebootByCmd(deviceId);
            }
            LogStatus(device, "Kết thúc chuẩn bị device");
        }
        public static void ClearCacheFb(DeviceObject device)
        {
            LogStatus(device, "Bắt đầu Clear cache fb ------------");
            string deviceID = device.deviceId;
            
            Device.ForceStop(deviceID, Constant.FACEBOOK_PACKAGE);
            Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);

            Thread.Sleep(500);

            RunAdb(deviceID, "shell rm -rf /sdcard/Android/data/com.facebook.katana");

            RunAdb(deviceID, "shell rm -rf /data/data/com.facebook.katana/*");
            RunAdb(deviceID, "shell rm -rf /data/data/com.facebook.lite/*");

            // 1. Clear Facebook & Messenger

            Thread.Sleep(300);
            Device.ClearCache(deviceID, Constant.FACEBOOK_BUSINESS_PACKAGE);
            
            Thread.Sleep(300);
            Device.ClearCache(deviceID, Constant.MESSENGER_PACKAGE);
            Thread.Sleep(300);

            Thread.Sleep(300);
            Device.ClearCache(deviceID, Constant.FACEBOOK_LITE_PACKAGE);
            Thread.Sleep(300);

            //Device.ClearCache(deviceID, "com.instagram.android");

            //Device.ClearCache(deviceID, "com.android.vending");

            //KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell pm grant com.facebook.katana android.permission.READ_CONTACTS");
            //KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell pm grant com.facebook.katana android.permission.CALL_PHONE");
            //KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell pm grant com.facebook.katana android.permission.READ_PHONE_STATE");

            // 2. Clear Google Play Services (optional)
            //RunAdb(deviceID, "shell pm clear com.google.android.gms");

            // 3. Reset Android ID (random 16-char hex)
            //string newAndroidId = Guid.NewGuid().ToString("N").Substring(0, 16);
            //RunAdb(deviceID, $"shell settings put secure android_id {newAndroidId}");



            //KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell su -c rm -r /data/system/graphicsstats/1658361600000/com.facebook.katana");
            //KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell su -c rm -r /data/system/graphicsstats/1658448000000/com.facebook.katana");
            //KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell su -c rm -r /data/system/graphicsstats/1658534400000/com.facebook.katana");
            //KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell su -c rm -r /data/system/graphicsstats");
            //KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell su -c rm -r /data/system/package_cache");

            //KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell su -c rm -r /Android/media/com.facebook.katana");
            //KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell su -c rm -r /Android/media/com.facebook.lite");
            //KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell su -c rm -r /Android/media/com.facebook.orca");

            //KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell su -c rm -r /mnt/user/0/primary/Android/data/com.facebook.katana");
            //KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell su -c rm -r /mnt/user/0/primary/Android/data/com.facebook.lite");
            //KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell su -c rm -r /mnt/user/0/primary/Android/data/com.facebook.orca");

            //KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell su -c rm -r /mnt/user/0/primary/Android/media/com.facebook.katana");
            //KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell su -c rm -r /mnt/user/0/primary/Android/media/com.facebook.lite");
            //KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell su -c rm -r /mnt/user/0/primary/Android/media/com.facebook.orca");

            //KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell su -c rm -rf /data/system_ce/0/launch_params/com.facebook.katana_.activity.FbMainTabActivity.xm");

            LogStatus(device, "Kết thúc Clear cache fb ------------");
        }

        
        public static bool OpenFacebookApp2Login(DeviceObject device, bool clearFbLite, bool regNormal = false)
        {
            
            string deviceID = device.deviceId;

            if (device.clearCache)
            {
                device.clearCacheLite = false;
            }

            Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);

            for (int i = 0;i < 20; i ++)
            {
                string dddd = GetUIXml(device.deviceId);
                if (CheckTextExist(deviceID, new string[] { "tạotàikhoảnmớicheckable", "createnewAccountCheckable" }))
                {
                    break;
                }
            }

            if ( WaitAndTapXML(deviceID, new string[] { "tạotàikhoảnmớicheckable", "createnewAccountCheckable" }))
            {
                string xmlCheck;
                bool check = false;
                for (int i = 0;i < 10; i ++)
                {
                    if (CheckTextExist(deviceID, new string[] { "bắt đầu", "nodeindex0textđồngý&amp;tiếptụcresourceidclassandroidviewviewpackagecomfacebookkatanacontentdescđồngý&amp;tiếptụccheckable" }))
                    {
                        check = true;
                        break;
                    }
                }
                if (check)
                {
                    Device.Back(deviceID);
                    for (int i = 0; i < 2; i ++)
                    {
                        xmlCheck = GetUIXml(deviceID);
                        if (CheckTextExist(deviceID, "tạotàikhoảnmớicheckable", 1, xmlCheck))
                        {

                            if (WaitAndTapXML(deviceID, 1, "englishusresourceid", xmlCheck))
                            {
                                if (WaitAndTapXML(deviceID, 5, "tiếngviệtcheckable"))
                                {
                                    //Thread.Sleep(1000);
                                }
                            }
                            if (WaitAndTapXML(deviceID, new string[] { "descthayđổicàiđặtcheckable", "textthayđổicàiđặtresourceid" }, xmlCheck))
                            {
                                WaitAndTapXML(deviceID, 10, "desckhôngchiasẻcheckable");
                                Thread.Sleep(2000);
                            }
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    
                }
                if (CheckTextExist(deviceID, "Bạn tên gì", 1))
                {
                    Device.Back(deviceID);
                    if (CheckTextExist(deviceID, "Bạn tên gì", 2))
                    {
                        Device.Back(deviceID);
                    }

                    if (CheckTextExist(deviceID, "tạotàikhoảnmớicheckable", 2))
                    {
                        return true;
                    } else
                    {
                        return false;
                    }
                } else
                {
                    return false;
                }
                if (CheckTextExist(deviceID, "khônghiểnthị", 2, xmlCheck))
                {
                    return false;
                }
            } else
            {
                return false;
            }

            Thread.Sleep(1000);
            string uiXML = GetUIXml(deviceID);
            if (CheckTextExist(deviceID, "mởbằngfacebook", 1, uiXML))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 84.7, 78.1);
                Thread.Sleep(2000);
            }
            if (CheckTextExist(deviceID, "mởbằnglite", 1, uiXML))
            {
                FindImageAndTap(deviceID, CHOOSE_FB, 1);
                Thread.Sleep(2000);
            }
            if (CheckTextExist(deviceID, "phản hồi", 1))
            {
                return false;
            }

            return Utility.CheckFacebookOpen(deviceID, regNormal);
        }
        public static bool OpenFacebookAppDoiTen(DeviceObject device, bool clearFbLite, bool regNormal = false)
        {

            string deviceID = device.deviceId;

            if (device.clearCache)
            {
                device.clearCacheLite = false;
            }

            Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);

            if (CheckTextExist(deviceID, "tạotàikhoảnmớicheckable", 30))
            {

                return true;

            }

            //Device.GotoFbRegister(deviceID);
            //Device.TapByPercent(deviceID, 44, 88);
            Thread.Sleep(1000);
            string uiXML = GetUIXml(deviceID);
            if (CheckTextExist(deviceID, "mởbằngfacebook", 1, uiXML))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 84.7, 78.1);
                Thread.Sleep(2000);
            }
            if (CheckTextExist(deviceID, "mởbằnglite", 1, uiXML))
            {
                FindImageAndTap(deviceID, CHOOSE_FB, 1);
                Thread.Sleep(2000);
            }
            if (CheckTextExist(deviceID, "phản hồi", 1))
            {
                return false;
            }

            return Utility.CheckFacebookOpen(deviceID, regNormal);
        }
        public static bool OpenFacebookAppRegnormal(DeviceObject device, bool clearFbLite, bool moiKatana, bool fast)
        {
            LogStatus(device, "Bắt đầu mở app fb normal");
            string deviceID = device.deviceId;

            if (device.clearCache)
            {
                device.clearCacheLite = false;
            }

            string uixml;
            Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
            Thread.Sleep(10000);
            WaitAndTapXML(deviceID, 1, "cho phép re");
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 47.3, 88.0);
            if (!moiKatana)
            {
               
                for (int i = 0; i < 10; i++)
                {
                    uixml = GetUIXml(deviceID);
                    WaitAndTapXML(deviceID, new string[] { "thửlạiresourceid", "thử lại" }, uixml);
                    if (WaitAndTapXML(deviceID, 1, "facebookresourceid", uixml))
                    {
                        if (WaitAndTapXML(deviceID, 1, "luônchọnresourceid"))
                        {
                            break;
                        }
                    }
                    if (CheckTextExist(deviceID, new string[] { "tạo tài khoản", "tạotàikhoảnmớicheckable", "tạo tài khoản facebook mới", "createnewAccountCheckable" }, uixml))
                    {
                        break;
                    }
                    
                    if (CheckTextExist(deviceID, new string[] {  "Đăng nhập bằng tài khoản khác", "tạotin", "chưaxem", "làmbạn", "lúc khác" , "thêm ảnh", "bỏ qua"}))
                    {
                        Device.GotoFbRegister(deviceID);// Device.GotoFbRegister(deviceID);
                        Thread.Sleep(2000);
                        if (CheckTextExist(deviceID, new string[] {"tiếp", "next" }))
                        {
                            return true;
                        }
                    }
                    if (CheckTextExist(deviceID, new string[] { "tiếp", "next" }, uixml))
                    {
                        return true;
                    }
                }
                if (CheckTextExist(deviceID, "trangnàyhiệnkhônghiểnthị", 1))
                {
                    return false;
                }
            }  else
            {
                Device.GotoFbRegister(deviceID);// Device.GotoFbRegister(deviceID);
            }


            Device.Swipe(deviceID, 33, 1500, 55, 500);
            Device.Swipe(deviceID, 44, 1300, 66, 400);

            if (fast)
            {
                for (int i = 0; i < 20; i++)
                {
                    uixml = GetUIXml(deviceID);
                    if (WaitAndTapXML(deviceID, 1, "facebookresourceid", uixml))
                    {
                        if (WaitAndTapXML(deviceID, 1, "luônchọnresourceid"))
                        {
                            break;
                        }
                    }

                    if (WaitAndTapXML(deviceID,  new string[] { "đăngkýcheckable", "createnewAccountCheckable", "tạotàikhoản", "bắt đầu", 
                        "tạotàikhoảnmớicheckable", "tạotàikhoảnfacebookmớicheckable"  }, uixml))
                    {
                        break;
                    }
                    if (CheckTextExist(deviceID, "tiếp", 1, uixml))
                    {
                        return true;
                    }
                    if (CheckTextExist(deviceID, "next", 1, uixml))
                    {
                        return true;
                    }
                    if (CheckTextExist(deviceID, new string[] { "tạotin", "chưaxem", "làmbạn", "lúc khác" }))
                    {
                        Device.GotoFbRegister(deviceID);// Device.GotoFbRegister(deviceID);
                        Thread.Sleep(2000);
                        if (CheckTextExist(deviceID, new string[] { "tiếp", "next" }))
                        {
                            return true;
                        }
                    }
                }
                for (int k = 0; k < 20; k++)
                {
                    uixml = GetUIXml(deviceID);
                    if (WaitAndTapXML(deviceID, new string[] { "cho phép re", "cho phép", "tạotàikhoảncheckablefalsecheckedfalseclickablefal", "tạotàikhoảnmớicheckable", "tạotàikhoảncheckable", "tạo tài khoản", "get started", "tiếp", "next", "bắt đầu", "nodeindex0textđồngý&amp;tiếptụcresourceidclassandroidviewviewpackagecomfacebookkatanacontentdescđồngý&amp;tiếptụccheckable", "getstarted" }, uixml))
                    {
                        return true;
                    }

                    if (CheckTextExist(deviceID, new string[] { "refresh", "làm mới", "trangnàyhiệnkhônghiểnthịr" }, uixml))
                    {
                        return false;
                    }
                    if (CheckTextExist(deviceID, new string[] { "tạotin", "chưaxem", "làmbạn", "lúc khác" }))
                    {
                        Device.GotoFbRegister(deviceID);// Device.GotoFbRegister(deviceID);
                        Thread.Sleep(2000);
                        if (CheckTextExist(deviceID, new string[] { "tiếp", "next" }))
                        {
                            return true;
                        }
                    }
                }
            }

            if (CheckTextExist(deviceID, "trangnàyhiệnkhônghiểnthịr", 1))
            {
                return false;
            }
            for (int i = 0; i < 20; i ++)
            {
                uixml = GetUIXml(deviceID);
                if (WaitAndTapXML(deviceID, 1, "facebookresourceid", uixml))
                {
                    if (WaitAndTapXML(deviceID, 1, "luônchọnresourceid"))
                    {
                        break;
                    }
                }
                if (CheckTextExist(deviceID,  "tạotàikhoảnmớicheckable", 1, uixml)
                    || CheckTextExist(deviceID, "createnewAccountCheckable", 1, uixml))
                {
                    break;
                }

                else if (WaitAndTapXML(deviceID, new string[] { "cho phép re", "cho phép", "tạotàikhoảnmớicheckable", "tạotàikhoảncheckable", "tạo tài khoản", "get started", "next", "bắt đầu", "nodeindex0textđồngý&amp;tiếptụcresourceidclassandroidviewviewpackagecomfacebookkatanacontentdescđồngý&amp;tiếptụccheckable" }))
                {
                    return true;
                }
                if (CheckTextExist(deviceID, new string[] { "tạotin", "chưaxem", "làmbạn" }))
                {
                    Device.GotoFbRegister(deviceID);// Device.GotoFbRegister(deviceID);
                    Thread.Sleep(2000);
                    if (CheckTextExist(deviceID, new string[] { "tiếp", "next" }))
                    {
                        return true;
                    }
                }
            }

            if (WaitAndTapXML(deviceID, new string[] { "descthayđổicàiđặtcheckable", "textthayđổicàiđặtresourceid" }))
            {
                WaitAndTapXML(deviceID, 10, "desckhôngchiasẻcheckable");
                Thread.Sleep(5000);
            }
            
            
            if (WaitAndTapXML(deviceID, 3, "tạotàikhoảnmớicheckable")
                || WaitAndTapXML(deviceID, 1, "createnewAccountCheckable"))
            {
                if (WaitAndTapXML(deviceID, 10, "bắt đầu"))
                {
                    return true;
                } else if (WaitAndTapXML(deviceID, 1, "nodeindex0textđồngý&amp;tiếptụcresourceidclassandroidviewviewpackagecomfacebookkatanacontentdescđồngý&amp;tiếptụccheckable"))
                {
                    return true;
                } else if (WaitAndTapXML(deviceID, 1, "next"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            } else if (WaitAndTapXML(deviceID, 1, "đăng nhập bằng tài khoản khác checkable"))
            {
                Thread.Sleep(3000);
                uixml = GetUIXml(deviceID);

                if (WaitAndTapXML(deviceID, 1, "tạotàikhoảnmớicheckable", uixml)
                || WaitAndTapXML(deviceID, 1, "createnewAccountCheckable", uixml))
                {
                    if (WaitAndTapXML(deviceID, 10, "bắt đầu"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            } else
            {
                return false;
            }
            
            if (CheckTextExist(deviceID, Language.Next(), 1))
            {
                return true;
            }
            
            string uiXML = GetUIXml(deviceID);
            
            if (CheckTextExist(deviceID, "mởbằngfacebook", 1, uiXML))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 84.7, 78.1);
                Thread.Sleep(2000);
            }
            if (CheckTextExist(deviceID, "mởbằnglite", 1, uiXML))
            {
                FindImageAndTap(deviceID, CHOOSE_FB, 1);
                Thread.Sleep(2000);
            }
            if (CheckTextExist(deviceID, "phản hồi", 1))
            {
                return false;
            }
            Thread.Sleep(2000);
            LogStatus(device, "Kết thúc mở app fb normal");
            return Utility.CheckFacebookNormalOpen(deviceID, true);
        }
        public static bool OpenFacebookApp(string deviceID)
        {
         
            Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);

            Thread.Sleep(7000);
            return true;
        }

      
        public static void OpenFacebookAppVerify(DeviceObject device)
        {
            string deviceID = device.deviceId;
            
            if (device.clearCache)
            {
                device.clearCacheLite = false;
            }

            Thread.Sleep(1500);

            Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);

            Thread.Sleep(15000);
            
        }

        public static void OpenMessengerApp(string deviceID, CheckBox fbLiteCheckbox)
        {
            //Utility.Log("Delete cache data on facebook", status);

            if (fbLiteCheckbox.Checked)
            {
                //Device.ClearCache(deviceID, "com.facebook.lite");
            }
            else
            {
                Device.ClearCache(deviceID, Constant.MESSENGER_PACKAGE);
            }

            Thread.Sleep(3000);

            if (fbLiteCheckbox.Checked)
            {
                //Device.OpenApp(deviceID, "com.facebook.lite");
            }
            else
            {
                Device.OpenApp(deviceID, Constant.MESSENGER_PACKAGE);
            }
            

            Thread.Sleep(15000);
        }
        public static void ChangeIpA30(string deviceID) 
        {
            try
            {
                Device.Home(deviceID);
                //Thread.Sleep(3000);
               // Utility.Log("Turn on/off airplan mode", status);
                //Device.Swipe(deviceID, 500, 1, 500, 700, 500);
                Device.openNotificationBar(deviceID);
                Thread.Sleep(1500);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 72.9, 16.4);
                Thread.Sleep(3000);

                KAutoHelper.ADBHelper.TapByPercent(deviceID, 72.9, 16.4);
                Thread.Sleep(1000);
                //QLong.Phone.Về_Màn_Hình_Chính(deviceID);
                Thread.Sleep(2000);
                // Check airplan mode
                if (Device.IsAirPlaneMode(deviceID))
                {
                    //Device.Swipe(deviceID, 500, 1, 500, 700, 500);
                    Device.openNotificationBar(deviceID);
                    Thread.Sleep(1000);

                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 72.9, 16.4);
                    Thread.Sleep(2000);
                    
                }
                Device.Home(deviceID);
            }
            catch (Exception) { }
        }

       
        public static bool ChangeIpByAirplane(DeviceObject device)
        {
            try
            {
                string deviceID = device.deviceId;

                Device.AirplaneOn(deviceID);
                Thread.Sleep(1000);
                Device.AirplaneOff(deviceID);

                Device.DisableWifi(deviceID);
                Device.EnableMobileData(deviceID);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("airplane:" + e.Message);
                return false;
            }
        }

        public static Account GetAccNoveri(bool fromServer, string deviceID, string language, bool force)
        {
            Account acc = null;
            for (int i = 0; i < 30; i++)
            {
                //acc = GetAccNoveriLocal();
                if (acc == null && fromServer)
                {
                    acc = ServerApi.TryToGetAccNoveri(deviceID, language, force);
                }

                if (acc == null || string.IsNullOrEmpty(acc.uid))
                {
                    continue;
                }
                var validator = new FacebookSessionValidator();
                string result = validator.CheckTokenStatusAsync(acc.token).Result;
                Console.WriteLine(result);

                if (FbUtil.CheckLiveWall(acc.uid) == Constant.DIE)
                {
                    //using (StreamWriter HDD = new StreamWriter("FileCLone/Die_CheckLive" + ".txt", true))
                    //{
                    //    HDD.WriteLine(acc.note);
                    //    HDD.Close();
                    //}
                    acc = null;
                    continue;
                }
                else
                {
                    break;
                }
                if (force)
                {
                    i = 30;
                }
            }

            return acc;
        }

        public static Account GetAccWaitInfo(bool fromServer, string deviceID, string language)
        {
            Account acc = null;
            for (int i = 0; i < 30; i++)
            {
                //acc = GetAccReupFull();
                if (acc == null && fromServer)
                {
                    acc = ServerApi.TryToGetAccWaitInfo(deviceID, language);
                }

                if (acc == null || string.IsNullOrEmpty(acc.uid))
                {
                    continue;
                }

                if (FbUtil.CheckLiveWall(acc.uid) == Constant.DIE)
                {
                    //using (StreamWriter HDD = new StreamWriter("FileCLone/Die_CheckLive" + ".txt", true))
                    //{
                    //    HDD.WriteLine(acc.note);
                    //    HDD.Close();
                    //}
                    acc = null;
                    continue;
                } else
                {
                    break;
                }
            }

            return acc;
        }
        
        public static Account GetAccCheckLogin()
        {
            string fileCheckLogin = "check_login.txt";

            Account acc;
            int tryAgain = 2;

            for (int i = 0; i < tryAgain; i++)
            {
                acc = GetAccFromFile(fileCheckLogin);
                if (acc != null)
                {
                    
                    return acc;
                }
            }

            return null;
        }

      
        public static Account GetAccFromFile(string fileName)
        {
            string line = FileUtil.GetAndDeleteLine(fileName);
            if (string.IsNullOrEmpty(line))
            {
                return null;
            }
            string[] listData = line.Split('|');
            if (listData != null && listData.Length >= 2)
            {
                Account acc = new Account();
                acc.uid = listData[0];
                acc.pass = listData[1];


                if (listData.Length > 8)
                {
                    acc.cookie = listData[2];
                    acc.token = listData[3];
                    acc.gender = listData[8];
                    acc.qrCode = listData[4];
                    if (!string.IsNullOrEmpty(acc.qrCode))
                    {
                        acc.has2fa = true;
                    }
                    else
                    {
                        acc.has2fa = false;
                    }
                    acc.email = listData[5];
                    acc.emailPass = listData[6];
                }
                acc.language = listData[listData.Length - 2];





                if (listData[listData.Length - 1] == "True")
                {
                    acc.hasAvatar = true;
                }
                else
                {
                    acc.hasAvatar = false;
                }
                // todo
                //acc.language = Constant.LANGUAGE_VN;
                //acc.hasAvatar = false;
                //acc.gender = Constant.FEMALE;
                ////

                acc.note = line;
                return acc;
            }
            return null;
        }

        public static bool SetProxySockDroid(string deviceID, Proxy proxy)
        {
            Device.ClearCache(deviceID, "net.typeblog.socks");
            Device.KillApp(deviceID, "net.typeblog.socks");
            Thread.Sleep(1000);
            Device.OpenApp(deviceID, "net.typeblog.socks");
            Thread.Sleep(1500);
            if (!string.IsNullOrEmpty(proxy.username))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.5, 93.2);
            }
            

            RemoveProxyDroid(deviceID); // stop proxy


            string uixml = GetUIXml(deviceID);
            
            // Tap to proxy ip textbox
            WaitAndTapXML(deviceID, 1, "serveripresource", uixml);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 58.0, 50.6);
            
            Device.DeleteChars(deviceID, 15);
            InputText(deviceID, proxy.host, true);

            WaitAndTapXML(deviceID, 1, "ok");

            WaitAndTapXML(deviceID, 1, "serverportresource", uixml);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 58.0, 50.6);
            Device.DeleteChars(deviceID, 8);
            InputText(deviceID, proxy.port, true);

            WaitAndTapXML(deviceID, 1, "ok");

            Device.Swipe(deviceID, 200, 2000, 200, 1500);
            Thread.Sleep(1000);
            
            if (!string.IsNullOrEmpty(proxy.username) && WaitAndTapXML(deviceID, 1, "usernameresource"))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 80.0, 50.6);
                Device.DeleteChars(deviceID, 20);
                Device.SelectAdbKeyboard(deviceID);
                InputText(deviceID, proxy.username, true);
                WaitAndTapXML(deviceID, 1, "ok");
                Device.SelectLabanKeyboard(deviceID);
            }

            if (!string.IsNullOrEmpty(proxy.pass) && WaitAndTapXML(deviceID, 1, "passwordresource"))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 58.0, 50.6);
                Device.DeleteChars(deviceID, 20);
                Device.SelectAdbKeyboard(deviceID);
                InputText(deviceID, proxy.pass, false);

                WaitAndTapXML(deviceID, 1, "ok");
                Device.SelectLabanKeyboard(deviceID);
            }
           

            Thread.Sleep(1000);
            StartProxySockDroid(deviceID);
            WaitAndTapXML(deviceID, 1, "okresourceid");
            Thread.Sleep(3000);
            
            return true;
        }
       
        public static void RemoveProxyCollege(string deviceID)
        {
            Device.OpenApp(deviceID, "com.cell47.College_Proxy");
            Thread.Sleep(1500);
            WaitAndTapXML(deviceID, 1, "OK"); // OK
            Thread.Sleep(1000);
            WaitAndTapXML(deviceID, 1, "stopproxyservice");
            WaitAndTapXML(deviceID, 1, "cancel");
        }
        public static void RemoveProxySockDroid(string deviceID)
        {
            Device.OpenApp(deviceID, "net.typeblog.socks");
            Thread.Sleep(1500);
            WaitAndTapXML(deviceID, 1, "textbậtresource"); // OK
            Thread.Sleep(1000);
            
        }

        public static void StartProxySockDroid(string deviceID)
        {
            Device.OpenApp(deviceID, "net.typeblog.socks");
            Thread.Sleep(1500);
            if (!WaitAndTapXML(deviceID, 2, "texttắtresource"))
            {
                Device.KillApp(deviceID, "net.typeblog.socks");
                Thread.Sleep(1000);
                Device.OpenApp(deviceID, "net.typeblog.socks");
                Thread.Sleep(1500);
                if (CheckTextExist(deviceID, "texttắtresource", 2))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 84.9, 6.8);
                }
            }
            Thread.Sleep(5000);

        }
        public static void RemoveProxyDroid(string deviceID)
        {
            Device.OpenApp(deviceID, "org.proxydroid");
            Thread.Sleep(1500);
            TurnOffProxy(deviceID);
        }
       
        public static void TurnOffProxy(string deviceID)
        {
            if (Utility.CheckTextExist(deviceID, Language.TurnOnProxy()))
            {
                Utility.WaitAndTapXML(deviceID, 1, Language.TurnOnProxy());
            }

        }
        public static void ChangeAndroidID(string deviceID)
        {
            Device.OpenApp(deviceID, "com.liamw.root.androididchanger");
            Thread.Sleep(1000);
            Device.TapByPercent(deviceID, 59.6, 62.0); // don't like app
            Thread.Sleep(1000);
            Device.TapByPercent(deviceID, 92.9, 6.9); // New iD
            Thread.Sleep(1000);
            Device.TapByPercent(deviceID, 46.5, 21.9); // Save new id
        }
        public static string GetUid(string deviceID)
        {
            string uid = "";

            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(300);

                string cookies = GetCookieFromPhone(deviceID);

                uid = Regex.Match(cookies, "c_user=[0-9]{0,}").ToString();
                if (!string.IsNullOrEmpty(uid))
                {
                    break;
                }
            }
            if (string.IsNullOrEmpty(uid))
            {
                return "";
            }
            uid = uid.Replace("c_user=", "");
            return uid;
        }
        
        public static string ChangeEmu(DeviceObject device, string sim, string country = "")
        {

        //    string deviceID = device.deviceId;
        //    Device.Unlockphone(deviceID);
        //    Device.KillApp(deviceID, "com.device.emulator.pro");
        //    Device.OpenApp(deviceID, "com.device.emulator.pro");
        //    Thread.Sleep(2000);
        //    string result = "";
        //    if (sim == Constant.TURN_OFF_EMU)
        //    {
        //        string[] emus = { "imei", "deviceid", "adsid", "gsfid", "serial", "bmac", 
        //            "wmac", "wssid", "mobno", "subsid", "simcs", "emailid" };

        //        for (int i = 0; i < emus.Length; i ++)
        //        {
        //            string key = emus[i];
                    
        //            Utility.WaitAndTapXML(deviceID, 2, Language.TurnOnEMU(key));
                    
        //            Thread.Sleep(200);
        //            Device.Swipe(deviceID, 500, 1500, 500, 1350);
        //        }
                
        //        goto FLASH_SIM;
        //    }
        //    if (sim == Constant.TURN_OFF_ALL)
        //    {
        //        string[] emus = { "imei", "deviceid", "adsid", "gsfid", "serial", "bmac",
        //            "wmac", "wssid", "mobno", "subsid", "simcs","operator", "emailid" };

        //        for (int i = 0; i < emus.Length; i++)
        //        {
        //            string key = emus[i];

        //            Utility.WaitAndTapXML(deviceID, 2, Language.TurnOnEMU(key));

        //            Thread.Sleep(200);
        //            Device.Swipe(deviceID, 500, 1500, 500, 1350);
        //        }

        //        goto FLASH_SIM;
        //    }
        //    if (sim == Constant.TURN_ON_EMU)
        //    {
        //        string[] emus = { "imei", "deviceid", "adsid", "gsfid", "serial", "bmac",
        //            "wmac", "wssid", "mobno", "subsid", "simcs", "emailid" };

        //        for (int i = 0; i < emus.Length; i++)
        //        {
        //            string key = emus[i];

        //            Utility.WaitAndTapXML(deviceID, 2, Language.TurnOffEMU(key));

        //            Thread.Sleep(200);
        //            Device.Swipe(deviceID, 500, 1500, 500, 1350);
        //        }

        //        goto FLASH_SIM;
        //    }

        //    if (sim == Constant.TURN_ON_ALL)
        //    {
        //        string[] emus = { "imei", "deviceid", "adsid", "gsfid", "serial", "bmac",
        //            "wmac", "wssid", "mobno", "subsid", "simcs","operator", "emailid" };

        //        for (int i = 0; i < emus.Length; i++)
        //        {
        //            string key = emus[i];

        //            Utility.WaitAndTapXML(deviceID, 2, Language.TurnOffEMU(key));

        //            Thread.Sleep(200);
        //            Device.Swipe(deviceID, 500, 1500, 500, 1350);
        //        }

        //        goto FLASH_SIM;
        //    }



        //    Device.Swipe(deviceID, 500, 1500, 500, 500);
        //    Thread.Sleep(3000);

        //    //Utility.WaitAndTapXML(deviceID, 2, Language.TurnOffOperatorSim());
        //    if (sim == Constant.TURN_ON_SIM_SUBCRIBE)
        //    {
        //        if (Utility.CheckTextExist(deviceID, Language.TurnOnEMU("subsid")))
        //        {
        //            return result;
        //        }
        //        Utility.WaitAndTapXML(deviceID, 2, Language.TurnOffEMU("subsid"));
        //        goto FLASH_SIM;
        //    }
        //    if (sim == Constant.TURN_OFF_SIM)
        //    {
        //        if (Utility.CheckTextExist(deviceID, Language.TurnOffOperatorSim()))
        //        {
        //            return result;
        //        }
        //        Utility.WaitAndTapXML(deviceID, 2, Language.TurnOnOperatorSim());
        //        goto FLASH_SIM;
        //    }

        //    if (sim == Constant.TURN_ON_SIM)
        //    {
        //        string uixml = GetUIXml(deviceID);
        //        // Find Vietnam sim
        //        if (!CheckTextExist(deviceID, "việtnam", 1, uixml))
        //        {
        //            Device.Swipe(deviceID, 396, 2000, 381, 1000);
        //            Thread.Sleep(2000);
        //            KAutoHelper.ADBHelper.TapByPercent(deviceID, 64.5, 69.9); // click edit
        //            Thread.Sleep(1500);
        //            KAutoHelper.ADBHelper.TapByPercent(deviceID, 33.2, 48.1); // dropdown box
        //            Thread.Sleep(1000);
        //            bool found = false;
        //            for (int i = 0; i < 44; i++)
        //            {
        //                uixml = GetUIXml(deviceID);

        //                if (WaitAndTapXML(deviceID, 1, "việtnam", uixml))
        //                {
        //                    Thread.Sleep(1000);
        //                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 82.4, 54.4);
        //                    Thread.Sleep(1000);
        //                    Utility.WaitAndTapXML(deviceID, 2, "textviettelResource");
        //                    Thread.Sleep(1000);
        //                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 81.0, 68.2);
        //                    found = true;
        //                    break;
        //                }

        //                Device.Swipe(deviceID, 531, 2324, 544, 2000);
        //            } 

        //            if (!found)
        //            {
        //                for (int i = 0; i < 44; i++)
        //                {
        //                    uixml = GetUIXml(deviceID);

        //                    if (WaitAndTapXML(deviceID, 1, "việtnam", uixml))
        //                    {
        //                        Thread.Sleep(1000);
        //                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 82.4, 54.4);
        //                        Thread.Sleep(1000);
        //                        Utility.WaitAndTapXML(deviceID, 2, "textviettelResource");
        //                        Thread.Sleep(1000);
        //                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 81.0, 68.2);
        //                        found = true;
        //                        break;
        //                    }

        //                    Device.Swipe(deviceID, 531, 2000, 544, 2300);
        //                }
        //            }
        //        }
        //        uixml = GetUIXml(deviceID);
        //        if (!CheckTextExist(deviceID, "việtnam", 1, uixml))
        //        {
        //            return result;
        //        }
        //        Utility.WaitAndTapXML(deviceID, 2, Language.TurnOffOperatorSim());
        //        goto FLASH_SIM;
        //    }

        //    if (sim == Constant.RANDOM_COUNTRY)
        //    {
        //        string uixml = GetUIXml(deviceID);

        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 64.5, 69.9); // click edit
        //        Thread.Sleep(1500);
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 82.4, 47.7); // dropdown box
        //        Thread.Sleep(1000);
        //        if (string.IsNullOrEmpty(country))
        //        {
        //            KAutoHelper.ADBHelper.TapByPercent(deviceID, 27.1, 95.7); // random contry
        //        } else
        //        {
        //            bool found = false;
        //            for (int i = 0; i < 44; i++)
        //            {
        //                uixml = GetUIXml(deviceID);

        //                if (WaitAndTapXML(deviceID, 1, country, uixml))
        //                {
        //                    Thread.Sleep(1000);
                            
        //                    found = true;
        //                    break;
        //                }

        //                Device.Swipe(deviceID, 531, 2324, 544, 2000);
        //            }

        //            if (!found)
        //            {
        //                for (int i = 0; i < 44; i++)
        //                {
        //                    uixml = GetUIXml(deviceID);

        //                    if (WaitAndTapXML(deviceID, 1, country, uixml))
        //                    {
        //                        Thread.Sleep(1000);
                                
        //                        break;
        //                    }

        //                    Device.Swipe(deviceID, 531, 2000, 544, 2300);
        //                }
        //            }
        //        }
                

        //        Thread.Sleep(1000);
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 78.5, 68.6);// save
        //        Thread.Sleep(1000);
        //        Utility.WaitAndTapXML(deviceID, 2, Language.TurnOffOperatorSim());
        //        goto FLASH_SIM;
        //    }
            
        //    if (Utility.CheckTextExist(deviceID, Language.TurnOffOperatorSim()))
        //    {
        //        Device.RebootDevice(deviceID);
        //        return result;
        //    }
        //    // Edit sim
        //    Device.TapByPercent(deviceID, 64.9, 69.9); // Tap to edit sim
        //    if (sim == Constant.US_PHONE)
        //    {
        //        Thread.Sleep(500);
        //        if (Utility.WaitAndTapXML(deviceID, 2, "vietnamresourceid"))
        //        {
        //            Device.Swipe(deviceID, 396, 1487, 381, 2011);
        //            //Thread.Sleep(1000);
        //            //Device.Swipe(deviceID, 396, 1487, 381, 2011);
        //            Utility.WaitAndTapXML(deviceID, 2, "unitedstatesresourceid");
        //            // Save
        //            Utility.WaitAndTapXML(deviceID, 5, "saveResource");
        //            Thread.Sleep(2000);
        //            goto FLASH_SIM;
        //        } else
        //        {
        //            return result;
        //        }
        //    }
        //    if (sim == Constant.VIET_PHONE)
        //    {
        //        Thread.Sleep(500);
        //        if (Utility.WaitAndTapXML(deviceID, 2, "vietnamresourceid"))
        //        {
        //            return result;
        //        }
        //        else if (Utility.WaitAndTapXML(deviceID, 2, "unitedstatesresourceid"))
        //        {
        //            Utility.WaitAndTapXML(deviceID, 2, "vietnamresourceid");
        //            // Save
        //            Utility.WaitAndTapXML(deviceID, 5, "saveResource");
        //            Thread.Sleep(2000);
        //            goto FLASH_SIM;
        //        }
        //    }


            

        //    KAutoHelper.ADBHelper.TapByPercent(deviceID, 69.9, 72.8);
        //    Thread.Sleep(2000);
        //    // Select  network
        //    Device.TapByPercent(deviceID, 30.9, 52.3);
            
        //    Thread.Sleep(2000);
        //    // Swipe to end list
        //    Device.Swipe(deviceID, 500, 2200, 500, 1500);
        //    Thread.Sleep(1500);
            
        //    if (sim == Constant.VIETTEL)
        //    {
        //        // Select Viettel
               
        //        Utility.WaitAndTapXML(deviceID, 2, "textviettelResource");
        //        //Device.TapByPercent(deviceID, 26.9, 55.1);

        //    } else if (sim == Constant.VINAPHONE)
        //    {
        //        Utility.WaitAndTapXML(deviceID, 2, "textvinaphone");
                
        //        //Device.TapByPercent(deviceID, 30.2, 70.7);
        //    }
        //    else if (sim == Constant.VIETNAM_MOBILE)
        //    {
        //        Device.TapByPercent(deviceID, 33.9, 86.3);
        //    }
        //    else if (sim == Constant.VIETTEL_MOBILE)
        //    {
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 25.5, 60.5);
        //    }
        //    else if (sim == Constant.VN_MOBIPHONE)
        //    {
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 27.3, 75.5);
        //    }
        //    else if (sim == Constant.VN_VINAPHONE)
        //    {
        //        Utility.WaitAndTapXML(deviceID, 2, "textvnvinaphone");
        //        //KAutoHelper.ADBHelper.TapByPercent(deviceID, 27.8, 92.2);
        //    }
        //    else if (sim == Constant.MOBI)
        //    {
        //        Device.TapByPercent(deviceID, 34.8, 81.4);
        //    } else if (sim == Constant.BEELINE)
        //    {
        //        Device.Swipe(deviceID, 500, 1500, 500, 2200);
        //        Thread.Sleep(1500);
        //        Device.TapByPercent(deviceID, 26.9, 55.1);
        //    } else if (sim == Constant.VIETTEL_TELECOM)
        //    {
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 32.0, 65.3);
        //    }
        //    Thread.Sleep(2000);

        //    // Save
        //    Utility.WaitAndTapXML(deviceID, 5, "saveResource");
        //    Thread.Sleep(2000);

        //    if (!string.IsNullOrEmpty(device.devicePhone))
        //    {
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 61.7, 21.9); // edit phone
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 74.1, 50.0); // cuối
        //        Device.DeleteChars(deviceID, 12);
        //        InputText(deviceID, device.devicePhone, true);
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 81.9, 35.4); // save
        //    }
           


        //FLASH_SIM:
            
        //    if (Utility.CheckTextExist(deviceID, Language.TurnOffOperatorSim()))
        //    {
        //        result = Constant.TURN_OFF_SIM;
        //    }
               
        //     if (Utility.CheckTextExist(deviceID, Language.TurnOnOperatorSim()))
        //    {
        //        result = Constant.TURN_ON_SIM;
        //    }
               

        //    // Flash seting
        //    Device.TapByPercent(deviceID, 84.7, 7.2);
        //    Thread.Sleep(7000);
        //    KAutoHelper.ADBHelper.TapByPercent(deviceID, 77.9, 89.8);
        //    Device.TapByPercent(deviceID, 82.4, 93.6); // press ok

            Device.RebootDevice(device.deviceId);

            return "";
        }
       
        public static void FlashSim(string deviceID)
        {
            Device.RebootDevice(deviceID);

            Thread.Sleep(1000);

            Device.OpenApp(deviceID, "com.device.emulator.pro");
            Thread.Sleep(2000);

            Thread.Sleep(1000);
            // Flash seting
            Device.TapByPercent(deviceID, 84.7, 7.2);
            Thread.Sleep(7000);
            Device.TapByPercent(deviceID, 82.4, 93.6); // press ok
            Thread.Sleep(6000);
            Device.RebootDevice(deviceID);

        }

        public static string CheckLiveWallFromDevice(DeviceObject device, OrderObject order)
        {
            if (string.IsNullOrEmpty(order.uid))
            {
                order.uid = GetUid(device.deviceId);
            }
            
            return CheckLiveWall(order.uid);
        }
        public static string CheckLiveWall(string uid)
        {
            RequestXNet requestXNet = new RequestXNet("", "", "", 0);
            try
            {
                string text = requestXNet.RequestGet("https://graph.facebook.com/" + uid + "/picture?redirect=false");
                if (!string.IsNullOrEmpty(text))
                {
                    if (text.Contains("height") && text.Contains("width"))
                    {
                        return Constant.LIVE;
                    }
                    return Constant.DIE;
                }
            }
            catch (Exception ex)
            {
                return Constant.UNKNOWN;
            }
            return Constant.UNKNOWN;
        }

        public static void PostStatus(string deviceID)
        {
            BackToFbHome(deviceID);
            Random rn = new Random();
            if (!Utility.WaitAndTapXML(deviceID, 2, "viếtbàitrênfacebook"))
            {
                return;
            }
            //KAutoHelper.ADBHelper.TapByPercent(deviceID, 28.5, 21.5);

            Utility.WaitAndTapXML(deviceID, 2, "xong");
            string status = Constant.statusOfFb[rn.Next(0, Constant.statusOfFb.Length - 1)];
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 15.8, 28.3);
            Thread.Sleep(500);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 25.3, 24.5); // Tap to text
            Thread.Sleep(500);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 25.3, 24.5); // Tap to text

            Utility.InputVietVNIText(deviceID,  status);
            Thread.Sleep(1000);
            
            int x = rn.Next(15, 80);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, x, 45.6);
            Thread.Sleep(1000);
            if (!Utility.WaitAndTapXML(deviceID, 2, "Đăng"))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 93.3, 7.0);
            }// Đăng
            Thread.Sleep(2000);
        }
        public static void AddFriend(string deviceID)
        {
            for (int i = 0; i < 2; i ++)
            {
                // Add friend
                if (Utility.CheckTextExist(deviceID, Language.AddFriend()))
                {
                    Utility.WaitAndTapXML(deviceID, 2, Language.AddFriend());
                    Utility.WaitAndTapXML(deviceID, 2, Language.AddFriend());
                    //Utility.WaitAndTapXML(deviceID, 2, "AddFriend");
                    return;
                }
                
                Device.Swipe(deviceID, 500, 2150, 500, 903, 500);
            }
            
        }

        public static void JoinGroup(string deviceID)
        {
            for (int i = 0; i < 3; i++)
            {
                // Add friend
                if (Utility.CheckTextExist(deviceID, "join"))
                {
                    Utility.WaitAndTapXML(deviceID, 2, "join");
                    Utility.WaitAndTapXML(deviceID, 2, "join");
                    Utility.WaitAndTapXML(deviceID, 2, "join");
                    return;
                }

                Device.Swipe(deviceID, 500, 2150, 500, 903, 500);
            }

        }
        public static void AddProfileDetal(string deviceID, string title, string item)
        {
            Utility.WaitAndTapXML(deviceID, 3, title);
            Thread.Sleep(1000);

            Utility.InputVietVNIText(deviceID, item);
            Thread.Sleep(1000);
            if (Utility.CheckTextExist(deviceID, "tìm thấy"))
            {
                for (int i = 0; i < item.Length; i++)
                {
                    Device.DeleteChar(deviceID);
                    Thread.Sleep(200);
                    if (!CheckTextExist(deviceID, "tìm thấy"))
                    {
                        break;
                    }
                }
            }
            Thread.Sleep(500);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 26.3, 20.1);
            if (!Utility.WaitAndTapXML(deviceID, 1, Language.Save()))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 78.0, 95.0);
            }
            Thread.Sleep(2000);
        }
        public static void UploadAvatarProfile3(string deviceID, OrderObject order)
        {
            Device.PushAvatar(deviceID, order); // Reveri acc 
            

            Device.GotoFbProfile(deviceID);
            for (int i = 0; i < 10; i++)
            {
                if (CheckTextExist(deviceID, "bắt đầu", 1))
                {
                    Device.Back(deviceID);
                    if (WaitAndTapXML(deviceID, 1, "textdừngresourcei"))
                    {
                        break;
                    }
                }

            }

            Thread.Sleep(1000);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 52.1, 18.7); // tap to ảnh bìa
            WaitAndTapXML(deviceID, 1, "Tải ảnh lên");
            Thread.Sleep(500);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.7, 7.1); // tap 'tai anh len'
            Thread.Sleep(2000);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 9.8, 87.6); // tap to galary
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 8.9, 95.2);
            Thread.Sleep(1000);
            if (!WaitAndTapXML(deviceID,1, "cho phép"))
            {
                Device.TapByPercent(deviceID, 51.8, 57.1, 1000);
            }
            Device.Back(deviceID);
            Device.Back(deviceID);
            Device.Back(deviceID);
            WaitAndTapXML(deviceID, 2, "descảnhđạidiệncheckable");
            WaitAndTapXML(deviceID, 2, "chọn ảnh đại diện");


            KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.5, 24.0); // chọn ảnh

            if (!CheckTextExist(deviceID, Language.Save(), 1)) {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 14.7, 31.3);
            } 
           
            Thread.Sleep(500);
            WaitAndTapXML(deviceID, 2, Language.Save());

            Thread.Sleep(5000);

        }
        public static bool UploadAvatarNormal(string deviceID, OrderObject order)
        {
            Device.PushAvatar(deviceID, order); // Reveri acc 
            if (!WaitAndTapXML(deviceID, 2, "allow access"))
            {
                FindImageAndTap(deviceID, CHO_PHEP_TRUY_CAP, 1);
                WaitAndTapXML(deviceID, 3, Language.AllowAll());
            } else
            {
                WaitAndTapXML(deviceID, 3, "Allowresource");
            }
            
            
            WaitAndTapXML(deviceID, 2, Language.AllowAll());
            Thread.Sleep(1500);
            Device.TapByPercent(deviceID, 83.5, 21.2, 1500); // tap to Thư Viện
            
            Device.TapByPercent(deviceID, 24.8, 21.6, 1700); // tap image
            
            Device.TapByPercent(deviceID, 15.3, 18.9, 2000); // tap image again
            if (FindImageAndTap(deviceID, BAT_DANH_BA, 1))
            {
                
                WaitAndTapXML(deviceID, 2, Language.AllowAll());

                if (CheckTextExist(deviceID, "đồng bộ"))
                {
                    for (int k = 0; k < 20; k++)
                    {
                        if (!CheckTextExist(deviceID, "đồng bộ"))
                        {
                            Console.WriteLine("đồng bộ:" + k);
                            break;
                        }
                    }
                }
            }

            if (!Utility.WaitAndTapXML(deviceID, 1, Language.Done()))
            {
                Device.TapByPercentDelay(deviceID, 56.8, 19.4);
                if (!Utility.WaitAndTapXML(deviceID, 1, Language.Done()))
                {
                    Device.TapByPercentDelay(deviceID, 82.9, 20.1);
                    Utility.WaitAndTapXML(deviceID, 1, Language.Done());
                } else
                {
                    Device.TapByPercentDelay(deviceID, 14.9, 18.7);
                }
            } else
            {
                Thread.Sleep(20000);
                return true;
            }
            
            Thread.Sleep(1000);
            //if (WaitAndTapXML(deviceID, 1, "thêmảnhcheckable"))
            //{
            //    WaitAndTapXML(deviceID, 3, "chọntừthưviệncheckable");
            //    goto UP_AVATAR_NORMAL;
            //}
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.9, 4.3);
            if (!WaitAndTapXML(deviceID, 2, "lưu"))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.0, 96.8);
            }

            Thread.Sleep(5000);
            for (int i = 0; i < 20; i ++)
            {
                if (!CheckTextExist(deviceID, "Đang tải lên"))
                {
                    break;
                }
            }
            return false;
        }
        public static void AddProfile(string deviceID)
        {
            Device.PushCover(deviceID);
            BackToFbHome(deviceID);
            Thread.Sleep(1000);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 6.2, 22.0);
            
            Utility.WaitAndTapXML(deviceID, 3, "bắt đầu");



            AddProfileDetal(deviceID, "chọnvịtríresourceid",
               cityList.OrderBy(x => Guid.NewGuid()).FirstOrDefault());

            AddProfileDetal(deviceID, "chọnvịtríresourceid",
               cityList.OrderBy(x => Guid.NewGuid()).FirstOrDefault());

            AddProfileDetal(deviceID, "tìm trường học resourceid",
                schoolList.OrderBy(x => Guid.NewGuid()).FirstOrDefault());

            AddProfileDetal(deviceID, "tìm trường đại học resourceid",
                universityList.OrderBy(x => Guid.NewGuid()).FirstOrDefault());

            AddProfileDetal(deviceID, "tìm kiếm công ty resourceid",
                companyList.OrderBy(x => Guid.NewGuid()).FirstOrDefault());

            Utility.WaitAndTapXML(deviceID, 2, "tình trạng mối quan hệ resourceid");
            Thread.Sleep(1000);

            KAutoHelper.ADBHelper.TapByPercent(deviceID, 81.8, 55.8);


            if (!Utility.WaitAndTapXML(deviceID, 1, "lưuresourceid"))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 78.0, 95.0);
            }

            // add cover
            Thread.Sleep(2000);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.3, 46.5);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.3, 46.5);
            Thread.Sleep(1000);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.3, 25.5);

            Thread.Sleep(1500);
            if (!Utility.WaitAndTapXML(deviceID, 2, Language.Save()))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.9, 7.3);
            }


            Thread.Sleep(5000);
            for(int i = 0; i < 10; i ++)
            {
                if (!Utility.CheckTextExist(deviceID, "ảnh bìa"))
                {
                    break;
                }
            }

            if (WaitAndTapXML(deviceID, 2, "texttiểusửresourceid"))
            {
                Thread.Sleep(500);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 25.1, 35.6);
                string description = descriptionList.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                InputVietVNIText(deviceID, description);
                Thread.Sleep(1000);
                if (!WaitAndTapXML(deviceID, 1, "Lưu"))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.6, 6.8);
                }
            }

            if (!Utility.WaitAndTapXML(deviceID, 2, "bỏ qua"))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 51.5, 94.8);
            }
            Thread.Sleep(500);
            if (!Utility.WaitAndTapXML(deviceID, 2, "bỏ qua"))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 51.5, 94.8);
            }
            if (!Utility.WaitAndTapXML(deviceID, 2, "xem trang cá nhân của bạn"))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 51.5, 94.8); // xem trang cá nhân
            }

            BackToFbHome(deviceID);
        }

        public static void AddMiniProfile(string deviceID)
        {
            FbUtil.BackToFbHome(deviceID);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 5.6, 21.3);  // Touch to avatar
            Thread.Sleep(2000);
            if (!Utility.WaitAndTapXML(deviceID, 5, Language.Begin()))
            {
                GoToProfile(deviceID);
                Utility.WaitAndTapXML(deviceID, 5, Language.Begin());
            }
            // Add city
            Thread.Sleep(1000);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 37.8, 44.1);
            Thread.Sleep(500);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 64.9, 95.5);
            Thread.Sleep(4000);
            // Add Hometown
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 41.1, 43.8);
            Thread.Sleep(500);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 64.9, 95.5);
            Thread.Sleep(4000);
            Thread.Sleep(1000);
            // Add school
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 19.4, 44.2);
            Thread.Sleep(500);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 64.9, 95.5);
            Thread.Sleep(4000);

            // skipp
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 21.0, 95.3);
            Thread.Sleep(500);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 21.0, 95.3);
            Thread.Sleep(500);
            for (int i = 0; i < 3; i ++)
            {
                if (Utility.CheckTextExist(deviceID, Language.AddCover()))
                {
                    break;
                }
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 21.0, 95.3);
                Thread.Sleep(500);
            }
            
           
            AddCover(deviceID);
            FbUtil.BackToFbHome(deviceID);
        }

        public static void GoToProfile(string deviceID)
        {
            string cmd = string.Format("adb -s {0} shell am start -a android.intent.action.VIEW -d fb://profile", deviceID);
            Device.ExecuteCMD(cmd);
        }
        public static void AddSingleCover_(String deviceID)
        {
            //BackToFbHome(deviceID);

            Device.GotoFbProfileEdit(deviceID);
            Device.PushCover(deviceID);

            Utility.WaitAndTapXML(deviceID, 5, "thãªmáº£nhbã¬acheckable");

            Utility.WaitAndTapXML(deviceID, 3, Language.AllowAll());
            Thread.Sleep(1000);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 52.9, 23.8); //  choose cover image
            Thread.Sleep(1000);
            if (!Utility.WaitAndTapXML(deviceID, 3, Language.Save()))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 91.5, 8.0);
            }
            Thread.Sleep(2000);
            BackToFbHome(deviceID);
        }

        public static void AddSingleCover(String deviceID)
        {
            BackToFbHome(deviceID);

            KAutoHelper.ADBHelper.TapByPercent(deviceID, 6.6, 21.5); // Tap to icon avatar
            Device.PushCover(deviceID);
            if (Utility.CheckTextExist(deviceID, Language.Begin(), 7))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 6.2, 7.1);
                Thread.Sleep(1000);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 88.8, 58.6);
                Thread.Sleep(1000);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 16.0, 15.0); // Tap to Select cover
                if (!Utility.WaitAndTapXML(deviceID, 3, "tải ảnh lên")) // Tải ảnh lên 
                {
                    return;
                }
                Utility.WaitAndTapXML(deviceID, 2, Language.AllowAll());
                Thread.Sleep(1000);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 14.8, 18.3); // Tap to image
                Thread.Sleep(1000);
                Utility.WaitAndTapXML(deviceID, 2, Language.Save());

                Thread.Sleep(4000);
                BackToFbHome(deviceID);
            }
            else
            {
                AddSingleCover_(deviceID);
            }

        }

        public static void AddCover(String deviceID)
        {
            Device.PushCover(deviceID);
            //GoToProfile(deviceID);
            Device.TapByPercent(deviceID, 90.2, 54.9);
            Thread.Sleep(1000);
            Device.TapByPercent(deviceID, 50.6, 24.3);
            Thread.Sleep(1000);
            Utility.WaitAndTapXML(deviceID, 2, Language.Save());
            Thread.Sleep(3000);
            
            // Back
            for (int i =0; i < 10; i ++)
            {
                Device.Back(deviceID);
                if (Utility.CheckTextExist(deviceID, Language.StopUpdateProfile()))
                {
                    // Cancel update profile
                    
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 86.0, 59.1);
                    for (int k = 0; k < 10; k ++)
                    {
                        Thread.Sleep(500);
                        Device.Back(deviceID);
                        if (Utility.CheckTextExist(deviceID, Language.StopUpdateProfile()))
                        {
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 86.0, 59.1);
                        } else
                        {
                            break;
                        }
                    }
                    break;
                }
            }

            
            BackToFbHome(deviceID);
        }

        public static bool PushCover(string deviceID, string pcPath)
        {
            try
            {
                string cmd = string.Format("adb -s {0} push \"{1}\" /sdcard/Download/1cover.png", deviceID, pcPath);
                string result = Device.ExecuteCMD(cmd);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }


        
        
        //public static bool PullBackupFb(string uid, string deviceID)
        //{
        //    try
        //    {
        //        string temp = deviceID.Replace(":", ".");
        //        if (!Directory.Exists("Authentication"))
        //        {
        //            Directory.CreateDirectory("Authentication");
        //        }
        //        string authenPath = Application.StartupPath + "\\" + string.Format("Authentication\\{0}\\", uid);
        //        if (!Directory.Exists(authenPath))
        //        {
        //            Directory.CreateDirectory(authenPath);
        //        }
        //        string cmd = Device.ExecuteCMD(string.Format(
        //            Device.CONSOLE_ADB + " shell \"su -c ' rm /data/data/com.facebook.katana/app_light_prefs/com.facebook.katana/App*'\"", deviceID));




        //        cmd = Device.ExecuteCMD(string.Format(
        //            Device.CONSOLE_ADB + "pull /data/data/com.facebook.katana/app_light_prefs/com.facebook.katana/ \"{2}/Authentication/{1}/com.facebook.katana\"", deviceID, uid, Application.StartupPath));
        //        cmd = Device.ExecuteCMD(string.Format(
        //            Device.CONSOLE_ADB + "pull /data/data/com.facebook.katana/databases/ \"{2}/Authentication/{1}/databases", deviceID,  uid, Application.StartupPath));
        //        cmd = Device.ExecuteCMD(string.Format(
        //            Device.CONSOLE_ADB + "pull /data/data/com.facebook.katana/app_sessionless_gatekeepers/ \"{2}/Authentication/{1}/app_sessionless_gatekeepers\"", deviceID, uid, Application.StartupPath));

        //        cmd = Device.ExecuteCMD(string.Format(
        //            Device.CONSOLE_ADB + "pull /data/data/com.facebook.katana/shared_prefs/ \"{2}/Authentication/{1}/shared_prefs\"", deviceID, uid, Application.StartupPath));

        //        //cmd = Device.ExecuteCMD(string.Format(
        //        //    Device.CONSOLE_ADB + "pull /data/data/com.facebook.katana/cache/ \"{2}/Authentication/{1}/cache\"", deviceID, uid, Application.StartupPath));

        //        //cmd = Device.ExecuteCMD(string.Format(
        //        //    Device.CONSOLE_ADB + "pull /data/data/com.facebook.katana/files/ \"{2}/Authentication/{1}/files\"", deviceID, uid, Application.StartupPath));

        //        //cmd = Device.ExecuteCMD(string.Format(
        //        //    Device.CONSOLE_ADB + "pull /data/data/com.facebook.katana/app_graph_service_cache/ \"{2}/Authentication/{1}/app_graph_service_cache\"", deviceID, uid, Application.StartupPath));


        //        //cmd = Device.ExecuteCMD(string.Format(
        //        //    Device.CONSOLE_ADB + "pull /data/data/com.facebook.katana/app_gatekeepers/ \"{2}/Authentication/{1}/app_gatekeepers\"", deviceID, uid, Application.StartupPath));


        //        //cmd = Device.ExecuteCMD(string.Format(
        //        //    Device.CONSOLE_ADB + "pull /data/data/com.facebook.katana/app_composer_sprouts_cache_storage/ \"{2}/Authentication/{1}/app_composer_sprouts_cache_storage\"", deviceID, uid, Application.StartupPath));

        //        //cmd = Device.ExecuteCMD(string.Format(
        //        //    Device.CONSOLE_ADB + "pull /data/data/com.facebook.katana/app_analytics/ \"{2}/Authentication/{1}/app_analytics\"", deviceID, uid, Application.StartupPath));

        //        //cmd = Device.ExecuteCMD(string.Format(
        //        //   Device.CONSOLE_ADB + "pull /data/data/com.facebook.katana/app_mib_msys/ \"{2}/Authentication/{1}/app_mib_msys\"", deviceID, uid, Application.StartupPath));
        //        //cmd = Device.ExecuteCMD(string.Format(
        //        //   Device.CONSOLE_ADB + "pull /data/data/com.facebook.katana/app_models_data/ \"{2}/Authentication/{1}/app_models_data\"", deviceID, uid, Application.StartupPath));

        //        //cmd = Device.ExecuteCMD(string.Format(
        //        //   Device.CONSOLE_ADB + "pull /data/data/com.facebook.lite/ \"{2}/Authentication/{1}/lite\"", deviceID, uid, Application.StartupPath));

        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        public static bool PullAllData(string uid, string deviceID)
        {
            try
            {
                if (!Directory.Exists("Authentication"))
                {
                    Directory.CreateDirectory("Authentication");
                }
                string authenPath = Application.StartupPath + "\\" + string.Format("Authentication\\{0}\\", uid);
                if (!Directory.Exists(authenPath))
                {
                    Directory.CreateDirectory(authenPath);
                }
                string cmd = Device.ExecuteCMD(string.Format(
                    Device.CONSOLE_ADB + " shell \"su -c ' rm /data/data/com.facebook.katana/app_light_prefs/com.facebook.katana/App*'\"", deviceID));
                cmd = Device.ExecuteCMD(string.Format(
                    Device.CONSOLE_ADB + "pull /data/data/com.facebook.katana/app_light_prefs/com.facebook.katana/ \"{2}/Authentication/{1}/com.facebook.katana\"", deviceID, uid, Application.StartupPath));
                cmd = Device.ExecuteCMD(string.Format(
                    Device.CONSOLE_ADB + "pull /data/data/com.facebook.katana/databases/ \"{2}/Authentication/{1}/databases", deviceID, uid, Application.StartupPath));
                cmd = Device.ExecuteCMD(string.Format(
                    Device.CONSOLE_ADB + "pull /data/data/com.facebook.katana/app_sessionless_gatekeepers/ \"{2}/Authentication/{1}/app_sessionless_gatekeepers\"", deviceID, uid, Application.StartupPath));
                cmd = Device.ExecuteCMD(string.Format(
                    Device.CONSOLE_ADB + "pull /data/data/com.facebook.katana/shared_prefs/ \"{2}/Authentication/{1}/shared_prefs\"", deviceID, uid, Application.StartupPath));

                //cmd = Device.ExecuteCMD(string.Format(
                //    Device.CONSOLE_ADB + "pull /data/data/com.facebook.katana/cache/ \"{2}/Authentication/{1}/cache\"", deviceID, uid, Application.StartupPath));

                //cmd = Device.ExecuteCMD(string.Format(
                //    Device.CONSOLE_ADB + "pull /data/data/com.facebook.katana/files/ \"{2}/Authentication/{1}/files\"", deviceID, uid, Application.StartupPath));

                //cmd = Device.ExecuteCMD(string.Format(
                //    Device.CONSOLE_ADB + "pull /data/data/com.facebook.katana/app_graph_service_cache/ \"{2}/Authentication/{1}/app_graph_service_cache\"", deviceID, uid, Application.StartupPath));


                //cmd = Device.ExecuteCMD(string.Format(
                //    Device.CONSOLE_ADB + "pull /data/data/com.facebook.katana/app_gatekeepers/ \"{2}/Authentication/{1}/app_gatekeepers\"", deviceID, uid, Application.StartupPath));


                //cmd = Device.ExecuteCMD(string.Format(
                //    Device.CONSOLE_ADB + "pull /data/data/com.facebook.katana/app_composer_sprouts_cache_storage/ \"{2}/Authentication/{1}/app_composer_sprouts_cache_storage\"", deviceID, uid, Application.StartupPath));

                //cmd = Device.ExecuteCMD(string.Format(
                //    Device.CONSOLE_ADB + "pull /data/data/com.facebook.katana/app_analytics/ \"{2}/Authentication/{1}/app_analytics\"", deviceID, uid, Application.StartupPath));

                //cmd = Device.ExecuteCMD(string.Format(
                //   Device.CONSOLE_ADB + "pull /data/data/com.facebook.katana/app_mib_msys/ \"{2}/Authentication/{1}/app_mib_msys\"", deviceID, uid, Application.StartupPath));
                //cmd = Device.ExecuteCMD(string.Format(
                //   Device.CONSOLE_ADB + "pull /data/data/com.facebook.katana/app_models_data/ \"{2}/Authentication/{1}/app_models_data\"", deviceID, uid, Application.StartupPath));

                //cmd = Device.ExecuteCMD(string.Format(
                //   Device.CONSOLE_ADB + "pull /data/data/com.facebook.lite/ \"{2}/Authentication/{1}/lite\"", deviceID, uid, Application.StartupPath));

                return true;
            }
            catch
            {
                return false;
            }
        }

        
        public static bool CheckFacebookFolderPermission(string deviceId, string folderPath = "/data/data/com.facebook.katana")
        {
            Console.WriteLine($"🔍 Đang kiểm tra quyền thư mục: {folderPath}");

            // 1. Lấy userId từ dumpsys
            string uidOutput = RunAdb(deviceId, "shell dumpsys package com.facebook.katana | grep userId");
            var match = System.Text.RegularExpressions.Regex.Match(uidOutput, @"userId=(\d+)");
            if (!match.Success)
            {
                Console.WriteLine("❌ Không tìm được userId của Facebook.");
                return false;
            }

            string uid = match.Groups[1].Value;
            string user = $"u0_a{uid.Substring(uid.Length - 3)}";

            // 2. Dùng ls -lR để liệt kê toàn bộ file + quyền
            string listOutput = RunAdb(deviceId, $"shell su -c \"ls -lR {folderPath}\"");

            var lines = listOutput.Split('\n');
            int wrongCount = 0;

            foreach (var line in lines)
            {
                if (line.StartsWith("-") || line.StartsWith("d")) // file hoặc folder
                {
                    var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 4)
                    {
                        string owner = parts[2];
                        string group = parts[3];

                        if (owner != user || group != user)
                        {
                            wrongCount++;
                            Console.WriteLine($"❌ Sai quyền: {line.Trim()}");
                            return false;
                        }
                    }
                }
            }

            if (wrongCount == 0)
                Console.WriteLine("✅ Tất cả file/folder trong Facebook đã đúng quyền.");
            else
                Console.WriteLine($"⚠️ Có {wrongCount} file/folder sai quyền. Nên chạy FixFacebookFolderPermission().");
            return true;
        }
        public static string FixFacebookFolderPermission(string deviceId, string folderPath)
        {
            string result = "";
            try
            {
                Console.WriteLine("🔍 Đang lấy userId của com.facebook.katana...");
                string output = RunAdb(deviceId, "shell dumpsys package com.facebook.katana | grep userId");

                // Parse userId=10124
                var match = System.Text.RegularExpressions.Regex.Match(output, @"userId=(\d+)");
                if (!match.Success)
                {
                    Console.WriteLine("❌ Không tìm được userId của Facebook.");
                    return result;
                }

                string uid = match.Groups[1].Value;
                string user = $"u0_a{uid.Substring(uid.Length - 3)}"; // Convert 10124 → u0_a124

                Console.WriteLine($"✅ UID = {uid}, mapped to user: {user}");
                user = uid;
                string chownCmd = $"shell su -c \"chown -R {user}:{user} {folderPath}\"";
                RunAdb(deviceId, chownCmd);

                chownCmd = $"shell su -c \"chmod -R 700 /data/data/com.facebook.katana\"";
                RunAdb(deviceId, chownCmd);

                Console.WriteLine($"✅ Đã set quyền {user} cho thư mục: {folderPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi khi set quyền thư mục: {ex.Message}");
            }
            return result;
        }
        private static string ToOneLine(string script)
        {
            return script.Replace("\r", "").Replace("\n", " && ");
        }
        public static void RestoreBackup(string deviceId, string tarFilePath)
        {
            if (!File.Exists(tarFilePath))
            {
                Console.WriteLine("File backup .tar không tồn tại!");
                return;
            }

            Console.WriteLine("🟡 Pushing backup file...");
            RunAdb(deviceId, $"push \"{tarFilePath}\" /sdcard/facebook_backup.tar");

            Console.WriteLine("🟡 Clearing app data...");
            //RunAdb(deviceId, "shell pm clear com.facebook.katana");

            Console.WriteLine("🟡 Restoring data...");
            var restoreScript = @"
su
cd /
tar -xpf /sdcard/facebook_backup.tar
";

            RunAdb(deviceId, $"shell \"{ToOneLine(restoreScript)}\"");

            FixFacebookFolderPermission(deviceId, "/data/data/com.facebook.katana");
            Console.WriteLine("🟢 Restarting Facebook app...");
            RunAdb(deviceId, "shell am start -n com.facebook.katana/.LoginActivity");
        }
        public static bool PushBackupFb(string uid, string deviceID )
        {
            try
            {
                
                //RestoreBackup(deviceID, Application.StartupPath + "\\Authentication\\" + uid + ".tar.gz");

                //return true;
                
                for (int i = 0; i < 5; i++)
                {
                    Device.ForceStop(deviceID, Constant.FACEBOOK_PACKAGE);
                    Device.ClearCache(deviceID, Constant.FACEBOOK_PACKAGE);
                    string cmd;
                    string packageFacebook = "com.facebook.katana";
                    cmd = string.Format(Device.CONSOLE_ADB + " shell \"su -c ' rm -rf  /sdcard/Alarms/*' \"", deviceID);
                    string result = Device.ExecuteCMD(cmd); // delete file tạm cũ


                    Thread.Sleep(1000);
                    cmd = Device.ExecuteCMD(string.Format(
                        Device.CONSOLE_ADB + "push \"{2}/Authentication/{1}.tar.gz\" /sdcard/Alarms/", deviceID, uid, Application.StartupPath)); // push file mới
                    cmd = " shell su -c \"ls -l /data/data | grep com.facebook.katana | awk '{print $3\\\":\\\"$4}'\"";
                    cmd = string.Format(Device.CONSOLE_ADB, deviceID) + cmd;
                    Thread.Sleep(1000);
                    string owner = Device.ExecuteCMD(cmd); // lấy userid
                    string[] temp = owner.Split('\n');

                    if (temp.Length > 1)
                    {
                        owner = temp[4].Trim().Replace("\r", "");
                        Console.WriteLine("owner:" + owner);
                    } else
                    {
                        continue;
                    }
                        Thread.Sleep(1000);
                    cmd = Device.ExecuteCMD("adb -s " + deviceID + " shell su -c cp /sdcard/Alarms/" + uid + ".tar.gz /data/data/com.facebook.katana/" + uid + ".tar.gz"); // chep file tạm vào com.facebook.katana

                    Thread.Sleep(1000);
                    cmd = Device.ExecuteCMD("adb -s " + deviceID + " shell su -c tar -xpf /data/data/" + packageFacebook + "/" + uid + ".tar.gz"); // giải nén file

                    cmd = string.Format(Device.CONSOLE_ADB + " shell \"su -c ' rm -rf  /data/data/com.facebook.katana/{1}.tar.gz' \"", deviceID, uid); // xóa file
                    result = Device.ExecuteCMD(cmd); // delete folder uid


                    string folderPath = "/data/data/com.facebook.katana";
                    string chownCmd = $"shell su -c \"chown -R {owner} {folderPath}\"";
                    RunAdb(deviceID, chownCmd);  //update user

                    chownCmd = $"shell su -c \"chmod -R 700 /data/data/com.facebook.katana\"";
                    RunAdb(deviceID, chownCmd);

                    Thread.Sleep(1000);

                    if (FbUtil.CheckLiveWall(uid) == Constant.DIE)
                    {
                        //using (StreamWriter HDD = new StreamWriter("FileCLone/Die_CheckLive" + ".txt", true))
                        //{
                        //    HDD.WriteLine(acc.note);
                        //    HDD.Close();
                        //}
                        return false;
                    }

                    Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                    Thread.Sleep(20000);
                    

                    var analyzer = new FacebookLogcatAnalyzer(deviceID);
                    var statusList = analyzer.Analyze();
                    foreach (var line in statusList) {
                        Console.WriteLine(line);
                        if (line.ToLower().Contains("checkpoint"))
                        {
                            //Device.RebootByCmd(deviceID);
                            continue;
                        }
                    }

                    if (WaitAndTapXML(deviceID,new string[] { "đăng nhập","tiếp tục"}))
                    {
                        Console.WriteLine("het session");
                    }
                    return true;
                    
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool PullBackupFbNew(string uid, string deviceID)
        {



            string cmd = "";

            cmd = string.Format(Device.CONSOLE_ADB + " shell \"su -c ' rm -rf  /sdcard/Alarms/*' \"", deviceID);
            string result = Device.ExecuteCMD(cmd); // delete folder uid

            cmd = string.Format(Device.CONSOLE_ADB + " shell \"su -c ' rm -rf  /data/data/com.facebook.katana/{1}.tar.gz' \"", deviceID, uid);
            result = Device.ExecuteCMD(cmd); // delete folder uid

            Thread.Sleep(1000);
            cmd = string.Format(Device.CONSOLE_ADB + " shell \"su -c ' mkdir -p /sdcard/Alarms/data/data/com.facebook.katana' \"", deviceID);
            result = Device.ExecuteCMD(cmd); // create folder uid
            cmd = string.Format(Device.CONSOLE_ADB + " shell \"su -c ' mkdir -p /sdcard/Alarms/data/data/com.facebook.katana/files' \"", deviceID);
            result = Device.ExecuteCMD(cmd); // create folder uid
            Thread.Sleep(1000);
            cmd = string.Format(Device.CONSOLE_ADB + " shell \"su -c ' cp -r  /data/data/com.facebook.katana/app_gatekeepers /sdcard/Alarms/data/data/com.facebook.katana' \"", deviceID);
            result = Device.ExecuteCMD(cmd); // copy app_gatekeepers
            Thread.Sleep(1000);
            cmd = string.Format(Device.CONSOLE_ADB + " shell \"su -c ' cp -r /data/data/com.facebook.katana/app_light_prefs /sdcard/Alarms/data/data/com.facebook.katana' \"", deviceID);
            result = Device.ExecuteCMD(cmd); // copy app_light_prefs
            Thread.Sleep(1000);
            cmd = string.Format(Device.CONSOLE_ADB + " shell \"su -c ' cp -r /data/data/com.facebook.katana/databases /sdcard/Alarms/data/data/com.facebook.katana' \"", deviceID);
            result = Device.ExecuteCMD(cmd); // copy databases
            Thread.Sleep(1000);

            cmd = string.Format(Device.CONSOLE_ADB + " shell \"su -c ' cp -r /data/data/com.facebook.katana/shared_prefs /sdcard/Alarms/data/data/com.facebook.katana' \"", deviceID);
            result = Device.ExecuteCMD(cmd); // copy shared_prefs

            cmd = string.Format(Device.CONSOLE_ADB + " shell \"su -c ' cp -r /data/data/com.facebook.katana/files/mobileconfig /sdcard/Alarms/data/data/com.facebook.katana/files' \"", deviceID);
            result = Device.ExecuteCMD(cmd); // copy shared_prefs

            Thread.Sleep(1000);
            cmd = string.Format(Device.CONSOLE_ADB + " shell \"su -c 'cd /sdcard/Alarms/ && tar  -cvz -f {1}.tar.gz data'  \"", deviceID, uid, uid);
            result = Device.ExecuteCMD(cmd); // Tar file

            Thread.Sleep(1000);
            string temp = deviceID.Replace(":", ".");
            if (!Directory.Exists("Authentication"))
            {
                Directory.CreateDirectory("Authentication");
            }
            Thread.Sleep(1000);

            cmd = Device.ExecuteCMD(string.Format(Device.CONSOLE_ADB + "pull /sdcard/Alarms/{1}.tar.gz \"{2}/Authentication/", deviceID, uid, Application.StartupPath));
            result = Device.ExecuteCMD(cmd); //
            Thread.Sleep(1000);
            cmd = string.Format(Device.CONSOLE_ADB + " shell \"su -c ' rm -rf  /sdcard/Alarms/*' \"", deviceID);
            
            result = Device.ExecuteCMD(cmd); // delete folder uid

            return true;
        }
        public static FBItems GetKatanaCookieFromBackup(string innn)
        {
            FBItems fff = new FBItems();
            return fff;
        }
        public static string Base64Decode(string base64Encoded)
        {
            byte[] bytes = Convert.FromBase64String(base64Encoded);
            return Encoding.UTF8.GetString(bytes);
        }
        public static string GetInfoAccountFromUidUsingCookie(string cookie, string useragent, string proxy, int typeProxy)
        {
            bool flag = false;
            string text = "";
            string text2 = "";
            string text3 = "";
            string text4 = "";
            string text5 = "";
            string text6 = "";
            string text7 = "";
            string text8 = "";
            string text9 = "";
            try
            {
                string value = Regex.Match(cookie + ";", "c_user=(.*?);").Groups[1].Value;
                RequestXNet requestXNet = new RequestXNet(cookie, useragent, proxy, typeProxy);
                string input = requestXNet.RequestGet("https://m.facebook.com/composer/ocelot/async_loader/?publisher=feed");
                string value2 = Regex.Match(input, Base64Decode("bmFtZT1cXCJmYl9kdHNnXFwiIHZhbHVlPVxcIiguKj8pXFwi")).Groups[1].Value;
                text8 = Regex.Match(input, "EAAA(.*?)\"").Value.TrimEnd(new char[]
                {
                    '"',
                    '\\'
                });
                text = Regex.Match(input, Base64Decode("cHJvZnBpY1xcIiBhcmlhLWxhYmVsPVxcIiguKj8pLA==")).Groups[1].Value;
                text = WebUtility.HtmlDecode(text);
                JObject jobject = JObject.Parse(requestXNet.RequestPost("https://www.facebook.com/api/graphql/", Base64Decode("LS0tLS0tV2ViS2l0Rm9ybUJvdW5kYXJ5MnlnMEV6RHBTWk9DWGdCUgpDb250ZW50LURpc3Bvc2l0aW9uOiBmb3JtLWRhdGE7IG5hbWU9ImZiX2R0c2ciCgp7e2ZiX2R0c2d9fQotLS0tLS1XZWJLaXRGb3JtQm91bmRhcnkyeWcwRXpEcFNaT0NYZ0JSCkNvbnRlbnQtRGlzcG9zaXRpb246IGZvcm0tZGF0YTsgbmFtZT0icSIKCm5vZGUoe3t1aWR9fSl7ZnJpZW5kc3tjb3VudH0sc3Vic2NyaWJlcnN7Y291bnR9LGdyb3VwcyxjcmVhdGVkX3RpbWV9Ci0tLS0tLVdlYktpdEZvcm1Cb3VuZGFyeTJ5ZzBFekRwU1pPQ1hnQlItLQ==").Replace("{{fb_dtsg}}", value2).Replace("{{uid}}", value), "multipart/form-data; boundary=----WebKitFormBoundary2yg0EzDpSZOCXgBR"));
                text6 = jobject[value]["friends"]["count"].ToString();
                text7 = jobject[value]["groups"]["count"].ToString();
                text9 = jobject[value]["created_time"].ToString();
                if (text6 == "")
                {
                    text6 = "0";
                }
                if (text7 == "")
                {
                    text7 = "0";
                }
            }
            catch
            {
                if (!CommonRequest.CheckLiveCookie(cookie, useragent, proxy, typeProxy).Contains("1|"))
                {
                    return "-1";
                }
            }
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}", new object[]
            {
                flag,
                text,
                text2,
                text3,
                text4,
                text5,
                text6,
                text7,
                text8,
                text9
            });
        }

        public static string CheckAccVeriMail(string tooken, string uid)
        {
            string mail = "";

            try
            {
                Dictionary<string, string> dict = GetInforUserFromUid(tooken, uid, "");
                mail = dict["email"];
            } catch (Exception ex)
            {
                
            }
            
            return mail;
        }
        public static Dictionary<string, string> GetInforUserFromUid(string token, string uid, string cookie = "")
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            try
            {
                RequestHTTP requestHTTP = new RequestHTTP();
                requestHTTP.SetSSL(SecurityProtocolType.Tls12);
                requestHTTP.SetKeepAlive(true);
                requestHTTP.SetDefaultHeaders(new string[]
                {
                    "content-type: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8",
                    "user-agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.131 Safari/537.36"
                });
                int num = 0;
                JObject jobject;
                string email;
                string text;
                string friend;
                string phone;
                string value3;
                string birthday;
                string value5;
                for (; ; )
                {
                    jobject = JObject.Parse(requestHTTP.Request("GET", "https://graph.facebook.com/v2.11/" + uid + "?fields=id,name,email,gender,birthday,friends.limit(0),groups.limit(5000){id}&access_token=" + token, null, null, true, null, 60000));
                    email = "";
                    text = "";
                    friend = "";
                    phone = "";
                    value3 = "";
                    birthday = "";
                    value5 = "Live";
                    jobject["name"].ToString();
                    try
                    {
                        birthday = jobject["birthday"].ToString();
                    }
                    catch
                    {
                    }
                    //try
                    //{
                    //    value3 = jobject["gender"].ToString();
                    //}
                    //catch
                    //{
                    //}
                    try
                    {
                        if (jobject.ContainsKey("email"))
                        {
                            email = jobject["email"].ToString();
                        }
                        
                    }
                    catch
                    {
                    }
                    //try
                    //{
                    //    phone = jobject["mobile_phone"].ToString();
                    //}
                    //catch
                    //{
                    //}
                    try
                    {
                        friend = jobject["friends"]["summary"]["total_count"].ToString();
                        break;
                    }
                    catch
                    {
                        if (!(cookie != "") || !(CheckLiveCookie(cookie) == "Live") || num != 0 || !PublicListFriends(cookie))
                        {
                            break;
                        }
                        num++;
                    }
                }
                if (friend == "")
                {
                    friend = "0";
                }
                //try
                //{
                //    text = (jobject["groups"]["data"].Count<JToken>().ToString() ?? "");
                //}
                //catch
                //{
                //}
                if (text == "")
                {
                    text = "0";
                }
                dictionary.Add("uid", jobject["id"].ToString());
                dictionary.Add("name", jobject["name"].ToString());
                dictionary.Add("birthday", birthday);
                dictionary.Add("gender", value3);
                dictionary.Add("token", token);
                dictionary.Add("email", email);
                dictionary.Add("phone", phone);
                dictionary.Add("friends", friend);
                dictionary.Add("groups", text);
                dictionary.Add("info", value5);
            }
            catch
            {
                dictionary.Add("info", "Die");
            }
            return dictionary;
        }
        public static bool PublicListFriends(string cookie)
        {
            bool result;
            try
            {
                RequestHTTP requestHTTP = new RequestHTTP();
                requestHTTP.SetSSL(SecurityProtocolType.Tls12);
                requestHTTP.SetKeepAlive(true);
                requestHTTP.SetDefaultHeaders(new string[]
                {
                    "content-type: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8",
                    "user-agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.131 Safari/537.36",
                    "cookie: " + cookie
                });
                requestHTTP.Request("GET", "https://mbasic.facebook.com" + Regex.Match(requestHTTP.Request("GET", "https://mbasic.facebook.com" + Regex.Match(requestHTTP.Request("GET", "https://mbasic.facebook.com/me/friends", null, null, true, null, 60000), "/privacyx/selector/(.*?)\"").Value.Replace("\"", "").Replace("amp;", ""), null, null, true, null, 60000), "/a/privacy/.px=300645083384735(.*?)\"").Value.Replace("\"", "").Replace("amp;", ""), null, null, true, null, 60000);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
        public static string CheckLiveCookie(string cookie)
        {
            cookie = ConvertToStandardCookie(cookie);
            string result = "Die";
            try
            {
                RequestHTTP requestHTTP = new RequestHTTP();
                requestHTTP.SetSSL(SecurityProtocolType.Tls12);
                requestHTTP.SetKeepAlive(true);
                requestHTTP.SetDefaultHeaders(new string[]
                {
                    "content-type: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8",
                    "user-agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.131 Safari/537.36",
                    "cookie: " + cookie
                });
                string value = Regex.Match(cookie, "c_user=(.*?);").Groups[1].Value;
                string text = requestHTTP.Request("GET", "https://www.facebook.com/me", null, null, true, null, 60000);
                if (!value.Equals("") && text.Contains(value) && text.Contains("entity_id") && !text.Contains("checkpointSubmitButton"))
                {
                    result = "Live";
                }
            }
            catch
            {
            }
            return result;
        }
        public static string ConvertToStandardCookie(string cookie)
        {
            string result;
            try
            {
                result = Regex.Match(cookie, "c_user=(.*?);").Value + Regex.Match(cookie, "xs=(.*?);").Value + Regex.Match(cookie, "fr=(.*?);").Value + Regex.Match(cookie, "datr=(.*?);").Value;
            }
            catch
            {
                result = cookie;
            }
            return result;
        }
    }
}
