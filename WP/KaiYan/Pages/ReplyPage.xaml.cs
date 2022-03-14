using KaiYan.Core;
using KaiYan.Core.Page.Card.Resource;
using KaiYan.Core.Page.Reply;
using KaiYan.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace KaiYan.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReplyPage : Page, IRefresh
    {
        public ReplyPage()
        {
            this.InitializeComponent();
        }
        ResourceItem resourceItem;

        public string CoverUrl { get => (string)GetValue(CoverUrlProperty); set { SetValue(CoverUrlProperty, value); } }
        public static DependencyProperty CoverUrlProperty { get; } = DependencyProperty.Register("CoverUrl", typeof(string), typeof(ReplyPage), new PropertyMetadata(""));

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is ResourceItem resourceItem)
            {
                this.resourceItem = resourceItem;
                frame.Navigate(typeof(ListPage), resourceItem.GetReplyTool(), new Windows.UI.Xaml.Media.Animation.SuppressNavigationTransitionInfo());
            }
            if (Account.IsLogin)
            {
                loginButton.Visibility = Visibility.Collapsed;
                CoverUrl = Account.Current.avatar;
            }
            else
            {
                inputGrid.Visibility = Visibility.Collapsed;
            }
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if (frame.Content != null)
                frame.Navigate(typeof(Page));
        }



        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.Current.SwipeControlShowPage(typeof(LoginPage));
        }

        private async void ReplySuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            try
            {
                sender.IsEnabled = false;
                await resourceItem.ReplyAsync(sender.Text);
                sender.Text = "";
                Refresh();

            }
            finally
            {
                sender.IsEnabled = true;
            }
        }

        public void Refresh()
        {
            (frame.Content as IRefresh)?.Refresh();
        }
    }
}
