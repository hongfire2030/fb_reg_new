using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fb_reg.Model
{
    public class LogEntryDevice
    {
        public string Pcname { get; set; }
        public string DeviceId { get; set; }
        public string Status { get; set; }
        public string ProxyIp { get; set; }
        public string Mail { get; set; }
        public string Step { get; set; }
        public string AndroidVersion { get; set; }
        public DateTime Time { get; set; }

    }
}
