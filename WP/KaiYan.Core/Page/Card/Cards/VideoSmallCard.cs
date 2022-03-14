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
    public class VideoSmallCard: PlayCardItem , IVideoCardItem, ICardItem
    {
        internal VideoSmallCard(JObject jobject) : base(jobject, jobject.GetValue("data").ToObject<JObject>())
        {
            Resource = ResourceFactory.Create(jobject.GetValue("data").ToObject<JObject>());
            if (Resource.ResourceType == ResourceType.video)
            {
                var lastViewTime = jobject.GetValue("data").ToObject<JObject>().GetValue("lastViewTime").ToString();
                if (!string.IsNullOrEmpty(lastViewTime))
                    LastViewTime = long.Parse(lastViewTime) / 1000;
            }
            else if (Resource.ResourceType == ResourceType.ugc_video)
            {
                Title = Description;
            }
            else
                Title = Description;


        }
        public long LastViewTime { get; private set; }

        public string Category =>  (Resource as IVideoResource)?.Category;

        public string DurationText => (Resource as IVideoResource)?.DurationText;

        public override ICover GetHeader() => Resource.Provider;

        ResourceItem Resource;
        public override ResourceItem GetResource()
        {
            return Resource;
        }

        public VideoResource GetVideoResource() => (VideoResource)Resource;

    }
}
