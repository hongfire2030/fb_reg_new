using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using fb_reg.RequestApi;
using fb_reg.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using static fb_reg.CacheServer;

namespace fb_reg
{
    class Device
    {
        
        public static string LDCONSOLE_PATH = @"C:/LDPlayer/LDPlayer4.0/dnconsole.exe";
        public static string LD_CONSOLE = "C:\\LDPlayer\\LDPlayer4.0\\ldconsole ";
        public static string LD_CONSOLE_ADB = LD_CONSOLE + " adb --name {0} --command ";
        public static string CONSOLE_ADB = "adb -s {0} ";

        // Token: 0x04000001 RID: 1
        private static string LIST_DEVICES = "adb devices";

        // Token: 0x04000002 RID: 2
        private static string TAP_DEVICES = CONSOLE_ADB + "shell input tap {1} {2}";

        // Token: 0x04000003 RID: 3
        private static string SWIPE_DEVICES = CONSOLE_ADB + " shell input swipe {1} {2} {3} {4} {5}";

        // Token: 0x04000004 RID: 4
        private static string KEY_DEVICES = CONSOLE_ADB + " shell input keyevent {1}";

        // Token: 0x04000005 RID: 5
        private static string INPUT_TEXT_DEVICES = CONSOLE_ADB + " shell input text \"{1}\"";

        // Token: 0x04000006 RID: 6
        //private static string CAPTURE_SCREEN_TO_DEVICES = CONSOLE_ADB + " shell screencap -p \"{1}\"";
        private static string CAPTURE_SCREEN_TO_DEVICES = CONSOLE_ADB + " exec-out screencap -p > {1}";

        // Token: 0x04000007 RID: 7
        private static string PULL_SCREEN_FROM_DEVICES = CONSOLE_ADB + " pull \"{1}\"";

        // Token: 0x04000008 RID: 8
        private static string REMOVE_SCREEN_FROM_DEVICES = CONSOLE_ADB + " shell rm -f \"{1}\"";

        // Token: 0x04000009 RID: 9
        private static string GET_SCREEN_RESOLUTION = CONSOLE_ADB + " shell dumpsys display | Find \"mCurrentDisplayRect\"";

        // Token: 0x0400000A RID: 10
        private const int DEFAULT_SWIPE_DURATION = 100;

        // Token: 0x0400000B RID: 11
        private static string ADB_FOLDER_PATH = "";

        // Token: 0x0400000C RID: 12
        private static string ADB_PATH = "";

        public static string GetAndroidVersion(string deviceID)
        {
            string androidVersion = "";
            try
            {
                string cmd = string.Format(CONSOLE_ADB + "shell getprop ro.build.version.release", deviceID);
                string ex = ExecuteCMD(cmd);
                
                string[] dd = ex.Split('\n');
                androidVersion = dd[4];
                androidVersion = androidVersion.Replace("\r", "");
                
            }
            catch { }

            return androidVersion;
        }
        public static void SendSMS(string deviceID, string phoneNumber)
        {

            string cmd = "";
            cmd = string.Format(CONSOLE_ADB + "shell am start -a android.intent.action.SENDTO -d sms:{1}", deviceID, phoneNumber);
            string ddd = ExecuteCMD(cmd);
        }

        public static void PushChargerFile(string deviceID)
        {
            string remoteTempPath = "/data/local/tmp/charger.rc";
            string cmd = "";
            cmd = string.Format(CONSOLE_ADB + "shell su -c ' mount -o rw,remount /'", deviceID);
            string ddd = ExecuteCMD(cmd);
            Console.WriteLine("ddd:" + ddd);
            cmd = string.Format(CONSOLE_ADB + " push charger.rc /data/local/tmp/", deviceID);
            ddd = ExecuteCMD(cmd);

            cmd = string.Format(CONSOLE_ADB, deviceID)  + $" shell su -c 'cp {remoteTempPath} /etc/init/charger.rc'";
            ddd = ExecuteCMD(cmd);
            Console.WriteLine("ddd:" + ddd);

            cmd = string.Format(CONSOLE_ADB + "shell su -c ' mount -o ro,remount /'", deviceID);
            ddd = ExecuteCMD(cmd);
            Console.WriteLine("ddd:" + ddd);
        }

        public static void PushFileRootPhone(string deviceID)
        {
            string cmd = "";
            cmd = string.Format(CONSOLE_ADB + " rm -rf /sdcard/Download/* ", deviceID);
            string ddd = ExecuteCMD(cmd);
            cmd = string.Format(CONSOLE_ADB + " push \"root_rom\\initial\\root\" /sdcard/Download ", deviceID);
            ddd = ExecuteCMD(cmd);
        }

        public static string GetSerialNo(string deviceID)
        {
            string cmd = "scrcpy -s " + deviceID + " shell getprop ro.serialno";
            string ddd = ExecuteCMD(cmd);

            return ddd;
        }
        public static string GetMacAddress(string deviceID)
        {
            string cmd = "scrcpy -s " + deviceID + " shell cat /sys/class/net/wlan0/address";
            string ddd = ExecuteCMD(cmd);

            return ddd;
        }

        public static void ViewScreen(string deviceID)
        {
            string cmd = "scrcpy -s " + deviceID + " --max-size 700 -b 2M --max-fps 15";
            string ddd = ExecuteCMD(cmd);
        }
        public static void ClearCache(string deviceID, string package)
        {
            string cmd = string.Format(CONSOLE_ADB + "shell pm clear {1}", deviceID, package);
            ExecuteCMD(cmd);
            Thread.Sleep(800); // Delay nhẹ cho thiết bị phản ứng
        }
        public static void OpenAppDetail(string deviceID, string package)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell am start -a android.settings.APPLICATION_DETAILS_SETTINGS  -d package:{1}", deviceID, package);
            ExecuteCMD(cmd);
        }
        public static void OpenAccountsSetting(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell am start -a android.settings.SYNC_SETTINGS", deviceID);
            ExecuteCMD(cmd);
        }

