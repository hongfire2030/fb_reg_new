using fb_reg.RequestApi;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace fb_reg.Utilities.FetchMail
{
    public class MailDispatcher
    {
        private readonly ConcurrentQueue<MailObject> _queue;
        
        private readonly HttpClient _client;

        public MailDispatcher(ConcurrentQueue<MailObject> queue)
        {
            _queue = queue;
            
            _client = new HttpClient();

            var thread = new Thread(SenderLoop) { IsBackground = true };
            thread.Start();
        }

        private void SenderLoop()
        {
            while (true)
            {
                if (!_queue.TryDequeue(out MailObject mail))
                {
                    Thread.Sleep(200);
                    continue;
                }

                try
                {
                    FetchController.SetState(FetchState.WaitingServer);
                    MailObject resp = CacheServer.AddMailServerCache(mail, PublicData.ServerCacheMail);
                    if (resp != null)
                    {
                        int cacheMail = resp.mailCount;
                        if (cacheMail > PublicData.maxMail)
                        {
                            FetchController.Pause();
                        }
                        else
                        {
                            FetchController.Resume();
                        }
                    }
                    else
                    {
                        FetchController.SetState(FetchState.ServerError);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ Server ERROR: " + ex.Message);
                    //File.AppendAllText("status.log", $"[ERROR] {DateTime.Now}: {ex.Message}\n");
                    FetchController.SetState(FetchState.ServerError);
                }

                Thread.Sleep(100);
            }
        }
    }
}
