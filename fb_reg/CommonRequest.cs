using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;


namespace fb_reg
{
	internal class CommonRequest
	{
		public static string CheckLiveCookie(string cookie = "", string userAgent = "", string proxy = "", int typeProxy = 0)
		{
			string result = "0|0";
			string value = Regex.Match(cookie + ";", "c_user=(.*?);").Groups[1].Value;
			try
			{
				RequestXNet requestXNet = new RequestXNet(cookie, userAgent, proxy, typeProxy);
				bool flag = value != "";
				if (flag)
				{
					string text = requestXNet.RequestGet("https://www.facebook.com/me").ToString();
					bool flag2 = text.Contains("id=\"code_in_cliff\"") || text.Contains("name=\"new\"") || text.Contains("name=\"c\"") || text.Contains("changeemail");
					if (flag2)
					{
						result = "1|0";
					}
					else
					{
						bool flag3 = Regex.Match(text, "\"USER_ID\":\"(.*?)\"").Groups[1].Value.Trim() == value.Trim() && !text.Contains("checkpointSubmitButton") && !text.Contains("checkpointBottomBar") && !text.Contains("captcha_response");
						if (flag3)
						{
							result = "1|1";
						}
					}
				}
			}
			catch
			{
			}
			return result;
		}
		public static List<string> GetMyListUidNameFriend(string token, string userAgent, string proxy, int typeProxy)
		{
			List<string> list = new List<string>();
			try
			{
				RequestXNet requestXNet = new RequestXNet("", userAgent, proxy, typeProxy);
				string json = requestXNet.RequestGet("https://graph.facebook.com/me/friends?limit=5000&fields=id,name&access_token=" + token);
				JObject jobject = JObject.Parse(json);
				bool flag = jobject["data"].Count<JToken>() > 0;
				if (flag)
				{
					for (int i = 0; i < jobject["data"].Count<JToken>(); i++)
					{
						string str = jobject["data"][i]["id"].ToString();
						string str2 = jobject["data"][i]["name"].ToString();
						list.Add(str + "|" + str2);
					}
				}
			}
			catch
			{
			}
			return list;
		}
		public static List<string> GetListUidNameFriendOfUid(string token, string uid, string userAgent, string proxy, int typeProxy)
		{
			List<string> list = new List<string>();
			try
			{
				RequestXNet requestXNet = new RequestXNet("", userAgent, proxy, typeProxy);
				string json = requestXNet.RequestGet("https://graph.facebook.com/" + uid + "/friends?limit=5000&fields=id,name&access_token=" + token);
				JObject jobject = JObject.Parse(json);
				bool flag = jobject["data"].Count<JToken>() > 0;
				if (flag)
				{
					for (int i = 0; i < jobject["data"].Count<JToken>(); i++)
					{
						string str = jobject["data"][i]["id"].ToString();
						string str2 = jobject["data"][i]["name"].ToString();
						list.Add(str + "|" + str2);
					}
				}
			}
			catch
			{
			}
			return list;
		}
		public static List<string> BackupImageOne(string uidFr, string nameFr, string token, string userAgent, string proxy, int typeProxy, int countImage = 20)
		{
			List<string> list = new List<string>();
			try
			{
				RequestXNet requestXNet = new RequestXNet("", userAgent, proxy, typeProxy);
				string text = requestXNet.RequestGet(string.Concat(new string[]
				{
					"https://graph.facebook.com/",
					uidFr,
					"/photos?fields=images&limit=",
					countImage.ToString(),
					"&access_token=",
					token
				}));
				JObject jobject = JObject.Parse(text);
				bool flag = jobject != null && text.Contains("images");
				if (flag)
				{
					for (int i = 0; i < jobject["data"].Count<JToken>(); i++)
					{
						int num = jobject["data"][i]["images"].ToList<JToken>().Count - 1;
						List<string> list2 = list;
						string[] array = new string[9];
						array[0] = uidFr;
						array[1] = "*";
						array[2] = nameFr;
						array[3] = "*";
						int num2 = 4;
						JToken jtoken = jobject["data"][i]["images"][num]["source"];
						array[num2] = ((jtoken != null) ? jtoken.ToString() : null);
						array[5] = "|";
						int num3 = 6;
						JToken jtoken2 = jobject["data"][i]["images"][num]["width"];
						array[num3] = ((jtoken2 != null) ? jtoken2.ToString() : null);
						array[7] = "|";
						int num4 = 8;
						JToken jtoken3 = jobject["data"][i]["images"][num]["height"];
						array[num4] = ((jtoken3 != null) ? jtoken3.ToString() : null);
						list2.Add(string.Concat(array));
					}
				}
			}
			catch
			{
			}
			return list;
		}
		public static List<string> GetMyListComments(string cookie, string userAgent, string proxy, int typeProxy)
		{
			List<string> list = new List<string>();
			try
			{
				RequestXNet requestXNet = new RequestXNet(cookie, userAgent, proxy, typeProxy);
				string value = Regex.Match(cookie + ";", "c_user=(.*?);").Groups[1].Value;
				string text = requestXNet.RequestGet("https://mbasic.facebook.com/" + value + "/allactivity/?category_key=commentscluster");
				string value2;
				do
				{
					text = WebUtility.HtmlDecode(text);
					MatchCollection matchCollection = Regex.Matches(text, "<span>(.*?)</h4>");
					for (int i = 0; i < matchCollection.Count; i++)
					{
						string text2 = matchCollection[i].Groups[1].Value;
						text2 = text2.Substring(0, text2.LastIndexOf('<'));
						MatchCollection matchCollection2 = Regex.Matches(text2, "<(.*?)>");
						for (int j = 0; j < matchCollection2.Count; j++)
						{
							text2 = text2.Replace(matchCollection2[j].Value, "");
						}
						bool flag = !list.Contains(text2);
						if (flag)
						{
							list.Add(text2);
						}
					}
					value2 = Regex.Match(text, "/allactivity.category_key(.*?)more_\\d").Value;
					text = requestXNet.RequestGet("http://mbasic.facebook.com/me" + value2);
				}
				while (value2 != "");
			}
			catch
			{
			}
			return list;
		}
		//public static List<string> GetMyListUidMessage(string cookie, string userAgent, string proxy, int typeProxy)
		//{
		//	List<string> list = new List<string>();
		//	try
		//	{
		//		RequestXNet requestXNet = new RequestXNet(cookie, userAgent, proxy, typeProxy);
		//		int num = 1;
		//		string input = requestXNet.RequestGet("https://mbasic.facebook.com/messages/");
		//		string text2;
		//		do
		//		{
		//			MatchCollection matchCollection = Regex.Matches(input, "#fua\">(.*?)<");
		//			for (int i = 0; i < matchCollection.Count; i++)
		//			{
		//				try
		//				{
		//					string text = matchCollection[i].Groups[1].Value.Replace("\"", "");
		//					text = Common.HtmlDecode(text);
		//					bool flag = !list.Contains(text);
		//					if (flag)
		//					{
		//						list.Add(text);
		//					}
		//				}
		//				catch
		//				{
		//				}
		//			}
		//			text2 = Regex.Match(input, "/messages/.pageNum=(.*?)\"").Value.Replace("amp;", "");
		//			input = requestXNet.RequestGet("https://mbasic.facebook.com" + text2);
		//			num++;
		//			bool flag2 = num >= 5;
		//			if (flag2)
		//			{
		//				break;
		//			}
		//		}
		//		while (text2 != "");
		//	}
		//	catch
		//	{
		//	}
		//	return list;
		//}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000A810 File Offset: 0x00008A10
		public static string GetMyBirthday(string token, string userAgent, string proxy, int typeProxy)
		{
			string result = "";
			try
			{
				RequestXNet requestXNet = new RequestXNet("", userAgent, proxy, typeProxy);
				string json = requestXNet.RequestGet("https://graph.facebook.com/me?fields=id,name,birthday&access_token=" + token);
				JObject jobject = JObject.Parse(json);
				return string.Concat(new string[]
				{
					jobject["id"].ToString(),
					"|",
					jobject["birthday"].ToString(),
					"|",
					jobject["name"].ToString()
				});
			}
			catch
			{
				bool flag = !CommonRequest.CheckLiveToken(token, userAgent, proxy, typeProxy);
				if (flag)
				{
					result = "-1";
				}
			}
			return result;
		}
		public static string Checkavatar(string TokenEAAG, string UID)
		{
			try
			{
				RequestXNet request = new RequestXNet();
				byte[] bytes = request.GetBytes("https://graph.facebook.com/" + UID + "/picture?type=square&redirect=true&width=50&height=50&access_token=" + TokenEAAG);
				MemoryStream memoryStream = new MemoryStream();
				memoryStream.Write(bytes, 0, Convert.ToInt32(bytes.Length));
				Bitmap Result1 = new Bitmap(memoryStream, false);
				memoryStream.Dispose();
				var Result2 = Image.FromStream(new MemoryStream(Convert.FromBase64String("/9j/4AAQSkZJRgABAQEASABIAAD/2wBDAAUDBAQEAwUEBAQFBQUGBwwIBwcHBw8LCwkMEQ8SEhEPERETFhwXExQaFRERGCEYGh0dHx8fExciJCIeJBweHx7/2wBDAQUFBQcGBw4ICA4eFBEUHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh7/wAARCAAyADIDASIAAhEBAxEB/8QAHAABAAIDAQEBAAAAAAAAAAAAAAQGAgMFBwEI/8QALRAAAQMCBAQEBwEAAAAAAAAAAQACAwQRBRIxQQYhUXETcoHBIiMzNWGRsfH/xAAXAQEBAQEAAAAAAAAAAAAAAAACAAMB/8QAFxEBAQEBAAAAAAAAAAAAAAAAABEBEv/aAAwDAQACEQMRAD8A/RSIvsQzSxg6F4B/a1FOocHq6oZ8giZqHSb9gpFRw9VRtvFJHL+NCrUizpPPnscx5Y4EOBsQdkXU4oEYxX4NSwZ+/wDlly0xERF1C3YfEJq6CNxyh7wLtUdfQSxwcx1iDcHopPQkUTC6gVdBHNfmRZ3fdSXODWlzuQAuVkSp8Uxtbipc05i9gJHTb2XMW2vnNVWS1BN855X2Gy0LTBZIsUXUyRgMjwyMOe46AC5K6GEYTJXfMcfDgBtfc9lZ6KipqNlqeMN6nUn1RqasEpn02GxRSCz+ZcOl1Ne3M0tOhFlkiBKHV081JKYZmFhBs0nQ9lqV8nhinjMc0bZGHYhV/FsC8Jrp6O5aOZjOvon0LhoiJJcsB+z03l91PRFkQiIpCIik88n+vJ5j/URExf/Z")));
				var Result3 = Image.FromStream(new MemoryStream(Convert.FromBase64String("/9j/4AAQSkZJRgABAQEASABIAAD/2wBDAAUDBAQEAwUEBAQFBQUGBwwIBwcHBw8LCwkMEQ8SEhEPERETFhwXExQaFRERGCEYGh0dHx8fExciJCIeJBweHx7/2wBDAQUFBQcGBw4ICA4eFBEUHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh7/wAARCAAyADIDASIAAhEBAxEB/8QAGwABAAIDAQEAAAAAAAAAAAAAAAUGAQMEBwj/xAAuEAABAwMCAgcJAAAAAAAAAAAAAQIDBAURBiESQRMUIjFRscE0NUJScXJzgZH/xAAXAQADAQAAAAAAAAAAAAAAAAABAgMA/8QAGBEBAQEBAQAAAAAAAAAAAAAAAAERAjH/2gAMAwEAAhEDEQA/APooA7rHTNqbgxjkyxnad+ipGaWz107GvSNrGu5udjbxwdFTYKqJiuieybHJNlLUCejjz4EtqamSGtSZqIjZkyv1TvIkoAADME3pDHWZ/HgTzIQlNNSMiuKuklZG3gVO0uMg6aLaACZ0DrBU6Gnbz418iuE3qxzHzQ8ErHKxFRzUXdCEKTwlAAFgHbaLe+uqFZlzI2bvd6Fmo7VRUuFjhRXp8Tt1Bemxp04yZluR0yuV0jlcnGu+ORvu8UktumZDnpMZbhd9jtBM7z5c5XOc88gu1Zb6Oq3mharvmTZf6Vy92zqCtkjcr4XrjfvRR5SYjAAMyyaQ9lm/J6ITi94BPr00ZAABCK1R7ok+5PMA0CqmACpX/9k=")));
				List<bool> ImgDowload = GetHash(new Bitmap(Result1));
				List<bool> ImgNam = GetHash(new Bitmap(Result2));
				List<bool> ImgNữ = GetHash(new Bitmap(Result3));
				double Nam = (double)(ImgDowload.Zip(ImgNam, (bool i, bool j) => i == j).Count((bool eq) => eq) / 256);
				double Nữ = (double)(ImgDowload.Zip(ImgNữ, (bool i, bool j) => i == j).Count((bool eq) => eq) / 256);
				if (Nam == 1)
				{
					return "Không";
				}
				if (Nữ == 1)
				{
					return "Không";
				}
			}
			catch
			{
				return "Lỗi";
			}
			return "Có";
		}
		private static Bitmap GetImageFromUid(string uid)
		{
			RequestXNet requestXNet = new RequestXNet("", "", "", 0);
			string url = "https://graph.facebook.com/" + uid + "/picture";
			byte[] bytes = requestXNet.GetBytes(url);
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.Write(bytes, 0, Convert.ToInt32(bytes.Length));
			Bitmap result = new Bitmap(memoryStream, false);
			memoryStream.Dispose();
			return result;
		}
		private static List<bool> GetHash(Bitmap bmpSource)
		{
			List<bool> list = new List<bool>();
			Bitmap bitmap = new Bitmap(bmpSource, new Size(16, 16));
			for (int i = 0; i < bitmap.Height; i++)
			{
				for (int j = 0; j < bitmap.Width; j++)
				{
					list.Add(bitmap.GetPixel(j, i).GetBrightness() < 0.5f);
				}
			}
			return list;
		}
		public static bool CheckLiveWall(string token, string uid)
		{
			RequestXNet requestXNet = new RequestXNet("", "", "", 0);
			try
			{
				string data = "fb_dtsg=AQG7-FIkZLYG:AQFIPo3-JPSo&q=node(" + uid + "){name}";
				string json = requestXNet.RequestPost("https://www.facebook.com/api/graphql", data, "application/x-www-form-urlencoded");
				JObject jobject = JObject.Parse(json);
				bool flag = jobject[uid]["name"] != null;
				if (flag)
				{
					return true;
				}
			}
			catch
			{
			}
			return false;
		}
		public static bool CheckLiveToken(string token, string useragent, string proxy, int typeProxy = 0)
		{
			bool result = false;
			RequestXNet requestXNet = new RequestXNet("", useragent, proxy, typeProxy);
			try
			{
				string text = requestXNet.RequestGet("https://graph.facebook.com/me?access_token=" + token);
				result = true;
			}
			catch
			{
			}
			return result;
		}
		public static string GetTokenEAAAAZ(string cookie, string useragent, string proxy, int typeProxy = 0)
		{
			string text = "";
			try
			{
				RequestXNet requestXNet = new RequestXNet(cookie, useragent, proxy, typeProxy);
				string input = requestXNet.RequestGet("https://m.facebook.com/composer/ocelot/async_loader/?publisher=feed");
				text = Regex.Match(input, "EAAAAZ(.*?)\"").Value.Replace("\"", "").Replace("\\", "");
			}
			catch
			{
				bool flag = !CommonRequest.CheckLiveCookie(cookie, useragent, proxy, typeProxy).StartsWith("1|");
				if (flag)
				{
					return "-1";
				}
			}
			bool flag2 = text == "";
			if (flag2)
			{
				bool flag3 = !CommonRequest.CheckLiveCookie(cookie, useragent, proxy, typeProxy).StartsWith("1|");
				if (flag3)
				{
					return "-1";
				}
			}
			return text;
		}
		public static string GetTokenEAAG(string cookie, string userAgent, string proxy, int typeProxy)
		{
			string text = "";
			try
			{
				RequestXNet requestXNet = new RequestXNet(cookie, "", proxy, 0);
				string input = requestXNet.RequestGet("https://business.facebook.com/business_locations/");
				text = Regex.Match(input, "EAAG(.*?)\"").Value.Replace("\"", "").Replace("\\", "");
			}
			catch
			{
				bool flag = !CommonRequest.CheckLiveCookie(cookie, userAgent, proxy, typeProxy).StartsWith("1|");
				if (flag)
				{
					return "-1";
				}
			}
			bool flag2 = text == "";
			if (flag2)
			{
				bool flag3 = !CommonRequest.CheckLiveCookie(cookie, userAgent, proxy, typeProxy).StartsWith("1|");
				if (flag3)
				{
					return "-1";
				}
			}
			return text;
		}
		//public static string CheckCheckpoint(string idMethod)
		//{
		//	string result = "";
		//	int num = 0;
		//	if (idMethod != null)
		//	{
		//		uint num2 = < PrivateImplementationDetails >.ComputeStringHash(idMethod);
		//		if (num2 <= 1315429348U)
		//		{
		//			if (num2 <= 822911587U)
		//			{
		//				if (num2 != 334175660U)
		//				{
		//					if (num2 != 401286136U)
		//					{
		//						if (num2 != 822911587U)
		//						{
		//							goto IL_34E;
		//						}
		//						if (!(idMethod == "4"))
		//						{
		//							goto IL_34E;
		//						}
		//					}
		//					else
		//					{
		//						if (!(idMethod == "14"))
		//						{
		//							goto IL_34E;
		//						}
		//						bool flag = num == 0;
		//						if (flag)
		//						{
		//							result = "Thiết bị";
		//						}
		//						else
		//						{
		//							result = "device";
		//						}
		//						return result;
		//					}
		//				}
		//				else
		//				{
		//					if (!(idMethod == "18"))
		//					{
		//						goto IL_34E;
		//					}
		//					bool flag2 = num == 0;
		//					if (flag2)
		//					{
		//						result = "Bình luận";
		//					}
		//					else
		//					{
		//						result = "comment";
		//					}
		//					return result;
		//				}
		//			}
		//			else if (num2 <= 923577301U)
		//			{
		//				if (num2 != 906799682U)
		//				{
		//					if (num2 != 923577301U)
		//					{
		//						goto IL_34E;
		//					}
		//					if (!(idMethod == "2"))
		//					{
		//						goto IL_34E;
		//					}
		//					bool flag3 = num == 0;
		//					if (flag3)
		//					{
		//						result = "Ngày sinh";
		//					}
		//					else
		//					{
		//						result = "Birthday";
		//					}
		//					return result;
		//				}
		//				else
		//				{
		//					if (!(idMethod == "3"))
		//					{
		//						goto IL_34E;
		//					}
		//					bool flag4 = num == 0;
		//					if (flag4)
		//					{
		//						result = "Ảnh";
		//					}
		//					else
		//					{
		//						result = "Image";
		//					}
		//					return result;
		//				}
		//			}
		//			else if (num2 != 1153637868U)
		//			{
		//				if (num2 != 1315429348U)
		//				{
		//					goto IL_34E;
		//				}
		//				if (!(idMethod == "id_upload"))
		//				{
		//					goto IL_34E;
		//				}
		//				return "Up ảnh";
		//			}
		//			else
		//			{
		//				if (!(idMethod == "72h"))
		//				{
		//					goto IL_34E;
		//				}
		//				bool flag5 = num == 0;
		//				if (flag5)
		//				{
		//					result = "72h";
		//				}
		//				else
		//				{
		//					result = "72 hours";
		//				}
		//				return result;
		//			}
		//		}
		//		else if (num2 <= 2347784130U)
		//		{
		//			if (num2 != 1718322808U)
		//			{
		//				if (num2 != 2331006511U)
		//				{
		//					if (num2 != 2347784130U)
		//					{
		//						goto IL_34E;
		//					}
		//					if (!(idMethod == "34"))
		//					{
		//						goto IL_34E;
		//					}
		//				}
		//				else
		//				{
		//					if (!(idMethod == "37"))
		//					{
		//						goto IL_34E;
		//					}
		//					return "Gửi OTP về mail";
		//				}
		//			}
		//			else
		//			{
		//				if (!(idMethod == "2fa"))
		//				{
		//					goto IL_34E;
		//				}
		//				return "Có 2fa";
		//			}
		//		}
		//		else if (num2 <= 2364561749U)
		//		{
		//			if (num2 != 2347931225U)
		//			{
		//				if (num2 != 2364561749U)
		//				{
		//					goto IL_34E;
		//				}
		//				if (!(idMethod == "35"))
		//				{
		//					goto IL_34E;
		//				}
		//				return "Login Google";
		//			}
		//			else
		//			{
		//				if (!(idMethod == "26"))
		//				{
		//					goto IL_34E;
		//				}
		//				bool flag6 = num == 0;
		//				if (flag6)
		//				{
		//					result = "Nhờ bạn bè";
		//				}
		//				else
		//				{
		//					result = "Friend";
		//				}
		//				return result;
		//			}
		//		}
		//		else if (num2 != 2381486463U)
		//		{
		//			if (num2 != 2517938561U)
		//			{
		//				goto IL_34E;
		//			}
		//			if (!(idMethod == "vhh"))
		//			{
		//				goto IL_34E;
		//			}
		//			bool flag7 = num == 0;
		//			if (flag7)
		//			{
		//				result = "Vô hiệu hóa";
		//			}
		//			else
		//			{
		//				result = "disable";
		//			}
		//			return result;
		//		}
		//		else
		//		{
		//			if (!(idMethod == "20"))
		//			{
		//				goto IL_34E;
		//			}
		//			bool flag8 = num == 0;
		//			if (flag8)
		//			{
		//				result = "Tin nhắn";
		//			}
		//			else
		//			{
		//				result = "Message";
		//			}
		//			return result;
		//		}
		//		return "Otp";
		//	}
		//IL_34E:
		//	File.AppendAllText("data\\dangcp.txt", idMethod);
		//	return result;
		//}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000B064 File Offset: 0x00009264
		//public static string CheckFacebookAccount(string email, string pass, string userAgent, string proxy, int typeProxy)
		//{
		//	string text = "";
		//	try
		//	{
		//		string data = "email=" + WebUtility.UrlEncode(email) + "&pass=" + WebUtility.UrlEncode(pass);
		//		RequestXNet requestXNet = new RequestXNet("", userAgent, proxy, typeProxy);
		//		string text2 = requestXNet.RequestPost("https://mbasic.facebook.com/login/device-based/regular/login/?refsrc=https%3A%2F%2Fmbasic.facebook.com%2F&lwv=100&refid=8", data, "application/x-www-form-urlencoded").ToString();
		//		bool flag = text2.Contains("id=\"checkpointSubmitButton\"");
		//		if (flag)
		//		{
		//			bool flag2 = text2.Contains("id=\"approvals_code\"");
		//			if (flag2)
		//			{
		//				text = "5|";
		//			}
		//			else
		//			{
		//				text = "2|";
		//				requestXNet = new RequestXNet("", userAgent, proxy, typeProxy);
		//				requestXNet.RequestGet("https://www.facebook.com").ToString();
		//				text2 = requestXNet.RequestPost("https://www.facebook.com/login/device-based/regular/login/?login_attempt=1&lwv=100", data, "application/x-www-form-urlencoded").ToString();
		//				string value = Regex.Match(text2, "name=\"fb_dtsg\" value=\"(.*?)\"").Groups[1].Value;
		//				string value2 = Regex.Match(text2, "name=\"jazoest\" value=\"(.*?)\"").Groups[1].Value;
		//				string value3 = Regex.Match(text2, "name=\"nh\" value=\"(.*?)\"").Groups[1].Value;
		//				string value4 = Regex.Match(text2, "\"__spin_r\":(.*?),").Groups[1].Value;
		//				string value5 = Regex.Match(text2, "\"__spin_t\":(.*?),").Groups[1].Value;
		//				string data2 = string.Concat(new string[]
		//				{
		//					"jazoest=",
		//					value2,
		//					"&fb_dtsg=",
		//					value,
		//					"&nh=",
		//					value3,
		//					"&submit[Continue]=Ti%E1%BA%BFp%20t%E1%BB%A5c&__user=0&__a=1&__dyn=7xe6Fo4OQ1PyUhxOnFwn84a2i5U4e1Fx-ewSwMxW0DUeUhw5cx60Vo1upE4W0OE2WxO0SobEa87i0n2US1vw4Ugao881FU3rw&__csr=&__req=5&__beoa=0&__pc=PHASED%3ADEFAULT&dpr=1&__rev=",
		//					value4,
		//					"&__s=op5tkm%3A2d4a9m%3A37z92b&__hsi=6789153697588537525-0&__spin_r=",
		//					value4,
		//					"&__spin_b=trunk&__spin_t=",
		//					value5
		//				});
		//				text2 = requestXNet.RequestPost("https://www.facebook.com/checkpoint/async?next=https%3A%2F%2Fwww.facebook.com%2F", data2, "application/x-www-form-urlencoded");
		//				text2 = requestXNet.RequestGet("https://www.facebook.com/checkpoint/?next");
		//				MatchCollection matchCollection = Regex.Matches(text2, "verification_method\" value=\"(.*?)\"");
		//				bool flag3 = matchCollection.Count > 0;
		//				if (flag3)
		//				{
		//					for (int i = 0; i < matchCollection.Count; i++)
		//					{
		//						text = text + CommonRequest.CheckCheckpoint(matchCollection[i].Groups[1].Value) + "-";
		//					}
		//					text = text.TrimEnd(new char[]
		//					{
		//						'-'
		//					});
		//				}
		//				else
		//				{
		//					bool flag4 = text2.Contains("/checkpoint/dyi/?referrer=disabled_checkpoint");
		//					if (flag4)
		//					{
		//						text += CommonRequest.CheckCheckpoint("vhh");
		//					}
		//					else
		//					{
		//						bool flag5 = text2.Contains("captcha-recaptcha");
		//						if (flag5)
		//						{
		//							text += CommonRequest.CheckCheckpoint("72h");
		//						}
		//						else
		//						{
		//							bool flag6 = text2.Contains("name=\"submit[Log Out]\"") || text2.Contains("name=\"submit[__placeholder__]\"");
		//							if (flag6)
		//							{
		//								text += "không thể xmdt";
		//							}
		//							else
		//							{
		//								bool flag7 = text2.Contains("name=\"submit[Continue]\"");
		//								if (flag7)
		//								{
		//									text += "Thiết bị";
		//								}
		//							}
		//						}
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			bool flag8 = text2.Contains("login_error");
		//			if (flag8)
		//			{
		//				bool flag9 = text2.Contains("m_login_email");
		//				if (flag9)
		//				{
		//					text = "3|";
		//				}
		//				else
		//				{
		//					text = "0|";
		//				}
		//			}
		//			else
		//			{
		//				bool flag10 = text2.Contains("action_set_contact_point");
		//				if (flag10)
		//				{
		//					text = "2|" + CommonRequest.CheckCheckpoint("34");
		//				}
		//				else
		//				{
		//					string cookie = requestXNet.GetCookie();
		//					bool flag11 = CommonRequest.CheckLiveCookie(cookie, userAgent, proxy, typeProxy).StartsWith("1|");
		//					if (flag11)
		//					{
		//						text = text + "1|" + cookie;
		//					}
		//					else
		//					{
		//						text = "2|";
		//					}
		//				}
		//			}
		//		}
		//	}
		//	catch
		//	{
		//		text = "0|";
		//	}
		//	return text;
		//}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000B424 File Offset: 0x00009624
		public static string GetInfoAccountFromUidUsingToken(string tokenTrungGian, string uid, string useragent, string proxy, int typeProxy)
		{
			bool flag = false;
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = "";
			string text5 = "";
			string text6 = "";
			string text7 = "";
			try
			{
				RequestXNet requestXNet = new RequestXNet("", useragent, proxy, typeProxy);
				bool flag2 = uid == "";
				if (flag2)
				{
					uid = "me";
				}
				string json = requestXNet.RequestGet("https://graph.facebook.com/v2.11/" + uid + "?fields=name,email,gender,birthday,friends.limit(0)&access_token=" + tokenTrungGian);
				JObject jobject = JObject.Parse(json);
				flag = true;
				text = jobject["name"].ToString();
				try
				{
					text2 = jobject["gender"].ToString();
				}
				catch
				{
				}
				try
				{
					text3 = jobject["birthday"].ToString();
				}
				catch
				{
				}
				try
				{
					text5 = jobject["email"].ToString();
				}
				catch
				{
				}
				try
				{
					text6 = jobject["friends"]["summary"]["total_count"].ToString();
				}
				catch
				{
				}
				bool flag3 = text6 == "";
				if (flag3)
				{
					text6 = "0";
				}
				json = requestXNet.RequestGet("https://graph.facebook.com/v2.11/" + uid + "/groups?fields=id&limit=5000&access_token=" + tokenTrungGian);
				jobject = JObject.Parse(json);
				try
				{
					text7 = (jobject["data"].Count<JToken>().ToString() ?? "");
				}
				catch
				{
				}
				bool flag4 = text7 == "";
				if (flag4)
				{
					text7 = "0";
				}
			}
			catch
			{
				bool flag5 = !CommonRequest.CheckLiveToken(tokenTrungGian, "", "", 0);
				if (flag5)
				{
					return "-1";
				}
			}
			return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", new object[]
			{
				flag,
				text,
				text2,
				text3,
				text4,
				text5,
				text6,
				text7
			});
		}
		//public static string GetInfoAccountFromUidUsingCookie(string cookie, string useragent, string proxy, int typeProxy)
		//{
		//	bool flag = false;
		//	string text = "";
		//	string text2 = "";
		//	string text3 = "";
		//	string text4 = "";
		//	string text5 = "";
		//	string text6 = "";
		//	string text7 = "";
		//	string text8 = "";
		//	string text9 = "";
		//	try
		//	{
		//		string value = Regex.Match(cookie + ";", "c_user=(.*?);").Groups[1].Value;
		//		RequestXNet requestXNet = new RequestXNet(cookie, useragent, proxy, typeProxy);
		//		string text10 = requestXNet.RequestGet("https://m.facebook.com/composer/ocelot/async_loader/?publisher=feed");
		//		string value2 = Regex.Match(text10, Common.Base64Decode("bmFtZT1cXCJmYl9kdHNnXFwiIHZhbHVlPVxcIiguKj8pXFwi")).Groups[1].Value;
		//		text8 = Regex.Match(text10, "EAAA(.*?)\"").Value.TrimEnd(new char[]
		//		{
		//			'"',
		//			'\\'
		//		});
		//		text = Regex.Match(text10, Common.Base64Decode("cHJvZnBpY1xcIiBhcmlhLWxhYmVsPVxcIiguKj8pLA==")).Groups[1].Value;
		//		text = WebUtility.HtmlDecode(text);
		//		string text11 = Common.Base64Decode("LS0tLS0tV2ViS2l0Rm9ybUJvdW5kYXJ5MnlnMEV6RHBTWk9DWGdCUgpDb250ZW50LURpc3Bvc2l0aW9uOiBmb3JtLWRhdGE7IG5hbWU9ImZiX2R0c2ciCgp7e2ZiX2R0c2d9fQotLS0tLS1XZWJLaXRGb3JtQm91bmRhcnkyeWcwRXpEcFNaT0NYZ0JSCkNvbnRlbnQtRGlzcG9zaXRpb246IGZvcm0tZGF0YTsgbmFtZT0icSIKCm5vZGUoe3t1aWR9fSl7ZnJpZW5kc3tjb3VudH0sc3Vic2NyaWJlcnN7Y291bnR9LGdyb3Vwc3tjb3VudH0sY3JlYXRlZF90aW1lfQotLS0tLS1XZWJLaXRGb3JtQm91bmRhcnkyeWcwRXpEcFNaT0NYZ0JSLS0=");
		//		text11 = text11.Replace("{{fb_dtsg}}", value2);
		//		text11 = text11.Replace("{{uid}}", value);
		//		text10 = requestXNet.RequestPost("https://www.facebook.com/api/graphql/", text11, "multipart/form-data; boundary=----WebKitFormBoundary2yg0EzDpSZOCXgBR");
		//		JObject jobject = JObject.Parse(text10);
		//		text6 = jobject[value]["friends"]["count"].ToString();
		//		text7 = jobject[value]["groups"]["count"].ToString();
		//		text9 = jobject[value]["created_time"].ToString();
		//		bool flag2 = text6 == "";
		//		if (flag2)
		//		{
		//			text6 = "0";
		//		}
		//		bool flag3 = text7 == "";
		//		if (flag3)
		//		{
		//			text7 = "0";
		//		}
		//	}
		//	catch
		//	{
		//		bool flag4 = !CommonRequest.CheckLiveCookie(cookie, useragent, proxy, typeProxy).Contains("1|");
		//		if (flag4)
		//		{
		//			return "-1";
		//		}
		//	}
		//	return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}", new object[]
		//	{
		//		flag,
		//		text,
		//		text2,
		//		text3,
		//		text4,
		//		text5,
		//		text6,
		//		text7,
		//		text8,
		//		text9
		//	});
		//}
	}
}
