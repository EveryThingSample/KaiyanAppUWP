using EveryThingSampleTools.WP.UI.Controls;
using KaiYan.Core.Page;
using KaiYan.Core.Page.Card;
using KaiYan.Controls;
using KaiYan.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace KaiYan.Templates
{
    public abstract class CardListItem : DependencyObject
    {
  
        public CardListItem(ICardItem cardItem)
        {
            CardItem = cardItem;
            
        }

        

        public ICardItem CardItem { get; }

        public double Width { get => (double)GetValue(WidthProperty); internal set { SetValue(WidthProperty, value); } }
        public static DependencyProperty WidthProperty { get; } = DependencyProperty.Register("Width", typeof(double), typeof(CardListItem), new PropertyMetadata(0D));
        public double Height { get => (double)GetValue(HeightProperty);internal set { SetValue(HeightProperty, value); } }
        public static DependencyProperty HeightProperty { get; } = DependencyProperty.Register("Height", typeof(double), typeof(CardListItem), new PropertyMetadata(0D));


        public virtual void Item_Size_Changed(object sender, SizeChangedEventArgs e)
        {
            if (Width != e.NewSize.Width)
                Width = e.NewSize.Width;
            if (Height != e.NewSize.Height)
                Height = e.NewSize.Height;
        }
        public async virtual void CardListItem_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (CardItem.ActionUrl != null)
            {
                var tool = ProtocolHelper.CreateTool(CardItem.ActionUrl);
                if (tool is IMainPageTool || tool is IPageTool)
                {
                    MainContentControl.Current.SwipeControlShowPage(typeof(PageManagerPage), tool);
                }
                else if (tool is Uri uri)
                {
                    await Windows.System.Launcher.LaunchUriAsync(uri);
                }
                else
                {
                    await Windows.System.Launcher.LaunchUriAsync(new Uri(CardItem.ActionUrl));
                }
            }

        }

    }
}
