using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.API
{
    static class TabListHttp
    {
        public static Task<JObject>GetAsync(string url)
        {
            return HttpBase.GETAsync<JObject>(url);
        }
    }
}
