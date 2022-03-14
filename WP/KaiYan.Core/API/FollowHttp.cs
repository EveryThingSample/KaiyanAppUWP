using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.API
{
    internal class FollowHttp : HttpBase
    {
        public static Task<HttpBase> AddAsync(string itemId, string itemType)
        {
            var url = "http://baobab.kaiyanapp.com/api/v1/follow/add";
            var _params = new Dictionary<string, string>();
            _params.Add("itemId", itemId);
            _params.Add("itemType", itemType);
            return POSTAsync<HttpBase>(url, _params);
        }
        public static Task<HttpBase> CancelAsync(string itemId, string itemType)
        {
            var url = "http://baobab.kaiyanapp.com/api/v1/follow/cancel";
            var _params = new Dictionary<string, string>();
            _params.Add("itemId", itemId);
            _params.Add("itemType", itemType);
            return POSTAsync<HttpBase>(url, _params);
        }
    }
}
