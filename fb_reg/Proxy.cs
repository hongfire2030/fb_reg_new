﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fb_reg
{
    public class Proxy
    {
        public string proxyId;
        public bool hasProxy = false;
        public string proxyDomain;
        public string host;
        public string port;
        public string username;
        public string pass;
        public int timeout;
        public bool isWait;
        public string message;
        public string toString()
        {
            return host + ":" + port;
        }
    }
}
