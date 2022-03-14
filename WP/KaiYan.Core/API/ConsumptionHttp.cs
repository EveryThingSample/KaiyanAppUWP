using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.API
{
    class ConsumptionHttp : HttpBase
    {

        public static Task<HttpBase> ReportTimeAsync(string videoId, int time)
        {
            var url = "https://baobab.kaiyanapp.com/api/v4/consumption/videoPlay";
            var _params = new Dictionary<string, string>();
            _params.Add("videoId", videoId);
            _params.Add("timePoint", time.ToString());
            return POSTAsync<HttpBase>(url, _params);
        }
        public static Task<string> AddAsync(string videoId)
        {
            var url = "https://baobab.kaiyanapp.com/api/v3/consumption/count";
            var _params = new Dictionary<string, string>();
            _params.Add("videoId", videoId);
            _params.Add("name", "play");
            _params.Add("action", "increase");
            _params.Add("timestamp", "1562340418810");
            _params.Add("token", "8ccf129aec3cf8b85a6e8b2291e5e477");
            return POSTAsync<string>(url, _params);
        }
    }
}
