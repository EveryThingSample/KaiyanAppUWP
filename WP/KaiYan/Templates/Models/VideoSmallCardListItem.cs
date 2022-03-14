using KaiYan.Core.Page.Card;
using KaiYan.Core.Page.Card.Cards;
using KaiYan.Core.Page.Card.Resource;
using KaiYan.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace KaiYan.Templates.Models
{
    public class VideoSmallCardListItem : ResourceCardListItem
    {
        public VideoSmallCardListItem(ICardItem cardItem) : base((IVideoCardItem) cardItem)
        {
            VideoSmallCard = (VideoSmallCard)cardItem;
            DurationText = VideoSmallCard.DurationText;
        
            LastViewTime = VideoSmallCard.LastViewTime;
        }


        VideoSmallCard VideoSmallCard;
        
        public long LastViewTime { get => (long)GetValue(LastViewTimeProperty); set { SetValue(LastViewTimeProperty, value); } }
        public static DependencyProperty LastViewTimeProperty { get; } = DependencyProperty.Register("LastViewTime", typeof(long), typeof(VideoSmallCardListItem), new PropertyMetadata(0));

        
        public double ImageHeight { get => (double)GetValue(ImageHeightProperty); set { SetValue(ImageHeightProperty, value); } }
        public static DependencyProperty ImageHeightProperty { get; } = DependencyProperty.Register("ImageHeight", typeof(double), typeof(VideoSmallCardListItem), new PropertyMetadata(0D));

        public double HalfWidth { get => (double)GetValue(HalfWidthProperty); set { SetValue(HalfWidthProperty, value); } }
        public static DependencyProperty HalfWidthProperty { get; } = DependencyProperty.Register("HalfWidth", typeof(double), typeof(VideoSmallCardListItem), new PropertyMetadata(0D));

        public override void Item_Size_Changed(object sender, SizeChangedEventArgs e)
        {
            base.Item_Size_Changed(sender, e);
            HalfWidth = e.NewSize.Width / 2;
            ImageHeight = HalfWidth * 0.5625;
        }
    }
}
