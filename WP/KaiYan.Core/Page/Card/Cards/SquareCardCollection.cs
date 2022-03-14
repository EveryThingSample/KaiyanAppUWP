using KaiYan.Core.Page.Card.Headers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Card.Cards
{
    public class SquareCardCollection:CardItem,ICollectionCard
    {
        internal SquareCardCollection(JObject jobject):base(jobject)
        {
            this.jobject = jobject;
            Items = new List<ICardItem>();
            init(jobject.GetValue("data").ToObject<JObject>());
        }
        public IList<ICardItem> Items { get; }

        void init(JObject jobject)
        {
            Header = new SquareCardHeader(jobject.GetValue("header").ToObject<JObject>());
            foreach(var item in jobject.GetValue("itemList").ToArray())
            {
                Items.Add(CardItemFactory.GetCardItem(item.ToObject<JObject>()));
            }
        }
        public SquareCardHeader Header { get; private set; }
        
    }
}
