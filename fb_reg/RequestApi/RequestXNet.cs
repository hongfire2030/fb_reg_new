using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using xNet;

namespace fb_reg
{
    class RequestXNet
    {
		public RequestXNet(string cookie = "", string userAgent = "", string proxy = "", int typeProxy = 0)
		{
			bool flag = userAgent == "";
			if (flag)
			{
				userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.131 Safari/537.36";
			}
			this.request = new xNet.HttpRequest
			{
				KeepAlive = true,
				AllowAutoRedirect = true,
				Cookies = new CookieDictionary(false),
				UserAgent = userAgent
			};
			this.request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
			this.request.AddHeader("Accept-Language", "en-US,en;q=0.9");
			bool flag2 = cookie != "";
			if (flag2)
			{
				this.AddCookie(cookie);
			}
			bool flag3 = proxy != "";
			if (flag3)
			{
				switch (proxy.Split(new char[]
				{
					':'
				}).Count<string>())
				{
					case 1:
						this.request.Proxy = Socks5ProxyClient.Parse("127.0.0.1:" + proxy);
						break;
					case 2:
						{
							bool flag4 = typeProxy == 0;
							if (flag4)
							{
								this.request.Proxy = HttpProxyClient.Parse(proxy);
							}
							else
							{
								this.request.Proxy = Socks5ProxyClient.Parse(proxy);
							}
							break;
						}
					case 4:
						{
							bool flag5 = typeProxy == 0;
							if (flag5)
							{
								this.request.Proxy = new HttpProxyClient(proxy.Split(new char[]
								{
							':'
								})[0], Convert.ToInt32(proxy.Split(new char[]
								{
							':'
								})[1]), proxy.Split(new char[]
								{
							':'
								})[2], proxy.Split(new char[]
								{
							':'
								})[3]);
							}
							else
							{
								this.request.Proxy = new Socks5ProxyClient(proxy.Split(new char[]
								{
							':'
								})[0], Convert.ToInt32(proxy.Split(new char[]
								{
							':'
								})[1]), proxy.Split(new char[]
								{
							':'
								})[2], proxy.Split(new char[]
								{
							':'
								})[3]);
							}
							break;
						}
				}
			}
		}
		public string RequestGet(string url)
		{
			return this.request.Get(url, null).ToString();
		}
		public byte[] GetBytes(string url)
		{
			return this.request.Get(url, null).ToBytes();
		}
		public string RequestPost(string url, string data = "", string contentType = "application/x-www-form-urlencoded")
		{
			bool flag = data == "" || contentType == "";
			string result;
			if (flag)
			{
				result = this.request.Post(url).ToString();
			}
			else
			{
				result = this.request.Post(url, data, contentType).ToString();
			}
			return result;
		}
		public void AddCookie(string cookie)
		{
			string[] array = cookie.Split(new char[]
			{
				';'
			});
			foreach (string text in array)
			{
				string[] array3 = text.Split(new char[]
				{
					'='
				});
				bool flag = array3.Count<string>() > 1;
				if (flag)
				{
					try
					{
						this.request.Cookies.Add(array3[0], array3[1]);
					}
					catch
					{
					}
				}
			}
		}
		public string GetCookie()
		{
			return this.request.Cookies.ToString();
		}
		private xNet.HttpRequest request;
	}
}
