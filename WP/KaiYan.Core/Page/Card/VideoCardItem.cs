using KaiYan.Core.Page.Card.Resource;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Card
{
    public abstract class VideoCardItem: PlayCardItem, IVideoCardItem
    {
        public VideoCardItem(JObject jobject, JObject datajobject) : base(jobject,datajobject)
        {

        }
        public string DurationText => ((IVideoResource)GetResource()).DurationText;

        public string Category => GetVideoResource().Category;

        public VideoResource GetVideoResource() => (VideoResource)GetResource();

        // public abstract VideoResource GetResource();
    }
}
