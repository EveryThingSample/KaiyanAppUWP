using EveryThingSampleTools.WP.UI.Controls;
using KaiYan.Core;
using KaiYan.Core.Page;
using KaiYan.Core.Page.Card.Resource;
using KaiYan.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace KaiYan.Controls
{
    public sealed partial class MainContentControl : UserControl
    {

        public MainContentControl()
        {
            Current = this;
            this.InitializeComponent();
            start();
        }
        public static MainContentControl Current { get; private set; }
        private PageManager pageManager;
        private async void start()
        {
            var build = new AccountBuilder();
            var account = await build.BuildAsync();
            pageManager = account.GetPageManager();
            frame0.Navigate(typeof(PageManagerPage), pageManager.GetRecomentMainPageTool());
            frame1.Navigate(typeof(PageManagerPage), pageManager.GetCategoryPageTool());
            frame2.Navigate(typeof(PageManagerPage), pageManager.GetUgcPageTool());
            if (Account.IsLogin)
                frame3.Navigate(typeof(MinePage));
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += MainContentControl_BackRequested;

        }

        public SwipeControl SwipeControl => swipeControl;

        public void PlayResource(ResourceItem resourceItem)
        {
            if (resourceItem is VideoResource videoResource)
            {
                PlayVideo(videoResource);
            }
            else if (resourceItem is UgcResourceItem ugcResource)
            {
                PlayUgcResource(ugcResource);
            }
        }

        private void PlayVideo(VideoResource videoResource, ElementTheme RequestedTheme = ElementTheme.Default)
        {
            swipeControl.ShowPage(typeof(PlayerPage), videoResource);
        }
        private void PlayUgcResource(UgcResourceItem ugcResource, ElementTheme RequestedTheme = ElementTheme.Default)
        {
            swipeControl.ShowPage(typeof(UgcPlayerPage), ugcResource);
        }
        public void SwipeControlShowPage(Type pageType, object parameter = null , ElementTheme requestedTheme = ElementTheme.Default)
        {
            swipeControl.ShowPage(pageType, parameter,requestedTheme);
        }
        
        //public void swipeControlGoBack()
        //{
        //    swipeControl.GoBack();
        //}

        public void ReLoad()
        {
            while(swipeControl.CanGoBack)
            {
                swipeControl.GoBack();
            }
            frame0.Navigate(typeof(PageManagerPage), pageManager.GetRecomentMainPageTool());
            frame1.Navigate(typeof(PageManagerPage), pageManager.GetCategoryPageTool());
            frame2.Navigate(typeof(PageManagerPage), pageManager.GetUgcPageTool());
            if (Account.IsLogin)
                frame3.Navigate(typeof(MinePage));
            mainContentFooter.SelectedIndex = 0;
        }



        private void MainContentFooter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            foreach(int i in e.AddedItems)
            {
                if (i >= 0)
                {
                    var frame = this.FindName("frame" + i) as Frame;
                    frame.Visibility = Visibility.Visible;
                }

            }
            foreach (int i in e.RemovedItems)
            {
                if (i >= 0)
                {
                    var frame = this.FindName("frame" + i) as Frame;
                    frame.Visibility = Visibility.Collapsed;
                }
            }
            if ((sender as MainContentFooter).SelectedIndex == 3)
            {
                if (!Account.IsLogin)
                {
                    SwipeControlShowPage(typeof(LoginPage));
                    (sender as MainContentFooter).SelectedIndex = (int)e.RemovedItems.First();
                }
            }
        }

        private long timeForBackRequested;
        private void MainContentControl_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            e.Handled = true;
            if (swipeControl.CanGoBack)
                swipeControl.GoBack();
            else
            {
                var currentTime = EveryThingSampleTools.WP.Tools.TimeTool.NowTimeStamp;
                if (currentTime - timeForBackRequested < 2)
                {
                    App.Current.Exit();
                }else
                {
                    timeForBackRequested = currentTime;
                    MainPage.Current.ESInAppNotification.ShowInAppNotification("再按一次退出", 2000);
                }
            }
        }


        public void Protocol(Uri uri)
        {
            switch(uri.Host)
            {
                case "homepage":
                    mainContentFooter.SelectedIndex = 0;
                    if (uri.LocalPath == "/selected")
                    {
                        var tabIndexStr = uri.Query.Substring(uri.Query.IndexOf("tabIndex=") + 9);
                        tabIndexStr = tabIndexStr.Substring(0, tabIndexStr.IndexOf("&"));
                        int tabIndex = int.Parse(tabIndexStr);
                        if (frame0.Content is ITapList tapList)
                        {
                            tapList.SelectTapIndex(tabIndex - 1);
                        }
                    }
                    break;
            }
        }

        private void mainContentFooter_DoubleSelected(MainContentFooter sender, int args)
        {
            if (args >= 0)
            {
                var frame = this.FindName("frame" + args) as Frame;
                (frame.Content as IRefresh)?.Refresh();
            }
        }
    }
}
