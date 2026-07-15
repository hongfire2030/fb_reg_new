using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace fb_reg.Utilities
{
    public static class LokiLogger
    {
        private static readonly ConcurrentQueue<LokiLogItem> Queue =
            new ConcurrentQueue<LokiLogItem>();

        private static readonly Timer Timer;
        private static int _isSending = 0;

        private static readonly string LokiUrl =
            "http://148.113.207.13:3100/loki/api/v1/push";

        static LokiLogger()
        {
            Timer = new Timer(SendBatch, null, 1000, 2000);
        }

        public static void Log(
            string status,
            string deviceId,
            string country = "",
            string email = "",
            string proxy = "",
            string message = "")
        {
            string fullMess = message;
            try
            {
                fullMess = LogSession.GetLogKey(deviceId) + "-" + Environment.MachineName + "-" + message;
            } catch (Exception ex)
            {

            }
            Queue.Enqueue(new LokiLogItem
            {
                Status = status,
                DeviceId = deviceId,
                Country = country,
                Email = email,
                Proxy = proxy,
                Message = fullMess,
                Time = DateTime.Now
            });
        }

        private static void SendBatch(object state)
        {
            if (Interlocked.Exchange(ref _isSending, 1) == 1)
                return;

            try
            {
                int maxBatch = 100;

                var grouped = new Dictionary<string, List<string[]>>();

                while (grouped.Count < maxBatch &&
                       Queue.TryDequeue(out var item))
                {
                    string status = string.IsNullOrWhiteSpace(item.Status)
                        ? "UNKNOWN"
                        : item.Status;

                    string key = status;

                    if (!grouped.ContainsKey(key))
                        grouped[key] = new List<string[]>();

                    //var ts= DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    var now =
                        DateTimeOffset.Now;

                    long lokiTimestampNs =
                        DateTimeOffset.UtcNow
                            .ToUnixTimeMilliseconds()
                            * 1000000L;
                    var body = new
                    {
                        deviceId = item.DeviceId,
                        country = item.Country,
                        email = item.Email,
                        proxy = item.Proxy,
                        message = item.Message,
                        // dùng để sort trong Grafana
                        ts = now.ToUnixTimeMilliseconds(),
                        time = item.Time.ToString("yyyy-MM-dd HH:mm:ss")
                    };

                    grouped[key].Add(new[]
                    {
                    lokiTimestampNs.ToString(),
                    JsonConvert.SerializeObject(body)
                });
                }

                if (grouped.Count == 0)
                    return;

                var streams = new List<object>();

                foreach (var group in grouped)
                {
                    streams.Add(new
                    {
                        stream = new
                        {
                            app = "fbtool",
                            pc = Environment.MachineName,
                            status = group.Key
                        },
                        values = group.Value
                    });
                }

                var payload = new
                {
                    streams = streams
                };

                string json = JsonConvert.SerializeObject(payload);

                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(2);

                    var content = new StringContent(
                        json,
                        Encoding.UTF8,
                        "application/json");

                    client.PostAsync(LokiUrl, content)
                          .GetAwaiter()
                          .GetResult();
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"LokiLogger error: {ex.Message}");
                // Không để lỗi Loki làm ảnh hưởng tool chính
            }
            finally
            {
                Interlocked.Exchange(ref _isSending, 0);
            }
        }

        private class LokiLogItem
        {
            public string Status { get; set; }
            public string DeviceId { get; set; }
            public string Country { get; set; }
            public string Email { get; set; }
            public string Proxy { get; set; }
            public string Message { get; set; }
            public DateTime Time { get; set; }
        }
    }
}
