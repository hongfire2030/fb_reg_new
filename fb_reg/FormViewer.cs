using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fb_reg.Viewer
{
    public partial class FormViewer : Form
    {
        private string deviceId;
        private Process scrcpyProcess;
        private bool isClosing = false;
        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        public FormViewer(string deviceId)
        {
            InitializeComponent();
            this.deviceId = deviceId;
        }
        private void FormViewer_Load(object sender, EventArgs e)
        {
            int maxSize = Math.Max(panelPhone.Width, panelPhone.Height);
            var psi = new ProcessStartInfo
            {
                FileName = @"Tools\scrcpy\scrcpy.exe",
                Arguments = $"-s {deviceId} --window-borderless --max-size={maxSize} --window-x=0 --window-y=0",
                UseShellExecute = false,
                CreateNoWindow = true
            };
            _ = WatchDeviceAndReconnect(deviceId); // chạy ngầm, không chặn UI
            scrcpyProcess = Process.Start(psi);

            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        if (scrcpyProcess == null || scrcpyProcess.HasExited) break;

                        if (scrcpyProcess.MainWindowHandle != IntPtr.Zero)
                        {
                            this.Invoke((MethodInvoker)(() =>
                            {
                                SetParent(scrcpyProcess.MainWindowHandle, panelPhone.Handle);
                                MoveWindow(scrcpyProcess.MainWindowHandle, 0, 0, panelPhone.Width, panelPhone.Height, true);
                            }));
                            break;
                        }

                        Thread.Sleep(100);
                        scrcpyProcess.Refresh(); // an toàn vì đã kiểm tra HasExited
                    }
                    catch
                    {
                        break; // Tránh crash nếu lỗi
                    }
                }
            });
        }
        private void RestartScrcpy(string deviceId)
        {
            if (isClosing) return;
            if (scrcpyProcess != null && !scrcpyProcess.HasExited)
            {
                scrcpyProcess.Kill();
            }

            string scrcpyPath = @"Tools\scrcpy\scrcpy.exe";
            int maxSize = Math.Max(panelPhone.Width, panelPhone.Height);

            var psi = new ProcessStartInfo
            {
                FileName = scrcpyPath,
                Arguments = $"-s {deviceId} --window-borderless --max-size={maxSize}",
                UseShellExecute = false,
                CreateNoWindow = true
            };

            scrcpyProcess = Process.Start(psi);

            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        if (scrcpyProcess == null || scrcpyProcess.HasExited) break;

                        if (scrcpyProcess.MainWindowHandle != IntPtr.Zero)
                        {
                            this.Invoke((MethodInvoker)(() =>
                            {
                                SetParent(scrcpyProcess.MainWindowHandle, panelPhone.Handle);
                                MoveWindow(scrcpyProcess.MainWindowHandle, 0, 0, panelPhone.Width, panelPhone.Height, true);
                            }));
                            break;
                        }

                        Thread.Sleep(100);
                        scrcpyProcess.Refresh(); // an toàn vì đã kiểm tra HasExited
                    }
                    catch
                    {
                        break; // Tránh crash nếu lỗi
                    }
                }
            });
        }

        private void panelPhone_Resize(object sender, EventArgs e)
        {
            if (scrcpyProcess != null && scrcpyProcess.MainWindowHandle != IntPtr.Zero)
            {
                MoveWindow(scrcpyProcess.MainWindowHandle, 0, 0, panelPhone.Width, panelPhone.Height, true);
            }
        }
        private async Task WatchDeviceAndReconnect(string deviceId)
        {
            while (true)
            {
                bool isOnline = CheckDeviceOnline(deviceId);

                if (isOnline && (scrcpyProcess == null || scrcpyProcess.HasExited))
                {
                    RestartScrcpy(deviceId);
                }

                await Task.Delay(3000);
            }
        }

        private bool CheckDeviceOnline(string deviceId)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "adb",
                Arguments = "devices",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var proc = Process.Start(psi))
            {
                string output = proc.StandardOutput.ReadToEnd();
                return output.Contains($"{deviceId}\tdevice");
            }
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            Device.RunAdb(deviceId, "shell input keyevent 3");
        }

        private void Backbutton_Click(object sender, EventArgs e)
        {
            Device.RunAdb(deviceId, "shell input keyevent 4");
        }

        private void powerbutton_Click(object sender, EventArgs e)
        {
            Device.RunAdb(deviceId, "shell input keyevent 26");
        }

        private void recentbutton_Click(object sender, EventArgs e)
        {
            Device.RunAdb(deviceId, "shell input keyevent 187");
        }

        private void FormViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            isClosing = true;

            try
            {
                if (scrcpyProcess != null && !scrcpyProcess.HasExited)
                {
                    scrcpyProcess.Kill();
                    scrcpyProcess.Dispose();
                }
            }
            catch { }
        }
    }
}
