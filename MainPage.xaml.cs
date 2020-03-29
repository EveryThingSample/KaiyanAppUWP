using EveryThingSampleTools.UWP.UI;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    public class ListViewItemMode
    {
        public string flag { get; set; }
        public string txt { get; set; }
        public List<GridViewItemMode> gridViewItems { get; set; }
    }
    public class GridViewItemMode
    {
        public string pic { get; set; }
        public string title { get; set; }
        public string description { get; set; }
    }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        
        public MainPage()
        {
            this.InitializeComponent();

            this.RequestedTheme = ElementTheme.Light;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var listView = new EveryThingSampleTools.UWP.UI.ESListView(SetUI) { SelectionMode = ListViewSelectionMode.None};
            this.Content = listView;
            listView.ListItemsControl.ItemContentUnloaded += ListItemsControl_ItemContentUnloaded;
           

            var list = new List<GridViewItemMode>()
            {
                new GridViewItemMode() { title = "title=翻唱 Gnash「I Hate U, I Love U」", description = "翻唱 Gnash & Olivia O'brien「I Hate U, I Love U」", pic = "ms-Appx:///Assets/02f75189027055e0ef0918ab27acac62.jpg" },
                new GridViewItemMode() { title = "title=翻唱 Gnash「I Hate U, I Love U」", description = "翻唱 Gnash & Olivia O'brien「I Hate U, I Love U」", pic = "ms-Appx:///Assets/02f75189027055e0ef0918ab27acac62.jpg" },
                new GridViewItemMode() { title = "title=翻唱 Gnash「I Hate U, I Love U」", description = "翻唱 Gnash & Olivia O'brien「I Hate U, I Love U」", pic = "ms-Appx:///Assets/02f75189027055e0ef0918ab27acac62.jpg" },
                new GridViewItemMode() { title = "title=翻唱 Gnash「I Hate U, I Love U」", description = "翻唱 Gnash & Olivia O'brien「I Hate U, I Love U」", pic = "ms-Appx:///Assets/02f75189027055e0ef0918ab27acac62.jpg" },
                new GridViewItemMode() { title = "title=翻唱 Gnash「I Hate U, I Love U」", description = "翻唱 Gnash & Olivia O'brien「I Hate U, I Love U」", pic = "ms-Appx:///Assets/02f75189027055e0ef0918ab27acac62.jpg" },
                new GridViewItemMode() { title = "title=翻唱 Gnash「I Hate U, I Love U」", description = "翻唱 Gnash & Olivia O'brien「I Hate U, I Love U」", pic = "ms-Appx:///Assets/02f75189027055e0ef0918ab27acac62.jpg" },
                new GridViewItemMode() { title = "title=翻唱 Gnash「I Hate U, I Love U」", description = "翻唱 Gnash & Olivia O'brien「I Hate U, I Love U」", pic = "ms-Appx:///Assets/02f75189027055e0ef0918ab27acac62.jpg" },
                new GridViewItemMode() { title = "title=翻唱 Gnash「I Hate U, I Love U」", description = "翻唱 Gnash & Olivia O'brien「I Hate U, I Love U」", pic = "ms-Appx:///Assets/02f75189027055e0ef0918ab27acac62.jpg" },
            };
            for (var i = 0; i < 1000; i++)
            {
                listView.ListItemsControl.AddItemData(new ListViewItemMode() { flag = "txt", txt = "视频" });
                listView.ListItemsControl.AddItemData(new ListViewItemMode() { flag = "list", gridViewItems = list });
            }
        }


        private void ListItemsControl_ItemContentUnloaded(ListItemsControl sender, ItemContentUnloadedEventArgs args)
        {
        }

        private void SetUI(ListItemsControl sender, ItemContentLoadingEventArgs args)
        {
            var data = args.DataContext as ListViewItemMode;
            FrameworkElement content = null;
            switch (data.flag)
            {
                case "txt":
                    content = GetTxtUI(data.txt);
                    break;
                case "list":
                    content = GetGridView(data.gridViewItems);
                    break;
            }
            args.SetContent(content);
        }

        private FrameworkElement GetTxtUI(string txt)
        {
            return new TextBlock() { Text = txt, FontWeight = new Windows.UI.Text.FontWeight() { Weight = 700 } };
        }
        private FrameworkElement GetGridView(List<GridViewItemMode> gridViewItems)
        {
            return new GridViewDemo(gridViewItems);
        }
    }
}
