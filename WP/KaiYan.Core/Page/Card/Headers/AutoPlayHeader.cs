using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Card.Headers
{
    public class AutoPlayHeader : KaiYanBase, ICover
    {
        internal AutoPlayHeader(JObject jobject) : base(jobject)
        {
            Title = jobject.GetValue("issuerName").ToObject<string>();
            Icon = jobject.GetValue("icon").ToObject<string>();
            Time = jobject.GetValue("time").ToObject<long>()/1000;
        }

        public string Title { get; }
        public string Description { get; }
        public string Icon { get; }
        public long Time { get; }

        public string GetDescription() => Description;

        public string GetIcon() => Icon;

        public string GetTitle() => Title;
    }
}