        public static void SyncAccountsSetting(string deviceID)
        {
            //string cmd = string.Format(CONSOLE_ADB + " shell requestsync -f", deviceID);
            //ExecuteCMD(cmd);
        }
        public static void OpenApp(string deviceID, string package)
        {
            string cmd = string.Format(CONSOLE_ADB + "shell monkey -p {1} -c android.intent.category.LAUNCHER 1", deviceID, package);
            ExecuteCMD(cmd);
            Device.TurnOnAutoRotate(deviceID);
            Device.PortraitRotate(deviceID);
        }
        public static void OpenAppDocumentSuit(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell am start -n com.android.documentsui/com.android.documentsui.files.FilesActivity", deviceID);
            ExecuteCMD(cmd);

            Device.PortraitRotate(deviceID);
        }
        public static void InputStringAdbKeyboard(string deviceID, string Chuỗi)
        {
            string str = Convert.ToBase64String(Encoding.UTF8.GetBytes(Chuỗi));
            string cmd = string.Format(CONSOLE_ADB + "shell am broadcast -a ADB_INPUT_B64 --es msg {1}", deviceID, str);
            ExecuteCMD(cmd);
        }
        public static void Home(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + "shell input keyevent KEYCODE_HOME",deviceID);
            ExecuteCMD(cmd);
        }

        public static string AdbConnect(string deviceID)
        {
            string rrr = "";
            if (deviceID.Contains(":"))
            {
                string cmd = string.Format("adb connect {0}", deviceID);
                rrr = ExecuteCMD(cmd);
                Thread.Sleep(1000);
            }
            return rrr;
        }
        public static string GetIpLocal(string deviceID)
        {
            int num = 0;
            string text = "";
            string cmd = string.Format(CONSOLE_ADB + "shell ip route", deviceID);
            string text2 = ExecuteCMD(cmd);
            List<string> list = text2.Split(new char[]
            {
                '\n'
            }).ToList<string>().ToList<string>();
            foreach (string input in list)
            {
                num++;
                bool flag = num == 5;
                if (flag)
                {
                    text = Regex.Match(input, "src [0-9.]{0,}").ToString();
                    text = text.Replace("src ", "");
                }
            }
            return text;
        }
        public static void RefeshConnection()
        {
            string cmd = string.Format("adb kill-server");
            ExecuteCMD(cmd);
            Thread.Sleep(1000);
            cmd = string.Format("adb devices");
            ExecuteCMD(cmd);
            Thread.Sleep(3000);
        }
        public static void CloseLD(string index)
        {
            string cmd = string.Format(LDCONSOLE_PATH + " quit --index {0} ", index);
            ExecuteCMD(cmd);
        }
        public static void ChangeLD(string ldIndex)
        {
            string cmdCommand = "";
            string[] MODEL_LIST = { "SM-G986WZKAXAC ", "SM-G986WLBAXAC ", "SM-G986WZKEXAC ", "SM-G985FZAAXSA ", 
                "SM-G985FZKAXSA ", "SM-G985FLBAXSA ", "SM-G986BZAAXSA ", "SM-G986BZKAXSA ", "SM-G986BLBAXSA ", 
                "SM-G986BZAEXSA ", "SM-G9860", "SM-G9868", "SM-G985F", "SM-G985FZADEUB ", "SM-G985FLBDEUB ", 
                "SM-G985FZKDEUB ", "SM-G986F", "SM-G986BZADEUB ", "SM-G986BLBDEUB ", "SM-G986BZKDEUB ", "SM-G986BZAGEUB ", 
                "SM-G986BLBGEUB ", "SM-G986BZKGEUB ", "SM-G986BZWDEUB ", "SM-G986N", "SM-G986NZAAKOO ", "SM-G986NLBAKOO ", 
                "SM-G986NZWAKOO ", "SM-G986F/DS (Du", "SM-G985FZADXSP ", "SM-G985FLBDXSP ", "SM-G985FZKDXSP ", "SM-G986B", 
                "SM-G986BZADEUA ", "SM-G986BLBDEUA ", "SM-G986BZKDEUA ", "SM-G986UZAAATT ", "SM-G986ULBAATT ", "SM-G986UZKAATT ", "SM-G986UZKEATT ", "SM-G986UZAASPR ", "SM-G986ULBASPR ", "SM-G986UZKASPR ", "SM-G986UZAATMB ", "SM-G986ULBATMB ", "SM-G986UZKATMB ", "SM-G986UZKETMB ", "SM-G986UZAAUSC ", "SM-G986ULBAUSC ", "SM-G986UZKAUSC ", "SM-G986UZAEUSC ", "SM-G986ULBEUSC ", "SM-G986UZKEUSC ", "SM-G986UZAACCT ", "SM-G986ULBACCT ", "SM-G986UZKACCT ", "SM-G986UZAECCT ", "SM-G986ULBECCT ", "SM-G986UZKECCT ", "SM-G986UZAACHA ", "SM-G986ULBACHA ", "SM-G986UZKACHA ", "SM-G986UZAECHA ", "SM-G986ULBECHA ", "SM-G986UZKECHA ", "SM-G986UZAAVZW ", "SM-G986ULBAVZW ", "SM-G986UZKAVZW ", "SM-G986UZKEVZW ", "SM-G986U", "SM-G986UZAAXAA ", "SM-G986ULBAXAA ", "SM-G986UZKAXAA ", "SM-G986UZKEXAA " };
            Random ran = new Random();

            string MODEL = MODEL_LIST[ran.Next(0, MODEL_LIST.Length - 1)];

            string[] MANUFACTURE_LIST = { "BINATONE", "INQ", "VERTU", "IBALL", "KARBONN MOBILES",
                "CELKON MOBILES", "ONIDA ELECTRONICS", "VIDEOCON", "RELIANCE COMMUNICATIONS", 
                "MICROMAX MOBILE", "XOLO", "SIMMTRONICS", "SPICE DIGITAL", "BANGLADESH", "WALTON", 
                "SYMPHONY", "GRADIENTE", "BLACKBERRY", "THURAYA", "AVVIO", "JABLOTRON", "VERZO", 
                "ACER", "ASUS", "BENQ", "DBTEL", "DOPOD", "GIGABYTE TECHNOLOGY", "E-TEN", "HTC", "LUMIGON", "AEG", 
                "GRUNDIG MOBILE", "TELEFUNKEN", "SIEMENS", "FAIRPHONE", "JOHN'S PHONE", "SAMSUNG", "KT TECH", "LG", 
                "PANTECH", "SAMSUNG", "INDONESIA", "NEXIAN", "LATVIA", "JUST5", "M DOT", "KYOTO ELECTRONICS", "LANIX", 
                "ZONDA", "APPLE", "BLU", "FIREFLY", "GARMIN", "INFOSONICS[1]", "PALM", "SONIM", "FUJITSU", "KYOCERA COMMUNICATIONS", 
                "NEC", "NEC CASIO MOBILE COMMUNICATIONS", "PANASONIC", "SANYO", "CHETAN", "SONY MOBILE COMMUNICATIONS", "TOSHIBA", "GRESSO", "SITRONICS", "YOTA", "PAKISTAN", "QMOBILE", "ALCATEL", "ARCHOS", "BULL", "MOBIWIRE", "THOMSON", "WIKO", "JOLLA", "TWIG COM", "NOKIA", "PHILIPPINES", "CHERRY MOBILE", "GEEKSPHONE", "VITELCOM", "BQ", "DORO", "HANDHELD GROUP", "DTAC PHONE", "TRUE PHONE", "WELLCOM", "TUNISIA", "EVERTEK", "BBK", "CECT", "HAIER", "HUAWEI", "K-TOUCH", "LENOVO", "MEIZU", "OPPO", "SAGETEL", "SYMPHONY", "TECNO[2]", "TCL CORPORATION", "XIAOMI", "ZOOM", "ZTE", "GIONEE", "ASANZO", "BKAV", "BAVAPEN", "MASSCOM", "MOBIISTAR", "VIETTEL", "VNPT", "VINSMART", "BRONDI", "OLIVETTI", "ONDA MOBILE COMMUNICATION", "TELIT" };

            cmdCommand = String.Format("{0} quit --index {1}", LDCONSOLE_PATH, ldIndex);
            ExecuteCMD(cmdCommand);

            string MANUFACTURER = MANUFACTURE_LIST[ran.Next(0, MANUFACTURE_LIST.Length - 1)];
            string ANDROID_ID = Utility.RandomAndroidID();
            cmdCommand = string.Format("{0} modify " +
                "--index {1}  " +
                "--imei auto " +
                "--manufacturer \"" + MANUFACTURER + "\" " +
                "--model \"" + MODEL + "\" " +
                "--androidid " + ANDROID_ID, 
                LDCONSOLE_PATH, ldIndex);
            ExecuteCMD(cmdCommand);
            Thread.Sleep(500);
            cmdCommand = string.Format("{0} launch --index {1}", LDCONSOLE_PATH, ldIndex);
            ExecuteCMD(cmdCommand);

            
            Thread.Sleep(10000);
            cmdCommand = string.Format("{0} sortWnd", LDCONSOLE_PATH);
            ExecuteCMD(cmdCommand);
        }
        public static void Uninstall(string deviceID, string package)
        {
            string cmd = string.Format(CONSOLE_ADB + "uninstall {1}", deviceID, package);
            ExecuteCMD(cmd);
        }
        public static void OpenWeb(string deviceID, string url)
        {
            //adb shell am start -a android.intent.action.VIEW -d http://www.stackoverflow.com
            string cmd = string.Format(CONSOLE_ADB +  "shell am start -a android.intent.action.VIEW -d {1}", deviceID, url);
            ExecuteCMD(cmd);
            Thread.Sleep(3000);
        }

        public static void EnableWifi(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + "shell su -c 'svc wifi enable'", deviceID);
            ExecuteCMD(cmd);
        }

        public static string GetVersionFB(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + "shell \"dumpsys package com.facebook.katana | grep versionName\"", deviceID);
            string version = ExecuteCMD(cmd);
            try
            {
                version = Regex.Match(version, "versionName=(.*?)\r\n").Groups[1].ToString();
            }
            catch { }
             
            return version;
        }

        public static string GetVersionFBLite(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + "shell \"dumpsys package com.facebook.lite | grep versionName\"", deviceID);
            string version = ExecuteCMD(cmd);
            try
            {
                version = Regex.Match(version, "versionName=(.*?)\n").Groups[1].ToString();
            }
            catch { }

            return version;
        }

        public static string GetIpSimProtocol(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + "shell \"ifconfig rmnet1 \"", deviceID);
            string version = ExecuteCMD(cmd);

         
            try
            {
                if (version.Contains("inet addr:"))
                {
                    return Constant.IP4;

                } else if (version.Contains("inet6") && version.Contains("rmnet1"))
                {
                    return Constant.IP6;
                } else 
                {
                    cmd = string.Format(CONSOLE_ADB + "shell \"ifconfig rmnet0 \"", deviceID);
                    version = ExecuteCMD(cmd);


                    try
                    {
                        if (version.Contains("inet addr:"))
                        {
                            return Constant.IP4;

                        }
                        else if (version.Contains("inet6") && version.Contains("rmnet0"))
                        {
                            return Constant.IP6;
                        }
                        else
                        {

                            return Constant.NO_INTERNET;
                        }
                    } catch (Exception ex)
                    {

                    }
                }
            }
            catch { }

            return "Unknown";
        }

        public static string GetVersionFBBusiness(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + "shell \"dumpsys package com.facebook.pages.app | grep versionName\"", deviceID);
            string version = ExecuteCMD(cmd);
            try
            {
                version = Regex.Match(version, "versionName=(.*?)\n").Groups[1].ToString();
            }
            catch { }

            return version;
        }

        public static string TestAPI(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + "shell su -c 'curl --location --request POST 'https://m.facebook.com/reg/submit/?cid=103&multi_step_form=1&skip_suma=0&shouldForceMTouch=1&privacy_mutation_token=eyJ0eXBlIjowLCJjcmVhdGlvbl90aW1lIjoxNjIyMzcyNjM4LCJjYWxsc2l0ZV9pZCI6OTA3OTI0NDAyOTQ4MDU4fQ%3D%3D%3D%3D' --header 'Content-Type: application/x-www-form-urlencoded' --header 'Cookie: c_user=100068454504371; datr=82-zYDHdHM9McaZv4SObmHSq; fr=1FdThZB3vwZXpcATP.AWU8LVCZ-WptNufk1Fc3_0leIiA.Bgs3KH.aJ.AAA.0.0.Bgs3hr.AWUnHnsfQhA; sb=82-zYPQhnk-Htro-9l9xUrWb; xs=30%3AaappzdazXN6I4g%3A2%3A1622374513%3A-1%3A-1' --data-urlencode 'k=' --data-urlencode 'lsd=AVoA0Mr05d0&jazoest2815' --data-urlencode 'ccp=2' --data-urlencode 'reg_instance=4qawYHbhCtRMJjgFa_oET1v8' --data-urlencode 'reg_impression_id=796c147e-1c9b-40a9-8549-612b16925fb6' --data-urlencode 'submission_request=true' --data-urlencode 'i=' --data-urlencode 'helper=' --data-urlencode 'ns=0' --data-urlencode 'zero_header_af_client=' --data-urlencode 'app_id=106' --data-urlencode 'ogger_id=' --data-urlencode 'field_names[0]=firstname' --data-urlencode 'field_names[1]=reg_email__' --data-urlencode 'field_names[2]=reg_email__' --data-urlencode 'field_names[3]=sex' --data-urlencode 'field_names[4]=reg_passwd__' --data-urlencode 'firstname=hoang vu' --data-urlencode 'lastname=luc' --data-urlencode 'reg_email__=0986667049' --data-urlencode 'name_suggest_elig=false' --data-urlencode 'was_shown_name_suggestions=false' --data-urlencode 'did_use_suggested_name=false' --data-urlencode 'sex=1' --data-urlencode 'custom_gender=' --data-urlencode 'preferred_pronoun=' --data-urlencode 'did_use_suggested_name=false' --data-urlencode 'did_use_age=false' --data-urlencode 'birthday_day=5' --data-urlencode 'birthday_month=1' --data-urlencode 'birthday_year=2001' --data-urlencode 'age_step_input=' --data-urlencode 'reg_passwd__=324343243' --data-urlencode 'submit=Đăng ký''", deviceID);
            return ExecuteCMD(cmd);
        }

        public static string TurnOnHotspot(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + "shell am start -n com.android.settings/.TetherSettings", deviceID);
             ExecuteCMD(cmd);

             cmd = string.Format(CONSOLE_ADB + "shell input keyevent 20", deviceID);
             ExecuteCMD(cmd);

             cmd = string.Format(CONSOLE_ADB + "shell input keyevent 66", deviceID);
             ExecuteCMD(cmd);

             cmd = string.Format(CONSOLE_ADB + "shell input keyevent 66", deviceID);
            return ExecuteCMD(cmd);
        }

        public static string GetPublicIPAPI(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + "shell curl -s -X GET http://ip-api.com/json", deviceID);
            //string temp = ExecuteCMD(cmd);
            string temp = "";
            return temp;
        }
        public static string GetPublicIpSeeIp(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + "shell curl -s -X GET http://ip.seeip.org/geoip", deviceID);
            return ExecuteCMD(cmd);
        }

        public static string GetRealSim(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + "shell getprop gsm.sim.operator.alpha", deviceID);
            string ex =  ExecuteCMD(cmd);
            string sim = "";
            try
            {
                string[] dd = ex.Split('\n');
                sim = dd[4];
            }
            catch { }

            //cmd = string.Format(CONSOLE_ADB + "shell getprop gsm.network.type", deviceID);
            //ex = ExecuteCMD(cmd);
            
            //try
            //{
            //    string[] dd = ex.Split('\n');
            //    sim = sim + "-" + dd[4];
            //}
            //catch { }

            return sim;
        }

        public static string CurlGoogle(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + "shell su -c 'curl https://www.google.com/'", deviceID);
            return ExecuteCMD(cmd);
        }

        public static string Decode_UTF8(string s)
        {
            string text = "";
            byte[] bytes = Encoding.Default.GetBytes(s);
            text = Encoding.UTF8.GetString(bytes);

            return text;
        }
        public static string GetPublicIpLumtest(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + "shell su -c 'curl http://lumtest.com/myip.json'", deviceID);
            string temp = ExecuteCMD(cmd);

            temp = Decode_UTF8(temp);
            return Regex.Match(temp, "org_name\":\"(.*?)\"").Groups[1].ToString() + "|" + Regex.Match(temp, "ip\":\"(.*?)\"").Groups[1].ToString() + "|" + Regex.Match(temp, "region_name\":\"(.*?)\"").Groups[1].ToString();
        }

        public class IpInfo
        {
            [JsonProperty("isp")]
            public ispInfo isp { get; set; }

            [JsonProperty("city")]
            public CityInfo city { get; set; }
            [JsonProperty("proxy")]
            public ProxyInfo proxy { get; set; }
            [JsonProperty("country")]
            public CountryInfo country { get; set; }
        }

        public class ispInfo
        {
            [JsonProperty("isp")]
            public string isp { get; set; }
            [JsonProperty("asn")]
            public string asn { get; set; }
            [JsonProperty("domain")]
            public string domain { get; set; }
            [JsonProperty("organization")]
            public string organization { get; set; }

        }

        public class CityInfo
        {
            [JsonProperty("name")]
            public string name { get; set; }


            [JsonProperty("code")]
            public string code { get; set; }

        }

        public class CountryInfo
        {
            [JsonProperty("code")]
            public string code { get; set; }


            [JsonProperty("name")]
            public string name { get; set; }
            [JsonProperty("continent")]
            public string continent { get; set; }
        }
        public class ProxyInfo
        {
            [JsonProperty("ip")]
            public string ip { get; set; }
        }



        public static string GetPublicIpSmartProxy(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + "shell su -c 'curl http://ip.smartproxy.com/json'", deviceID);
            string temp = ExecuteCMD(cmd);

            temp = Decode_UTF8(temp);
            //string temp = Regex.Match(temp1, "\\{(.|\\s)*\\}\\n").Groups[1].ToString();
            //IpInfo data = JsonConvert.DeserializeObject<IpInfo>(temp);
            if (!string.IsNullOrEmpty(temp) && temp.Length > 250)
            {
                temp = temp.Substring(150);
            } else
            {
                return "";
            }
            temp = temp.Replace(" ", "").Replace("\\r", "").Replace(System.Environment.NewLine, "").Replace(System.Environment.NewLine, "");
            //return temp;
            var brownserMatch = Regex.Match(temp, "name\":\"(.*?)\"");
            var cityNameMatch = brownserMatch.NextMatch();
            string cityName = cityNameMatch.Groups[1].ToString();
            var countryNameMatch = cityNameMatch.NextMatch();
            string countryName = countryNameMatch.Groups[1].ToString();
            return  Regex.Match(temp, "ip\":\"(.*?)\"").Groups[1].ToString()+ "|" + cityName + "|" + Regex.Match(temp, "organization\":\"(.*?)\"").Groups[1].ToString();

            //if (data != null)
            //{
            //    return data.country.code + "|" + data.country.name + "|" + data.country.continent + "|" + data.isp.isp + "|" + data.isp.asn + "|" + data.isp.domain + "|" + data.city.name + "|" + data.city.code + "|" + data.proxy.ip;
            //}
            //return temp;
        }
        public static void DisableWifi(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + "shell su -c 'svc wifi disable'", deviceID);
            ExecuteCMD(cmd);
        }
        public static void AirplaneOn(string deviceID)
        {
            if (deviceID.Contains(":"))
            {
                return;
            }
            string ssid = Device.GetWifiStatus(deviceID);
            if (!ssid.Contains("unknown"))
            {
                return;
            }
            string cmd = string.Format(CONSOLE_ADB + "shell settings put global airplane_mode_on 1", deviceID);
            ExecuteCMD(cmd);
            cmd = string.Format(CONSOLE_ADB + "shell am broadcast -a android.intent.action.AIRPLANE_MODE --ez state true", deviceID);
            ExecuteCMD(cmd);
        }
        public static void OpenSetting(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + "shell am start -a android.settings.SETTINGS", deviceID);
            ExecuteCMD(cmd);
        }
        public static void OpenSettingAccount(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + "shell am start -a android.settings.SYNC_SETTINGS", deviceID);
            ExecuteCMD(cmd);
        }
        public static void OpenDebugingSetting(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + "shell am start -a com.android.settings.APPLICATION_DEVELOPMENT_SETTINGS", deviceID);
            ExecuteCMD(cmd);
        }

        public static void OpenApnSetting(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + "shell am start -a android.settings.APN_SETTINGS", deviceID);
            ExecuteCMD(cmd);
        }

        public static void MaxBright(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + "shell settings put system screen_brightness_mode 0", deviceID);
            ExecuteCMD(cmd);
            cmd = string.Format(CONSOLE_ADB + "shell settings put system screen_brightness 80", deviceID);
            ExecuteCMD(cmd);
        }
        public static void MinBright(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + "shell settings put system screen_brightness_mode 0", deviceID);
            ExecuteCMD(cmd);
            cmd = string.Format(CONSOLE_ADB + "shell settings put system screen_brightness 0", deviceID);
            ExecuteCMD(cmd);
        }
        public static void OpenRoamingSetting(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + "shell am start -a android.settings.DATA_ROAMING_SETTINGS", deviceID);
            ExecuteCMD(cmd);
        }

        public static bool AdbRoot(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " root", deviceID);
            string result = ExecuteCMD(cmd);
            if (result.Contains("disabled"))
            {
                return false;
            }
            return true;
        }

        public static void AirplaneOff(string deviceID)
        {
            if (deviceID.Contains(":"))
            {
                return;
            }
            string ssid = Device.GetWifiStatus(deviceID);
            if (!ssid.Contains("unknown"))
            {
                return;
            }
            string cmd = string.Format(CONSOLE_ADB + " shell settings put global airplane_mode_on 0", deviceID);
            ExecuteCMD(cmd);
            cmd = string.Format(CONSOLE_ADB + " shell am broadcast -a android.intent.action.AIRPLANE_MODE --ez state true", deviceID);
            ExecuteCMD(cmd);
        }

        public static void MonkeyTouch1(string deviceID, string package)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell monkey -p {1} -v 10", deviceID, package);
            ExecuteCMD(cmd);
        }
        public static void SetScreenTimeout(string deviceID, int timeoutMinutes)
        {
            timeoutMinutes = timeoutMinutes * 60000;
            string cmd = string.Format(CONSOLE_ADB + " shell settings put system screen_off_timeout {1}", deviceID, timeoutMinutes);
             ExecuteCMD(cmd);
        }

        public static void MakePhoneCall(string deviceID, string phone)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell am start -a android.intent.action.CALL -d tel:{1}", deviceID, phone);
            ExecuteCMD(cmd);

            Thread.Sleep(5000);
            
            cmd = string.Format(CONSOLE_ADB + " adb shell input keyevent 6", deviceID);
            ExecuteCMD(cmd);
        }
        public static void EndCall(string deviceID)
        {

            string cmd = string.Format(CONSOLE_ADB + " adb shell input keyevent 6", deviceID);
            ExecuteCMD(cmd);
        }
        public static void SetTimeZone(string deviceID, string timezone)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell service call alarm 3 s16 {1}", deviceID, timezone);
            ExecuteCMD(cmd);
        }

        public static void SetWifi(string deviceID, string ssid, string pass)
        {
            EnableWifi(deviceID);
            Thread.Sleep(1000);
            ClearCache(deviceID, "com.steinwurf.adbjoinwifi");
            ssid = ssid.Replace(" ", "\\ ");
            string cmd = string.Format(CONSOLE_ADB + " shell am start -n com.steinwurf.adbjoinwifi/.MainActivity " +
                "-e ssid \"{1}\" -e password_type WPA  -e password {2} ", deviceID, ssid, pass);
            string result = ExecuteCMD(cmd);

            Thread.Sleep(3000);
            ClearCache(deviceID, "com.steinwurf.adbjoinwifi");
        }
        public static void SetProxyCmd(string deviceID,  string proxyPort)
        {
            if (proxyPort.Contains("@"))
            {
                string[] ll = proxyPort.Split('@');

                string username = ll[0].Split(':')[0];
                string pass = ll[0].Split(':')[1];
                string host = ll[1].Split(':')[0];
                string port = ll[1].Split(':')[1];

                string cmd = string.Format(CONSOLE_ADB + " shell settings put global http_proxy {1}", deviceID, ll[1]);
                ExecuteCMD(cmd);

                cmd = string.Format(CONSOLE_ADB + " shell settings put global http_proxy_host {1}", deviceID, host);
                ExecuteCMD(cmd);

                cmd = string.Format(CONSOLE_ADB + " shell settings put global http_proxy_port {1}", deviceID, port);
                ExecuteCMD(cmd);

                cmd = string.Format(CONSOLE_ADB + " shell settings put global http_proxy_username {1}", deviceID, username);
                ExecuteCMD(cmd);
                cmd = string.Format(CONSOLE_ADB + " shell settings put global http_proxy_password {1}", deviceID, pass);
                ExecuteCMD(cmd);


            } else
            {
                Thread.Sleep(1000);
                // Set proxy
                string cmd = string.Format(CONSOLE_ADB + " shell settings put global http_proxy {1}", deviceID, proxyPort);
                ExecuteCMD(cmd);
            }
           
        }
        public static void RemoveProxy(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell settings put global http_proxy :0", deviceID);
            ExecuteCMD(cmd);
        }
        public static string GetScreenStatus(string deviceID)
        {
            try
            {
                string status = "";
                string cmd = string.Format(CONSOLE_ADB + " shell dumpsys nfc", deviceID);
                string result = ExecuteCMD(cmd);
                if (result.Contains("mScreenState"))
                {
                    status = Regex.Match(result, "mScreenState=(.*?)\r\n").Groups[1].ToString();
                }


                return status;
            } catch(Exception ex)
            {
                return "miss";
            }
        }

        public static string GetWifiStatus(string deviceID)
        {
            string status = "";
            string cmd = string.Format(CONSOLE_ADB + " shell dumpsys wifi", deviceID);
            string result = ExecuteCMD(cmd);
            if (result.Contains("mWifiInfo SSID"))
            {
                status = Regex.Match(result, "mWifiInfo SSID:(.*?),").Groups[1].ToString();
            }


            return status;
        }

        public static void InstallApp(string deviceID, string app)
        {
            string cmd = string.Format(CONSOLE_ADB + " install \"{1}\"", deviceID, app);
            ExecuteCMD(cmd);
        }
        

        public static void ForceStop(string deviceID, string package)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell am force-stop {1}", deviceID, package);
            ExecuteCMD(cmd);
        }
        public static void RebootByCmd(string deviceID)
        {
            RebootDevice(deviceID);
        }
        public static void RebootDevice(string deviceID)
        {
            DeviceObject device = DeviceManager.GetDevice(deviceID);
            if (device == null)
            {
                return;
            }
            device.adbStatus = Constant.ADB_DEVICE_RESTART;
            device.deviceId = DeviceManager.GetRealDeviceId(deviceID) + Constant.ADB_DEVICE_RESTART;

            RunAdb(deviceID, "shell su -c reboot");
            WaitForBootComplete(deviceID);
            if (Utility.isScreenLock(deviceID))
            {
                Device.Unlockphone(deviceID);
            }

            device.adbStatus = Constant.ADB_DEVICE_NORMAL;
            device.deviceId = DeviceManager.GetRealDeviceId(deviceID);
        }
        public static bool WaitForBootComplete(string deviceId = "", int timeoutSeconds = 60)
        {
            RunAdb(deviceId, "wait-for-device");
            var sw = Stopwatch.StartNew();
            while (sw.Elapsed.TotalSeconds < timeoutSeconds)
            {
                string output = RunAdb(deviceId, "shell getprop sys.boot_completed");
                if (output.Trim() == "1")
                {
                    Console.WriteLine("✅ Device boot completed.");
                    return true;
                }

                Thread.Sleep(2000);
            }
            Console.WriteLine("❌ Timeout waiting for device boot.");
            return false;
        }

        public static void ResetConnectionDevices()
        {
            ExecuteCMD("adb kill-server");
            Thread.Sleep(1000);
            ExecuteCMD("adb devices");
            Thread.Sleep(3000);
        }
        
        public static void SelectAdbKeyboard(string deviceID, bool isAdbKeyboard)
        {
            if (isAdbKeyboard)
            {
                SelectAdbKeyboard(deviceID);
            } else
            {
                StopAdbKeyboard(deviceID);
            }
        }

        public static void KillApp(string deviceID, string package)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell am force-stop {1}", deviceID, package);
            ExecuteCMD(cmd);
        }

        public static void SelectAdbKeyboard(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell ime set com.android.adbkeyboard/.AdbIME", deviceID);
            ExecuteCMD(cmd);
        }
        public static void StopAdbKeyboard(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell am force-stop com.android.adbkeyboard", deviceID);
            ExecuteCMD(cmd);
        }

        public static void SelectLabanKeyboard(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell ime set com.vng.inputmethod.labankey/.LatinIME", deviceID);
            ExecuteCMD(cmd);
        }
        public static void StopLabanKeyboard(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell am force-stop com.vng.inputmethod.labankey/.LatinIME", deviceID);
            ExecuteCMD(cmd);
        }

        public static void SelectDefaultKeyboard(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell ime set com.android.inputmethod.latin/.LatinIME", deviceID);
            ExecuteCMD(cmd);
        }

        public static void InputText(string deviceID, string text)
        {
            string cmdCommand = string.Format(INPUT_TEXT_DEVICES, deviceID, text.Replace(" ", "%s").Replace("&", "\\&").Replace("<", "\\<").Replace(">", "\\>").Replace("?", "\\?").Replace(":", "\\:").Replace("{", "\\{").Replace("}", "\\}").Replace("[", "\\[").Replace("]", "\\]").Replace("|", "\\|"));
            string text2 = ExecuteCMD(cmdCommand);
        }

        
        public static string GetIPV6(string deviceID)
        {
            try
            {
                string ip = "";
                string cmd = string.Format(CONSOLE_ADB + " shell ip addr", deviceID);
                string text = ExecuteCMD(cmd);
                ip = Regex.Match(text, "inet6 (.*?) scope link").Groups[1].ToString();
                return ip;
            } catch (Exception e)
            {
                return "";
            }
            
        }

        public static string GetPublicIP(string deviceID)
        {
            try
            {
                string ip = "";
                string cmd = string.Format(CONSOLE_ADB + " shell ip addr", deviceID);
                string text = ExecuteCMD(cmd);
                if (!string.IsNullOrEmpty(text) && text.Contains("scope global"))
                {
                    ip = Regex.Match(text, "inet(.*?) scope global").Groups[1].ToString();
                }
                
                return ip;
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public static void DeleteChar(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell input keyevent 67", deviceID);
            ExecuteCMD(cmd);
        }

        public static void TurnOnAutoRotate(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell settings put system accelerometer_rotation 1", deviceID);
            ExecuteCMD(cmd);
        }
        
        public static void PortraitRotate(string deviceID)
        {
            //string cmd = string.Format(CONSOLE_ADB + " shell settings put system accelerometer_rotation 0", deviceID);
            //ExecuteCMD(cmd);
            // cmd = string.Format(CONSOLE_ADB + " shell content insert --uri content://settings/system --bind name:s:accelerometer_rotation --bind value:i:0", deviceID);
            //ExecuteCMD(cmd);

        }

        public static void ChangeLanguageVN(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell pm grant net.sanapeli.adbchangelanguage android.permission.CHANGE_CONFIGURATION", deviceID);
            ExecuteCMD(cmd);
            cmd = string.Format(CONSOLE_ADB + " shell am start -n net.sanapeli.adbchangelanguage/.AdbChangeLanguage -e language vi -e country VN", deviceID);
            ExecuteCMD(cmd);
        }

        public static void ChangeLanguageUS(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell pm grant net.sanapeli.adbchangelanguage android.permission.CHANGE_CONFIGURATION", deviceID);
            ExecuteCMD(cmd);
            cmd = string.Format(CONSOLE_ADB + " shell am start -n net.sanapeli.adbchangelanguage/.AdbChangeLanguage -e language en -e country US", deviceID);
            ExecuteCMD(cmd);
        }

        public static string GetDeviceInfo(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell getprop", deviceID);
            return ExecuteCMD(cmd);
        }

        public static void LandscapeRotate(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell content insert --uri content://settings/system --bind name:s:user_rotation --bind value:i:1", deviceID);
            ExecuteCMD(cmd);
        }
        public static void MoveEndTextbox(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell input keyevent KEYCODE_MOVE_END", deviceID);
            ExecuteCMD(cmd);
        }
        public static void DeleteAllChars(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell input keyevent --longpress KEYCODE_DEL", deviceID);
            
            ExecuteCMD(cmd);

            cmd = string.Format(CONSOLE_ADB + " shell input keyevent KEYCODE_CLEAR", deviceID);

            ExecuteCMD(cmd);

        }

        public static void DeleteChars(string deviceID, int numChar = 1)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell input keyevent ", deviceID);
            for (int i = 0; i < numChar; i++)
            {
                cmd = cmd + " KEYCODE_DEL ";
            }
            ExecuteCMD(cmd);
        }

        public static void Unlockphone(string deviceID)
        {
            if (Utility.isScreenLock(deviceID))
            {
                string cmd = "";
                if (!deviceID.StartsWith("R"))
                {
                    cmd = string.Format(CONSOLE_ADB + " shell input keyevent KEYCODE_POWER", deviceID);
                    ExecuteCMD(cmd);
                }
                
                 cmd = string.Format(CONSOLE_ADB + " shell input keyevent 3", deviceID);
                ExecuteCMD(cmd);
                Thread.Sleep(500);
                Swipe(deviceID, 500, 2000, 500, 100, 500);
            }
        }
        public static string ReadClipboardAndroid(string deviceID, bool isFblite)
        {
            OpenApp(deviceID, "ch.pete.adbclipboard");
            Thread.Sleep(1000);
            string cmd = string.Format(CONSOLE_ADB + " shell am broadcast -n \"ch.pete.adbclipboard/.ReadReceiver\"", deviceID);
            string result = ExecuteCMD(cmd);
            int pFrom = result.IndexOf("data=\"") + "data=\"".Length;
            int pTo = result.LastIndexOf("\"");
            result = result.Substring(pFrom, pTo - pFrom);
            result = result.Replace(" ", "");
            Console.WriteLine(result);
            // Clear clipboard
            cmd = string.Format(CONSOLE_ADB + "shell am broadcast -n \"ch.pete.adbclipboard/.WriteReceiver\" -e text \" \"", deviceID);
            ExecuteCMD(cmd);

            if (isFblite)
            {
                OpenApp(deviceID, "com.facebook.lite");
            } else
            {
                OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
            }
            return result;
        }

        public static string ResetClipboardAndroid(string deviceID, bool isFbLite)
        {
            OpenApp(deviceID, "ch.pete.adbclipboard");
            Thread.Sleep(1000);
            string cmd = string.Format(CONSOLE_ADB + " shell am broadcast -n \"ch.pete.adbclipboard/.WriteReceiver\" -e \"urlEncodedString\"", deviceID);
            string result = ExecuteCMD(cmd);
            int pFrom = result.IndexOf("data=\"") + "data=\"".Length;
            int pTo = result.LastIndexOf("\"");
            result = result.Substring(pFrom, pTo - pFrom);
            result = result.Replace(" ", "");
            Console.WriteLine(result);
            Thread.Sleep(1000);
            
            if (isFbLite)
            {
                OpenApp(deviceID, "com.facebook.lite");
            } else
            {
                OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
            }
             
            return result;
        }
        public static bool IsAirPlaneMode(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell settings get global airplane_mode_on", deviceID);
            string result = ExecuteCMD(cmd);
            if (result.Contains("airplane_mode_on" + Environment.NewLine + "1"))
            {
                return true;
            }
            return false;
        }
        public static void DeleteAllImages(string deviceId)
        {
            string[] folders = { "/sdcard/DCIM/", "/sdcard/Pictures/", "/sdcard/Download/" };

            foreach (var folder in folders)
            {
                RunAdb(deviceId, $"shell rm -rf {folder}*");
                RunAdb(deviceId, $"shell am broadcast -a android.intent.action.MEDIA_SCANNER_SCAN_FILE -d file://{folder}");
            }

            Console.WriteLine("🧹 Đã xoá toàn bộ ảnh và cập nhật thư viện Android.");
        }

        public static void DeleteAllScreenshot(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell  \" su -c rm -f /sdcard/Pictures/Facebook/*.jpg ", deviceID);
            string result = ExecuteCMD(cmd);
            cmd = string.Format(CONSOLE_ADB + " shell  \" su -c rm -f /sdcard/Pictures/Screenshots/*.png ", deviceID);
            result = ExecuteCMD(cmd);
            Thread.Sleep(200);
            cmd = string.Format(CONSOLE_ADB + "shell  \" su -c rm -rf  /storage/emulated/0/*.png ", deviceID);
            result = ExecuteCMD(cmd);
            cmd = string.Format(CONSOLE_ADB + " shell  \" su -c rm -rf  /sdcard/Download/*.png ", deviceID);
            result = ExecuteCMD(cmd);
            Thread.Sleep(1000);
            //DeleteAllImages(deviceID);
            //Thread.Sleep(1000);
            ClearCache(deviceID, "com.android.gallery3d");
            Thread.Sleep(1000);
        }
        public static void DeleteTxtSdcard(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell  \" su -c rm -f /sdcard/*.txt ", deviceID);
            string result = ExecuteCMD(cmd);
            cmd = string.Format(CONSOLE_ADB + " shell  \" su -c rm -f /sdcard/*.png ", deviceID);
            result = ExecuteCMD(cmd);
        }

        public static void DeleteFolderSdcard( string deviceID, string folder)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell  \" su -c rm -rf /sdcard/" + folder + "\"", deviceID);
            string result = ExecuteCMD(cmd);
        }
        public static bool PushAvatarRaw(string deviceID, string pcPath)
        {
            try
            {
                string cmd = string.Format(CONSOLE_ADB + " push \"{1}\" /sdcard/Download/0avatar.png", deviceID, pcPath);
                string result = ExecuteCMD(cmd);
                Thread.Sleep(1000);
                cmd = string.Format(CONSOLE_ADB + " shell  \" su -c am broadcast -a android.intent.action.MEDIA_SCANNER_SCAN_FILE -d file:///sdcard/Download/0avatar.png\"", deviceID);
                result = ExecuteCMD(cmd);
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            
            return true;
        }
        public static bool PushFile2Alarms(string deviceID, string pcPath)
        {
            try
            {
                string cmd = string.Format(CONSOLE_ADB + " push \"{1}\" /sdcard/Alarms", deviceID, pcPath);
                string result = ExecuteCMD(cmd);
                Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        public static bool PushFileBuildProp(string deviceID, string pcPath)
        {
            try
            {
                // unmount

                string cmd = string.Format(CONSOLE_ADB + " push \"{1}\" /sdcard/Alarms", deviceID, pcPath);
                string result = ExecuteCMD(cmd);
                Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }


        public static bool PushFile2Pictures(string deviceID, string pcPath)
        {
            try
            {
                string cmd = string.Format(CONSOLE_ADB + " push \"{1}\" /sdcard/pictures", deviceID, pcPath);
                string result = ExecuteCMD(cmd);
                Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        public static bool PushFile2Sdcard(string deviceID, string pcPath, string fileName)
        {
            try
            {
                string cmd = string.Format(CONSOLE_ADB + " push {1} /sdcard/" + fileName, deviceID, pcPath);
                string result = ExecuteCMD(cmd);
                Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }
        public static bool PushFile2Music(string deviceID, string pcPath)
        {
            try
            {
                string cmd = string.Format(CONSOLE_ADB + " push \"{1}\" /sdcard/music", deviceID, pcPath);
                string result = ExecuteCMD(cmd);
                Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }
        public static bool PushFile2AndroidFolder(string deviceID, string pcPath)
        {
            try
            {
                string cmd = string.Format(CONSOLE_ADB + " push \"{1}\" /sdcard/Android", deviceID, pcPath);
                string result = ExecuteCMD(cmd);
                Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        public static bool PushBase64ToDeviceAndDecode(string deviceID, string base64, string outputPathOnDevice = "/sdcard/output.bin")
        {
            try
            {
                string tempBase64Path = "/sdcard/temp.b64";

                Console.WriteLine("🛠️ Đang gửi base64 lên thiết bị...");

                // Bước 1: Xóa file cũ nếu có
                RunAdb(deviceID, $"shell su -c \" rm {tempBase64Path}\"");

                // Bước 2: Cắt base64 thành từng dòng nhỏ và gửi qua echo
                int chunkSize = 3000;
                for (int i = 0; i < base64.Length; i += chunkSize)
                {
                    string chunk = base64.Substring(i, Math.Min(chunkSize, base64.Length - i));
                    string safeChunk = chunk.Replace("\"", "\\\""); // Escape dấu nháy
                    RunAdb(deviceID, $"shell echo \"{safeChunk}\" >> {tempBase64Path}");
                }

                Console.WriteLine("📦 Đã gửi xong base64, đang giải mã...");

                // Bước 3: Decode base64 trên thiết bị → file thực
                RunAdb(deviceID, $"shell su -c \" base64 -d {tempBase64Path} > {outputPathOnDevice}\"");
                Thread.Sleep(1000);
                ///RunAdb(deviceID, "shell am broadcast -a android.intent.action.MEDIA_SCANNER_SCAN_FILE -d file:///sdcard/DCIM/0avatar.jpg");
                Thread.Sleep(1000);
                string cmd = string.Format(CONSOLE_ADB + " shell  \" su -c am broadcast -a android.intent.action.MEDIA_SCANNER_SCAN_FILE -d file:///sdcard/Download/0avatar.png\"", deviceID);
                ExecuteCMD(cmd);
                Console.WriteLine($"✅ Đã tạo file: {outputPathOnDevice}");
            } catch (Exception ex)
            {
                return false;
            }
            return true;
        }


        public static bool PushAvatar(string deviceID, OrderObject order)
        {

            //if (order.pushAvatar)
            //{
            //    return false;
            //}
            Device.DeleteAllScreenshot(deviceID);


            AvatarObject cacheName = CacheServer.GetAvatarLocalCache( PublicData.CacheServerUri, order.gender, deviceID);

            if (!PushBase64ToDeviceAndDecode(deviceID, cacheName.base64, "/sdcard/Download/0avatar.png"))
            {
                return false;
            }

            //string pcPath = Utility.RandomAvatar(deviceID, order.gender, order.language, order.forceAvatarUs);
            //if (string.IsNullOrEmpty(pcPath) || !File.Exists(pcPath))
            //{
            //    order.pushAvatar = false;
            //    return false;
            //}
            //byte[] imageBytes = File.ReadAllBytes(pcPath);

            //using (var ms = new MemoryStream(imageBytes))
            //{
            //    //var image = Image.FromStream(ms);
            //    string newPath = pcPath.Insert(pcPath.Length - 5, "temp");
            //    //string randomImage = Utility.DownloadRandomCover();
            //    //if (File.Exists(randomImage))
            //    //{
            //    //    Image imageBackground = image;
            //    //    Image imageOverlay = Image.FromFile(randomImage);

            //    //    Image img = new Bitmap(imageBackground.Width, imageBackground.Height);
            //    //    using (Graphics gr = Graphics.FromImage(img))
            //    //    {
            //    //        gr.DrawImage(imageBackground, new Point(0, 0));
            //    //        gr.DrawImage(imageOverlay, new Point(0, 0));
            //    //    }
            //    //    img.Save(newPath, ImageFormat.Png);
            //    //}
            //    PushAvatarRaw(deviceID, pcPath);

            //    if (File.Exists(newPath))
            //    {
            //        // If file found, delete it    
            //        File.Delete(newPath);
            //        Console.WriteLine("File deleted.");
            //    }
            //}

            //// Delete
            //if (File.Exists(pcPath))
            //{
            //    // If file found, delete it    
            //    File.Delete(pcPath);
            //    Console.WriteLine("File deleted.");
            //}
            order.pushAvatar = true;
            return true;
        }
        public static void PushCoverAvatar(string deviceID, OrderObject order)
        {
            Device.DeleteAllScreenshot(deviceID);
            string pcPath = Utility.RandomCover();
            byte[] imageBytes = File.ReadAllBytes(pcPath);

            using (var ms = new MemoryStream(imageBytes))
            {
                var image = Image.FromStream(ms);
                string newPath = pcPath.Insert(pcPath.Length - 5, "temp");
                //string randomImage = Utility.DownloadRandomCover();
                //if (File.Exists(randomImage))
                //{
                //    Image imageBackground = image;
                //    Image imageOverlay = Image.FromFile(randomImage);

                //    Image img = new Bitmap(imageBackground.Width, imageBackground.Height);
                //    using (Graphics gr = Graphics.FromImage(img))
                //    {
                //        gr.DrawImage(imageBackground, new Point(0, 0));
                //        gr.DrawImage(imageOverlay, new Point(0, 0));
                //    }
                //    img.Save(newPath, ImageFormat.Png);
                //}
                PushAvatarRaw(deviceID, pcPath);

                if (File.Exists(newPath))
                {
                    // If file found, delete it    
                    File.Delete(newPath);
                    Console.WriteLine("File deleted.");
                }
            }

            // Delete
            if (File.Exists(pcPath))
            {
                // If file found, delete it    
                File.Delete(pcPath);
                Console.WriteLine("File deleted.");
            }
            order.pushCoverAvatar = true;
        }
        public static void PushCover(string deviceID)
        {

            Device.DeleteAllScreenshot(deviceID);
            string pcPath = Utility.RandomCover();
            PushAvatarRaw(deviceID, pcPath);
            if (File.Exists(pcPath))
            {
                // If file found, delete it    
                File.Delete(pcPath);
                Console.WriteLine("File deleted.");
            }
        }
        public static void TapByPercentDelay(string deviceID, double x, double y, int delay = 1000)
        {
            TapByPercent(deviceID, x, y,  delay);
        }
        public static void TapByPercent(string deviceID, double x, double y, int sleep = 0)
        {
            Point screenResolution = GetScreenResolution(deviceID);
            int num = (int)(x * ((double)screenResolution.X * 1.0 / 100.0));
            int num2 = (int)(y * ((double)screenResolution.Y * 1.0 / 100.0));
            string text = string.Format(TAP_DEVICES, deviceID, num, num2);
            for (int i = 1; i < 1; i++)
            {
                text = text + " && " + string.Format(TAP_DEVICES, deviceID, x, y);
            }
            string text2 = ExecuteCMD(text);
            if (sleep > 0)
            {
                Thread.Sleep(sleep); ;
            }
        }

        public static Point GetScreenResolution(string deviceID)
        {
            string cmdCommand = string.Format(GET_SCREEN_RESOLUTION, deviceID);
            string text = ExecuteCMD(cmdCommand);
            text = text.Substring(text.IndexOf("- "));
            text = text.Substring(text.IndexOf(' '), text.IndexOf(')') - text.IndexOf(' '));
            string[] array = text.Split(new char[]
            {
        ','
            });
            int x = Convert.ToInt32(array[0].Trim());
            int y = Convert.ToInt32(array[1].Trim());
            return new Point(x, y);
        }
        public static void TapDelay(string deviceID, double x, double y, int delay = 1000)
        {
            Tap(deviceID, (int)x, (int)y);
            Thread.Sleep(delay);
        }
        public static string Tap(string deviceID, int x, int y, int count = 1)
        {
            string text = string.Format(CONSOLE_ADB + "shell input tap {1} {2}", deviceID, x, y);
            for (int i = 1; i < count; i++)
            {
                text = text + " && " + string.Format(CONSOLE_ADB + "shell input tap {1} {2}", deviceID, x, y);
            }
            string rr = ExecuteCMD(text);
            Thread.Sleep(100);
            return rr;
        }

        public static string TapRoot(string deviceID, int x, int y, int count = 1)
        {
            string text = string.Format("shell input tap {0} {1}", x, y);
            //for (int i = 1; i < count; i++)
            //{
            //    text = text + " && " + string.Format(CONSOLE_ADB + "shell input tap {1} {2}", deviceID, x, y);
            //}
            return RunAdb(deviceID, text);
        }
        public static string TapPoint(string deviceID, Point p)
        {
            return Tap(deviceID, p.X, p.Y);
        }
        public static void Swipe(string deviceID, int x1, int y1, int x2, int y2, int duration = 100)
        {
            string cmdCommand = string.Format(CONSOLE_ADB + "shell input swipe {1} {2} {3} {4} {5}", new object[]
            {
                deviceID,
                x1,
                y1,
                x2,
                y2,
                duration
            });
            string text = ExecuteCMD(cmdCommand);
            Thread.Sleep(200);
        }
        public static void TabPress(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell input keyevent 61", deviceID);
            ExecuteCMD(cmd);
            Thread.Sleep(200);
        }
        public static void EnterPress(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell input keyevent 66", deviceID);
            ExecuteCMD(cmd);

        }

        public static void Back(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell input keyevent 4", deviceID);
            ExecuteCMD(cmd);
            Thread.Sleep(1000);
        }
        
        public static void GotoFbSettings(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell am start -a android.intent.action.VIEW -d fb://settings", deviceID);
            ExecuteCMD(cmd);
            Thread.Sleep(3000);
        }
        public static void GotoFbSettingWeb(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell am start -a android.intent.action.VIEW -d fb://faceweb/f?href=https://m.facebook.com/security/2fac/settings/", deviceID);
            ExecuteCMD(cmd);
            Thread.Sleep(3000);
        }
        public static void GotoFbAccountSettings(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell am start -a android.intent.action.VIEW -d fb://account_settings", deviceID);
            ExecuteCMD(cmd);

        }
        public static void PermissionAppReadContact(string deviceID, string package)
        {
            KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell pm grant " + package + " android.permission.READ_CONTACTS");
        }

        public static void PermissionReadContact(string deviceID)
        {
            KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell pm grant com.facebook.katana android.permission.READ_CONTACTS");
        }
        public static void PermissionAppCallPhone(string deviceID, string package)
        {
            KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell pm grant " + package + " android.permission.CALL_PHONE");
        }
        public static void PermissionCallPhone(string deviceID)
        {
            KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell pm grant com.facebook.katana android.permission.CALL_PHONE");
        }
        public static void PermissionCamera(string deviceID)
        {
            KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell pm grant com.facebook.katana android.permission.CAMERA");
        }

        public static void PermissionAppReadPhoneState(string deviceID, string package)
        {
            KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell pm grant " + package + " android.permission.READ_PHONE_STATE");
        }

        public static void PermissionApp(string deviceID, string package, string permission)
        {
            KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell pm grant " + package + " android.permission." + permission);
        }

        public static void PermissionReadPhoneState(string deviceID)
        {
            KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell pm grant com.facebook.katana android.permission.READ_PHONE_STATE");
        }
        public static void GotoFbRegister(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell am start -a android.intent.action.VIEW -d fb://registration", deviceID);
            ExecuteCMD(cmd);

        }
        public static void GotoOnline(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell am start -a android.intent.action.VIEW -d fb://online", deviceID);
            ExecuteCMD(cmd);

        }

        public static void GotoFbSecuritySettings(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell am start -a android.intent.action.VIEW -d fb://security_settings", deviceID);
            ExecuteCMD(cmd);

        }
        public static bool IsInstallApp(string deviceID, string appName = "com.facebook.katana")
        {
            bool result = false;
            int i = 0;
            while (i < 3)
            {
                if (ExecuteCMD(string.Format(CONSOLE_ADB + "shell pm list packages", deviceID)).Trim().Contains(appName))
                {
                    result = true;
                    return result;
                }
                i++;
                Thread.Sleep(1000);
            }
            return result;
        }
        public static void CloseApp(string deviceID, string AppPackage)
        {
            ExecuteCMD(string.Format(CONSOLE_ADB + "shell am force-stop ", deviceID) + AppPackage);
        }
        public static int Connect2Wifi(string deviceID, string userName, string passWord, string type = "WPA")
        {
            int result = 0;
            try
            {
                string cmd = string.Format(CONSOLE_ADB + " shell settings put global http_proxy :0", deviceID);
                
                ExecuteCMD(cmd);
                if (IsInstallApp(deviceID, "com.steinwurf.adbjoinwifi"))
                {
                    CloseApp(deviceID, "com.steinwurf.adbjoinwifi");
                    Thread.Sleep(750);
                    string cmd2 = string.Concat(new string[]
                    {
                        "adb -s ",
                        deviceID,
                        " shell am start -n com.steinwurf.adbjoinwifi/.MainActivity -e ssid '",
                        userName,
                        "' -e password_type ",
                        type,
                        " -e password '",
                        passWord,
                        "'"
                    });
                    ExecuteCMD( cmd2);
                    result = 1;
                    Thread.Sleep(500);
                }
                else
                {
                    result = -1;
                }
            }
            catch (Exception)
            {
            }
            return result;
        }
        public static void OpenWifiSetting(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell am start -a android.settings.WIFI_SETTINGS", deviceID);
            ExecuteCMD(cmd);
        }
        public static void SetPortrait(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell content insert --uri content://settings/system --bind name:s:user_rotation --bind value:i:0", deviceID);
            ExecuteCMD(cmd);

        }
        public static void GotoFbConfirm(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell am start -a android.intent.action.VIEW -d fb://dbl_login_activity", deviceID);
            ExecuteCMD(cmd);
        }

        public static string StartMicerService(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell am start -n eu.micer.clipboardservice/eu.micer.clipboardservice.EmptyActivity", deviceID);
            return ExecuteCMD(cmd);
        }

        public static void Paste(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell input keyevent 279", deviceID);
            ExecuteCMD(cmd);
        }
        public static string CopyClipboardDevice(string deviceID, string text)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell am startservice -a eu.micer.ClipboardService -e text \"{1}\"", deviceID, text);
            return ExecuteCMD(cmd);
        }
        public static void GotoFbFriend(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell am start -a android.intent.action.VIEW -d fb://friend", deviceID);
            ExecuteCMD(cmd);

        }
        public static void GotoFbProfile(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell am start -a android.intent.action.VIEW -d fb://profile", deviceID);
            ExecuteCMD(cmd);

        }

        public static void GotoFbProfileEdit(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell am start -a android.intent.action.VIEW -d fb://profile_edit", deviceID);
            ExecuteCMD(cmd);
            string uiXML = Utility.GetUIXml(deviceID);
            if (Utility.CheckTextExist(deviceID, "mởbằngfacebook", 1, uiXML))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 84.7, 78.1);
                Thread.Sleep(2000);
            }
            if (Utility.CheckTextExist(deviceID, "mởbằnglite", 1, uiXML))
            {
                Utility.FindImageAndTap(deviceID, Utility.CHOOSE_FB, 1);
                Thread.Sleep(2000);
            }
        }

        public static void GotoFbFriendRequests(string deviceID)
        {
            Device.PermissionReadContact(deviceID);
            Device.PermissionCallPhone(deviceID);
            Device.PermissionReadPhoneState(deviceID);
            Device.PermissionCamera(deviceID);
            string cmd = string.Format(CONSOLE_ADB + " shell am start -a android.intent.action.VIEW -d fb://requests", deviceID);
            ExecuteCMD(cmd);

        }

        public static void GotoFbGroup(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell am start -a android.intent.action.VIEW -d fb://groups", deviceID);
            ExecuteCMD(cmd);

        }
        public static void EnableMobileData(string deviceID) // 3g 4g
        {
            string cmd = string.Format(CONSOLE_ADB + " shell svc data enable", deviceID);
            ExecuteCMD(cmd);
        }
        public static void disableMobileData(string deviceID) // 3g 4g
        {
            string cmd = string.Format(CONSOLE_ADB + " shell svc data disable", deviceID);
            ExecuteCMD(cmd);
        }
        public static string RunAdbAsRoot(string deviceId, string shellCommand, int timeoutMs = 10000)
        {
            try
            {
                string fullCommand = $"shell su -c \"{shellCommand}\"";
                string adbArgs = (string.IsNullOrEmpty(deviceId) ? "" : $"-s {deviceId} ") + fullCommand;

                var startInfo = new ProcessStartInfo
                {
                    FileName = "adb",
                    Arguments = adbArgs,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                var process = new Process { StartInfo = startInfo };
                var outputBuilder = new StringBuilder();
                var errorBuilder = new StringBuilder();

                process.OutputDataReceived += (s, e) => { if (e.Data != null) outputBuilder.AppendLine(e.Data); };
                process.ErrorDataReceived += (s, e) => { if (e.Data != null) errorBuilder.AppendLine(e.Data); };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                if (!process.WaitForExit(timeoutMs))
                {
                    process.Kill();
                    throw new TimeoutException("⏰ ADB (su) command timed out.");
                }

                string error = errorBuilder.ToString().Trim();
                if (!string.IsNullOrWhiteSpace(error))
                    Console.WriteLine("⚠️ ADB su stderr: " + error);

                return outputBuilder.ToString().Trim();
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ RunAdbAsRoot exception: " + ex.Message);
                return "";
            }
        }

        public static string RunAdb(string deviceId, string args, int timeoutMs = 15000)
        {
            try
            {
                string modifiedArgs = args;

                if (args.StartsWith("shell") && !args.Contains("su -c"))
                {
                    // Chuyển lệnh shell thường thành shell su -c
                    var shellPart = args.Substring(6).Trim(); // Bỏ "shell"
                    modifiedArgs = $"shell su -c \"{shellPart}\"";
                }

                string adbArgs = (string.IsNullOrEmpty(deviceId) ? "" : $"-s {deviceId} ") + modifiedArgs;


                var startInfo = new ProcessStartInfo
                {
                    FileName = "adb",
                    Arguments = adbArgs,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.UTF8,
                    StandardErrorEncoding = Encoding.UTF8
                };

                var process = new Process { StartInfo = startInfo };

                var outputBuilder = new StringBuilder();
                var errorBuilder = new StringBuilder();

                process.OutputDataReceived += (s, e) => { if (e.Data != null) outputBuilder.AppendLine(e.Data); };
                process.ErrorDataReceived += (s, e) => { if (e.Data != null) errorBuilder.AppendLine(e.Data); };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                if (!process.WaitForExit(timeoutMs))
                {
                    process.Kill();
                    throw new TimeoutException("⏰ ADB command timed out.");
                }

                process.WaitForExit(); // đảm bảo stdout/stderr đọc xong

                string error = errorBuilder.ToString().Trim();
                if (!string.IsNullOrWhiteSpace(error))
                {
                    Console.WriteLine($"⚠️ ADB stderr: {error}");
                }

                return outputBuilder.ToString().Trim();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ RunAdb exception: {ex.Message}");
                return string.Empty;
            }

        }
            public static string ExecuteCMD1(string cmdCommand)
        {
            string result;
            try
            {
                Process process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = ADB_FOLDER_PATH,
                    FileName = "cmd.exe",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true
                };
                process.Start();
                process.StandardInput.WriteLine(cmdCommand);
                process.StandardInput.Flush();
                process.StandardInput.Close();
                process.WaitForExit();
                string text = process.StandardOutput.ReadToEnd();
                result = text;
            }
            catch
            {
                result = null;
            }
            return result;
        }
        public static string ExecuteCMD(string cmdCommand)
        {
            try
            {
                // get DeviceID
                string deviceID = "";
                if (cmdCommand.Contains("adb"))
                {
                    string check = Regex.Match(cmdCommand, "adb -s (.*?) ").ToString();
                    deviceID = check.Replace("adb -s ", "").Trim();
                }
                

                Process process = new Process();
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.WorkingDirectory = ADB_FOLDER_PATH;
                processStartInfo.FileName = "cmd.exe";
                processStartInfo.CreateNoWindow = true;
                processStartInfo.UseShellExecute = false;
                processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processStartInfo.RedirectStandardInput = true;
                processStartInfo.RedirectStandardOutput = true;
                process.StartInfo = processStartInfo;
                //process.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);

                //if (!string.IsNullOrEmpty(deviceID) && deviceID.Contains("5555"))
                //{
                //    process.Start();
                //process.StandardInput.WriteLine("adb connect " + deviceID);
                //process.StandardInput.Flush();
                //process.StandardInput.Close();
                //}
                

                process.Start();
                process.StandardInput.WriteLine(cmdCommand);
                process.StandardInput.Flush();
                process.StandardInput.Close();

                string result = process.StandardOutput.ReadToEnd();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public static string ExecuteCMDTimeout(string cmdCommand, int second)
        {
            try
            {
                Process process = new Process();
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.WorkingDirectory = ADB_FOLDER_PATH;
                processStartInfo.FileName = "cmd.exe";
                processStartInfo.CreateNoWindow = true;
                processStartInfo.UseShellExecute = false;
                processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processStartInfo.RedirectStandardInput = true;
                processStartInfo.RedirectStandardOutput = true;
                process.StartInfo = processStartInfo;
                //process.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);

                process.Start();
                process.StandardInput.WriteLine(cmdCommand);
                process.StandardInput.Flush();
                process.StandardInput.Close();
                if (!process.WaitForExit(second * 1000))
                {
                    process.Kill();

                    return "error";
                }

                return process.StandardOutput.ReadToEnd();
            }
            catch
            {
                return null;
            }
        }

        public static void openNotificationBar(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell cmd statusbar expand-notifications", deviceID);
            ExecuteCMD(cmd);
        }
        
        public static void ClearContact(string deviceID)
        {
            string clearCmd = string.Format(CONSOLE_ADB + " shell pm clear com.android.providers.contacts", deviceID);
            ExecuteCMD(clearCmd);
        }

        public static void DeleteAllFileAlarms(string deviceID)
        {
            string clearCmd = string.Format(CONSOLE_ADB + " shell rm /sdcard/Alarms/*", deviceID);
            ExecuteCMD(clearCmd);
        }

        public static void DeleteAllFilePictures(string deviceID)
        {
            string clearCmd = string.Format(CONSOLE_ADB + " shell rm /sdcard/Pictures/*", deviceID);
            ExecuteCMD(clearCmd);
        }
        public static void DeleteAllFileMusic(string deviceID)
        {
            string clearCmd = string.Format(CONSOLE_ADB + " shell rm /sdcard/Music/*", deviceID);
            ExecuteCMD(clearCmd);
        }
        public static List<String> GetDeviceLDs()
        {

            List<string> list = new List<string>();
            string input = ExecuteCMD(LD_CONSOLE + " runninglist");
            string pattern = "(?<=ldconsole  runninglist)(\\r\\n[^\\r\\n]+)+";
            MatchCollection matchCollection = Regex.Matches(input, pattern, RegexOptions.Singleline);
            if (matchCollection.Count > 0)
            {
                string value = matchCollection[0].Groups[0].Value;
                string[] array = Regex.Split(value, "\r\n");
                string[] array2 = array;
                foreach (string text in array2)
                {
                    if (!string.IsNullOrEmpty(text) && text != " ")
                    {
                        string text2 = text.Trim().Replace("device", "");
                        list.Add(text2.Trim());
                    }
                }
            }
            return list;
        }
        public static List<string> GetDevices()
        {
            List<string> list = new List<string>();
            string input = ExecuteCMD("adb devices");
            string pattern = "(?<=List of devices attached)([^\\n]*\\n+)+";
            MatchCollection matchCollection = Regex.Matches(input, pattern, RegexOptions.Singleline);
            bool flag = matchCollection.Count > 0;
            if (flag)
            {
                string value = matchCollection[0].Groups[0].Value;
                string[] array = Regex.Split(value, "\r\n");
                foreach (string text in array)
                {
                    bool flag2 = !string.IsNullOrEmpty(text) && text != " ";
                    if (flag2)
                    {
                        string text2 = text.Trim().Replace("device", "");
                        list.Add(text2.Trim());
                    }
                }
            }
            return list;
        }
        public static List<string> GetLanDevices(string range)
        {
            List<string> list = new List<string>();
            string input = ExecuteCMD("arp -a");
            string pattern = "(?<=Internet Address      Physical Address      Type)([^\\n]*\\n+)+";
            MatchCollection matchCollection = Regex.Matches(input, pattern, RegexOptions.Singleline);
            bool flag = matchCollection.Count > 0;
            if (flag)
            {
                string value = matchCollection[0].Groups[0].Value;
                string[] array = Regex.Split(value, "\r\n");
                foreach (string text in array)
                {
                    if (string.IsNullOrEmpty(text) || text.Length < 41 || !text.Contains(range))
                    {
                        continue;
                    }
                    string temp = text.Substring(0, 20).Replace(" ", "");
                    string ip = temp + ":5555";
                    string connect = AdbConnect(ip);
                    if (!string.IsNullOrEmpty(connect) && connect.ToLower().Contains("connected"))
                    {
                        list.Add(ip);
                    }

                }
            }
            
            return list;
        }
        public static Point? FindOutPointt(Bitmap mainBitmap, Bitmap subBitmap, double percent = 0.9)
        {
            if (subBitmap == null || mainBitmap == null)
            {
                return null;
            }
            if (subBitmap.Width > mainBitmap.Width || subBitmap.Height > mainBitmap.Height)
            {
                return null;
            }
            Image<Bgr, byte> val = new Image<Bgr, byte>(mainBitmap);
            Image<Bgr, byte> val2 = new Image<Bgr, byte>(subBitmap);
            Point? result = null;
            Image<Gray, float> val3 = val.MatchTemplate(val2, (TemplateMatchingType)5);
            try
            {
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                val3.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);
                if (maxValues[0] > percent)
                {
                    result = maxLocations[0];
                }
            }
            finally
            {
                ((IDisposable)val3)?.Dispose();
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            return result;
        }
        public static Point? FindOutPoint(Bitmap b, Bitmap subB, double percent = 0.9)
        {
            try
            {

                var topPoint = FindOutPointt(b, subB, percent);
                if (topPoint == null)
                {
                    return null;
                }
                return (Point)topPoint;
            }
            catch (Exception e)
            {
               
                return null;
            }

        }
        public static Bitmap ScreenShoot(string deviceId = null, bool isDeleteImageAfterCapture = true, string fileName = "screenShoot.png")
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "adb",
                Arguments = (deviceId != "" ? $"-s {deviceId} " : "") + "exec-out screencap -p",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = Process.Start(startInfo);
            Thread.Sleep(200);
            var ms = new MemoryStream();
            process.StandardOutput.BaseStream.CopyTo(ms);
            process.WaitForExit();

            ms.Position = 0;
            return new Bitmap(ms);
        }

        public static Bitmap ScreenShootOld(string deviceID = null, bool isDeleteImageAfterCapture = true, string fileName = "screenShoot.png")
        {
            Bitmap result;
            if (string.IsNullOrEmpty(deviceID))
            {
                //List<string> devices = GetDevices();
                //if (devices == null || devices.Count <= 0)
                //{
                //    return null;
                //}
                //deviceID = devices[0];
                return null;
            }
            string temp = deviceID.Replace(":", ".");
            string text = Path.GetFileNameWithoutExtension(fileName) + temp + Path.GetExtension(fileName);
            while (File.Exists(text))
            {
                try
                {
                    File.Delete(text);
                }
                catch
                {
                    continue;
                }
                break;
            }
            //string str2 = string.Format(CAPTURE_SCREEN_TO_DEVICES, deviceID, "/sdcard/" + text);
            //str2 = str2 + Environment.NewLine + string.Format(PULL_SCREEN_FROM_DEVICES, deviceID, "/sdcard/" + text);
            //str2 = str2 + Environment.NewLine + string.Format(REMOVE_SCREEN_FROM_DEVICES, deviceID, "/sdcard/" + text) + Environment.NewLine;
            //string text2 = ExecuteCMD(str2);
            string text2 = string.Format(CAPTURE_SCREEN_TO_DEVICES, deviceID, text);
            text2 = ExecuteCMD(text2);
            if ("error".Equals(text2))
            {
                return null;
            }

            using (Bitmap original = new Bitmap(text))
            {
                result = new Bitmap(original);
            }
            if (isDeleteImageAfterCapture)
            {
                try
                {
                    File.Delete(text);
                }
                catch
                {
                }
            }
            return result;
        }
        public static Bitmap ScreenShootRaw(string deviceID = null, bool isDeleteImageAfterCapture = true, string fileName = "screenShoot.png")
        {

            if (string.IsNullOrEmpty(deviceID))
            {
                List<string> devices = GetDevices();
                if (devices == null || devices.Count <= 0)
                {
                    return null;
                }
                deviceID = devices[0];
            }

            string text = Path.GetFileNameWithoutExtension(fileName) + Path.GetExtension(fileName);
            while (File.Exists(text))
            {
                try
                {
                    File.Delete(text);
                }
                catch
                {
                    continue;
                }
                break;
            }
            string str2 = string.Format(CAPTURE_SCREEN_TO_DEVICES, deviceID, "/sdcard/" + text);
            str2 = str2 + Environment.NewLine + string.Format(PULL_SCREEN_FROM_DEVICES, deviceID, "/sdcard/" + text);
            str2 = str2 + Environment.NewLine + string.Format(REMOVE_SCREEN_FROM_DEVICES, deviceID, "/sdcard/" + text) + Environment.NewLine;
            string text2 = ExecuteCMD(str2);
            if ("error".Equals(text2))
            {
                return null;
            }
            Bitmap result;
            using (Bitmap original = new Bitmap(text))
            {
                result = new Bitmap(original);
            }
            if (isDeleteImageAfterCapture)
            {
                try
                {
                    File.Delete(text);
                }
                catch
                {
                }
            }
            return result;
        }

        public static void RandomAndroidID(string deviceID)
        {
            string randomAdId = Guid.NewGuid().ToString("N").Substring(0, 16);
            SetAndroidID(deviceID, randomAdId);
            // adb shell content query --uri content://settings/secure --where "name=\'android_id\'"
        }
        public static void SetAndroidID(string deviceID, string androidId)
        {
            
            string cmd = string.Format(CONSOLE_ADB + " shell su -c \"settings put secure android_id {1}\"", deviceID, androidId);
            ExecuteCMD(cmd);
            // adb shell content query --uri content://settings/secure --where "name=\'android_id\'"
        }

        public static string GetAndroidID(string deviceID)
        {
            string androidId = "";
            string randomAdId = Utility.RandomAndroidID();
            string cmd = string.Format(CONSOLE_ADB + " shell content query --uri content://settings/secure --where \"name =\'android_id\'\"", deviceID, randomAdId);
            androidId =  ExecuteCMD(cmd);
            try
            {
                androidId =  Regex.Match(androidId, "value=(.*?)\r\n").Groups[1].ToString();
            }
            catch
            {

            }
            
            return androidId;
        }
        public static bool CheckAppInstall(string deviceID, string package)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell pm list packages {1}", deviceID, package);
            string result =  ExecuteCMD(cmd);
            if (!string.IsNullOrEmpty(result) && result.Contains("package:" + package))
            {
                return true;
            }
            return false;
        }

        public static void RebootRecovery(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " reboot recovery", deviceID);
            ExecuteCMD(cmd);
        }
        public static void WaitForRecovery(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " wait-for-recovery", deviceID);
            ExecuteCMD(cmd);
        }

        public static void HienInfo(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " hien infoce" + deviceID, deviceID);
            ExecuteCMD(cmd);
        }
        public static void TwrpWipeData(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell twrp wipe data" , deviceID);
            ExecuteCMD(cmd);
            cmd = string.Format(CONSOLE_ADB + " shell twrp wipe cache" , deviceID);
            ExecuteCMD(cmd);
            cmd = string.Format(CONSOLE_ADB + " shell twrp wipe dalvik" , deviceID);
            ExecuteCMD(cmd);
            cmd = string.Format(CONSOLE_ADB + " shell twrp wipe system" , deviceID);
            ExecuteCMD(cmd);
        }
        public static void PushRom10(string deviceID)
        {
            string cmd = string.Format(CONSOLE_ADB + " push Rom10 /sdcard/", deviceID);
            ExecuteCMD(cmd);
        }

        public static void InstallRom(string deviceID, string file)
        {
            string cmd = string.Format(CONSOLE_ADB + " shell twrp install /sdcard/Rom10/" + file, deviceID);
            ExecuteCMD(cmd);
        }

        public static void ExecuteFileBat(string batFile)
        {
            Process.Start(batFile).WaitForExit();
        }

        public static void ForgetAllNetworks(string deviceId)
        {
            var output = RunAdb(deviceId, "shell su -c \"cmd wifi list-networks\"");
            var lines = output.Split('\n');

            foreach (var line in lines)
            {
                if (!line.Contains("Network"))
                {
                    var parts = line.Split(' ');
                    string id = parts[0]; // networkId số sau dấu cách
                    RunAdb(deviceId, $"shell su -c \"cmd wifi forget-network {id}\"");
                }
            }
        }
    }
}
