using KaiYan.Core.Page;
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
    public sealed partial class PageManagerPage : Page, ITapList, IRefresh
    {

        public PageManagerPage()
        {
            this.InitializeComponent();
        }

        private IMainPageTool mainPageTool;

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is IMainPageTool mainPageTool)
            {
                this.mainPageTool = mainPageTool;
                var pageTools = await mainPageTool.GetPageToolsAsync();
                //if (pageTools.Count == 1)
                //{
                //    var frame = new Frame();
                //    (Content as Panel).Children.Clear();
                //    (Content as Panel).Children.Add(frame);
                //    frame.Navigate(typeof(ListPage), pageTools.First(), new Windows.UI.Xaml.Media.Animation.SuppressNavigationTransitionInfo());
                //}
                if (pageTools.Count > 5)
                {
                    navigationView.Visibility = Visibility.Visible;
                    frame.Visibility = Visibility.Collapsed;
                    myPivot.Visibility = Visibility.Collapsed;
                    foreach (var pageTool in pageTools)
                    {
                        var item = new Microsoft.UI.Xaml.Controls.NavigationViewItem();
                        item.Content = new TextBlock() { Text = pageTool.Name, FontSize = 17, };
                        item.DataContext = pageTool;
                        navigationView.MenuItems.Add(item);
                    }
                    navigationView.SelectedItem = navigationView.MenuItems[0];
                }
                else
                {
                    navigationView.Visibility = Visibility.Collapsed;
                    frame.Visibility = Visibility.Collapsed;
                    myPivot.Visibility = Visibility.Visible;
                    foreach (var pageTool in pageTools)
                    {
                        var frame = new Frame();

                        frame.Navigate(typeof(ListPage), pageTool);
                        myPivot.Items.Add(new PivotItem()
                        {
                            Header = new TextBlock() { Text = pageTool.Name, FontSize = 17, },
                            Content = frame,
                        });
                    }
                }
            }
            else if (e.Parameter is IPageTool pageTool)
            {
                if (pageTool is ReplyTool)
                    RequestedTheme = ElementTheme.Dark;
                navigationView.Visibility = Visibility.Collapsed;
                myPivot.Visibility = Visibility.Collapsed;
                frame.Visibility = Visibility.Visible;
                frame.Navigate(typeof(ListPage), pageTool, new Windows.UI.Xaml.Media.Animation.SuppressNavigationTransitionInfo());
            }
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if ((Content as Panel).Children.Count == 1 && (Content as Panel).Children.First() is Frame frame)
            {
                frame.Navigate(typeof(Page));
            }
            else if (myPivot.Items.Count > 0)
            {
                foreach(PivotItem pivotItem in myPivot.Items)
                {
                    var _frame = pivotItem.Content as Frame;
                    _frame.Navigate(typeof(Page));
                }
            }
            else if (navigationViewFrame.Content != null)
            {
                navigationViewFrame.Navigate(typeof(Page));
            }
        }

        private void myPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (sender as Pivot).SelectedItem as PivotItem;
            foreach(PivotItem item in (sender as Pivot).Items)
            {
                if (item == selectedItem)
                    ((item.Content as Frame).Content as ListPage).SelectedThisPage();
                else

                    ((item.Content as Frame).Content as ListPage).UnSelectedThisPage();
            }
        }

        

        private void navigationView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {

            if (sender.SelectedItem != null)
            {
                var pageTool = (sender.SelectedItem as FrameworkElement).DataContext as PageTool;
                navigationViewFrame.Navigate(typeof(ListPage), pageTool, new Windows.UI.Xaml.Media.Animation.SuppressNavigationTransitionInfo());
            }
        }

        public void SelectTapIndex(int i)
        {
            if (myPivot.Visibility == Visibility.Visible)
            {
                myPivot.SelectedIndex = i;
            }
            else if (navigationView.Visibility == Visibility.Visible)
            {
                navigationView.SelectedItem = navigationView.MenuItems[i];
            }
        }

        public void Refresh()
        {
            if (myPivot.Visibility == Visibility.Visible)
            {
                if (myPivot.SelectedItem is PivotItem pivotItem)
                {
                    ((pivotItem.Content as Frame).Content as IRefresh)?.Refresh();
                }
            }
            else if (navigationView.Visibility == Visibility.Visible)
            {

                (navigationViewFrame.Content as IRefresh)?.Refresh();

            }
            else
            {
                (frame.Content as IRefresh)?.Refresh();
            }
        }
    }
}
