using KaiYan.Core.Exceptions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace KaiYan.Core.API
{
    public class HttpBase
    {
        protected HttpBase()
        {
        }

        public int error { get; set; }
        public string msg { get; set; }
        public int errorCode { get; set; }
        public string errorMessage { get; set; }
        private static void addSystemParams(IDictionary<string, string> _params)
        {
            _params.Add("f", "iphone");
            _params.Add("net", "wifi");
            _params.Add("size", "700X500");
            _params.Add("p_product", "EYEPETIZER_IOS");
            _params.Add("u",Account.Current?.udid);

            _params.Add("v", "5.5.0");
            _params.Add("vc", "5913");
        }
        public static string convertParams(IDictionary<string, string> _params)
        {
            string str = "";
            foreach (var item in _params)
            {
                str += item.Key + "=" + (string.IsNullOrEmpty(item.Value) ? null : Uri.EscapeDataString(item.Value)) + "&";
            }
            return str.Substring(0, str.Length - 1);
        }
        internal static async Task<T> GETAsync<T>(string url, IDictionary<string, string> _params = null)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            object result = null;
            if (_params == null)
            {
                _params = new Dictionary<string, string>();
            }
            addSystemParams(_params);
            if (url.Contains("uid=") == false)
            {
                _params.Add("uid", Account.Current?.uid);
            }
            url += (url.Contains("?") ? (url.EndsWith("&") ? "" : "&") : "?" )+ convertParams(_params);
          
            request = (HttpWebRequest)HttpWebRequest.Create(url);

            request.Method = "GET";
            request.Headers[HttpRequestHeader.Accept] = "*/*";
            request.Headers[HttpRequestHeader.AcceptEncoding] = "gzip";
            request.Headers[HttpRequestHeader.UserAgent] = string.Format("Eyepetizer/{0} CFNetwork/978.0.7 Darwin/18.6.0", 5913);

            if (Account.IsLogin)
            {
                request.Headers[HttpRequestHeader.Cookie] = "ky_auth=" + Account.Current.GetAuthKey();
            }

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
            response.Dispose();
            if (typeof(T) == typeof(string))
            {
                result = responseText;
            }
            else if (typeof(T) == typeof(JObject))
            {
                result = JObject.Parse(responseText);
            }
            else
            {
                if (!string.IsNullOrEmpty(responseText))
                {
                    result = JsonSerialize<T>(responseText);
                    if (result is HttpBase httpBase)
                    {
                        if (httpBase.error != 0)
                        {
                            throw new HttpException(httpBase.error, httpBase.msg);
                        }
                        else if (httpBase.errorCode != 0)
                        {
                            throw new HttpException(httpBase.errorCode, httpBase.errorMessage);
                        }
                    }
                }
            }
            return (T)result;
        }

        internal static async Task<T> POSTAsync<T>(string url, IDictionary<string, string> _params = null)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            object result = null;
            if (_params == null)
            {
                _params = new Dictionary<string, string>();
            }
            addSystemParams(_params);
            _params.Add("uid", Account.Current?.uid);
            request = (HttpWebRequest)HttpWebRequest.Create(url);

            request.Method = "POST";
            request.Headers[HttpRequestHeader.Accept] = "*/*";
            request.Headers[HttpRequestHeader.AcceptEncoding] = "gzip";
            request.Headers[HttpRequestHeader.UserAgent] = string.Format("Eyepetizer/{0} CFNetwork/978.0.7 Darwin/18.6.0", 5913);

            if (Account.IsLogin)
            {
                request.Headers[HttpRequestHeader.Cookie] = "ky_auth=" + Account.Current.GetAuthKey();
                //request.CookieContainer = Account.Current.GetCookieContainer();
            }

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
            response.Dispose();
            if (typeof(T) == typeof(string))
            {
                result = responseText;
            }
            else if (typeof(T) == typeof(JObject))
            {
                result = JObject.Parse(responseText);
            }
            else
            {
                if (!string.IsNullOrEmpty(responseText))
                {
                    result = JsonSerialize<T>(responseText);
                    if (result is HttpBase httpBase)
                    {
                        if (httpBase.error != 0)
                        {
                            throw new HttpException(httpBase.error, httpBase.msg);
                        }
                        else if (httpBase.errorCode != 0)
                        {
                            throw new HttpException(httpBase.errorCode, httpBase.errorMessage);
                        }
                    }
                }
            }
            return (T)result;
        }

       

        private static T JsonSerialize<T>(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }
    }
    
}
