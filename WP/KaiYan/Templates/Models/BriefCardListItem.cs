using KaiYan.Core.Page.Body;
using KaiYan.Core.Page.Card;
using KaiYan.Core.Page.Card.Cards;
using KaiYan.Controls;
using KaiYan.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace KaiYan.Templates.Models
{
    public class BriefCardListItem:CardListItem
    {
        public BriefCardListItem(ICardItem cardItem):base(cardItem)
        {
            BriefCard = (BriefCard)cardItem;
            BodyBaseItem = new BodyBaseItem(BriefCard.Data);
            Description = BriefCard.Data.Description;
            if (BriefCard.Data.BodyType == BodyType.author)
            {
                RoundImageVisibility = Visibility.Visible;
            }
            else if (BriefCard.Data.BodyType == BodyType.tag)
            {
                SquareImageVisibility = Visibility.Visible;
            }
            TagWidth = _width;
        }
        BriefCard BriefCard;

        public BodyBaseItem BodyBaseItem { get; }

        private static double _width;

        public string Description { get => (string)GetValue(DescriptionProperty); set { SetValue(DescriptionProperty, value); } }
        public static DependencyProperty DescriptionProperty { get; } = DependencyProperty.Register("Description", typeof(string), typeof(BriefCardListItem), new PropertyMetadata(""));


        public Visibility RoundImageVisibility { get => (Visibility)GetValue(RoundImageVisibilityProperty); set { SetValue(RoundImageVisibilityProperty, value); } }
        public static DependencyProperty RoundImageVisibilityProperty { get; } = DependencyProperty.Register("RoundImageVisibility", typeof(Visibility), typeof(BodyBaseItem), new PropertyMetadata(Visibility.Collapsed));

        public Visibility SquareImageVisibility { get => (Visibility)GetValue(SquareImageVisibilityProperty); set { SetValue(SquareImageVisibilityProperty, value); } }
        public static DependencyProperty SquareImageVisibilityProperty { get; } = DependencyProperty.Register("SquareImageVisibility", typeof(Visibility), typeof(BodyBaseItem), new PropertyMetadata(Visibility.Collapsed));

        public double TagWidth { get => (double)GetValue(TagWidthProperty); private set { SetValue(TagWidthProperty, value); } }
        public static DependencyProperty TagWidthProperty { get; } = DependencyProperty.Register("TagWidth", typeof(double), typeof(CardListItem), new PropertyMetadata(0D));

        public override void Item_Size_Changed(object sender, SizeChangedEventArgs e)
        {
            base.Item_Size_Changed(sender, e);
            if (e.NewSize.Width > 0)
                _width = e.NewSize.Width;
            TagWidth = e.NewSize.Width;
        }

        public override void CardListItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MainContentControl.Current.SwipeControlShowPage(typeof(PageManagerPage), BriefCard.Data);
        }


    }
}
