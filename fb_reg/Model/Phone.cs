
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using static fb_reg.OutsideServer;
using static fb_reg.ServerApi;

namespace fb_reg
{
    public class Phone
    {
        public static string DRK_DOMAIN = "";
        public static string DRK_KEY = "d383c5f02d834518f0cc7f132e8764f902c87357";
        public static string CODE_TEXT_NOW_KEY = "3a6ef99e17b19d3356537e22e86bb79b";
        public static string OTPMMO_KEY = "ZVND5AMQH4CWUT9O1632202376";
        public static PhoneTextNow GetRandomPhoneTextnow()
        {
            PhoneTextNow phoneT = new PhoneTextNow();
            Random ran = new Random();
            int rr = ran.Next(1, 100);
            if (rr < 50)
            {
                phoneT = GetPhoneOtpmmo();
            }
            else
            {
                phoneT = GetPhoneCodeTextNow(phoneT);
            }
            return phoneT;
        }
        public static PhoneTextNow GetPhoneOtpmmo()
        {
            try
            {
                PhoneTextNow phoneT = new PhoneTextNow();
                string apiGetHotMail = string.Format("https://otpmmo.online/textnow/api.php?apikey={0}&&type=getphone&qty=1", OTPMMO_KEY);
                var request = (HttpWebRequest)WebRequest.Create(apiGetHotMail);
                request.Method = "GET";
                request.Accept = "application/json";
                request.ContentType = "application/json; charset=utf-8";

                var response = (HttpWebResponse)request.GetResponse();
                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd().ToString();

                phoneT.message = Utility.Decode_UTF8(responseString);
                phoneT.phone = responseString;
                phoneT.requestId = "";
                phoneT.source = Constant.OTP_MMO_TEXTNOW;

                return phoneT;
            } catch (Exception)
            {
                return null;
            }
            
        }
        public static PhoneTextNow TryToGetPhone(OrderObject order, int time)
        {
            for (int i = 0; i < time; i++)
            {
                order.phoneT = Phone.GetPhoneTextNow(order.phoneT);

                if (Utility.IsDigitsOnly(order.phoneT.phone))
                {
                    return order.phoneT;
                }
                Thread.Sleep(3000);
            }
            return new PhoneTextNow();
        }
        public static PhoneTextNow GetPhoneTextNow(PhoneTextNow phoneT)
        {
            if (phoneT.source == Constant.CODE_TEXTNOW)
            {
                phoneT = GetPhoneCodeTextNow(phoneT);
            }
            else if (phoneT.source == Constant.OTP_MMO_TEXTNOW)
            {
                phoneT = GetPhoneOtpmmo();
            } else if (phoneT.source == Constant.DRK_TEXTNOW)
            {
                phoneT = GetPhoneDrk(phoneT);
            }
            else
            {
                phoneT = GetRandomPhoneTextnow();
            }
            return phoneT;
        }

        public static string GetConfirmCodeTextNow(PhoneTextNow phoneT)
        {
            string code = "";
            if (phoneT.source == Constant.OTP_MMO_TEXTNOW)
            {
                code = GetConfirmCodeOtpMMO(phoneT);
            }
            else if (phoneT.source == Constant.CODE_TEXTNOW)
            {
                code = GetConfirmCodeCodeTextNow(phoneT);
            } else if (phoneT.source == Constant.DRK_TEXTNOW)
            {
                code = GetConfirmCodeDrkTextNow(phoneT);
            }
            return code;
        }
        public static string GetConfirmCodeOtpMMO(PhoneTextNow phoneT)
        {
            try
            {
                string apiGetHotMail = string.Format("https://otpmmo.online/textnow/api.php?apikey={0}&&type=getotp&sdt={1}",
                OTPMMO_KEY, phoneT.phone);
                var request = (HttpWebRequest)WebRequest.Create(apiGetHotMail);
                request.Method = "GET";
                request.Accept = "application/json";
                request.ContentType = "application/json; charset=utf-8";

                var response = (HttpWebResponse)request.GetResponse();
                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd().ToString();

                string temp = Regex.Match(responseString, "\"otp\":\" [0-9]{0,}").ToString();
                return temp.Replace("\"otp\":\" ", "");
            } catch (Exception ex)
            {
                return "";
            }
            
        }

        public static PhoneTextNow GetPhoneCodeTextNow(PhoneTextNow phoneT)
        {
            try
            {
                string apiGetHotMail = string.Format("http://codetextnow.com/api.php?apikey={0}&action=create-request&serviceId=1&count=1",
                CODE_TEXT_NOW_KEY);
                var request = (HttpWebRequest)WebRequest.Create(apiGetHotMail);
                request.Method = "GET";
                request.Accept = "application/json";
                request.ContentType = "application/json; charset=utf-8";

                var response = (HttpWebResponse)request.GetResponse();
                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd().ToString();
                Console.WriteLine("-----------phone:" + responseString);
                Utility.WriteFileLog(responseString, "phone_code_textnow.txt");
                phoneT.message = Utility.Decode_UTF8(responseString);
                //string responseString = ServerApi.GetPhoneCodeTextNow(phoneT.cookie);
                if (!responseString.Contains("sdt") && !responseString.Contains("sdt") || responseString.Contains("vui l"))
                {
                    // Try get phone by cookie
                    phoneT.phone = "";
                    return phoneT;
                }
                if (Regex.Match(responseString, "\"sdt\":\"(.*?)\"").Groups.Count > 0)
                {
                    phoneT.phone = Regex.Match(responseString, "\"sdt\":\"(.*?)\"").Groups[1].ToString();
                }
                if (Regex.Match(responseString, "\"requestId\":(.*?)}").Groups.Count > 0)
                {
                    phoneT.requestId = Regex.Match(responseString, "\"requestId\":(.*?)}").Groups[1].ToString();
                }

                phoneT.source = Constant.CODE_TEXTNOW;
            } catch (Exception ex)
            {
                phoneT.message = ex.Message;
            }
            
            return phoneT;
        }

