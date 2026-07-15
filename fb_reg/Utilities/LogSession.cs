using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fb_reg.Utilities
{
    public static class LogSession
    {
        private static readonly ConcurrentDictionary<string, DateTime> _sessions
        = new ConcurrentDictionary<string, DateTime>();

        /// <summary>
        /// Lấy session key cho device.
        /// Lần đầu sẽ tạo mới.
        /// Những lần sau sẽ trả về session cũ.
        /// </summary>
        public static string GetLogKey(string deviceId)
        {
            if (string.IsNullOrEmpty(deviceId))
            {
                return "unknown_device";
            }
            var startTime = _sessions.GetOrAdd(deviceId, _ => DateTime.Now);

            return $"{deviceId}_{startTime:yyyyMMdd_HH-mm-ss-fff}";
        }

        /// <summary>
        /// Bắt đầu session mới cho device.
        /// </summary>
        public static string RefreshLogKey(string deviceId)
        {
            var startTime = DateTime.Now;

            _sessions.AddOrUpdate(
                deviceId,
                startTime,
                (_, __) => startTime);

            return $"{deviceId}_{startTime:yyyyMMdd_HH-mm-ss-fff}";
        }

        /// <summary>
        /// Kết thúc session.
        /// </summary>
        public static void Remove(string deviceId)
        {
            _sessions.TryRemove(deviceId, out _);
        }

        /// <summary>
        /// Lấy startTime hiện tại.
        /// </summary>
        public static bool TryGetStartTime(string deviceId, out DateTime startTime)
        {
            return _sessions.TryGetValue(deviceId, out startTime);
        }
    }
}
