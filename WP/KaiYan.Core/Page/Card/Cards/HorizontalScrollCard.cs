using KaiYan.Core.API;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Card.Cards
{
    public class HorizontalScrollCard : CardItem
    {
        public HorizontalScrollCard(JObject jobject) : base(jobject)
        {

            var jitemList = jobject.GetValue("data").ToObject<JObject>().GetValue("itemList").ToArray();

            Items = new List<ICardItem>(jitemList.Length);
            foreach (var item in jitemList)
            {
                Items.Add(CardItemFactory.GetCardItem(item.ToObject<JObject>()));
            }
        }

        public IList<ICardItem> Items { get; }


     
    }
}