        public static PhoneTextNow GetPhoneDrk(PhoneTextNow phoneT)
        {
            try
            {
                if (DRK_DOMAIN.EndsWith("/"))
                {
                    DRK_DOMAIN = DRK_DOMAIN.Substring(0, DRK_DOMAIN.Length - 1);
                }
                string apiGetHotMail = string.Format(DRK_DOMAIN +  "/api/v1/get-phonenumber?token={0}&platformId=1",
                DRK_KEY);
                var request = (HttpWebRequest)WebRequest.Create(apiGetHotMail);
                request.Method = "GET";
                request.Accept = "application/json";
                request.ContentType = "application/json; charset=utf-8";

                var response = (HttpWebResponse)request.GetResponse();
                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd().ToString();
                Console.WriteLine("-----------phone:" + responseString);
                Utility.WriteFileLog(responseString, "phone_code_textnow.txt");
                phoneT.message = Utility.Decode_UTF8(responseString);
                
                if (!responseString.Contains("phone_number") && !responseString.Contains("phone_number"))
                {
                    // Try get phone by cookie
                    phoneT.phone = "";
                    return phoneT;
                }
                if (Regex.Match(responseString, "\"phone_number\":\"(.*?)\"").Groups.Count > 0)
                {
                    phoneT.phone = Regex.Match(responseString, "\"phone_number\":\"(.*?)\"").Groups[1].ToString();
                }
                if (Regex.Match(responseString, "\"request_id\":(.*?)}").Groups.Count > 0)
                {
                    phoneT.requestId = Regex.Match(responseString, "\"request_id\":(.*?)}").Groups[1].ToString();
                }

                if (!string.IsNullOrEmpty(phoneT.phone))
                {
                    phoneT.phone = phoneT.phone.Replace("+1", "").Replace("(", "").Replace(")", "").Replace("-","").Trim();
                }
                phoneT.source = Constant.DRK_TEXTNOW;
                
            } catch (Exception ex)
            {
                phoneT.message = ex.Message;
            }
            return phoneT;
        }

        public static List<PhoneTextNow> GetListPhoneCodeTextNow(int count = 1)
        {
            List<PhoneTextNow> listPhone = new List<PhoneTextNow>();
            string apiGetHotMail = string.Format("http://codetextnow.com/api.php?apikey={0}&action=create-request&serviceId=1&count={1}",
                CODE_TEXT_NOW_KEY, count);
            var request = (HttpWebRequest)WebRequest.Create(apiGetHotMail);
            request.Method = "GET";
            request.Accept = "application/json";
            request.ContentType = "application/json; charset=utf-8";

            var response = (HttpWebResponse)request.GetResponse();
            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd().ToString();
            try
            {
                ResponsePhoneTextNow data = JsonConvert.DeserializeObject<ResponsePhoneTextNow>(responseString);
                Console.WriteLine("-----------phone:" + responseString);

                if (data!= null && data.data != null && data.data.Length > 0)
                {
                    for (int i = 0; i < data.data.Length; i ++)
                    {
                        PhoneTextNow phoneT = new PhoneTextNow();
                        phoneT.message = Utility.Decode_UTF8(responseString);
                        phoneT.phone = data.data[i].sdt;
                        phoneT.createdAt = data.data[i].created_time;
                        phoneT.requestId = Convert.ToString(data.data[i].requestId);
                        phoneT.source = Constant.CODE_TEXTNOW;
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return listPhone;
        }
        public static string GetConfirmCodeCodeTextNow(PhoneTextNow phoneT)
        {
            try
            {
                string apiGetHotMail = string.Format("http://codetextnow.com/api.php?apikey={0}&action=data-request&requestId={1}",
                CODE_TEXT_NOW_KEY, phoneT.requestId);
                var request = (HttpWebRequest)WebRequest.Create(apiGetHotMail);
                request.Method = "GET";
                request.Accept = "application/json";
                request.ContentType = "application/json; charset=utf-8";

                var response = (HttpWebResponse)request.GetResponse();
                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd().ToString();
                if (!responseString.Contains("otp"))
                {
                    return "";
                }
                return Regex.Match(responseString, "\"otp\":\"(.*?)\"").Groups[1].ToString();
            } catch(Exception ex)
            {
                return "";
            }
            
            //return ServerApi.GetCodeFromCodeTextNow(phoneT.phone, phoneT.cookie);
        }

        public static string GetConfirmCodeDrkTextNow(PhoneTextNow phoneT)
        {
            try
            {
                string apiGetHotMail = string.Format(DRK_DOMAIN + "/api/v1/get-message?requestId={1}&token={2}",
                    phoneT.domain,
                phoneT.requestId, DRK_KEY);
                var request = (HttpWebRequest)WebRequest.Create(apiGetHotMail);
                request.Method = "GET";
                request.Accept = "application/json";
                request.ContentType = "application/json; charset=utf-8";

                var response = (HttpWebResponse)request.GetResponse();
                
                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd().ToString();
                Console.WriteLine(responseString);
                if (!responseString.Contains("otp"))
                {
                    return "";
                }
                return Regex.Match(responseString, "\"otp\":\"(.*?)\"").Groups[1].ToString();
            }
            catch (Exception ex) {

                return "";
            }
            
            //return ServerApi.GetCodeFromCodeTextNow(phoneT.phone, phoneT.cookie);
        }
    }
}
