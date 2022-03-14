using KaiYan.Core.Page.Body;
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
    public class AutoPlayFollowCardListItem : ResourceCardListItem
    {
        public AutoPlayFollowCardListItem(ICardItem cardItem) : base((IPlayCardItem)cardItem)
        {
            AutoPlayFollowCard = (AutoPlayFollowCard)cardItem;
            //DurationText = AutoPlayFollowCard.DurationText;
            FolllowHeader = new FolllowHeader(AutoPlayFollowCard.GetResource());

            ImageMaxHeight = 300;


        }
        private AutoPlayFollowCard AutoPlayFollowCard;
        public string Area => (AutoPlayFollowCard.GetResource() as UgcResourceItem).Area??"";
        public AutoPlayHeader Header => (AutoPlayHeader)AutoPlayFollowCard.Header;

        public FolllowHeader FolllowHeader { get; }
        public ResourceItem ResourceItem => AutoPlayFollowCard.GetResource();

        public double ImageMaxHeight { get => (double)GetValue(ImageMaxHeightProperty); set { SetValue(ImageMaxHeightProperty, value); } }
        public static DependencyProperty ImageMaxHeightProperty { get; } = DependencyProperty.Register("ImageMaxHeight", typeof(double), typeof(AutoPlayFollowCardListItem), new PropertyMetadata(0D));

        public override void Item_Size_Changed(object sender, SizeChangedEventArgs e)
        {
            base.Item_Size_Changed(sender, e);
            ImageMaxHeight = e.NewSize.Width * 0.6;
        }
    }
}
