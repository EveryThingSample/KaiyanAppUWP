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
    public class SquareCardCollectionListItem: CardListItem
    {
        public SquareCardCollectionListItem(ICardItem cardItem) : base(cardItem)
        {
            SquareCardCollection = (SquareCardCollection)cardItem;
            Items = new List<CardListItem>();
            foreach(var card in SquareCardCollection.Items)
            {
                Items.Add(CardListItemFactory.Create(card));
            }
            Title = SquareCardCollection.Header.Title;
            SubTitle = SquareCardCollection.Header.SubTitle;
            RightText = SquareCardCollection.Header.RightText;

        }
        SquareCardCollection SquareCardCollection;
        public IList<CardListItem> Items { get; }

        public string Title { get => (string)GetValue(TitleProperty); set { SetValue(TitleProperty, value); } }
        public static DependencyProperty TitleProperty { get; } = DependencyProperty.Register("Title", typeof(string), typeof(ResourceCardListItem), new PropertyMetadata(""));

        public string SubTitle { get => (string)GetValue(SubTitleProperty); set { SetValue(SubTitleProperty, value); } }
        public static DependencyProperty SubTitleProperty { get; } = DependencyProperty.Register("SubTitle", typeof(string), typeof(ResourceCardListItem), new PropertyMetadata(""));

        public string RightText { get => (string)GetValue(RightTextProperty); set { SetValue(RightTextProperty, value); } }
        public static DependencyProperty RightTextProperty { get; } = DependencyProperty.Register("RightText", typeof(string), typeof(ResourceCardListItem), new PropertyMetadata(""));

        public async override void CardListItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri(SquareCardCollection.Header.ActionUrl));
        }

    }
}
