using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Card.Resource
{
    public class UgcVideoResource : UgcResourceItem, IVideoResource
    {
        internal UgcVideoResource(JObject jobject) : base(jobject)
        {
            PlayUrl = jobject.GetValue("playUrl").ToString();
            if (jobject.GetValue("tags")?.ToArray().Length>0)
            Category = jobject.GetValue("tags").ToArray().First().ToObject<JObject>().GetValue("name").ToString();
            Duration = jobject.GetValue("duration").ToObject<int>();
        }
        public string PlayUrl { get; }

        public string Category {get;}

        public int Duration { get; }

        public string DurationText => (Duration / 60).ToString("d2") + ":" + (Duration % 60).ToString("d2");
    }
}
