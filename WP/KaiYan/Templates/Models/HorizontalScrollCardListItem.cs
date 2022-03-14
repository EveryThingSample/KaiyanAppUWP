using KaiYan.Core.Page.Card;
using KaiYan.Core.Page.Card.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Templates.Models
{
    class HorizontalScrollCardListItem : CardListItem
    {
        public HorizontalScrollCardListItem(ICardItem cardItem) : base(cardItem)
        {
            HorizontalScrollCard = (HorizontalScrollCard)cardItem;
            Items = new List<CardListItem>();
            foreach (var card in HorizontalScrollCard.Items)
            {
                var item = CardListItemFactory.Create(card);
                item.Width = 345;
                item.Height = 200;
                Items.Add(item);
            }

        }
        HorizontalScrollCard HorizontalScrollCard;
        public IList<CardListItem> Items { get; }
    }
}