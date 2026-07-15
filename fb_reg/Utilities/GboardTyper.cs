using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace fb_reg
{
    public static class GboardTyper
    {
        public class KeyMapItem
        {
            public string KeyCode { get; set; }
            public bool Shift { get; set; }

            public KeyMapItem(string keyCode, bool shift = false)
            {
                KeyCode = keyCode;
                Shift = shift;
            }
        }
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
            ['9'] = "KEYCODE_9",
            ['-'] = "KEYCODE_MINUS"
        };
        public static readonly Dictionary<char, KeyMapItem> AndroidKeyMap =
    new Dictionary<char, KeyMapItem>
    {
        // lowercase
        ['a'] = new KeyMapItem("KEYCODE_A"),
        ['b'] = new KeyMapItem("KEYCODE_B"),
        ['c'] = new KeyMapItem("KEYCODE_C"),
        ['d'] = new KeyMapItem("KEYCODE_D"),
        ['e'] = new KeyMapItem("KEYCODE_E"),
        ['f'] = new KeyMapItem("KEYCODE_F"),
        ['g'] = new KeyMapItem("KEYCODE_G"),
        ['h'] = new KeyMapItem("KEYCODE_H"),
        ['i'] = new KeyMapItem("KEYCODE_I"),
        ['j'] = new KeyMapItem("KEYCODE_J"),
        ['k'] = new KeyMapItem("KEYCODE_K"),
        ['l'] = new KeyMapItem("KEYCODE_L"),
        ['m'] = new KeyMapItem("KEYCODE_M"),
        ['n'] = new KeyMapItem("KEYCODE_N"),
        ['o'] = new KeyMapItem("KEYCODE_O"),
        ['p'] = new KeyMapItem("KEYCODE_P"),
        ['q'] = new KeyMapItem("KEYCODE_Q"),
        ['r'] = new KeyMapItem("KEYCODE_R"),
        ['s'] = new KeyMapItem("KEYCODE_S"),
        ['t'] = new KeyMapItem("KEYCODE_T"),
        ['u'] = new KeyMapItem("KEYCODE_U"),
        ['v'] = new KeyMapItem("KEYCODE_V"),
        ['w'] = new KeyMapItem("KEYCODE_W"),
        ['x'] = new KeyMapItem("KEYCODE_X"),
        ['y'] = new KeyMapItem("KEYCODE_Y"),
        ['z'] = new KeyMapItem("KEYCODE_Z"),

        // uppercase
        ['A'] = new KeyMapItem("KEYCODE_A", true),
        ['B'] = new KeyMapItem("KEYCODE_B", true),
        ['C'] = new KeyMapItem("KEYCODE_C", true),
        ['D'] = new KeyMapItem("KEYCODE_D", true),
        ['E'] = new KeyMapItem("KEYCODE_E", true),
        ['F'] = new KeyMapItem("KEYCODE_F", true),
        ['G'] = new KeyMapItem("KEYCODE_G", true),
        ['H'] = new KeyMapItem("KEYCODE_H", true),
        ['I'] = new KeyMapItem("KEYCODE_I", true),
        ['J'] = new KeyMapItem("KEYCODE_J", true),
        ['K'] = new KeyMapItem("KEYCODE_K", true),
        ['L'] = new KeyMapItem("KEYCODE_L", true),
        ['M'] = new KeyMapItem("KEYCODE_M", true),
        ['N'] = new KeyMapItem("KEYCODE_N", true),
        ['O'] = new KeyMapItem("KEYCODE_O", true),
        ['P'] = new KeyMapItem("KEYCODE_P", true),
        ['Q'] = new KeyMapItem("KEYCODE_Q", true),
        ['R'] = new KeyMapItem("KEYCODE_R", true),
        ['S'] = new KeyMapItem("KEYCODE_S", true),
        ['T'] = new KeyMapItem("KEYCODE_T", true),
        ['U'] = new KeyMapItem("KEYCODE_U", true),
        ['V'] = new KeyMapItem("KEYCODE_V", true),
        ['W'] = new KeyMapItem("KEYCODE_W", true),
        ['X'] = new KeyMapItem("KEYCODE_X", true),
        ['Y'] = new KeyMapItem("KEYCODE_Y", true),
        ['Z'] = new KeyMapItem("KEYCODE_Z", true),

        // numbers
        ['0'] = new KeyMapItem("KEYCODE_0"),
        ['1'] = new KeyMapItem("KEYCODE_1"),
        ['2'] = new KeyMapItem("KEYCODE_2"),
        ['3'] = new KeyMapItem("KEYCODE_3"),
        ['4'] = new KeyMapItem("KEYCODE_4"),
        ['5'] = new KeyMapItem("KEYCODE_5"),
        ['6'] = new KeyMapItem("KEYCODE_6"),
        ['7'] = new KeyMapItem("KEYCODE_7"),
        ['8'] = new KeyMapItem("KEYCODE_8"),
        ['9'] = new KeyMapItem("KEYCODE_9"),

        // symbols without shift
        [' '] = new KeyMapItem("KEYCODE_SPACE"),
        ['-'] = new KeyMapItem("KEYCODE_MINUS"),
        ['='] = new KeyMapItem("KEYCODE_EQUALS"),
        ['['] = new KeyMapItem("KEYCODE_LEFT_BRACKET"),
        [']'] = new KeyMapItem("KEYCODE_RIGHT_BRACKET"),
        ['\\'] = new KeyMapItem("KEYCODE_BACKSLASH"),
        [';'] = new KeyMapItem("KEYCODE_SEMICOLON"),
        ['\''] = new KeyMapItem("KEYCODE_APOSTROPHE"),
        [','] = new KeyMapItem("KEYCODE_COMMA"),
        ['.'] = new KeyMapItem("KEYCODE_PERIOD"),
        ['/'] = new KeyMapItem("KEYCODE_SLASH"),
        ['`'] = new KeyMapItem("KEYCODE_GRAVE"),

        // symbols with shift
        ['!'] = new KeyMapItem("KEYCODE_1", true),
        ['@'] = new KeyMapItem("KEYCODE_2", true),
        ['#'] = new KeyMapItem("KEYCODE_3", true),
        ['$'] = new KeyMapItem("KEYCODE_4", true),
        ['%'] = new KeyMapItem("KEYCODE_5", true),
        ['^'] = new KeyMapItem("KEYCODE_6", true),
        ['&'] = new KeyMapItem("KEYCODE_7", true),
        ['*'] = new KeyMapItem("KEYCODE_8", true),
        ['('] = new KeyMapItem("KEYCODE_9", true),
        [')'] = new KeyMapItem("KEYCODE_0", true),

        ['_'] = new KeyMapItem("KEYCODE_MINUS", true),
        ['+'] = new KeyMapItem("KEYCODE_EQUALS", true),
        ['{'] = new KeyMapItem("KEYCODE_LEFT_BRACKET", true),
        ['}'] = new KeyMapItem("KEYCODE_RIGHT_BRACKET", true),
        ['|'] = new KeyMapItem("KEYCODE_BACKSLASH", true),
        [':'] = new KeyMapItem("KEYCODE_SEMICOLON", true),
        ['"'] = new KeyMapItem("KEYCODE_APOSTROPHE", true),
        ['<'] = new KeyMapItem("KEYCODE_COMMA", true),
        ['>'] = new KeyMapItem("KEYCODE_PERIOD", true),
        ['?'] = new KeyMapItem("KEYCODE_SLASH", true),
        ['~'] = new KeyMapItem("KEYCODE_GRAVE", true),
    };
        public static void TypeText(string deviceID, string text, int delayMs = 200)
        {
            Random ran = new Random();
            foreach (char c in text)
            {
                bool isUpper = char.IsUpper(c);
                char lower = char.ToLower(c);


                if (c == '_' )
                {
                    Device.InputText(deviceID, c + "");
                    Thread.Sleep(delayMs / 2);

                } else
                {
                    if (KeyMap.TryGetValue(lower, out string key))
                    {
                        ///RunAdbCommand($"shell input keyevent {key}");
                        if (isUpper)
                        {
                            // Gửi SHIFT trước rồi gửi ký tự
                            //Device.ExecuteCMD(string.Format(Device.CONSOLE_ADB + " shell input keyevent 59", deviceID)); // SHIFT_LEFT
                            Device.InputText(deviceID, c + "");
                            Thread.Sleep(delayMs / 2);
                        }
                        else
                        {
                            string cmd = string.Format(Device.CONSOLE_ADB + "shell input keyevent {1}", deviceID, key);

                            Device.ExecuteCMD(cmd);
                        }


                        
                    }
                }
                Thread.Sleep(ran.Next(200, 400)); // delay mô phỏng người gõ
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
