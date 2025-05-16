using fb_reg.RequestApi;
using fb_reg.Utilities.FetchMail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace fb_reg.Utilities
{
    public static class FetchAllMail
    {
        public static void FetchGmail(DeviceObject device)
        {
            try
            {
                if (!FetchController.IsFetchAllowed())
                {

                    Thread.Sleep(500);
                    return;
                }
                FetchController.SetState(FetchState.Fetching);
                MailObject mail = Mail.GetTempmail(true, "", Constant.GMAIL_SUPERTEAM, "", false);

                if (mail != null && !string.IsNullOrEmpty(mail.email))
                {
                    try
                    {
                        Utility.LogStatus(device, mail.source + "-" + mail.email + "b:" + mail.balanceAfter);
                        //maxMaillabel.Text = mail.balanceAfter + "";
                        FetchController.SetState(FetchState.WaitingServer);
                        MailObject resp = CacheServer.AddMailServerCache(mail, PublicData.ServerCacheMail);
                        if (resp != null)
                        {
                            int cacheMail = resp.mailCount;
                            if (cacheMail > PublicData.maxMail)
                            {
                                FetchController.Pause();
                            }
                            else
                            {
                                FetchController.SetState(FetchState.Fetching);
                            }
                        }
                        else
                        {
                            FetchController.SetState(FetchState.ServerError);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("❌ Server ERROR: " + ex.Message);
                        //File.AppendAllText("status.log", $"[ERROR] {DateTime.Now}: {ex.Message}\n");
                        FetchController.SetState(FetchState.ServerError);
                    }

                    Thread.Sleep(200); // giả lập delay API mail
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
