using KaiYan.Core.Page.Card;
using KaiYan.Core.Page.Reply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace KaiYan.Templates.Models
{
    public class ReplyCardListItem: CardListItem
    {
        public ReplyCardListItem(ICardItem cardItem) : base(cardItem)
        {
            
            replyItem = (IReplyItem)cardItem;
            Message = replyItem.Message;
            if (replyItem is ReplyItem _replyItem)
            {
                CreateTime = _replyItem.CreateTime;
                LikeCount = _replyItem.LikeCount;
                IsLiked = _replyItem.IsLiked;
            }
            UserName = replyItem.User.Name;
            UserIconUrl = replyItem.User.Avatar;
            if (replyItem.ParentReply != null)
            {
                Parent = new ReplyCardListItem(replyItem.ParentReply);
                ParentVisibility = Visibility.Visible;
            }
        }
        public IReplyItem replyItem { get; }
        public string Message { get => (string)GetValue(MessageProperty); set { SetValue(MessageProperty, value); } }
        public static DependencyProperty MessageProperty { get; } = DependencyProperty.Register("Message", typeof(string), typeof(ReplyCardListItem), new PropertyMetadata(""));

        public long CreateTime { get => (long)GetValue(CreateTimeProperty); set { SetValue(CreateTimeProperty, value); } }
        public static DependencyProperty CreateTimeProperty { get; } = DependencyProperty.Register("CreateTime", typeof(long), typeof(ReplyCardListItem), new PropertyMetadata(0));

        public int LikeCount { get => (int)GetValue(LikeCountProperty); set { SetValue(LikeCountProperty, value); } }
        public static DependencyProperty LikeCountProperty { get; } = DependencyProperty.Register("LikeCount", typeof(int), typeof(ReplyCardListItem), new PropertyMetadata(0));


        public string UserName { get => (string)GetValue(UserNameProperty); set { SetValue(UserNameProperty, value); } }
        public static DependencyProperty UserNameProperty { get; } = DependencyProperty.Register("UserName", typeof(string), typeof(ReplyCardListItem), new PropertyMetadata(""));

        public string UserIconUrl { get => (string)GetValue(UserIconUrlProperty); set { SetValue(UserIconUrlProperty, value); } }
        public static DependencyProperty UserIconUrlProperty { get; } = DependencyProperty.Register("UserIconUrl", typeof(string), typeof(ReplyCardListItem), new PropertyMetadata(""));
        
        public bool IsLiked { get => (bool)GetValue(IsLikedProperty); set { SetValue(IsLikedProperty, value); } }
        public static DependencyProperty IsLikedProperty { get; } = DependencyProperty.Register("IsLiked", typeof(bool), typeof(ReplyCardListItem), new PropertyMetadata(false));

        public ReplyCardListItem Parent { get => (ReplyCardListItem)GetValue(ParentProperty); set { SetValue(ParentProperty, value); } }
        public static DependencyProperty ParentProperty { get; } = DependencyProperty.Register("Parent", typeof(ReplyCardListItem), typeof(ReplyCardListItem), new PropertyMetadata(null));

        public Visibility ParentVisibility { get => (Visibility)GetValue(ParentVisibilityProperty); set { SetValue(ParentVisibilityProperty, value); } }
        public static DependencyProperty ParentVisibilityProperty { get; } = DependencyProperty.Register("ParentVisibility", typeof(bool), typeof(ReplyCardListItem), new PropertyMetadata(Visibility.Collapsed));


        public async void LikeTypped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs args)
        {
            if (await (replyItem as ReplyItem).SetIsLikedAsync(!IsLiked))
            {
                IsLiked = (replyItem as ReplyItem).IsLiked;
                LikeCount = (replyItem as ReplyItem).LikeCount;
            }
        }
    }
}
