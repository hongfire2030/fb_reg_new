using HttpRequest;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static fb_reg.Utility;

namespace fb_reg
{
    class Form1Util
    {
        public static bool LoginFbLite(string deviceID, Account acc)
        {
            //Thread.Sleep(1000);
            Device.TapByPercent(deviceID, 46.1, 17.0,  500); // uid
            Thread.Sleep(1000);
            //if (!WaitAndTapXML(deviceID, 1, "nodenaftrueindex4textresourceid"))
            //{
                Device.TapByPercent(deviceID, 56.1, 17.0, 500); // uid
            //}
            
            Thread.Sleep(1000);
            InputText(deviceID, acc.uid, true);
            Thread.Sleep(500);
            Device.TapByPercent(deviceID, 76.1, 17.0, 500); // uid
            Thread.Sleep(1500);
            //if (!WaitAndTapXML(deviceID, 1, "nodenaftrueindex6textresourcei"))
            //{
                Device.TapByPercent(deviceID, 49.1, 25.6); // pass
            //}
           
            Thread.Sleep(1000);
            InputText(deviceID, acc.pass, true);
            Thread.Sleep(1500);
            //if (!WaitAndTapXML(deviceID, 1, "nodenaftrueindex7textresourceid"))
            //{
                Device.TapByPercent(deviceID, 50.9, 32.2);
            //}
            
            // tap dang nhap
            //Thread.Sleep(6000);
            //WaitAndTapXML(deviceID, 2, Language.AllowAll());
            //if (FindImage(deviceID, FBLITE_2FA, 10))
            //{
            //    Device.TapByPercent(deviceID, 49.5, 54.8); // ok
            //                                                              // Get code
            //    //RequestHTTP httpRequest = new RequestHTTP();
            //    //string faCode = httpRequest.Request("GET", "http://2fa.live/tok/" + acc.qrCode, null).ToString();
            //    //string token = Regex.Match(faCode, ":\"(.*?)\"}$").Groups[1].ToString();

            //    var base32Bytes = Base32Encoding.ToBytes(acc.qrCode);

            //    var otp = new Totp(base32Bytes);
            //    string token = otp.ComputeTotp();

            //    //Thread.Sleep(2000);
            //    Device.TapByPercent(deviceID, 46.1, 17.0); // uid
            //    InputText(deviceID, acc.uid, false);
            //    Thread.Sleep(1500);
            //    Device.TapByPercent(deviceID, 49.1, 25.6,  500); // pass
            //    Device.DeleteChars(deviceID, 13);
            //    InputText(deviceID, token, false);
            //    Thread.Sleep(500);
            //    Device.TapByPercent(deviceID, 50.9, 32.2); // tap dang nhap
            //    Thread.Sleep(7000);
            //    Device.TapByPercent(deviceID, 77.5, 90.1);
            //}
            
            for (int i = 0; i < 10; i ++)
            {
                if (!CheckTextExist(deviceID, acc.uid, 1))
                {
                    break;
                }
            }

            if (CheckTextExist(deviceID, acc.uid, 2))
            {
                // Login fail - 
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.2, 55.7); // ok
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 74.6, 54.8);
                return false;
            }
            return true;
        }

        public static void LogRegStatus(DataGridView dataGridView, DeviceObject device, string status)
        {
            device.regStatus = device.regStatus + "-" + status;
            dataGridView.Rows[device.index].Cells[13].Value = device.regStatus;
        }

        

    }
}
