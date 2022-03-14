using KaiYan.Core.Page.Body;
using KaiYan.Core.Page.Card.Headers;
using KaiYan.Core.Page.Card.Resource;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Card.Cards
{
    public class AutoPlayFollowCard : VideoCardItem
    {
        internal AutoPlayFollowCard(JObject jobject) : base(jobject, jobject.GetValue("data").ToObject<JObject>().GetValue("content")
                .ToObject<JObject>().GetValue("data").ToObject<JObject>())
        {
            Header = new AutoPlayHeader(jobject.GetValue("data").ToObject<JObject>().GetValue("header").ToObject<JObject>());
            VideoResource = ResourceFactory.Create(jobject.GetValue("data").ToObject<JObject>().GetValue("content")
                .ToObject<JObject>().GetValue("data").ToObject<JObject>());
            
        }
        public ICover Header { get; }
        public override ICover GetHeader() => Header;

        ResourceItem VideoResource;
        public override ResourceItem GetResource()
        {
            return VideoResource;
        }
    }
}
