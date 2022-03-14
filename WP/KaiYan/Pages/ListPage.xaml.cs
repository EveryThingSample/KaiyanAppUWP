using KaiYan.Core;
using KaiYan.Core.Page;
using KaiYan.Core.Page.Card;
using KaiYan.Core.Page.Reply;
using KaiYan.Controls;
using KaiYan.Templates;
using KaiYan.Templates.Models;
using Microsoft.Toolkit.Collections;
using Microsoft.Toolkit.Uwp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace KaiYan.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListPage : Page, IRefresh
    {
        CardListIncrementalLoadingCollection CardListItems { get; } = new CardListIncrementalLoadingCollection();
        public static ListPage Current { get; private set; }
        public ListPage()
        {
            this.InitializeComponent();
            Current = this;
       }


        IPageTool pageTool;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is IPageTool pageTool)
            {
                pageTool.Reset();
                this.pageTool = pageTool;
                CardListItems.SetPageTool(pageTool);
                CardListItems.Source.Loading += Source_Loading;
                CardListItems.Source.NoMore += Source_NoMore;
            }
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            CardListItems.Source.Loading -= Source_Loading;
            CardListItems.Source.NoMore -= Source_NoMore;
            listView.ItemsSource = null;
        }
        public void SelectedThisPage()
        {
            CardListItems.Source.Blocked = false;
        }
        /// <summary>
        /// 防止可能会发生的无限加载
        /// </summary>
        public void UnSelectedThisPage()
        {

            CardListItems.Source.Blocked = true;
        }

        private void Source_NoMore(CardListItemSource sender, object args)
        {
            theEndTextBlock.Visibility = Visibility.Visible;
        }

        private void Source_Loading(CardListItemSource sender, bool args)
        {
            loadingESProgressRing.IsActive = args;
            
        }


        private async void ShowReplyBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!Account.IsLogin)
            {
                MainContentControl.Current.SwipeControlShowPage(typeof(LoginPage));
            }

            var replyAutoSuggestBox = ((sender as FrameworkElement).Parent as FrameworkElement).FindName("replyAutoSuggestBox") as AutoSuggestBox;
            replyAutoSuggestBox.Visibility = Visibility.Visible;
            await Task.Delay(100);
            replyAutoSuggestBox.Focus(FocusState.Keyboard);
        }

        private void replyAutoSuggestBox_LostFocus(object sender, RoutedEventArgs e)
        {
            (sender as FrameworkElement).Visibility = Visibility.Collapsed;
        }

        private async void replyAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            sender.Visibility = Visibility.Collapsed;
            var text = sender.Text;
            var cardItem = sender.DataContext as ReplyCardListItem;
            if (await (cardItem.replyItem as ReplyItem).ReplyAsync(text))
            {
                sender.Text = "";
                pageTool.Reset();
                CardListItems.RefreshAsync();
            }
        }


        private async void Refresh_Border_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                (sender as UIElement).Tapped -= Refresh_Border_Tapped;
                pageTool.Reset();
                await CardListItems.RefreshAsync();
            }
            finally
            {
                (sender as UIElement).Tapped += Refresh_Border_Tapped;
            }
        }
        public async void Refresh()
        {
            pageTool.Reset();
            await CardListItems.RefreshAsync();
        }
    }
}
