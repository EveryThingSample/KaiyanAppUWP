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
    public class PictureFollowCard : PlayCardItem,IPictureCard
    {
        internal PictureFollowCard(JObject jobject) : base(jobject, jobject.GetValue("data").ToObject<JObject>().GetValue("content")
                .ToObject<JObject>().GetValue("data").ToObject<JObject>())
        {
            Header = new AutoPlayHeader(jobject.GetValue("data").ToObject<JObject>().GetValue("header").ToObject<JObject>());
            resource = new UgcPictureResource(jobject.GetValue("data").ToObject<JObject>().GetValue("content")
                .ToObject<JObject>().GetValue("data").ToObject<JObject>());
        }

        public ICover Header { get; }

        UgcPictureResource resource;
        public UgcPictureResource GetPictureResource()
        {
            return resource;
        }

        public override ICover GetHeader() => Header;

        public override ResourceItem GetResource() => GetPictureResource();

    }
}
