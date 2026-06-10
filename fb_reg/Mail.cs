using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

using MailKit.Net.Imap;
using MailKit;
using MailKit.Search;
using System.Net;
using ActiveUp.Net.Mail;
using static fb_reg.ServerApi;
using static fb_reg.OutsideServer;
using EAGetMail;
using System.Windows.Forms;
using fb_reg.RequestApi;
using System.Data;
using fb_reg.Utilities;
using static fb_reg.CacheServer;
using System.Drawing;
using Chilkat;
using Microsoft.Graph.AuditLogs;
using Microsoft.Graph.Groups.Item.Team.Schedule.TimeCards.Item.EndBreak;
using Google.Apis.Util;

namespace fb_reg
{
  
    public class MailRepository
    {
        private readonly string mailServer, login, password;
        private readonly int port;
        private readonly bool ssl;

        
        public MailRepository(string mailServer, int port, bool ssl, string login, string password)
        {
            this.mailServer = mailServer;
            this.port = port;
            this.ssl = ssl;
            this.login = login;
            this.password = password;
        }


        
        public List<string> GetAllMailSubjects()
        {
            var messages = new List<string>();

            using (var client = new ImapClient())
            {
                client.CheckCertificateRevocation = false;
                client.Connect(mailServer, port, ssl);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(login, password);

                //for (int i = 0;i < 5; i ++)
                //{
                //    try
                //    {
                //        client.Authenticate(login, password);
                //        break;
                //    }
                //    catch (Exception ex)
                //    {
                //        try
                //        {
                //            client.Authenticate(login, password);
                //            break;
                //        } catch (Exception ed)
                //        {
                //            Console.WriteLine("ed : " + ed.Message  + "-" + i);
                //        }
                        
                //    }
                //    Thread.Sleep(4000);
                //}
                
                

                // The Inbox folder is always available on all IMAP servers...
                var inbox = client.Inbox;
                if (inbox != null )
                {
                    inbox.Open(FolderAccess.ReadOnly);

                    //Console.WriteLine("Total messages: {0}", inbox.Count);
                    //Console.WriteLine("Recent messages: {0}", inbox.Recent);

                    for (int i = 0; i < inbox.Count; i++)
                    {
                        var message = inbox.GetMessage(i);
                        //Console.WriteLine("Subject: {0}", message.Subject);
                        messages.Add(message.Subject + "|" + message.Body);
                    }

                    client.Disconnect(true);
                }
                    
            }

            return messages;
        }
    }

    public static class Mail
    {
        public static MailObject GetHotmail(DeviceObject device, OrderObject order)
        {
            try
            {
                MailObject mail = new MailObject();
                for (int i = 0; i < 100; i++)
                {
                    mail = CacheServer.GetHotmailLocalCache(PublicData.CacheServerUri, "");
                    if (!IsMailEmpty(mail))
                    {
                        if (!PublicData.last100Mails.Contains(mail.email))
                        {
                            PublicData.last100Mails.Enqueue(mail.email);
                            if (PublicData.last100Mails.Count > 100)
                            {
                                PublicData.last100Mails.Dequeue(); // xóa mail cũ nhất
                            }
                            break;
                        }
                        else
                        {
                            Utility.LogStatus(device, i + " - Get hotmail từ server cache - mail đã tồn tại---", 500);
                        }
                    }
                }


                if (IsMailEmpty(mail))
                {
                    return GetHotmailOutsite(device, order);
                }
                else
                {
                    Utility.LogStatus(device, "Get mail từ server cache---", 1000);
                    return mail;
                }
            } catch (Exception ee)
            {
                Utility.LogStatus(device, "Lỗi get hotmail từ cache server: " + ee.Message);
            }
            return null;
        }

        public static MailObject getOtherMail(DeviceObject device, OrderObject order, bool getDecision, DataGridView dataGridView, bool forceGmail)
        {
            try
            {
                string deviceID = device.deviceId;


                Utility.LogStatus(device, "Kiểm tra mạng ổn định mới get mail:");
                if (!order.proxyWfi && order.hasproxy && order.proxy != null)
                {
                    Proxy proxy = order.proxy;
                    OutsideServer.WaitUntilProxyStable(order.proxy);
                }
                else
                {
                    OutsideServer.WaitUntilNetworkStable();
                }

                for (int i = 0; i < PublicData.soLanChoMail; i++)
                {
                    if (getDecision)
                    {
                        Decision shouldStop = CacheServer.CheckDecision(device.deviceId);
                        if (shouldStop != null && shouldStop.stop)
                        {

                            order.isSuccess = true;
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                            return null;
                        }
                    }
                    Utility.LogStatus(device, "Get gmail lần:" + i);
                    Utility.UnlockScreenPhone(deviceID);

                    string xmmnmm = Utility.GetUIXml(deviceID);
                    if (!Utility.CheckTextExist(deviceID, "nhậpemail", 1, xmmnmm) && !Utility.CheckTextExist(deviceID, "email mới", 1, xmmnmm))
                    {
                        Device.Unlockphone(deviceID);
                        Thread.Sleep(5000);
                        Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                        Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                        Utility.LogStatus(device, "Mất màn hình nhập mail - return ", 20000);
                        if (order.currentMail == null)
                        {
                            order.currentMail = new MailObject();
                            order.currentMail.message = "mất màn hình nhập mail";
                        }
                        return order.currentMail;
                    }


                    bool cache = false;
                    bool vip = false;
                    if (i % 2 == 0)
                    {
                        cache = true;
                    }
                    if (i % 2 == 0)
                    {
                        vip = true;
                    }
                    order.currentMail = GetTempmail(deviceID, vip, "", order.tempmailType, PublicData.CacheServerUri, cache);

                    if (!IsMailEmpty(order.currentMail))
                    {
                        return order.currentMail;
                    }

                    if (PublicData.ThoatGmail && i > 20)
                    {
                        Utility.LogStatus(device, "Thoát get mail sớm vì đã thử nhiều lần nhưng chưa có mail", 5000);
                        break;
                    }
                }
                Utility.LogStatus(device, "Kết thúc vòng lặp lấy mail mà chưa có mail:" + PublicData.soLanChoMail, 10000);

                if (IsMailEmpty(order.currentMail) && forceGmail)
                {
                    Utility.LogStatus(device, "Không thể lấy gmail -> store nvr - color: DarkSeaGreen");
                    return null;
                }

                if (IsMailEmpty(order.currentMail)) // get hotmail
                {
                    order.isHotmail = true;
                    MailObject mail = GetHotmail(device, order);

                    if (IsMailEmpty(mail))
                    {
                        order.currentMail = Mail.GetHotmailOutsite(device, order);
                        Utility.LogStatus(device, "Get hotmail");
                    }
                    else
                    {
                        Utility.LogStatus(device, "Get mail từ server cache---", 1000);
                        order.currentMail = mail;
                    }
                }
                if (IsMailEmpty(order.currentMail)) // get tempmail
                {
                    order.isHotmail = false;

                    Utility.LogStatus(device, " tempmail generator");
                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.DarkGoldenrod;

                    order.tempmailType = Constant.TEMP_GENERATOR_EMAIL;
                    order.currentMail = Mail.GetTempmail(deviceID, true, "", order.tempmailType, PublicData.CacheServerUri);
                    Thread.Sleep(2000);
                }
                if (!IsMailEmpty(order.currentMail))
                {
                    Thread.Sleep(3000);
                    return order.currentMail;
                }
            }
            catch (Exception ex)
            {
                Utility.LogStatus(device, "Lỗi trang mail exception");
                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.DarkMagenta;
                return null;
            }
            return null;
        }
        public static MailObject GetMail(DeviceObject device, OrderObject order, bool getDecision, DataGridView dataGridView, bool forceGmail)
        {
            if (!IsMailEmpty(order.currentMail))
            {
                return order.currentMail;
            }
            if (order.isHotmail)
            {
                return GetHotmail(device, order);
            } else // get gmail 
            {
                return getOtherMail(device, order, getDecision, dataGridView, forceGmail);
            }
        }

