using EveryThingSampleTools.WP.UI.Controls;
using KaiYan.Core;
using KaiYan.Core.API;
using KaiYan.Core.Page.Card;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
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

namespace KaiYan
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage Current { get; private set; }
        public MainPage()
        {
            Current = this;
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
            ESInAppNotification = new ESInAppNotification();
            (this.Content as Panel).Children.Add(ESInAppNotification);
        }
        public ESInAppNotification ESInAppNotification { get; }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is Uri uri)
                MainContentControl.Protocol(uri);
        }
    }

}
