using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Card.Resource
{
    public class PlayInfo
    {
        internal PlayInfo(JObject jobject)
        {
            url = jobject.GetValue("url").ToObject<string>();
            name = jobject.GetValue("name").ToObject<string>();
        }
        internal PlayInfo(string name, string url)
        {
            this.url = url;
            this.name = name;
        }
        public string url { get; }
        public string name { get; }
        public override string ToString()
        {
            return name;
        }
    }
}
