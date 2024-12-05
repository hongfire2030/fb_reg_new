using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace fb_reg
{
    public class OperationHandler
    {
        private MyOperation _operation;
        private Timer _timer;

        public OperationHandler(MyOperation operation)
        {
            _operation = operation;
        }

        public void StartWithTimeout(int timeoutMillis, bool isServer, OrderObject order, string deviceID, string password, string Hotmail, string qrCode,
            string gender, int yearOld, string isVerified, string status = "checking", bool isLite = false)
        {
            if (_timer != null)
                return;

            _timer = new Timer(
                state =>
                {
                    _operation.DoOperation( isServer,  order,  deviceID,  password,  Hotmail,  qrCode,
             gender,  yearOld,  isVerified,  status ,  isLite);
                    DisposeOfTimer();
                }, null, timeoutMillis, timeoutMillis);
        }

        //public string StartWithTimeoutIP(int timeoutMillis, string deviceID)
        //{
        //    string ip = "";
        //    if (_timer != null)
        //        return "";

        //    _timer = new Timer(
        //        state =>
        //        {
        //            ip = _operation.DoOperationIP(deviceID);
        //            DisposeOfTimer();
        //        }, null, timeoutMillis, timeoutMillis);
        //    return ip;
        //}

        public void StopOperationIfNotStartedYet()
        {
            DisposeOfTimer();
        }

        private void DisposeOfTimer()
        {
            if (_timer == null)
                return;
            var temp = _timer;
            _timer = null;
            temp.Dispose();
        }
    }
    
}
