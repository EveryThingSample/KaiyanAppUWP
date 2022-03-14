using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Card.Headers
{
    public class Header:KaiYanBase,ICover
    {
        internal Header(JObject jobject):base(jobject)
        {
            Title = jobject.GetValue("title").ToObject<string>();
            Description = jobject.GetValue("description")?.ToObject<string>();
            Icon = jobject.GetValue("icon")?.ToObject<string>();
          
            if (string.IsNullOrEmpty(Icon))
                Icon = "https://home.eyepetizer.net/favicon.ico";
        }

        public string Title { get; }
        public string Description { get; }
        public string Icon { get; }
        public bool IsPgc { get; }
        public string GetDescription() => Description;

        public string GetIcon() => Icon;

        public string GetTitle() => Title;
    }
}
