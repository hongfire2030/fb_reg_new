using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fb_reg
{
    public class MailObject
    {
        public string mailWeb;
        public string email;
        public string password;
        public string refreshToken;
        public string accessToken;
        public string clientId;
        public string passwordTemp;
        public string status;
        public string type;
        public string token;
        public int orderId;
        public string message;
        public string source;
        public bool otpVandong;
        public MailRepository mailRepository;
        public string toString()
        {
            return email + "|" + password;
        }
        public MailObject()
        {
            email = "";
            password = "";
            status = "";
            refreshToken = "";
            clientId = "";
            string mailWeb = "";
            otpVandong = false;
        }
    }
}
