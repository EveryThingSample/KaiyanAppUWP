using KaiYan.Core.Page.Card;
using KaiYan.Core.Page.Card.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace KaiYan.Templates.Models
{
    public  class BannerCardListItem: CardListItem
    {
        public BannerCardListItem(ICardItem cardItem):base(cardItem)
        {
            bannerCard = (BannerCard)cardItem;
            ImageUrl = bannerCard.Image;
        }
        BannerCard bannerCard;
        public string ImageUrl { get => (string)GetValue(ImageUrlProperty); set { SetValue(ImageUrlProperty, value); } }
        public static DependencyProperty ImageUrlProperty { get; } = DependencyProperty.Register("ImageUrl", typeof(string), typeof(BannerCardListItem), new PropertyMetadata(""));
        public override void Item_Size_Changed(object sender, SizeChangedEventArgs e)
        {

            if (Width != e.NewSize.Width)
            {
                Width = e.NewSize.Width;
                Height = Width / 1.725;
            }
        }

    }
}
