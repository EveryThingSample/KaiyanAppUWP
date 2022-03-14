using KaiYan.Core.Page.Card;
using KaiYan.Core.Page.Card.Cards;
using KaiYan.Core.Page.Card.Headers;
using KaiYan.Core.Page.Card.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace KaiYan.Templates.Models
{
    public class PictureFollowCardListItem : ResourceCardListItem
    {
        public PictureFollowCardListItem(ICardItem cardItem) : base((IPlayCardItem)cardItem)
        {
            PictureFollowCard = (PictureFollowCard)cardItem;
            //DurationText = AutoPlayFollowCard.DurationText;
            FolllowHeader = new FolllowHeader(PictureFollowCard.GetResource());

            ImageMaxHeight = 300;
            PictureCount = PictureFollowCard.GetPictureResource().GetPictureUrls().Count;

        }
        private PictureFollowCard PictureFollowCard;
        public string Area => (PictureFollowCard.GetResource() as UgcResourceItem).Area ?? "";
        public AutoPlayHeader Header => (AutoPlayHeader)PictureFollowCard.Header;

        public FolllowHeader FolllowHeader { get; }
        public ResourceItem ResourceItem => PictureFollowCard.GetResource();

        public int PictureCount { get => (int)GetValue(PictureCountProperty); set { SetValue(PictureCountProperty, value); } }
        public static DependencyProperty PictureCountProperty { get; } = DependencyProperty.Register("PictureCount", typeof(int), typeof(PictureFollowCardListItem), new PropertyMetadata(0));



        public double ImageMaxHeight { get => (double)GetValue(ImageMaxHeightProperty); set { SetValue(ImageMaxHeightProperty, value); } }
        public static DependencyProperty ImageMaxHeightProperty { get; } = DependencyProperty.Register("ImageMaxHeight", typeof(double), typeof(AutoPlayFollowCardListItem), new PropertyMetadata(0D));



        public override void Item_Size_Changed(object sender, SizeChangedEventArgs e)
        {
            base.Item_Size_Changed(sender, e);
            ImageMaxHeight = e.NewSize.Width * 0.6;
        }
    }
}
