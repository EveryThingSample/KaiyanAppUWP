using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Card
{
    public class CardItem:KaiYanBase,ICardItem
    {
        internal CardItem(JObject jobject):base(jobject)
        {
            if (jobject.ContainsKey("type"))
            {
                cardType = jobject.GetValue("type").ToObject<CardType>();
            }
        }

        internal CardType cardType;

        public CardType GetCardType() => cardType;
        public virtual string ActionUrl { get; }
    }
}
