using KaiYan.Core.Page.Card.Resource;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Card
{
    public abstract class PlayCardItem : CardItem,IPlayCardItem
    {
        public PlayCardItem(JObject jobject, JObject datajobject) : base(jobject)
        {
            //if (datajobject.ContainsKey("title") == false)
            //    datajobject = 
            Title = datajobject.GetValue("title").ToObject<string>();
            Description = datajobject.GetValue("description")?.ToObject<string>();
        }
     
        public string Title { get; protected set; }
        public string Description { get; }
        public string Cover => GetResource().DetailedCover;

        public abstract ICover GetHeader();

        public abstract ResourceItem GetResource();
    }
}
