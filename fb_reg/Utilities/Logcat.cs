using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fb_reg
{
    
    class Logcat
    {
        private static Process listeningProc;
        public static void listenLogcat()
        {
            listeningProc = new Process();
            listeningProc.StartInfo.FileName = "cmd.exe";
            listeningProc.StartInfo.UseShellExecute = false;
            listeningProc.StartInfo.RedirectStandardOutput = true;
            listeningProc.StartInfo.RedirectStandardInput = true;
            listeningProc.Start();

            listeningProc.OutputDataReceived += new DataReceivedEventHandler(
                (s, e) => {
                    Console.WriteLine("data logcat: " + e.Data);
                    if (e.Data == "SPECIFIC_COMMAND")
                    {
                        Console.WriteLine("SPECIFIC_COMMAND: " + e.Data);
                        // do something
                    }
                }
            );
            listeningProc.BeginOutputReadLine();
        }
        public static void ListenToCommands(string deviceID)
        {
            listeningProc.StandardInput.WriteLine(string.Format("adb -s {0} shell logcat", deviceID)); //Listening for Info Logs titled "Command"

            while (true) { }
        }

    }

    

}
