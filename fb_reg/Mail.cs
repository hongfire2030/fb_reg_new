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
                    mail = ServerApi.GetHotmailLocalCache(server, type);
                    
                    if (mail == null)
                    {
                        if (type == Constant.OUTLOOK_DOMAIN)
                        {
                            string type2 = Constant.OUTLOOK;
                            string type3 = Constant.HOTMAIL;
                            mail = ServerApi.GetHotmailLocalCache(server, type2);
                            if (mail == null)
                            {
                                mail = ServerApi.GetHotmailLocalCache(server, type3);
                            }
                        }
                        if (type == Constant.OUTLOOK)
                        {
                            string type2 = Constant.OUTLOOK_DOMAIN;
                            string type3 = Constant.HOTMAIL;
                            mail = ServerApi.GetHotmailLocalCache(server, type2);
                            if (mail == null)
                            {
                                mail = ServerApi.GetHotmailLocalCache(server, type3);
                            }
                        }
                        if (type == Constant.HOTMAIL)
                        {
                            string type2 = Constant.OUTLOOK_DOMAIN;
                            string type3 = Constant.OUTLOOK;
                            mail = ServerApi.GetHotmailLocalCache(server, type2);
                            if (mail == null)
                            {
                                mail = ServerApi.GetHotmailLocalCache(server, type3);
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
            MailObject mail = ServerApi.Get1SecMail();
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
            MailObject mail = ServerApi.getTempmailLol(); 
           
            mail.password = "tempmail";
            return mail;
        }

        public static MailObject GetGmailSellGmail(string server)
        {
            MailObject mail = ServerApi.GetSellGmailLocalCache(server);
            if (Utility.IsMailEmpty(mail))
            {
                mail = ServerApi.GetGmailSellGmail();
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

        public static MailObject GetGmailSuperGmail(string server)
        {
            MailObject mail = ServerApi.GetSuperGmailLocalCache(server);
            if (Utility.IsMailEmpty(mail))
            {
                mail = ServerApi.GetGmailSuperTeam();
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

        public static MailObject GetGmailOtp(string server)
        {
            MailObject mail = ServerApi.GetGmailOtpLocalCache(server);
            if (Utility.IsMailEmpty(mail))
            {
                mail = ServerApi.GetGmailOtp();
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

        public static MailObject GetGmailDichVuGmail(string server)
        {

            MailObject mail = ServerApi.GetDichvuGmailLocalCache(server);
            if (Utility.IsMailEmpty(mail))
            {
                mail = ServerApi.GetGmailDichVuGmail("Facebook", "PtcRfCJe0UjBk4iJ2umU98ZnE7rzp0sJ");
                if (mail != null && !string.IsNullOrEmpty(mail.email))
                {
                    mail.message = "dichvu gmail from cache";
                }
            } else
            {
                Console.WriteLine("Get mail cache thanh cong:" + mail.email);
            }
            
            return mail;
        }
        

        public static MailObject GetMailOtpGmail()
        {
            MailObject mail = ServerApi.GetMailOtp();
            return mail;
        }
        public static MailObject GetMail48hGmail()
        {
            MailObject mail = ServerApi.GetGmail48h();
            return mail;
        }
        public static MailObject GetGmail30Min()
        {
            MailObject mail = ServerApi.GetGmail30Min();
            return mail;
        }

        public static MailObject GetGmailOtpGmail()
        {
            MailObject mail = ServerApi.GetGmailOtp();
            return mail;
        }

        public static MailObject GetGmailSuperTeam()
        {
            MailObject mail = ServerApi.GetGmailSuperTeam();
            return mail;
        }

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
                    string code = ServerApi.getCodeTempmailLol(token);
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

        public static string GetOtpMailOtp(int orderId)
        {
            string otp = Constant.FAIL;


            try
            {
                for (int i = 0; i < 30; i++)
                {
                    string code = ServerApi.GetCodeMailOtp(orderId);
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
                    string code = ServerApi.GetOtpGmailOtp(inMail.orderId);
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
                    string code = ServerApi.GetOtpGmailSuperTeam(inMail.email);
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
                    string code = ServerApi.GetOtpGmail48h(inMail.orderId);
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
                string otp = ServerApi.GetOtpSellGmail(mail.email);
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
                string otp = ServerApi.GetGmailDichVuGmailOtp(mail.token);
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
                string otp = ServerApi.GetGmailDichVuGmailOtp(mail.token, "eY33ElMh9EHWJmImm14D50V74ryl56M0");
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

        public static MailObject GetTrustMailVandong()
        {
            MailObject mailObj = new MailObject();
            ResponseDongVanObject responseData = GetHotMailDongVanApi(1, 5);
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
                        mail = ServerApi.GetHotmailLocalCache(server, type);
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
                                mail = ServerApi.GetHotmailLocalCache(server, type2);
                            }
                            if (mail == null)
                            {
                                if (!getHotmailKieuMoi)
                                {
                                    mail = ServerApi.GetMailServer(type3);
                                }
                                else
                                {
                                    mail = ServerApi.GetHotmailLocalCache(server, type3);
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
                                mail = ServerApi.GetHotmailLocalCache(server, type2);
                            }
                            if (mail == null)
                            {
                                if (!getHotmailKieuMoi)
                                {
                                    mail = ServerApi.GetMailServer(type3);
                                }
                                else
                                {
                                    mail = ServerApi.GetHotmailLocalCache(server, type3);
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
                                mail = ServerApi.GetHotmailLocalCache(server, type2);
                            }
                            if (mail == null)
                            {
                                if (!getHotmailKieuMoi)
                                {
                                    mail = ServerApi.GetMailServer(type3);
                                }
                                else
                                {
                                    mail = ServerApi.GetHotmailLocalCache(server, type3);
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

        public static MailObject GetTempmail(string duoiMail, string tempmailType, string server)
        {
            MailObject dd = new MailObject();

            if (tempmailType == Constant.TEMP_GENERATOR_EMAIL)
            {
                dd = CreateTempMail("", duoiMail);
            }
            else if (tempmailType == Constant.TEMP_GENERATOR_1_SEC_EMAIL)
            {
                dd = Get1SecMail();
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
                dd = GetGmailSuperGmail(server);
            }
            else if (tempmailType == Constant.GMAIL_48h)
            {
                dd = GetMail48hGmail();
            }

            return dd;
        }
    }
}
