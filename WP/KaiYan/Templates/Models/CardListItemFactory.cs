using KaiYan.Core.Page.Card;
using KaiYan.Core.Page.Card.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Templates.Models
{
    static class CardListItemFactory
    {
        public static CardListItem Create(ICardItem cardItem)
        {
            CardListItem result = null;
            switch (cardItem?.GetCardType())
            {
                case KaiYan.Core.Page.Card.CardType.followCard:
                    result = new FollowCardListItem(cardItem);
                    break;
                case KaiYan.Core.Page.Card.CardType.pictureFollowCard:
                    result = new PictureFollowCardListItem(cardItem);
                    break;
                case KaiYan.Core.Page.Card.CardType.reply:
                    result = new ReplyCardListItem(cardItem);
                    break;
                case KaiYan.Core.Page.Card.CardType.squareCardCollection:
                    result = new SquareCardCollectionListItem(cardItem);
                    break;
                case KaiYan.Core.Page.Card.CardType.textCard:
                    result = new TextCardListItem(cardItem);
                    break;
                case KaiYan.Core.Page.Card.CardType.videoSmallCard:
                    result = new VideoSmallCardListItem(cardItem);
                    break;
                case CardType.autoPlayFollowCard:
                    result = new AutoPlayFollowCardListItem(cardItem);
                    break;
                case CardType.briefCard:
                    result = new BriefCardListItem(cardItem);
                    break;
                case CardType.banner:
                    result = new BannerCardListItem(cardItem);
                    break;
                case CardType.horizontalScrollCard:
                    result = new HorizontalScrollCardListItem(cardItem);
                    break;
            }
            return result;
        }
    }
}
