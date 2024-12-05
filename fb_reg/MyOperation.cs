using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace fb_reg
{
    public class MyOperation 
    {
        public void DoOperation(bool isServer, OrderObject order, string deviceID, string password, string Hotmail, string qrCode,
            string gender, int yearOld, string isVerified, string status = "checking", bool isLite = false)
        {
            Console.WriteLine("Operation started");

            var watch = System.Diagnostics.Stopwatch.StartNew();
            // string Cookies = QLong.Phone.Lấy_Cooki(deviceID);
            string Cookies = "";
            string uid = "";

            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(300);
                if (isLite)
                {
                    Cookies = FbUtil.GetCookieFromFbLite(deviceID);
                }
                else
                {
                    Cookies = FbUtil.GetCookieFromPhone(deviceID);
                }
                uid = Regex.Match(Cookies, "c_user=[0-9]{0,}").ToString();
                if (!string.IsNullOrEmpty(uid))
                {
                    break;
                }
            }
            if (string.IsNullOrEmpty(uid))
            {
                return ;
            }
            uid = uid.Replace("c_user=", "");
            if (string.IsNullOrEmpty(uid))
            {
                return ;
            }
            if (string.IsNullOrEmpty(Hotmail) || Hotmail == Constant.FAIL)
            {
                Hotmail = Constant.FAIL + "|" + Constant.FAIL;
            }

            string rawData = uid + "|" + password
                + "|" + Cookies + "|" + qrCode + "|" + Hotmail + "|" + gender + "|" + yearOld;

            string hasAvatar;
            string has2Fa;

            string emailType = "";

            string fileName = "FileCLone/";
            if (Utility.CheckAvatarFromUid( uid, "") == "true")
            {
                fileName = fileName + "avatar";
                hasAvatar = Constant.TRUE;
            }
            else
            {
                fileName = fileName + "NoAvatar";
                hasAvatar = Constant.FALSE;
            }
            
            fileName = fileName + "_" + order.language;
               
            if (!string.IsNullOrWhiteSpace(qrCode))
            {
                fileName = fileName + "_2fa";
                has2Fa = Constant.TRUE;
            }
            else
            {
                fileName = fileName + "_no2fa";
                has2Fa = Constant.FALSE;
            }
            try
            {
                string mailPass = Hotmail.Split('|')[1];
                if (mailPass == "tempmail")
                {
                    fileName = fileName + "_tempmail";
                    emailType = Constant.TEMPMAIL;
                }
                else
                {
                    if (order.isHotmail)
                    {
                        fileName = fileName + "_hotmail";
                        emailType = Constant.HOTMAIL;
                    }
                    else
                    {
                        fileName = fileName + "_tempmail";
                        emailType = Constant.TEMPMAIL;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            if (isVerified == Constant.TRUE)
            {
                fileName = fileName + "_veri";
            }
            else
            {
                fileName = fileName + "_Noveri";
            }




            string data = rawData + "|" + hasAvatar + "|" + has2Fa
                + "|" + order.language + "|" + emailType + "|" + isVerified
                + "|" + Environment.MachineName + "|" + deviceID;

            if (isVerified == Constant.TRUE)
            {
                // Double check
                string mailPass = Hotmail.Split('|')[1];
                if (!string.IsNullOrEmpty(mailPass) && mailPass != "tempmail")
                {
                    // Check mail live
                    MailObject mail = new MailObject();
                    mail.email = Hotmail.Split('|')[0];
                    mail.password = mailPass;
                    MailObject check = Utility.CheckLiveHotmail(mail);
                    if (check.status == Constant.FAIL)
                    {
                        Console.WriteLine("mail die");
                        using (StreamWriter HDD = new StreamWriter(fileName + "_Mail_Die" + ".txt", true))
                        {
                            HDD.WriteLine(data);
                            HDD.Close();
                        }
                        return ;
                    }
                }
            }
            if (isServer)
            {
                if (!ServerApi.PostData(isServer, data, status))
                {
                    bool checkOk = GoogleSheet.WriteAccount(data, fileName.Substring(10));
                    if (!checkOk)
                    {
                        using (StreamWriter HDD = new StreamWriter(fileName + "_Missing" + ".txt", true))
                        {
                            HDD.WriteLine(data);
                            HDD.Close();
                        }
                    }
                }
            }


            using (StreamWriter HDD = new StreamWriter("local/" + fileName + ".txt", true))
            {
                HDD.WriteLine(data);
                HDD.Close();
            }

            Thread.Sleep(1000);
            //if (isVerified != Constant.TRUE)
            //{
            FbUtil.PullBackupFb(uid, deviceID);
            Thread.Sleep(2000);
            ZipFile.CreateFromDirectory("Authentication/" + uid, "Authentication/" + uid + ".zip");
            Thread.Sleep(2000);

            // push to server
            ServerApi.UploadAuthAcc("Authentication/" + uid + ".zip", uid);
            Thread.Sleep(2000);
            try
            {
                var dir = new DirectoryInfo("Authentication/" + uid);
                dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                dir.Delete(true);
                File.Delete("Authentication/" + uid + ".zip");
            }
            catch (IOException ex)
            {
                Console.WriteLine("ex:" + ex.Message);
            }
            //}

            watch.Stop();
            long second = watch.ElapsedMilliseconds / 1000;
            Console.WriteLine($"---------------------Store Time: {second} ms");
        }

        //public string DoOperationIP(string deviceID)
        //{
        //    Console.WriteLine("Operation started");
        //    var watch = System.Diagnostics.Stopwatch.StartNew();

        //    string ip = Utility.GetCurrentIpInfo(deviceID);
        //    watch.Stop();
        //    long second = watch.ElapsedMilliseconds / 1000;
        //    Console.WriteLine("currentip in thread-----" + ip);
        //    Console.WriteLine($"---------------------Store Time: {second} ms");
        //    return ip;
        //}
    }
}
