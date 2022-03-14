using KaiYan.Templates;
using KaiYan.Templates.Models;
using Microsoft.Toolkit.Uwp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace KaiYan.Template
{
    class ListItemDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate followCardDataTemplate { get; set; }
        public DataTemplate pictureFollowCardDataTemplate { get; set; }
        public DataTemplate replyDataTemplate { get; set; }
        public DataTemplate squareCardCollectionDataTemplate { get; set; }
        public DataTemplate textCardDataTemplate { get; set; }
        public DataTemplate videoSmallCardDataTemplate { get; set; }
        public DataTemplate autoPlayFollowCardDataTemplate { get; set; }
        public DataTemplate briefCardListItemDataTemplate { get; set; }
        public DataTemplate horizontalScrollCardListItemDataTemplate { get; set; }
        public DataTemplate bannerCardListItemDataTemplate { get; set; }
        // private static ResourceDictionary sResources;
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var fe = container as FrameworkElement;
            var obj = item as CardListItem;
            DataTemplate dt = null;
            if (obj != null && fe != null)
            {
                
                switch (obj.CardItem.GetCardType())
                {
                    case KaiYan.Core.Page.Card.CardType.followCard:
                        dt = this.followCardDataTemplate;
                        break;
                    case KaiYan.Core.Page.Card.CardType.pictureFollowCard:
                        dt = pictureFollowCardDataTemplate;
                        break;
                    case KaiYan.Core.Page.Card.CardType.reply:
                        dt = replyDataTemplate;
                        break;
                    case KaiYan.Core.Page.Card.CardType.squareCardCollection:
                        dt = squareCardCollectionDataTemplate;
                        break;
                    case KaiYan.Core.Page.Card.CardType.textCard:
                        dt = textCardDataTemplate;
                        break;
                    case KaiYan.Core.Page.Card.CardType.videoSmallCard:
                        dt = videoSmallCardDataTemplate;
                        break;
                    case KaiYan.Core.Page.Card.CardType.autoPlayFollowCard:
                        dt = autoPlayFollowCardDataTemplate;
                        break;
                    case KaiYan.Core.Page.Card.CardType.briefCard:
                        dt = briefCardListItemDataTemplate;
                        break;
                    case KaiYan.Core.Page.Card.CardType.banner:
                        dt = bannerCardListItemDataTemplate;
                        break;
                    case KaiYan.Core.Page.Card.CardType.horizontalScrollCard:
                        dt = horizontalScrollCardListItemDataTemplate;
                        break;
                }
            }
            return dt;
        }
    }
}
