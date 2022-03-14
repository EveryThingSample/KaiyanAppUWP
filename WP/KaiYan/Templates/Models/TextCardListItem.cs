using KaiYan.Core.Page;
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
    public class TextCardListItem:CardListItem
    {
        public TextCardListItem(ICardItem cardItem):base(cardItem)
        {
            TextCard = (TextCard)cardItem;
            Text = TextCard.Text;
            switch(TextCard.TextCardType)
            {
                case TextCardType.header8:
                case TextCardType.header7:
                case TextCardType.header6:
                case TextCardType.header5:
                case TextCardType.header4:
                case TextCardType.header3:
                case TextCardType.header2:
                case TextCardType.header1:
                    FontSize = 20;
                    TextHorizontalAlignment = HorizontalAlignment.Left;
                    break;
                case TextCardType.footer2:
                case TextCardType.footer3:
                case TextCardType.footer1:
                    FontSize = 15;
                    TextHorizontalAlignment = HorizontalAlignment.Right;
                    break;
            }
        }
        public TextCard TextCard { get; }
        
        

        public string Text { get => (string)GetValue(TextProperty); set { SetValue(TextProperty, value); } }
        public static DependencyProperty TextProperty { get; } = DependencyProperty.Register("Text", typeof(string), typeof(TextCardListItem), new PropertyMetadata(""));

        public double FontSize { get => (double)GetValue(FontSizeProperty); set { SetValue(FontSizeProperty, value); } }
        public static DependencyProperty FontSizeProperty { get; } = DependencyProperty.Register("FontSize", typeof(double), typeof(TextCardListItem), new PropertyMetadata(20d));

        public HorizontalAlignment TextHorizontalAlignment { get => (HorizontalAlignment)GetValue(TextHorizontalAlignmentProperty); set { SetValue(TextHorizontalAlignmentProperty, value); } }
        public static DependencyProperty TextHorizontalAlignmentProperty { get; } = DependencyProperty.Register("TextHorizontalAlignment", typeof(HorizontalAlignment), typeof(TextCardListItem), new PropertyMetadata(HorizontalAlignment.Left));



    }
}
