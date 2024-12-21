using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fb_reg
{
    public class Account
    {
        public string emailType;
        public string deviceID;
        public string cookie;
        public string status;
        public int index;
        public string data;
        public string qrCode;
        public string language;
        public string message;
        public string token;
        public string birthday;
        public bool verified;
        public string note { get; set; }
        
        public string uid { get; set; }
      
        public string pass { get; set; }
       
        public string email { get; set; }
        
        public string emailPass { get; set; }
       
        public string gender { get; set; }
        
        public bool hasAvatar { get; set; }
       
        public bool has2fa { get; set; }
       
        public string pcName { get; set; }
       
        public string createdAt { get; set; }
        
        public string updatedAt { get; set; }
        public string type { get; set; }
    }
}
