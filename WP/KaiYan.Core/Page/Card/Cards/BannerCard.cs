using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Card.Cards
{
    public class BannerCard:CardItem
    {
        public BannerCard(JObject jobject):base(jobject)
        {
            Image = jobject.GetValue("data").ToObject<JObject>().GetValue("image").ToString();
            ActionUrl = jobject.GetValue("data").ToObject<JObject>().GetValue("actionUrl").ToString();
        }
        public string Image { get; }
        public override string ActionUrl { get; }
    }
}
