using KaiYan.Core.API;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page
{
    public class TabListTool : IMainPageTool
    {
        string url;
        internal JObject jobject;
        internal TabListTool(string url)
        {
            this.url = url;
        }
        
        public async Task<IList<PageTool>> GetPageToolsAsync()
        {
            jobject = await TabListHttp.GetAsync(url);
            var jtabList = jobject.GetValue("tabInfo").ToObject<JObject>().GetValue("tabList").ToArray();
            IList<PageTool> tabList = new List<PageTool>(jtabList.Length);
            foreach (var item in jtabList)
            {
                var name = item.ToObject<JObject>().GetValue("name").ToString();
                var url = item.ToObject<JObject>().GetValue("apiUrl").ToString();
                tabList.Add(new PageTool(name, url));
            }
            return tabList;
        }
    }
}
