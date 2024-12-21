using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fb_reg
{
    public class PhoneTextNow
    {
        public string domain;
        public string phone;
        public string source;
        public string requestId;
        public string cookie;
        public string message;
        public bool isDirect;
        public long createdAt;
        public PhoneTextNow()
        {
            domain = "";
            phone = "";
            source = "";
            requestId = "";
            cookie = "";
            message = "";
            isDirect = false;
        }
    }
}
