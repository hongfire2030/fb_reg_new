using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace fb_reg.Model
{
    public class Setting
    {
        public int stop = 0;
        public string info;
        public int speed;
        public Double rate;
        public string recentRate;
        public int mailCacheCount;
        public int totalAcc;
        public double rLong = 0;
        public double rShort = 0;
        public double spike = 0;
        [JsonExtensionData]
        Dictionary<string, JToken> RecentStatsByPc;
        public string ToString()
        {
            string temp = stop + "|Total:" + totalAcc + "| Speed:" + speed + "| Tỉ lệ:" + rate + "|rLong:" + (rLong * 100) + "|rShort:" + (rShort * 100) + "|spike:" + (spike * 100) + "|" + recentRate + "| Mail:" + mailCacheCount;
            return temp.Replace("\"", "").Replace("{", "").Replace("}","");
        }
    }
}
