using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.API
{
    internal class ReplyHttp:HttpBase
    {

        public static Task<JObject> GetRepliesAsync(string videoId, string type, int num = 20, int lastId = -1)
        {
            var url = "https://baobab.kaiyanapp.com/api/v2/replies/video";
            var _params = new Dictionary<string, string>();
            _params.Add("videoId", videoId);
            _params.Add("type", type);
            _params.Add("num", num.ToString());
            if (lastId > 0)
                _params.Add("lastId", lastId.ToString());
            return GETAsync<JObject>(url, _params);
        }

        public static Task<HttpBase> LikeAsync(bool isLike, string replyId)
        {
            var url = "https://baobab.kaiyanapp.com/api/v1/replies/like";
            var _params = new Dictionary<string, string>();
            _params.Add("replyId", replyId);
            _params.Add("action", isLike?"like":"cancel");
            return POSTAsync<HttpBase>(url, _params);
        }
        public static Task<HttpBase> AddAsync(string videoId, string type, string message,string replyId = null)
        {
            var url = "https://baobab.kaiyanapp.com/api/v2/replies/add";
            var _params = new Dictionary<string, string>();
            _params.Add("videoId", videoId);
            _params.Add("type", type);
            _params.Add("message", message);
            if (replyId != null)
                _params.Add("parentReplyId", replyId);
            return POSTAsync<HttpBase>(url, _params);
        }
    }
}
