using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Card.Cards
{
    public class TextCard : CardItem
    {
        internal TextCard(JObject jobject) : base(jobject)
        {
            var data = jobject.GetValue("data").ToObject<JObject>();
            TextCardType = data.GetValue("type").ToObject<TextCardType>();
            Text = data.GetValue("text").ToObject<string>();
            ActionUrl = data.GetValue("actionUrl").ToObject<string>();
        }
        public TextCardType TextCardType { get; }
        public string Text { get; }
        public override string ActionUrl { get; }
    }

}
