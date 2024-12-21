using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
namespace fb_reg
{
    class LDDevice
    {
        public static string LD_CONSOLE = "D:\\LDPlayer\\LDPlayer4.0\\ldconsole ";
        public static string LD_CONSOLE_ADB = LD_CONSOLE + " adb --name {0} --command ";
        private static string CAPTURE_SCREEN_TO_DEVICES = LD_CONSOLE_ADB + " \" shell screencap -p \"{1}\" \"";
        private static string PULL_SCREEN_FROM_DEVICES = LD_CONSOLE_ADB + " \" pull \"{1}\" \"";
        private static string REMOVE_SCREEN_FROM_DEVICES = LD_CONSOLE_ADB + " \" shell rm -f \"{1}\" \"";
        private static string ADB_FOLDER_PATH = "";
        private static string TAP_DEVICES = LD_CONSOLE_ADB + " \"shell input tap {1} {2} \"";
        private static string KEY_DEVICES = LD_CONSOLE_ADB + " \" shell input keyevent {1} \"";
        private static string INPUT_TEXT_DEVICES = LD_CONSOLE_ADB + " \" shell input text \"{1}\" \"";
        private static string GET_SCREEN_RESOLUTION = LD_CONSOLE_ADB + " \" shell dumpsys display | Find \"mCurrentDisplayRect\" \"";

        private static string KILL_APP = LD_CONSOLE_ADB + " \" shell pm clear com.garena.game.fo4mvn \"";

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static Bitmap ScreenShot(string deviceId)
        {
            try
            {
                var screen = ScreenShoot(deviceId);
                int retry = 5;
                while (screen == null)
                {
                    screen = ScreenShoot(deviceId);
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    retry--;
                    if (retry == 0)
                    {
                        return null;
                    }
                }
                return screen;
            }
            catch (Exception e)
            {
                log.Error("ScreenShot :" + e.ToString());
                return null;
            }
        }

        public static void ClearApp(string deviceId)
        {
            //String cmd = "adb -s " + deviceId + " shell am force-stop com.garena.game.fo4mvn";
            //String cmd = "adb -s " + deviceId + " shell pm clear com.garena.game.fo4mvn";
            string cmd = string.Format(KILL_APP, deviceId);
            ExecuteCMD(cmd);
            Key(deviceId, ADBKeyEvent.KEYCODE_HOME);
        }
        public static void Tap(string deviceId, int x, int y, int time = 1)
        {
            string text = string.Format(TAP_DEVICES, deviceId, x, y);
            for (int i = 1; i < time; i++)
            {
                text = text + " && " + string.Format(TAP_DEVICES, deviceId, x, y);
            }
            string text2 = ExecuteCMD(text);
        }

        
        public static Bitmap ScreenShoot(string deviceID = null, bool isDeleteImageAfterCapture = true, string fileName = "screenShoot.png")
        {
            if (string.IsNullOrEmpty(deviceID))
            {
                List<string> devices = GetDevices();
                if (devices == null || devices.Count <= 0)
                {
                    return null;
                }
                deviceID = devices.First();
            }

            string text = Path.GetFileNameWithoutExtension(fileName) + deviceID + Path.GetExtension(fileName);
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


        public static string ExecuteCMD(string cmdCommand)
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
                process.Start();
                process.StandardInput.WriteLine(cmdCommand);
                process.StandardInput.Flush();
                process.StandardInput.Close();
                if (!process.WaitForExit(5000))
                {
                    process.Kill();
                    log.Info(cmdCommand + " : error");

                    return "error";
                }
                return process.StandardOutput.ReadToEnd();
            }
            catch
            {
                return null;
            }
        }

        public static void Key(string deviceId, ADBKeyEvent keyCode)
        {
            string cmdCommand = string.Format(KEY_DEVICES, deviceId, keyCode);
            string text = ExecuteCMD(cmdCommand);
        }
        public static void InputText(string deviceId, string text)
        {
            string cmdCommand = string.Format(INPUT_TEXT_DEVICES, deviceId, text.Replace(" ", "%s").Replace("&", "\\&").Replace("<", "\\<")
                .Replace(">", "\\>")
                .Replace("?", "\\?")
                .Replace(":", "\\:")
                .Replace("{", "\\{")
                .Replace("}", "\\}")
                .Replace("[", "\\[")
                .Replace("]", "\\]")
                .Replace("|", "\\|"));
            string text2 = ExecuteCMD(cmdCommand);
        }
        public static void TapByPercent(string deviceId, double x, double y, int time = 1)
        {
            //Point screenResolution = GetScreenResolution(deviceId);
            int num = (int)(x * 360 / 100.0);
            int num2 = (int)(y * 640 * 1.0 / 100.0);
            string text = string.Format(TAP_DEVICES, deviceId, num, num2);
            for (int i = 1; i < time; i++)
            {
                text = text + " && " + string.Format(TAP_DEVICES, deviceId, x, y);
            }
            string text2 = ExecuteCMD(text);
        }
        public static Point GetScreenResolution(string deviceID)
        {
            string cmdCommand = string.Format(GET_SCREEN_RESOLUTION, deviceID);
            string text = ExecuteCMD(cmdCommand);
            text = text.Substring(text.IndexOf("- "));
            text = text.Substring(text.IndexOf(' '), text.IndexOf(')') - text.IndexOf(' '));
            string[] array = text.Split(',');
            int x = Convert.ToInt32(array[0].Trim());
            int y = Convert.ToInt32(array[1].Trim());
            return new Point(x, y);
        }

        //public static void ExecuteCMD(string cmd)
        //{
        //    KAutoHelper.ADBHelper.ExecuteCMD(cmd);
        //}

        public static List<String> GetDevices()
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
    }
}
