using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace fb_reg
{
    public static class GboardTyper
    {
        static readonly Dictionary<char, string> KeyMap = new Dictionary<char, string>
        {
            ['a'] = "KEYCODE_A",
            ['b'] = "KEYCODE_B",
            ['c'] = "KEYCODE_C",
            ['d'] = "KEYCODE_D",
            ['e'] = "KEYCODE_E",
            ['f'] = "KEYCODE_F",
            ['g'] = "KEYCODE_G",
            ['h'] = "KEYCODE_H",
            ['i'] = "KEYCODE_I",
            ['j'] = "KEYCODE_J",
            ['k'] = "KEYCODE_K",
            ['l'] = "KEYCODE_L",
            ['m'] = "KEYCODE_M",
            ['n'] = "KEYCODE_N",
            ['o'] = "KEYCODE_O",
            ['p'] = "KEYCODE_P",
            ['q'] = "KEYCODE_Q",
            ['r'] = "KEYCODE_R",
            ['s'] = "KEYCODE_S",
            ['t'] = "KEYCODE_T",
            ['u'] = "KEYCODE_U",
            ['v'] = "KEYCODE_V",
            ['w'] = "KEYCODE_W",
            ['x'] = "KEYCODE_X",
            ['y'] = "KEYCODE_Y",
            ['z'] = "KEYCODE_Z",
            [' '] = "KEYCODE_SPACE",
            ['.'] = "KEYCODE_PERIOD",
            ['@'] = "KEYCODE_AT",
            ['0'] = "KEYCODE_0",
            ['1'] = "KEYCODE_1",
            ['2'] = "KEYCODE_2",
            ['3'] = "KEYCODE_3",
            ['4'] = "KEYCODE_4",
            ['5'] = "KEYCODE_5",
            ['6'] = "KEYCODE_6",
            ['7'] = "KEYCODE_7",
            ['8'] = "KEYCODE_8",
            ['9'] = "KEYCODE_9"
        };
        public static void TypeText(string deviceID, string text, int delayMs = 100)
        {
            Random ran = new Random();
            foreach (char c in text.ToLower())
            {
                if (KeyMap.TryGetValue(c, out string key))
                {
                    ///RunAdbCommand($"shell input keyevent {key}");

                    string cmd = string.Format(Device.CONSOLE_ADB + "shell input keyevent {1}", deviceID, key);

                    Device.ExecuteCMD(cmd);
                    
                    Thread.Sleep(ran.Next(100, 200)); // delay mô phỏng người gõ
                }
            }
        }

        private static void RunAdbCommand(string args)
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "adb",
                    Arguments = args,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            proc.Start();
            proc.WaitForExit();
        }
    }
}
