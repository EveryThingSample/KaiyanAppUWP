using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Card
{
    public class SquareCardItem : CardItem
    {
        public SquareCardItem(JObject jobject) : base(jobject)
        {
            JObject datajobject = jobject.GetValue("data").ToObject<JObject>();
            Title = datajobject.GetValue("title").ToString();

        }

        public string Title { get; }

    }
}