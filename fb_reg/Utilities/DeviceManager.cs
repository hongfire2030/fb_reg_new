using ActiveUp.Net.Security.OpenPGP.Packets;
using fb_reg.RequestApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fb_reg.Utilities
{
    public static class DeviceManager
    {
        public static string GetRealDeviceId(string deviceS)
        {
            if (!string.IsNullOrEmpty(deviceS))
            {
                return deviceS.Replace(Constant.ADB_DEVICE_OFFLINE, "").Replace(Constant.ADB_DEVICE_RECOVERY, "").Replace(Constant.ADB_DEVICE_DISCONNECT, "").Replace(Constant.ADB_DEVICE_RESTART, "");
            }
            else
            {
                return deviceS;
            }
        }

        public static bool CompareDevceId(string deviceID1, string deviceID2)
        {
            if (GetRealDeviceId(deviceID1) == GetRealDeviceId(deviceID2))
            {
                return true;
            }
            return false;
        }
        public static string GetAdbStatusDevice(string deviceID)
        {
            if (deviceID.Contains(Constant.ADB_DEVICE_OFFLINE))
            {
                return Constant.ADB_DEVICE_OFFLINE;
            }
            if (deviceID.Contains(Constant.ADB_DEVICE_RECOVERY))
            {
                return Constant.ADB_DEVICE_RECOVERY;
            }
            return Constant.ADB_DEVICE_NORMAL;
        }
        public static DeviceObject GetDevice(string deviceID)
        {
            foreach (DeviceObject device in PublicData.listDeviceObject)
            {
                if (CompareDevceId(device.deviceId, deviceID))
                {
                    return device;
                }
            }
            return null;
        }
    }
}
