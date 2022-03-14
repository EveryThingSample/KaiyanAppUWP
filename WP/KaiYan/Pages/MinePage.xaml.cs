using KaiYan.Core;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace KaiYan.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MinePage : Page
    {
        public MinePage()
        {
            this.InitializeComponent();
            NameTextBlock.Text = Account.Current.name;
            avatarPicture.ProfilePicture = new BitmapImage(new Uri(Account.Current.avatar));
        }


        private void Logout_Button_Click(object sender, RoutedEventArgs e)
        {
            Account.Current.Logout();
            MainContentControl.Current.ReLoad();
        }


        private void History_HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.Current.SwipeControlShowPage(typeof(PageManagerPage), Account.Current.GetHistoryPageTool());
        }

        private void Colloct_HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.Current.SwipeControlShowPage(typeof(PageManagerPage), Account.Current.GetColloctionPageTool());
        }

        private void Follow_HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.Current.SwipeControlShowPage(typeof(PageManagerPage), Account.Current.GetFollowTabListTool());
        }
    }
}
