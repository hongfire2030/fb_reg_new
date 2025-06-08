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
                Utility.LogStatus(device, "Status before :" + FetchController.GetState());
                if (!FetchController.IsFetchAllowed())
                {
                    return;
                }
                FetchController.SetState(FetchState.Fetching);
                MailObject mail = Mail.GetTempmail(true, "", Constant.GMAIL_SUPERTEAM, "", false);

                if (mail != null && !string.IsNullOrEmpty(mail.email))
                {
                    try
                    {
                        PublicData.dataGridView.Rows[device.index].Cells[13].Value = mail.source + "-" + mail.email + "-b:" + mail.balanceAfter;
                        FetchController.SetState(FetchState.WaitingServer);
                        MailObject resp = CacheServer.AddMailServerCache(mail, PublicData.ServerCacheMail);
                        PublicData.PublicmaxMaillabel.Text = mail.email + "-" + mail.source + "-" + mail.balanceAfter + "-" + PublicData.FetchMailLog;
                        if (resp != null)
                        {
                            int cacheMail = resp.mailCount;
                            if (cacheMail > PublicData.maxMail)
                            {
                                FetchController.Pause();
                                PublicData.MaxThreadGetMail = 1;
                                PublicData.PublicmaxThreadMailTextbox.Text = "1";
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
                        Utility.LogStatus(device, "exception status :" + FetchController.GetState() + " er:" + ex.Message);
                    }
                    //Utility.LogStatus(device, "Status affter :" + FetchController.GetState());
                    Thread.Sleep(2000); // giả lập delay API mail
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
