using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fb_reg
{
    public class NamServer
    {
        public static bool PostData(Account acc, OrderObject order, DeviceObject device)
        {
            try
            {
                
                var client = new RestClient("https://reclub.vn/helloadm-fbc/");

                var request = new RestRequest("create-tk");
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("note", acc.note);
                request.AddParameter("data", acc.data);
                request.AddParameter("uid", acc.uid);
                request.AddParameter("pass", acc.pass);
                request.AddParameter("qrcode", acc.qrCode);
                request.AddParameter("email", order.currentMail.email);
                request.AddParameter("email_pass", order.currentMail.password);
                request.AddParameter("gender", acc.gender);
                request.AddParameter("birthday", acc.birthday);
                request.AddParameter("language", acc.language);
                request.AddParameter("email_type", order.currentMail.type);
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
                request.AddParameter("proxy", order.proxy.toString());
                request.AddParameter("phone-reg",order.phoneReg);
                request.AddParameter("phone-reg-type", order.phoneRegType);
                request.AddParameter("version-fb", order.versionFb);
                //request.AddParameter("source", order.source);

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
    }
}
