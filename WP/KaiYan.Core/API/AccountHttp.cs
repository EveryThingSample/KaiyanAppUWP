using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.API
{
    public class AccountHttp:HttpBase
    {
		internal static Task<AccountHttp> Loginasync(string username, string password)
		{
			var _params = new Dictionary<string, string>();
			_params.Add("username", username);
			_params.Add("password", password);
			return LoginAsync("https://account.kaiyanapp.com/v1/api/login", _params);
		}
		internal string ky_auth;

		internal static Task<AccountHttp> GetProfileAsync()
        {
			var url = "https://account.kaiyanapp.com/v1/api/profile";
			var _params = new Dictionary<string, string>();
			_params.Add("fields", "COMPLETE_SOCIAL");
			return POSTAsync<AccountHttp>(url, _params);
		}

		private static async Task<AccountHttp> LoginAsync(string url, IDictionary<string, string> _params)
		{
			HttpWebRequest request = null;
			HttpWebResponse response = null;
			AccountHttp result = null;
			if (_params == null)
			{
				_params = new Dictionary<string, string>();
			}
			_params.Add("f", "iphone");
			_params.Add("net", "wifi");
			_params.Add("size", "700X500");
			_params.Add("p_product", "EYEPETIZER_IOS");
			_params.Add("u", Account.Current?.udid);
			_params.Add("uid", Account.Current?.uid);
			_params.Add("v", "5.5.0");
			_params.Add("vc", "5913");
			request = (HttpWebRequest)HttpWebRequest.Create(url);

			request.Method = "POST";
			request.Headers[HttpRequestHeader.Accept] = "*/*";
			request.Headers[HttpRequestHeader.AcceptEncoding] = "gzip";
			request.Headers[HttpRequestHeader.UserAgent] = string.Format("Eyepetizer/{0} CFNetwork/978.0.7 Darwin/18.6.0", 5913);

			string sendData = convertParams(_params);
			ASCIIEncoding encoding = new ASCIIEncoding();
			byte[] bytepostData = encoding.GetBytes(sendData);
			request.Headers[HttpRequestHeader.ContentLength] = bytepostData.Length.ToString();
			//发送数据 using结束代码段释放
			Stream requestStm = await request.GetRequestStreamAsync();//.GetRequestStream();
			requestStm.Write(bytepostData, 0, bytepostData.Length);
			requestStm.Dispose();
			request.ContentType = "application/x-www-form-urlencoded";

			//响应
			response = await request.GetResponseAsync() as HttpWebResponse;

			string responseText = string.Empty;
			using (Stream responseStm = response.GetResponseStream())
			{
				MemoryStream memoryStream = new MemoryStream();
				await responseStm.CopyToAsync(memoryStream);

				var bytes = EveryThingSampleTools.WP.Tools.ZipHelper.Decompress(memoryStream.ToArray());
				responseText = Encoding.UTF8.GetString(bytes);
			}
			string ky_auth=null;
			if (response.Headers[HttpResponseHeader.SetCookie] != null)
			{
				var setcookieText = response.Headers[HttpResponseHeader.SetCookie];
				ky_auth = setcookieText.Substring(setcookieText.IndexOf("ky_auth=") + 8);
				ky_auth = ky_auth.Substring(0, ky_auth.IndexOf(';'));
			}
			response.Dispose();
			
			{
				if (!string.IsNullOrEmpty(responseText))
				{
					result = Newtonsoft.Json.JsonConvert.DeserializeObject<AccountHttp>(responseText);
					result.ky_auth = ky_auth;
					if (result is HttpBase httpBase)
					{
						if (httpBase.error != 0)
						{
							throw new Exception(httpBase.msg);
						}
					}

				}
			}
			return result;
		}
		public LoginMember member { get; set; }

		public class DevicesItem
		{
			/// <summary>
			/// 
			/// </summary>
			public string id { get; set; }
			/// <summary>
			/// 
			/// </summary>
			public string udid { get; set; }
			/// <summary>
			/// 
			/// </summary>
			public string versionName { get; set; }
		}
		public class Social
		{
			public string platform { get; set; }
			public string bindTime { get; set; }
			public bool valid { get; set; }
		}

		public class LoginMember
		{
			/// <summary>
			/// 
			/// </summary>
			public string uid { get; set; }
			/// <summary>
			/// 开眼用户_0e8t1d
			/// </summary>
			public string nick { get; set; }
			/// <summary>
			/// 
			/// </summary>
			public string avatar { get; set; }
			/// <summary>
			/// 
			/// </summary>
			public string username { get; set; }
			/// <summary>
			/// 
			/// </summary>
			public string telephone { get; set; }
			/// <summary>
			/// 
			/// </summary>
			public string emailValidated { get; set; }
			/// <summary>
			/// 
			/// </summary>
			public string telephoneValidated { get; set; }
			/// <summary>
			/// 
			/// </summary>
			public List<Social> socials { get; set; }
			/// <summary>
			/// 
			/// </summary>
			public List<DevicesItem> devices { get; set; }
			/// <summary>
			/// 
			/// </summary>
			public List<string> roles { get; set; }
			/// <summary>
			/// 
			/// </summary>
			public string registerSource { get; set; }
			/// <summary>
			/// 
			/// </summary>
			public string needUpdatePassword { get; set; }
			/// <summary>
			/// 
			/// </summary>
			public string trusted { get; set; }
			/// <summary>
			/// 
			/// </summary>
			public string complete { get; set; }
			/// <summary>
			/// 
			/// </summary>
			public string enabled { get; set; }
			/// <summary>
			/// 
			/// </summary>
			public string registDate { get; set; }
			/// <summary>
			/// 
			/// </summary>
			public string bindStatus { get; set; }
		}
    }
}
