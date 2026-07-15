using EAGetMail;
using fb_reg.Model;
using fb_reg.RequestApi;
using fb_reg.Utilities.FetchMail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static fb_reg.CacheServer;

namespace fb_reg.Utilities
{
    public static class FetchAllMail
    {
        public static int CheckHasMail(bool tempmail)
        {
            //MailObject mail = new MailObject();
            //if (!tempmail)
            //{
            //    mail.isHotmail = true;
            //}
            return CacheServer.GetMailCacheCount(!tempmail);
            
            //if (resp != null)
            //{
            //    return resp.mailCount;
                
            //}
            return -1;
        }
        public static void FetchGmail(DeviceObject device, bool tempmail, bool trustMail)
        {
            try
            {
                if (PublicData.stopAll)
                {
                    return;
                }   
                Utility.LogStatus(device, "Status before :" + FetchController.GetState());
                if (!FetchController.IsFetchAllowed())
                {
                    return;
                }
                FetchController.SetState(FetchState.Fetching);
                List<MailObject> listmail = new List<MailObject>();
                if (!tempmail)
                {
                    listmail = Mail.GetHotmailTool(trustMail);
                } else
                {
                    MailObject mailGmail = Mail.GetTempmail("", true, "", Constant.GMAIL_SUPERTEAM, "", false);
                    if (mailGmail != null && !string.IsNullOrEmpty(mailGmail.email))
                    {
                        listmail = new List<MailObject>() { mailGmail };
                    }
                }

                if (listmail != null && listmail.Count > 0 )
                {
                    try
                    {
                        PublicData.dataGridView.Rows[device.index].Cells[13].Value = listmail[0].source + "-" + listmail[0].email + "-b:" + listmail[0].balanceAfter + "-" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        FetchController.SetState(FetchState.WaitingServer);
                        MailObject resp = new MailObject();
                        for (int k = 0; k < listmail.Count; k++) {
                            
                            resp = CacheServer.ForceAddMailServerCache(listmail[k], device);
                            Utility.LogStatus(device, "Fetch from API: " + listmail[k].toString());
                            Thread.Sleep(500);
                        }
                        
                        PublicData.PublicmaxMaillabel.Text = listmail.Count + " -" +  listmail[0].email + "-" + listmail[0].source + "-" + listmail[0].balanceAfter + "-" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-" + PublicData.FetchMailLog ;
                        
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
                            Thread.Sleep(1000);
                            Setting rateT = SettingLogServer(-1, -1, -1, -1, -1, -1, -1, -1, -1);
                            if (rateT == null)
                            {
                                FetchController.SetState(FetchState.ServerError);
                            }
                        }
                        Utility.WriteFileLog(listmail[0].toString(), "fetchmail.log");
                    }
                    catch (Exception ex)
                    {
                        FetchController.SetState(FetchState.ServerError);
                        Utility.LogStatus(device, "FetchMail exception :" + FetchController.GetState() + " er:" + ex.Message);
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
