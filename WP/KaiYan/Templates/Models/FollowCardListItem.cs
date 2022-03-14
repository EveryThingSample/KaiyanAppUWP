using KaiYan.Core.Page.Card;
using KaiYan.Core.Page.Card.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace KaiYan.Templates.Models
{
    public class FollowCardListItem : ResourceCardListItem
    {
        public FollowCardListItem(ICardItem cardItem) : base((IVideoCardItem)cardItem)
        {
            FollowCard = (FollowCard)cardItem;
            //DurationText = FollowCard.GetVideoResource().DurationText;
        }
        private FollowCard FollowCard;

    }
}
