using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static fb_reg.Utility;
namespace fb_reg.Utilities
{
    public static class MailUtility
    {
        public static string getOtpEdu(MailObject mail, string deviceID, bool docmailEdu)
        {
            if (docmailEdu)// docMailEducheckBox.Checked)
            {
                if (mail == null)
                {
                    return "";
                }

                Device.OpenApp(deviceID, Constant.BROWSER_PACKAGE);

                Thread.Sleep(4000);
                Utility.WaitAndTapXML(deviceID, 2, "đăng nhập");
                Thread.Sleep(2000);
                if (CheckTextExist(deviceID, "sử dụng tài khoản google", 2))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 44.0, 45.1);
                    InputMail(deviceID, mail.email, true);
                    Thread.Sleep(1000);
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 91.1, 95.6); // OK hạ bàn phím


                    if (CheckTextExist(deviceID, "chào mừng", 5))
                    {
                        Thread.Sleep(1000);
                        InputMail(deviceID, mail.password, true);
                        Thread.Sleep(1000);
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 91.1, 95.6); // OK hạ bàn phím

                        Thread.Sleep(2000);
                        CheckTextExist(deviceID, "chào mừng", 5);
                        Device.Swipe(deviceID, 100, 1500, 200, 200);
                        Device.Swipe(deviceID, 100, 1500, 200, 200);
                        Device.Swipe(deviceID, 100, 1500, 200, 200);


                        if (!WaitAndTapXML(deviceID, 2, "tôi hiểu"))
                        {
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.7, 76.6);
                        }
                        Thread.Sleep(2000);
                        //KAutoHelper.ADBHelper.TapByPercent(deviceID, 81.7, 14.5);
                        //Thread.Sleep(4000);
                        //KAutoHelper.ADBHelper.TapByPercent(deviceID, 56.4, 28.6);
                        //Thread.Sleep(5000);
                        Device.OpenWeb(deviceID, "https://mail.google.com/mail/mu/mp/900/");
                        Thread.Sleep(15000);
                        for (int i = 0; i < 5; i++)
                        {
                            //KAutoHelper.ADBHelper.TapByPercent(deviceID, 83.3, 7.1);
                            //Thread.Sleep(2000);
                            //KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.5, 95.4);
                            //Thread.Sleep(10000);
                            string xxxmm = GetUIXml(deviceID);

                            if (CheckTextExist(deviceID, "facebook"))
                            {
                                string otp = Regex.Match(xxxmm, "facebook(.*?)l").Groups[1].ToString();
                                if (!string.IsNullOrEmpty(otp))
                                {
                                    return otp;
                                }

                            }
                            Device.OpenWeb(deviceID, "mail.google.com/mail/mu/mp/900/");
                            Thread.Sleep(10000);
                        }
                    }


                    return "";
                }
            }
            return "";
        }
    }
}