        public static bool IsMailEmpty(MailObject currentMail)
        {
            if (currentMail == null || string.IsNullOrEmpty(currentMail.email)) { return true; }
            return false;
        }

        public static List<MailObject> GetHotmailTool()
        {
            List<MailObject> listmail = new List<MailObject>();
                
            int rand = new Random().Next(1, 7);
            listmail = GetMultiMailVandong(1, rand);
            if (listmail != null && listmail.Count > 0)
            {
                return listmail;
            }
            listmail = GetMultiMailVandong(2, rand);
            if (listmail != null && listmail.Count > 0)
            {
                return listmail;
            }
            List<MailObject> mailUnli = GetMultiHotmailUnlimitTime(rand);
            if (mailUnli != null && mailUnli.Count > 0)
            {
                listmail.AddRange(mailUnli);
                return listmail;
            }
            return listmail;
        }
        public static MailObject GetHotmailOutsite(DeviceObject device, OrderObject order)
        {
            int count = 50;
            if (order.getTrustmail)
            {
                count = 2;
            }
            for (int i = 0; i < count; i ++)
            {
                order.currentMail = CacheServer.GetHotmailLocalCache(PublicData.CacheServerUri, "");
                if (IsMailEmpty(order.currentMail))
                {
                    order.currentMail = GetMailVandong(1);
                    if (IsMailEmpty(order.currentMail))
                    {
                        order.currentMail = GetMailVandong(2);
                        if (IsMailEmpty(order.currentMail))
                        {
                            order.currentMail = GetHotmailUnlimitTime();
                            if (IsMailEmpty(order.currentMail))
                            {
                                order.currentMail = GetHotmailClonenha();
                            }
                        }
                    }
                    if (!IsMailEmpty(order.currentMail))
                    {
                        //List<string> otps = OutsideServer.GetOtpByOAuth2(order.currentMail);
                        
                        return order.currentMail;
                    }
                }
                Utility.LogStatus(device, "Get hotmail từ tool lần: " + i);
            }
            
            

            if (order.currentMail == null || order.currentMail.status == Constant.FAIL)
            {
                if (order.getTrustmail)
                {
                    order.currentMail = Mail.GetHotmailUnlimited(1, "5");
                    if (order.currentMail == null || order.currentMail.status == Constant.FAIL)
                    {
                        order.currentMail = Mail.GetTrustMailVandong(5);
                        if (IsMailEmpty(order.currentMail))
                        {
                            Utility.LogStatus(device, "Không thể lấy mail từ vandong 5, thử lấy từ vandong 59");
                            order.currentMail = Mail.GetTrustMailVandong(59);
                            if (IsMailEmpty(order.currentMail))
                            {
                                Utility.LogStatus(device, "Không thể lấy mail từ vandong 5, thử lấy từ vandong 60");
                                order.currentMail = Mail.GetTrustMailVandong(60);
                            }
                        }
                    }

                }
                //if (PublicData.ForceHotmail)
                //{
                    return order.currentMail;
                //}
                if ( order.currentMail == null || order.currentMail.status == Constant.FAIL)
                {
                    Utility.LogStatus(device, "Hotmail error: ------------get gmail", 2000);
                    order.isHotmail = false;
                    order.tempmailType = Constant.GMAIL_SUPERTEAM;
                    Utility.LogStatus(device, "Get super gmail ");
                    order.currentMail = Mail.GetTempmail(device.deviceId, true, "", order.tempmailType, PublicData.CacheServerUri);

                    if (order.currentMail == null || order.currentMail.status == Constant.FAIL)
                    {
                        order.tempmailType = Constant.TEMP_GENERATOR_1_SEC_EMAIL;
                        Utility.LogStatus(device, "Get tempmail 1 sec   ", 2000);
                        order.currentMail = Mail.GetTempmail(device.deviceId, true, "", order.tempmailType, PublicData.CacheServerUri);

                    }

                    if (order.currentMail != null)
                    {
                        Utility.LogStatus(device, "Gmail ok kkkkkkkk: " + order.currentMail.message);
                    }
                }
            }
            return order.currentMail;
        }
        public static MailObject GetHotmailClonenha()
        {
            List<int> types = new List<int> {  86, 87};
            MailObject mail = new MailObject();
            foreach (int type in types)
            {
                mail = OutsideServer.GetHotmailClonenha(type);
                if (mail != null && mail.status != Constant.FAIL && !string.IsNullOrEmpty(mail.email))
                {
                    return mail;
                }
            }

            return mail;
        }
        public static MailObject GetHotmailUnlimitTime()
        {
            MailObject mail = new MailObject();
            if (!PublicData.unlimit)
            {
                return null;
            }
            
            List<string> types = new List<string> { "5", "6", "14", "15", "16", "45", "46" };

            foreach (string type in types)
            {
                mail = Mail.GetHotmailUnlimited(1, type);
                if (mail != null && mail.status != Constant.FAIL && !string.IsNullOrEmpty(mail.email))
                {
                    return mail;
                }
            }
            
            return mail;
        }
        public static List<MailObject> GetMultiHotmailUnlimitTime(int count)
        {
            List<MailObject> mail = new List<MailObject>();
            if (!PublicData.unlimit)
            {
                return mail;
            }

            List<string> types = new List<string> { "5", "6", "14", "15", "16", "45", "46" };

            foreach (string type in types)
            {
                List<MailObject> mailtemp = Mail.GetMultiHotmailUnlimited(count, type);
                if (mailtemp != null && mailtemp.Count > 0)
                {
                    mail.AddRange(mailtemp);
                }
            }

            return mail;
        }
        public static MailObject GetHotmailUnlimited(int amount, string type)
        {
            try
            {
                HotmailUnlimitedResponse responseData = GetHotMailUnlimited(amount, type);
                if (responseData != null && responseData.status && responseData.data != null && responseData.data.Length > 0)
                {

                    for (int i = 0; i < responseData.data.Length; i++)
                    {
                        MailObject mailObj = new MailObject();
                        mailObj.email = responseData.data[i].email;
                        mailObj.password = responseData.data[i].password;
                       
                        mailObj.accessToken = responseData.data[i].access_token;
                        mailObj.refreshToken = responseData.data[i].refresh_token;
                        mailObj.clientId = responseData.data[i].client_id;
                        mailObj.source = "unlimit";
                        mailObj.unlimitType = type;
                        mailObj.isHotmail = true;
                        return mailObj;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }
        public static List<MailObject> GetMultiHotmailUnlimited(int amount, string type)
        {
            if (!PublicData.unlimit)
            {
                return new List<MailObject>();
            }
            List<MailObject> mail = new List<MailObject>();
            try
            {
                
                HotmailUnlimitedResponse responseData = GetHotMailUnlimited(amount, type);
                if (responseData != null && responseData.status && responseData.data != null && responseData.data.Length > 0)
                {

                    for (int i = 0; i < responseData.data.Length; i++)
                    {
                        MailObject mailObj = new MailObject();
                        mailObj.email = responseData.data[i].email;
                        mailObj.password = responseData.data[i].password;

                        mailObj.accessToken = responseData.data[i].access_token;
                        mailObj.refreshToken = responseData.data[i].refresh_token;
                        mailObj.clientId = responseData.data[i].client_id;
                        mailObj.source = "unlimit";
                        mailObj.unlimitType = type;
                        mailObj.isHotmail = true;
                        mail.Add(mailObj);
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return mail;
        }
        public static string CreateRandomString(int lengText, Random rd = null)
        {
            string outPut = "";
            if (rd == null)
                rd = new Random();
            string validChars = "abcdefghijklmnopqrstuvwxyz";
            for (int i = 0; i < lengText; i++)
            {
                outPut += validChars[rd.Next(0, validChars.Length)];
            }
            return outPut;
        }
        public static string GetDuoiMail(string duoiMail = "")
        {
            if (!string.IsNullOrEmpty(duoiMail))
            {
                return "@" + duoiMail;
            }
            RequestHttpFb request = new RequestHttpFb();

            string respond = request.RequestGet("https://generator.email/");
            MatchCollection collection = Regex.Matches(respond, "change_dropdown_list\\(this.innerHTML\\)\" id=\"(.*?)\"");
            List<string> lstDuoiMail = new List<string>();

            
            for (int i = 0; i < collection.Count; i++)
            {
                string temp = collection[i].Groups[1].Value;
                if (CheckBasicString(temp) && !IsContainNumber(temp) && !temp.Contains("-")
                    && (temp.EndsWith(".com") || temp.EndsWith(".org") || temp.EndsWith(".info")))
                    lstDuoiMail.Add(temp);
            }

            if (lstDuoiMail.Count <= 0)
                return "";

            Random rd = new Random();
            return "@" + lstDuoiMail[rd.Next(0, lstDuoiMail.Count)];
        }
        public static string GetDuoiMailEmailFake(string duoiMail = "")
        {
            if (!string.IsNullOrEmpty(duoiMail))
            {
                return "@" + duoiMail;
            }
            RequestHttpFb request = new RequestHttpFb();

            string respond = request.RequestGet("https://emailfake.com/");
            MatchCollection collection = Regex.Matches(respond, "change_dropdown_list\\(this.innerHTML\\)\" id=\"(.*?)\"");
            List<string> lstDuoiMail = new List<string>();


            for (int i = 0; i < collection.Count; i++)
            {
                string temp = collection[i].Groups[1].Value;
                if (CheckBasicString(temp) && !IsContainNumber(temp) && !temp.Contains("-") && (temp.EndsWith(".com") || temp.EndsWith(".org") || temp.EndsWith(".info")))
                    lstDuoiMail.Add(temp);
            }

            if (lstDuoiMail.Count <= 0)
                return "";

            Random rd = new Random();
            return "@" + lstDuoiMail[rd.Next(0, lstDuoiMail.Count)];
        }

        public static MailObject GetMailfakemailgenerator()
        {
            MailObject dd = new MailObject();
            
            RequestHttpFb request = new RequestHttpFb();

            string respond = request.RequestGet("https://www.fakemailgenerator.com/");
            MatchCollection collection = Regex.Matches(respond, "@(.*?)<");
            List<string> lstDuoiMail = new List<string>();
            MatchCollection tenMailTemp = Regex.Matches(respond, "value=\"(.*?)\"");
            string tenMail = tenMailTemp[0].Groups[1].Value;
            for (int i = 0; i < collection.Count; i++)
            {
                string temp = collection[i].Groups[1].Value;
                if (CheckBasicString(temp) && !IsContainNumber(temp) && !temp.Contains("-") 
                    //&& (temp.EndsWith(".com") || temp.EndsWith(".org") || temp.EndsWith(".info"))
                    )
                    lstDuoiMail.Add(temp);
            }

            if (lstDuoiMail.Count <= 0)
                return dd;

            Random rd = new Random();
            string mail = tenMail.ToLower() + "@" + lstDuoiMail[rd.Next(0, lstDuoiMail.Count)];

            dd.email = mail;
            dd.password = Constant.TEMPMAIL;
            return dd;
        }
        private static bool IsContainNumber(string pValue)
        {
            foreach (Char c in pValue)
            {
                if (Char.IsDigit(c))
                    return true;
            }
            return false;

        }
        private static bool CheckBasicString(string text)
        {
            bool result = true;
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (!((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == '.'))
                {
                    result = false;
                    break;
                }
            }
            return result;

        }
        public static MailObject GetHotmail(string server, bool isHotmail, string type, string tempName ="", string tempSubfix = "")
        {
            MailObject mail = new MailObject();
            string[] lineTemp = FileUtil.GetAndDeleteLine("FileHotmail.txt").Split('|'); // Get mail form local first
            if (lineTemp == null || lineTemp.Length < 2)
            {
                if (isHotmail)
                {
                    mail = CacheServer.GetHotmailLocalCache(server, type);
                    
                    if (mail == null)
                    {
                        if (type == Constant.OUTLOOK_DOMAIN)
                        {
                            string type2 = Constant.OUTLOOK;
                            string type3 = Constant.HOTMAIL;
                            mail = CacheServer.GetHotmailLocalCache(server, type2);
                            if (mail == null)
                            {
                                mail = CacheServer.GetHotmailLocalCache(server, type3);
                            }
                        }
                        if (type == Constant.OUTLOOK)
                        {
                            string type2 = Constant.OUTLOOK_DOMAIN;
                            string type3 = Constant.HOTMAIL;
                            mail = CacheServer.GetHotmailLocalCache(server, type2);
                            if (mail == null)
                            {
                                mail = CacheServer.GetHotmailLocalCache(server, type3);
                            }
                        }
                        if (type == Constant.HOTMAIL)
                        {
                            string type2 = Constant.OUTLOOK_DOMAIN;
                            string type3 = Constant.OUTLOOK;
                            mail = CacheServer.GetHotmailLocalCache(server, type2);
                            if (mail == null)
                            {
                                mail = CacheServer.GetHotmailLocalCache(server, type3);
                            }
                        }
                    }
                    return mail;
                } else
                {
                    return null;
                }
            }
            mail.email = lineTemp[0];
            mail.password = lineTemp[1];
            return mail;
        }
        public static MailObject CreateTempMail(string tenMail = "", string duoiMail = "")
        {
            MailObject mail = new MailObject();
            

            int dem = 0;
            if (!string.IsNullOrEmpty(duoiMail))
            {
                duoiMail = "@" + duoiMail;
            } else
            {
                while (duoiMail == "")
                {
                    dem++;
                    duoiMail = GetDuoiMail(duoiMail);
                    if (dem == 10)
                        return null;
                }
            }
            

            if (string.IsNullOrEmpty(tenMail))
            {
                string language = "";
                string gender = "";
                if (string.IsNullOrEmpty(language))
                {
                    Random r = new Random();
                    if (r.Next(1, 100) < 10)
                    {
                        language = Constant.LANGUAGE_VN;
                    }
                    else
                    {
                        language = Constant.LANGUAGE_US;
                    }

                    if (r.Next(100,200) > 150)
                    {
                        gender = Constant.FEMALE;
                    } else
                    {
                        gender = Constant.MALE;
                    }
                }


                if (language == Constant.LANGUAGE_US)
                {
                    tenMail = Utility.GetFirtName(language, gender).Trim().Replace(" ", "") + Utility.RandomNumberString(6);
                }
                else
                {
                    tenMail = Utility.ConvertToUnsign(Utility.GetFirtName(language, gender)).Trim().Replace(" ", "") + Utility.RandomNumberString(6);
                }
            }
            mail.email = tenMail.ToLower() + duoiMail;
            mail.password = Constant.TEMPMAIL;
            return mail;
        }

        public static MailObject Get1SecMail()
        {
            MailObject mail = OutsideServer.Get1SecMail();
            return mail;
        }

        public static MailObject CreateFakeEmail(string tenMail = "", string duoiMail = "")
        {
            MailObject mail = new MailObject();


            int dem = 0;
            if (!string.IsNullOrEmpty(duoiMail))
            {
                duoiMail = "@" + duoiMail;
            }
            else
            {
                while (duoiMail == "")
                {
                    dem++;
                    duoiMail = GetDuoiMailEmailFake(duoiMail);
                    if (dem == 10)
                        return null;
                }
            }


            if (string.IsNullOrEmpty(tenMail))
            {
                string language = "";
                string gender = "";
                if (string.IsNullOrEmpty(language))
                {
                    Random r = new Random();
                    if (r.Next(1, 100) < 90)
                    {
                        language = Constant.LANGUAGE_VN;
                    }
                    else
                    {
                        language = Constant.LANGUAGE_US;
                    }

                    if (r.Next(100, 200) > 150)
                    {
                        gender = Constant.FEMALE;
                    }
                    else
                    {
                        gender = Constant.MALE;
                    }
                }


                if (language == Constant.LANGUAGE_US)
                {
                    tenMail = Utility.GetFirtName(language, gender).Trim().Replace(" ", "") + Utility.RandomNumberString(6);
                }
                else
                {
                    tenMail = Utility.ConvertToUnsign(Utility.GetFirtName(language, gender)).Trim().Replace(" ", "") + Utility.RandomNumberString(6);
                }
            }
            mail.email = tenMail.ToLower() + duoiMail;
            mail.password = "tempmail";
            return mail;
        }
        public static MailObject CreateTempMail2(string deviceID, string tenMail = "", string duoiMail = "")
        {
            MailObject mail = new MailObject();


            int dem = 0;
            if (!string.IsNullOrEmpty(duoiMail))
            {
                duoiMail = "@" + duoiMail;
            }
            else
            {
                
                for (int i = 0; i < 10; i ++)
                {
                    Device.OpenWeb(deviceID, "https://www.fakemailgenerator.com/");
                    Thread.Sleep(3000);
                    string uiXml = Utility.GetRawUIXml(deviceID);
                    string tempmail = Regex.Match(uiXml, "([a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\\.[a-zA-Z0-9_-]+)").Groups[1].ToString();
                    if (!string.IsNullOrEmpty(tempmail))
                    {
                        mail.email = tempmail.ToLower();
                        mail.password = "tempmail";
                        return mail;
                    }
                }
            }

            mail.email = "";
            mail.password = "tempmail";
            return mail;
        }
        public static MailObject CreateTempMail3(string deviceID, string tenMail = "", string duoiMail = "")
        {
            MailObject mail = new MailObject();


            int dem = 0;
            if (!string.IsNullOrEmpty(duoiMail))
            {
                duoiMail = "@" + duoiMail;
            }
            else
            {

                for (int i = 0; i < 10; i++)
                {
                    Device.OpenWeb(deviceID, "https://fakermail.com/");
                    Thread.Sleep(3000);
                    for (int j = 0; j < 10; j++)
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 49.7, 49.5);
                        Thread.Sleep(2000);
                        string uiXml = Utility.GetRawUIXml(deviceID);
                        string tempmail = Regex.Match(uiXml, "([a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\\.[a-zA-Z0-9_-]+)").Groups[1].ToString();
                        if (!string.IsNullOrEmpty(tempmail) && tempmail.EndsWith(".com") || tempmail.EndsWith(".org") || tempmail.EndsWith(".info"))
                        {
                            mail.email = tempmail.ToLower();
                            mail.password = "tempmail";
                            return mail;
                        }
                    }
                   
                }
            }

            mail.email = "";
            mail.password = "tempmail";
            return mail;
        }

        //public static MailObject CreateTempMail4(string deviceID, string tenMail = "", string duoiMail = "")
        //{
        //    MailObject mail = new MailObject();
            

        //    int dem = 0;
        //    if (!string.IsNullOrEmpty(duoiMail))
        //    {
        //        duoiMail = "@" + duoiMail;
        //    }
        //    else
        //    {

        //        for (int i = 0; i < 10; i++)
        //        {
        //            Device.OpenWeb(deviceID, "https://www.emailnator.com/");
        //            Thread.Sleep(5000);
        //            for (int j = 0; j < 10; j++)
        //            {
        //                Device.Swipe(deviceID, 200, 800, 300, 600);
        //                Thread.Sleep(1000);
        //                if (Utility.FindAndTap(deviceID, Utility.MAIL_GO, 1))
        //                {
        //                    break;
        //                }
        //            }
        //            //KAutoHelper.ADBHelper.TapByPercent(deviceID, 49.7, 49.5);
        //            Thread.Sleep(2000);
        //            Device.Swipe(deviceID, 200, 800, 300, 500);
        //            Thread.Sleep(1000);
        //            string uiXml = Utility.GetRawUIXml(deviceID);
        //            string tempmail = Regex.Match(uiXml, "([a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\\.[a-zA-Z0-9_-]+)").Groups[1].ToString();
        //            if (!string.IsNullOrEmpty(tempmail) && tempmail.EndsWith(".com") || tempmail.EndsWith(".org") || tempmail.EndsWith(".info"))
        //            {
        //                mail.email = tempmail.ToLower();
        //                mail.password = "tempmail";
        //                return mail;
        //            }
        //        }
        //    }

        //    mail.email = "";
        //    mail.password = "tempmail";
        //    return mail;
        //}

        public static MailObject GetTempmailLol()
        {
            MailObject mail = OutsideServer.getTempmailLol(); 
           
            mail.password = "tempmail";
            return mail;
        }

        public static MailObject GetGmailSellGmail(string server)
        {
            MailObject mail = CacheServer.GetSellGmailLocalCache(server);
            if (Mail.IsMailEmpty(mail))
            {
                mail = OutsideServer.GetGmailSellGmail();
                if (mail != null && !string.IsNullOrEmpty(mail.email))
                {
                    mail.message = "sell gmail from cache";
                }
            }
            else
            {
                Console.WriteLine("Get mail cache thanh cong:" + mail.email);
            }
            return mail;
        }

        public static MailObject GetGmailSptGmail(string deviceId, bool vip, string server, bool cache = true)
        {
            
            MailObject mail = new MailObject();
            if (cache)
            {
                mail = CacheServer.GetSuperGmailLocalCache(server, deviceId);
            }
             
            if (IsMailEmpty(mail))
            {
                mail = OutsideServer.GetGmailAllSite(vip);
                if (mail != null && !string.IsNullOrEmpty(mail.email))
                {
                    mail.createdAt = DateTime.UtcNow;
                    Utility.LogStatus(deviceId, "Get mail truc tiep tu tool:" + mail.email + " - " + mail.createdAt);
                    if (string.IsNullOrEmpty(mail.message))
                    {
                        mail.message = "gmail trực tiếp từ tool";
                    }   
                }
            }
            else
            {
                Utility.LogStatus(deviceId, "Get mail cache thanh cong:" + mail.email + " - " + mail.createdAt);
            }
            if (!IsMailEmpty(mail) && !string.IsNullOrEmpty(deviceId) && !deviceId.Contains("Virtual"))
            {
                mail.deviceId = deviceId;
                mail.pcName = Environment.MachineName;
                CacheServer.LogMailServerCache(mail, PublicData.LogServerUri);
            }
            return mail;
        }

        public static MailObject GetGmailOtp(string server)
        {
            MailObject mail = CacheServer.GetGmailOtpLocalCache(server);
            if (IsMailEmpty(mail))
            {
                mail = OutsideServer.GetGmailOtp();
                if (mail != null && !string.IsNullOrEmpty(mail.email))
                {
                    mail.message = "gmail otp trực tiếp";
                }
            }
            else
            {
                Console.WriteLine("Get mail cache thanh cong:" + mail.email);
            }
            return mail;
        }

        public static MailObject GetGmailDichVuGmail(string server)
        {

            MailObject mail = CacheServer.GetDichvuGmailLocalCache(server);
            if (IsMailEmpty(mail))
            {
                mail = OutsideServer.GetGmailDichVuGmail(RequestApi.PublicData.AccessTokenDvgmNormal, "Facebook");
                if (mail != null && !string.IsNullOrEmpty(mail.email))
                {
                    mail.message = "dichvu gmail trực tiếp";
                }
            } else
            {
                Console.WriteLine("Get mail cache thanh cong:" + mail.email);
            }
            
            return mail;
        }
        

        public static MailObject GetMailOtpGmail()
        {
            MailObject mail = OutsideServer.GetMailOtp();
            return mail;
        }
        public static MailObject GetMail48hGmail()
        {
            MailObject mail = OutsideServer.GetGmail48h();
            return mail;
        }
        public static MailObject GetGmail30Min()
        {
            MailObject mail = OutsideServer.GetGmail30Min();
            return mail;
        }

        public static MailObject GetGmailOtpGmail()
        {
            MailObject mail = OutsideServer.GetGmailOtp();
            return mail;
        }

        //public static MailObject GetGmailSuperTeam()
        //{
        //    MailObject mail = OutsideServer.GetGmailSuperTeam();
        //    return mail;
        //}

        public static MailObject CreateTempHotmail(string tenMail = "")
        {
            MailObject mail = new MailObject();
            string duoiMail = "@outlook.com";

            if (tenMail == "")
            {
                tenMail = Utility.GetFirtName(Constant.LANGUAGE_US, Constant.MALE).Trim().Replace(" ", "") + CreateRandomString(5) + Utility.RandomNumberString(5);
            }
            mail.email = tenMail + duoiMail;
            mail.password = "tempmail";
            return mail;
        }

        public static MailObject MailGenerator(string suffix, string gender, string language)
        {
            string tenMail = "";
            if (tenMail == "")
            {
                if (string.IsNullOrEmpty(language))
                {
                    Random r = new Random();
                    if (r.Next(1, 100) < 50)
                    {
                        language = Constant.LANGUAGE_VN;
                    }
                    else
                    {
                        language = Constant.LANGUAGE_US;
                    }
                }
                
                
                if (language == Constant.LANGUAGE_US)
                {
                    tenMail = Utility.GetFirtName(language, gender).Trim().Replace(" ", "") + Utility.RandomNumberString(6);
                    //tenMail = Utility.RandomString(2) + Utility.RandomNumberString(2) + Utility.RandomString(4) + Utility.RandomNumberString(2);
                } else
                {
                    tenMail = Utility.ConvertToUnsign(Utility.GetFirtName(language, gender)).Trim().Replace(" ", "")  + Utility.RandomNumberString(6);                    
                    //tenMail = Utility.RandomString(2) + Utility.RandomNumberString(2) + Utility.RandomString(4) + Utility.RandomNumberString(2);
                }
            }
            MailObject mail = new MailObject();
            mail.email = tenMail.ToLower().Replace("=", "") + suffix;
            mail.password = "noveri_mail";
            return mail;
        }
        public static string GetOTPGenerator(string mail, int time)
        {
            RequestHttpFb request = new RequestHttpFb();

            string otp = Constant.FAIL;

            int timeStart = Environment.TickCount;
            for (int k = 0; k < time; k++)
            {
                try
                {
                    Thread.Sleep(1000);

                    string respond = request.RequestGet("https://generator.email/" + mail);
                    Console.WriteLine(respond);
                    otp = Regex.Match(respond, ">\\d{5}<").Value.Replace(" ", "").Replace(">","").Replace("<","");
                    if (otp != "")
                    {
                        return otp;
                    }
                    otp = Regex.Match(respond, ">\\d{5} is your Facebook confirmation").Value.Replace(" ", "").Replace(">", "").Replace("<", "");
                    if (otp != "")
                    {
                        return otp.Substring(0, 5);
                    }
                    otp = Regex.Match(respond, "d{5}\\s").Value.Replace(" ", "");
                    if (otp != "")
                    {
                        return otp;
                    }
                }
                catch {

                    return Constant.FAIL;
                }
            }
            Console.WriteLine("Tempmail otp:" + otp);
            if (otp == null || otp.Trim() == "")
            {
                Console.WriteLine("Otp fail");
                return Constant.FAIL;
            }
            return otp;
        }

        public static string GetOTPEmailFake(string mail, int time)
        {
            RequestHttpFb request = new RequestHttpFb();

            string otp = Constant.FAIL;

            int timeStart = Environment.TickCount;
            for (int k = 0; k < time; k++)
            {
                try
                {
                    Thread.Sleep(1000);

                    string respond = request.RequestGet("https://emailfake.com/" + mail);
                    Console.WriteLine(respond);
                    otp = Regex.Match(respond, ">\\d{5}<").Value.Replace(" ", "").Replace(">", "").Replace("<", "");
                    if (otp != "")
                    {
                        return otp;
                    }
                    otp = Regex.Match(respond, ">\\d{5} is your Facebook confirmation").Value.Replace(" ", "").Replace(">", "").Replace("<", "");
                    if (otp != "")
                    {
                        return otp.Substring(0, 5);
                    }
                    otp = Regex.Match(respond, "d{5}\\s").Value.Replace(" ", "");
                    if (otp != "")
                    {
                        return otp;
                    }
                }
                catch
                {

                    return Constant.FAIL;
                }
            }
            Console.WriteLine("Tempmail otp:" + otp);
            if (otp == null || otp.Trim() == "")
            {
                Console.WriteLine("Otp fail");
                return Constant.FAIL;
            }
            return otp;
        }
        public static string GetOTPfakemailgenerator(string mail, int time)
        {
            RequestHttpFb request = new RequestHttpFb();

            string otp = Constant.FAIL;

            int timeStart = Environment.TickCount;
            string[] temp = mail.Split('@');
            for (int k = 0; k < time; k++)
            {
                try
                {
                    Thread.Sleep(5000);
                    string url = "https://www.fakemailgenerator.com/inbox/" + temp[1] + "/" + temp[0];
                    string respond = request.RequestGet(url);
                    Console.WriteLine(respond);

                    if (respond.Contains("message-"))
                    {
                        MatchCollection tenMailTemp = Regex.Matches(respond, "message-(.*?)\\/");
                        string tenMail = tenMailTemp[0].Groups[1].Value;
                        url = url + "/message-" + tenMail + "/"  ;
                        url = url.Replace("inbox", "email");
                        respond = request.RequestGet(url);
                        otp = Regex.Match(respond, ">\\d{5}<").Value.Replace(" ", "").Replace(">", "").Replace("<", "");
                        if (otp != "")
                        {
                            return otp;
                        }
                        otp = Regex.Match(respond, ">\\d{5} is your Facebook confirmation").Value.Replace(" ", "").Replace(">", "").Replace("<", "");
                        if (otp != "")
                        {
                            return otp.Substring(0, 5);
                        }
                        otp = Regex.Match(respond, "d{5}\\s").Value.Replace(" ", "");
                        if (otp != "")
                        {
                            return otp;
                        }
                    }
                    if (Utility.forceStopGetOtp && k > 3)
                    {
                        k = time;
                    }

                }
                catch
                {

                    return Constant.FAIL;
                }
            }
            Console.WriteLine("Tempmail otp:" + otp);
            if (otp == null || otp.Trim() == "")
            {
                Console.WriteLine("Otp fail");
                return Constant.FAIL;
            }
            return otp;
        }

        public static string GetCodeTempmailLol(string token, int time)
        {
            string otp = Constant.FAIL;

            
            try
            {
                for (int i = 0;i < time; i ++)
                {
                    string code = OutsideServer.getCodeTempmailLol(token);
                    if (!string.IsNullOrEmpty(code))
                    {
                        return code;
                    }
                    Thread.Sleep(5000);
                }
            }
            catch
            {
                return Constant.FAIL;
            }

            
            return otp;
        }

        public static string GetOtpMailOtp(string orderId)
        {
            string otp = Constant.FAIL;


            try
            {
                for (int i = 0; i < 30; i++)
                {
                    string code = OutsideServer.GetCodeMailOtp(orderId);
                    if (!string.IsNullOrEmpty(code))
                    {
                        return code;
                    }
                    Thread.Sleep(5000);
                }
            }
            catch
            {
                return Constant.FAIL;
            }
            return otp;
        }

        public static string GetOtpGmailOtpGmail(MailObject inMail, int time)
        {
            string otp = Constant.FAIL;

            try
            {
                for (int i = 0; i < time; i++)
                {
                    string code = OutsideServer.GetOtpGmailOtp(inMail.orderId);
                    if (!string.IsNullOrEmpty(code))
                    {
                        return code;
                    }
                    Thread.Sleep(5000);
                }
            }
            catch
            {
                return Constant.FAIL;
            }
            return otp;
        }

        public static string GetOtpGmailSuperTeam(MailObject inMail, int time)
        {
            string otp = Constant.FAIL;

            try
            {
                for (int i = 0; i < time; i++)
                {
                    string code = "";
                    if (!string.IsNullOrEmpty(inMail.source) && inMail.source == "unlimit_gmail")
                    {
                        code = OutsideServer.GetOtpGmailUnlimited(inMail);
                    }
                    if (!string.IsNullOrEmpty(inMail.source) && inMail.source == "shopgmail_gmail")
                    {
                        code = OutsideServer.GetOtpGmailShopgmail(inMail);
                    } else if (!string.IsNullOrEmpty(inMail.source) && 
                        (inMail.source == "thuesim_gmail") || inMail.source == "thuesim_gmail_vip")
                    {
                        code = OutsideServer.GetOtpGmailThuesim(inMail);
                    }
                    //else if (!string.IsNullOrEmpty(inMail.source) && inMail.source == "thuesim_gmail_vip")
                    //{
                    //    code = OutsideServer.GetOtpGmailThuesim(inMail);
                    //}
                    else if (!string.IsNullOrEmpty(inMail.source) && inMail.source == "super_gmail")
                    {
                        code = OutsideServer.GetOtpGmailSuperTeam(inMail);
                    } else if (!string.IsNullOrEmpty(inMail.source) && inMail.source == "dvgm")
                    {
                        code = OutsideServer.GetGmailDichVuGmailOtp(inMail);
                    } else if (!string.IsNullOrEmpty(inMail.source) && inMail.source == "otpcheap_gmail")
                    {
                        code = OutsideServer.GetCodeMailOtp(inMail.orderId);
                    } else if (!string.IsNullOrEmpty(inMail.source) && inMail.source == "hvl")
                    {
                        code = GetOtpHvl(inMail);
                    }
                    else if (!string.IsNullOrEmpty(inMail.source) && inMail.source == "shopmailmmo")
                    {
                        code = GetOtpShopgmailmmo(inMail);
                    }
                    else if (!string.IsNullOrEmpty(inMail.source) && inMail.source == PublicData.SourceClonenha)
                    {
                        code = GetOtpClonenha(inMail);
                    }

                    if (!string.IsNullOrEmpty(code))
                    {
                        if (code.Length == 5)
                        {
                            return code;
                        }
                        
                    }
                    Thread.Sleep(10000);
                }
            }
            catch
            {
                return Constant.FAIL;
            }
            return otp;
        }

        public static string GetOtp1Sec(MailObject inMail)
        {
            List<string> subjects = ServerApi.GetAllSubject1SecMail(inMail);
            string code = "";
            ////truy xuất nội dung từng mail
            foreach (string mail in subjects)
            {
                Console.WriteLine("subject:" + mail);
                code = Utility.FindCodeInSubject(mail);
                if (code != Constant.FAIL)
                {
                    break;
                }
            }
            return code;
        }

        public static string GetOtpGmail48h(MailObject inMail, int time)
        {
            string otp = Constant.FAIL;

            try
            {
                for (int i = 0; i < time; i++)
                {
                    string code = OutsideServer.GetOtpGmail48h(inMail.orderId);
                    if (!string.IsNullOrEmpty(code))
                    {
                        return code;
                    }
                    Thread.Sleep(5000);
                }
            }
            catch
            {
                return Constant.FAIL;
            }
            return otp;
        }

        public static string GetOtpGmail30(MailObject inMail, int time)
        {
            string code = "";

            try
            {
                var mailRepository = new GmailRepository(
                            "imap.gmail.com",
                            993,
                            true,
                           inMail.email,
                            inMail.passwordTemp
                        );

                var emailList = mailRepository.GetAllMails("inbox");

                foreach (ActiveUp.Net.Mail.Message email in emailList)
                {
                    Console.WriteLine("<p>{0}: {1}</p><p>{2}</p>", email.From, email.Subject, email.BodyHtml.Text);
                    if (email.Attachments.Count > 0)
                    {
                        foreach (MimePart attachment in email.Attachments)
                        {
                            Console.WriteLine("<p>Attachment: {0} {1}</p>", attachment.ContentName, attachment.ContentType.MimeType);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Get code mail:" + inMail.email + "|" + inMail.password + " - " + ex.Message);
                return Constant.FAIL;
            }
            return code;
        }
        //public static string GetOTPGenerator4(string deviceID, string mail, int timeOut = 60)
        //{
        //    string otp = Constant.FAIL;

        //    Device.ClearCache(deviceID, "org.lineageos.jelly");
        //    try
        //    {
        //        Thread.Sleep(5000);
        //        string url = "https://www.emailnator.com/inbox/" + mail;
        //        Device.OpenWeb(deviceID, url);

        //        Thread.Sleep(3000);
        //        Device.Swipe(deviceID, 200, 800, 300, 600);
        //        for (int i = 0; i < 10; i ++) 
        //        {

        //            if (Utility.FindAndTap(deviceID, Utility.MAIL_FB_CHECK, 1))
        //            {
        //                Thread.Sleep(3000);
        //                string respond = Utility.GetUIXml(deviceID);
        //                otp = Regex.Match(respond, ">\\d{5}<").Value.Replace(" ", "").Replace(">", "").Replace("<", "");
        //                if (otp != "")
        //                {
        //                    return otp;
        //                }
        //                otp = Regex.Match(respond, ">\\d{5} is your Facebook confirmation").Value.Replace(" ", "").Replace(">", "").Replace("<", "");
        //                if (otp != "")
        //                {
        //                    return otp.Substring(0, 5);
        //                }
        //                otp = Regex.Match(respond, "d{5}\\s").Value.Replace(" ", "");
        //                if (otp != "")
        //                {
        //                    return otp;
        //                }
        //            }
        //            Thread.Sleep(5000);
        //            Utility.FindAndTap(deviceID, Utility.MAIL_RELOAD, 1);
        //            Thread.Sleep(1000);
        //        }
        //    }
        //    catch
        //    {
        //        return Constant.FAIL;
        //    }

        //    Console.WriteLine("Tempmail otp:" + otp);
        //    if (otp == null || otp.Trim() == "")
        //    {
        //        Console.WriteLine("Otp fail");
        //        return Constant.FAIL;
        //    }
        //    return otp;
        //}

        public static string GetOtpSellGmail(MailObject mail, int time)
        {
            for (int i = 0; i < time; i ++)
            {
                string otp = OutsideServer.GetOtpSellGmail(mail.email);
                if (!string.IsNullOrEmpty(otp))
                {
                    if (otp == "0")
                    {
                        return Constant.FAIL;
                    }
                    return otp;
                }
                Thread.Sleep(10000);
                if (Utility.forceStopGetOtp && i > 3)
                {
                    break;
                }
            }
            return Constant.FAIL;
        }
        public static string GetOtpDichVuGmail(MailObject mail, int time)
        {
            int count = time + 5;
            for (int i = 0; i < count; i++)
            {
                string otp = OutsideServer.GetGmailDichVuGmailOtp(mail);
                if (!string.IsNullOrEmpty(otp))
                {
                    return otp;
                }
                Thread.Sleep(10000);
                if (Utility.forceStopGetOtp && i > 3)
                {
                    break;
                }
            }
            return Constant.FAIL;
        }
        public static string GetOtpDichVuGmail2(MailObject mail, int time)
        {
            for (int i = 0; i < time; i++)
            {
                string otp = OutsideServer.GetGmailDichVuGmailOtp(mail);
                if (!string.IsNullOrEmpty(otp))
                {
                    return otp;
                }
                Thread.Sleep(10000);
                if (Utility.forceStopGetOtp && i > 3)
                {
                    break;
                }
            }
            return Constant.FAIL;
        }

        public static MailObject GetTrustMailVandong(int type)
        {
            MailObject mailObj = new MailObject();
            ResponseDongVanObject responseData = GetHotMailDongVanApi(1, type);
            try
            {
                if (responseData != null && responseData.error_code == 200 && responseData.status && responseData.data.quality > 0
                    && responseData.data.list_data != null && responseData.data.list_data.Length > 0)
                {
                   
                    string[] temp = responseData.data.list_data[0].Split('|');

                   
                    mailObj.email = temp[0];
                    mailObj.password = temp[1];
                    mailObj.refreshToken = temp[2];
                    mailObj.clientId = temp[3];
                    mailObj.source = "vandong";
                    
                }
                else
                {
                    mailObj.status = Constant.FAIL;
                }
            }
            catch
            {

            }
            return mailObj;
        }
        public static MailObject GetMailVandong(int typeId, int count = 1)
        {
            MailObject mailObj = new MailObject();
            ResponseDongVanObject responseData = GetHotMailDongVanApi(count, typeId);
            try
            {
                if (responseData != null && responseData.error_code == 200 && responseData.status && responseData.data.quality > 0
                    && responseData.data.list_data != null && responseData.data.list_data.Length > 0)
                {

                    string[] temp = responseData.data.list_data[0].Split('|');


                    mailObj.email = temp[0];
                    mailObj.password = temp[1];
                    mailObj.refreshToken = temp[2];
                    mailObj.clientId = temp[3];
                    mailObj.source = "vandong";
                    mailObj.isHotmail = true;
                }
                else
                {
                    mailObj.status = Constant.FAIL;
                }
            }
            catch
            {

            }
            return mailObj;
        }
        public static List<MailObject> GetMultiMailVandong(int typeId, int count = 1)
        {
            List<MailObject> mailObj = new List<MailObject>();
            ResponseDongVanObject responseData = GetHotMailDongVanApi(count, typeId);
            try
            {
                if (responseData != null && responseData.error_code == 200 && responseData.status && responseData.data.quality > 0
                    && responseData.data.list_data != null && responseData.data.list_data.Length > 0)
                {
                    for (int i = 0; i < responseData.data.list_data.Length; i++)
                    {
                        string[] temp = responseData.data.list_data[i].Split('|');
                        MailObject mm = new MailObject();

                        mm.email = temp[0];
                        mm.password = temp[1];
                        mm.refreshToken = temp[2];
                        mm.clientId = temp[3];
                        mm.source = "vandong";
                        mm.isHotmail = true;
                        mailObj.Add(mm);
                    }
                }
            }
            catch
            {

            }
            return mailObj;
        }
        public static MailObject GetHotmail(string server, string type, bool getHotmailKieuMoi)
        {
            try
            {
                MailObject mail = new MailObject();
                string[] lineTemp = FileUtil.GetAndDeleteLine("FileHotmail.txt").Split('|'); // Get mail form local first
                if (lineTemp != null && lineTemp.Length >= 2) // Get mail local
                {
                    mail.email = lineTemp[0];
                    mail.password = lineTemp[1];
                } else // Get mail from server
                {
                    if (!getHotmailKieuMoi)
                    {
                        mail = ServerApi.GetMailServer(type);
                    } else
                    {
                        mail = CacheServer.GetHotmailLocalCache(server, type);
                    }
                    

                    if (mail == null)
                    {
                        if (type == Constant.OUTLOOK_DOMAIN)
                        {
                            string type2 = Constant.OUTLOOK;
                            string type3 = Constant.HOTMAIL;
                           // mail = ServerApi.GetHotmailLocalCache(server, type2);
                            if (!getHotmailKieuMoi)
                            {
                                mail = ServerApi.GetMailServer(type2);
                            }
                            else
                            {
                                mail = CacheServer.GetHotmailLocalCache(server, type2);
                            }
                            if (mail == null)
                            {
                                if (!getHotmailKieuMoi)
                                {
                                    mail = ServerApi.GetMailServer(type3);
                                }
                                else
                                {
                                    mail = CacheServer.GetHotmailLocalCache(server, type3);
                                }
                            }
                        }
                        if (type == Constant.OUTLOOK)
                        {
                            string type2 = Constant.OUTLOOK_DOMAIN;
                            string type3 = Constant.HOTMAIL;
                            if (!getHotmailKieuMoi)
                            {
                                mail = ServerApi.GetMailServer(type2);
                            }
                            else
                            {
                                mail = CacheServer.GetHotmailLocalCache(server, type2);
                            }
                            if (mail == null)
                            {
                                if (!getHotmailKieuMoi)
                                {
                                    mail = ServerApi.GetMailServer(type3);
                                }
                                else
                                {
                                    mail = CacheServer.GetHotmailLocalCache(server, type3);
                                }
                            }
                        }
                        if (type == Constant.HOTMAIL)
                        {
                            string type2 = Constant.OUTLOOK_DOMAIN;
                            string type3 = Constant.OUTLOOK;
                            if (!getHotmailKieuMoi)
                            {
                                mail = ServerApi.GetMailServer(type2);
                            }
                            else
                            {
                                mail = CacheServer.GetHotmailLocalCache(server, type2);
                            }
                            if (mail == null)
                            {
                                if (!getHotmailKieuMoi)
                                {
                                    mail = ServerApi.GetMailServer(type3);
                                }
                                else
                                {
                                    mail = CacheServer.GetHotmailLocalCache(server, type3);
                                }
                            }
                        }
                    }
                }

                return mail;
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public static MailObject GetTempmail(string deviceId, bool vip, string duoiMail, string tempmailType, string server, bool cache = true)
        {
            MailObject dd = new MailObject();
            
            if (tempmailType == Constant.TEMP_GENERATOR_EMAIL)
            {
                dd = CreateTempMail("", duoiMail);
            }
            else if (tempmailType == Constant.TEMP_GENERATOR_1_SEC_EMAIL)
            {
                // dd = Get1SecMail();
                dd = CreateTempMail("", duoiMail);
            }
            else if (tempmailType == Constant.TEMP_FAKE_EMAIL)
            {
                dd = CreateFakeEmail("", duoiMail);
            }
            else if (tempmailType == Constant.TEMP_TEMPMAIL_LOL)
            {
                dd = GetTempmailLol();
            }
            else if (tempmailType == Constant.GMAIL_SELL_GMAIL)
            {
                return dd;
                dd = GetGmailSellGmail(server);
            }
            else if (tempmailType == Constant.GMAIL_SELL_GMAIL_SERVER)
            {
                dd = GetGmailSellGmail(server);
            }
            else if (tempmailType == Constant.GMAIL_DICH_VU_GMAIL)
            {
                return dd;
                dd = GetGmailDichVuGmail(server);
            }
            else if (tempmailType == Constant.GMAIL_DICH_VU_GMAIL2)
            {
                return dd;
                dd = GetGmailDichVuGmail(server);
            }
            else if (tempmailType == Constant.FAKE_MAIL)
            {
                dd = GetMailfakemailgenerator();
            } else if (tempmailType == Constant.MAIL_OTP)
            {
                dd = GetMailOtpGmail();
            } else if (tempmailType == Constant.GMAIL_30_MIN)
            {
                dd = GetGmail30Min();
            } else if (tempmailType == Constant.GMAIL_OTP_GMAIL)
            {
                dd = GetGmailOtp(server);
            }
            else if (tempmailType == Constant.GMAIL_SUPERTEAM)
            {
                dd = GetGmailSptGmail(deviceId, vip, server, cache);
            }
            else if (tempmailType == Constant.GMAIL_48h)
            {
                dd = GetMail48hGmail();
            }
            if (dd!= null && dd.createdAt != null)
            {
                try
                {
                    DateTime local = TimeZoneInfo.ConvertTimeFromUtc(dd.createdAt, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
                    if (local.AddMinutes(12).CompareTo(DateTime.Now) < 0)
                    {
                        Utility.LogStatus(deviceId, "Mail hết hạn:" + dd.email + "|" + dd.createdAt);
                        dd.status = Constant.FAIL;
                        return null;
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return dd;
        }
    }
}
