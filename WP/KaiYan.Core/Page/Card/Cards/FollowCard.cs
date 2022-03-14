using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KaiYan.Core.Page.Card.Headers;
using KaiYan.Core.Page.Card.Resource;

namespace KaiYan.Core.Page.Card.Cards
{
    public class FollowCard : VideoCardItem
    {
        internal FollowCard(JObject jobject) : base(jobject,jobject.GetValue("data").ToObject<JObject>().GetValue("content")
                .ToObject<JObject>().GetValue("data").ToObject<JObject>())
        {
            Header = new Header(jobject.GetValue("data").ToObject<JObject>().GetValue("header").ToObject<JObject>());
            VideoResource = new VideoResource(jobject.GetValue("data").ToObject<JObject>().GetValue("content")
                .ToObject<JObject>().GetValue("data").ToObject<JObject>());
        }

        public ICover Header { get; }
        public override ICover GetHeader() => Header;
        VideoResource VideoResource;
        public override ResourceItem GetResource()
        {
            return VideoResource;
        }
    }
}
