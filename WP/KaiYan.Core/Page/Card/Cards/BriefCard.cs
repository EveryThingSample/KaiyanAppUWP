using KaiYan.Core.Page.Body;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Card.Cards
{
    public class BriefCard :CardItem
    {
        internal BriefCard(JObject jobject) : base(jobject)
        {
            Data = new Tag(jobject.GetValue("data").ToObject<JObject>());
        }

        public Tag Data { get; }
    }
}
