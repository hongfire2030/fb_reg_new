using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace fb_reg
{
    public class NamServer
    {
        public static string NAM_SERVER_URL = "https://reclub.vn/helloadm-fbc/";

        static string ComputeHMACSHA256(string key, string message)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public static bool UploadFileAuth(string path, string uid)
        {
            try
            {
                string uploadKey = "dladsow2leodlweoe0kdldgigiogkhljfdnjkcjklneriuerkjhfd;asd;lasddlalasd"; // Thay thế bằng giá trị thực tế
                string fileKey = uid;     // Thay thế bằng giá trị thực tế

                string hashKey = ComputeHMACSHA256(uploadKey, fileKey);
                Console.WriteLine(hashKey);

                var client = new RestClient(NAM_SERVER_URL);

                var request = new RestRequest("upload");
                request.AddParameter("sign", hashKey);
                request.AddParameter("key", uid);
                request.AddFile("file", path);
                var response = client.Post(request);
                var resString = response.Content; // Raw content as string
                if (!string.IsNullOrEmpty(resString))
                {
                    return true;
                }
            }
            catch (xNet.HttpException ex)
            {
                Console.WriteLine(ex.HttpStatusCode);
                return false;
            }


            return false;
        }

        public static bool GetBackupAcc(string uid)
        {
            try
            {
                var client = new RestClient(NAM_SERVER_URL);

                var request = new RestRequest("download/" + uid);

                byte[] response = client.DownloadData(request);
                if (response != null && response.Length > 0)
                {
                    File.WriteAllBytes("Authentication/" + uid + ".zip", response);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (xNet.HttpException ex)
            {
                Console.WriteLine("ServerApi get Backup acc" + ex.HttpStatusCode);
                return false;
            }
        }
        public static bool PostData(Account acc, OrderObject order, DeviceObject device)
        {
            try
            {
                
                var client = new RestClient(NAM_SERVER_URL);

                var request = new RestRequest("create-tk");
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("note", acc.note);
                request.AddParameter("data", acc.data);
                request.AddParameter("uid", acc.uid);
                request.AddParameter("pass", acc.pass);
                request.AddParameter("qrcode", acc.qrCode);
                string email = "";
                string emailpass = "";
                string emailType = "";

                if(order.currentMail != null)
                {
                    email = order.currentMail.email;
                    emailpass = order.currentMail.password;
                    emailType = order.currentMail.type;
                }
                request.AddParameter("email", email);
                request.AddParameter("email_pass", emailpass);
                request.AddParameter("gender", acc.gender);
                request.AddParameter("birthday", acc.birthday);
                request.AddParameter("language", acc.language);
                request.AddParameter("email_type", emailType);
                request.AddParameter("pc_name", acc.pcName);
                request.AddParameter("device_id", device.deviceId);
                request.AddParameter("friends", "friends");
                request.AddParameter("name", "");
                request.AddParameter("status", "status");
                request.AddParameter("type", "type");
                request.AddParameter("has_avatar", acc.hasAvatar);
                request.AddParameter("has2fa", acc.has2fa);
                request.AddParameter("verified", acc.verified);
                request.AddParameter("ip", device.currentPublicIp);
                if (order.proxy != null)
                {
                    request.AddParameter("proxy", order.proxy.toString());
                }
                
                request.AddParameter("phone-reg",order.phoneReg);
                request.AddParameter("phone-reg-type", order.phoneRegType);
                request.AddParameter("version-fb", order.versionFb);
                request.AddParameter("source", order.source);

                request.AddParameter("sign", "e0b8a8463991547ef97936981c354fd5");
                var response = client.Post(request);
                var resString = response.Content; // Raw content as string


                if (resString == "true")
                {
                    return true;
                }
            }
            catch (xNet.HttpException ex)
            {
                Console.WriteLine(ex.HttpStatusCode);
                return false;
            }


            return false;
        }

        public static bool LogProxy(string pcName, string deviceId, string ip, string proxy, bool top)
        {
            try
            {

                var client = new RestClient(NAM_SERVER_URL);

                var request = new RestRequest("log-proxy");
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("pc_name", pcName);
                request.AddParameter("device_id", deviceId);
                request.AddParameter("ip", ip);
                request.AddParameter("proxy", proxy);
                request.AddParameter("top", top);

                request.AddParameter("sign", "e0b8a8463991547ef97936981c354fd5");
                var response = client.Post(request);
                var resString = response.Content; // Raw content as string


                if (resString == "true")
                {
                    return true;
                }
            }
            catch (xNet.HttpException ex)
            {
                Console.WriteLine(ex.HttpStatusCode);
                return false;
            }


            return false;
        }


        public class TopProxyData
        {
            [JsonProperty("code")]
            public int code { get; set; }

            [JsonProperty("list")]
            public TopProxyItem[] list { get; set; }


        }

        public class TopProxyItem
        {

            [JsonProperty("city")]
            public string city { get; set; }

            [JsonProperty("country")]
            public string country { get; set; }
            [JsonProperty("counter")]
            public string counter { get; set; }
        }
        public static List<string> TopProxy()
        {
            List<string> result = new List<string>();
            try
            {

                var client = new RestClient(NAM_SERVER_URL);

                var request = new RestRequest("top-proxy");
                
                request.AddParameter("sign", "e0b8a8463991547ef97936981c354fd5");
                var response = client.Get(request);
                var resString = response.Content; // Raw content as string

                try
                {
                    TopProxyData data = JsonConvert.DeserializeObject<TopProxyData>(resString);
                    Console.WriteLine(resString);
                    for (int i = 0; i < data.list.Length; i ++)
                    {
                        result.Add(data.list[i].country + "|" + data.list[i].city);
                    }
                }
                catch (Exception error)
                {
                    // Log
                }
            }
            catch (xNet.HttpException ex)
            {
                Console.WriteLine(ex.HttpStatusCode);
                return result;
            }


            return result;
        }

    }
}
