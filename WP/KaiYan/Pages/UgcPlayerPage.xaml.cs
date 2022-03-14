using KaiYan.Core.Page.Card.Resource;
using KaiYan.Class;
using KaiYan.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Playback;
using Windows.Storage.Streams;
using Windows.System.Display;
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
    public sealed partial class UgcPlayerPage : Page, ISwipeControlItem
    {
        public UgcPlayerPage()
        {
            this.InitializeComponent();
            
        }
        public System.Collections.ObjectModel.ObservableCollection<ImageViewItem> imageItems { get; } = new System.Collections.ObjectModel.ObservableCollection<ImageViewItem>();

        public string Description { get => (string)GetValue(DescriptionProperty); set { SetValue(DescriptionProperty, value); } }
        public static DependencyProperty DescriptionProperty { get; } = DependencyProperty.Register("Description", typeof(string), typeof(UgcPlayerPage), new PropertyMetadata(""));

        public string CoverUrl { get => (string)GetValue(CoverUrlProperty); set { SetValue(CoverUrlProperty, value); } }
        public static DependencyProperty CoverUrlProperty { get; } = DependencyProperty.Register("CoverUrl", typeof(string), typeof(UgcPlayerPage), new PropertyMetadata(""));


        public string HeaderName { get => (string)GetValue(HeaderNameProperty); set { SetValue(HeaderNameProperty, value); } }
        public static DependencyProperty HeaderNameProperty { get; } = DependencyProperty.Register("HeaderName", typeof(string), typeof(UgcPlayerPage), new PropertyMetadata(""));

        public string Area { get => (string)GetValue(AreaProperty); set { SetValue(AreaProperty, value); } }
        public static DependencyProperty AreaProperty { get; } = DependencyProperty.Register("Area", typeof(string), typeof(UgcPlayerPage), new PropertyMetadata(""));



        public long CreateTime { get => (long)GetValue(CreateTimeProperty); set { SetValue(CreateTimeProperty, value); } }
        public static DependencyProperty CreateTimeProperty { get; } = DependencyProperty.Register("CreateTime", typeof(long), typeof(UgcPlayerPage), new PropertyMetadata(0));

        public string HeaderIconUrl { get => (string)GetValue(HeaderIconUrlProperty); set { SetValue(HeaderIconUrlProperty, value); } }
        public static DependencyProperty HeaderIconUrlProperty { get; } = DependencyProperty.Register("HeaderIconUrl", typeof(string), typeof(UgcPlayerPage), new PropertyMetadata(""));

        public bool IsFollowed { get => (bool)GetValue(IsFollowedProperty); set { SetValue(IsFollowedProperty, value); } }
        public static DependencyProperty IsFollowedProperty { get; } = DependencyProperty.Register("IsFollowed", typeof(bool), typeof(UgcPlayerPage), new PropertyMetadata(false));

        public bool IsCollected { get => (bool)GetValue(IsCollectedProperty); set { SetValue(IsCollectedProperty, value); } }
        public static DependencyProperty IsCollectedProperty { get; } = DependencyProperty.Register("IsCollected", typeof(bool), typeof(UgcPlayerPage), new PropertyMetadata(false));


        public int CollectionCount { get => (int)GetValue(CollectionCountProperty); set { SetValue(CollectionCountProperty, value); } }
        public static DependencyProperty CollectionCountProperty { get; } = DependencyProperty.Register("CollectionCount", typeof(int), typeof(UgcPlayerPage), new PropertyMetadata(0));

        public int ReplyCount { get => (int)GetValue(ReplyCountProperty); set { SetValue(ReplyCountProperty, value); } }
        public static DependencyProperty ReplyCountProperty { get; } = DependencyProperty.Register("ReplyCount", typeof(int), typeof(UgcPlayerPage), new PropertyMetadata(0));

        public bool CanGoback => videoPlayerConcrol.IsFullWindow;

        UgcResourceItem ugcResourceItem;
        MediaPlayer MediaPlayer;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is UgcResourceItem ugcResourceItem)
            {
                MediaPlayer = videoPlayerConcrol.MediaPlayerElement.MediaPlayer;
                this.ugcResourceItem = ugcResourceItem;
                load();
            }
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            unload();
            base.OnNavigatedFrom(e);
        }

        private void load()
        {
            Description = ugcResourceItem.Description;
            CoverUrl = ugcResourceItem.DetailedCover;
            HeaderName = ugcResourceItem.Owner.Name;
            Area = ugcResourceItem.Area;
            HeaderIconUrl = ugcResourceItem.Owner.GetIcon();
            CreateTime = ugcResourceItem.CreateTime;
            CollectionCount = ugcResourceItem.CollectionCount;
            ReplyCount = ugcResourceItem.ReplyCount;
            IsFollowed = ugcResourceItem.Provider.IsFollowed;
            IsCollected = ugcResourceItem.IsCollected;
            if (ugcResourceItem is UgcPictureResource pictureResource)
            {
                videoPlayerConcrol.Visibility = Visibility.Collapsed;

                var urls = pictureResource.GetPictureUrls();
                for (int i = 0; i < urls.Count; i++)
                {
                    var url = urls[i];

                    imageItems.Add(new ImageViewItem() { ImageSource = new BitmapImage(new Uri(url)), IndexStr = (i + 1) + "/" + urls.Count, });
                }
            }
            else
                flipView.Visibility = Visibility.Collapsed;
        }
        private DisplayRequest appDisplayRequest = null;
        private void unload()
        {
          
            if (MediaPlayer.Source != null)
            {
                Task.Run(() =>
                {
                    
                    if (MediaPlayer.PlaybackSession.CanPause)
                        MediaPlayer.Pause();
                    var source = MediaPlayer.Source;
                    MediaPlayer.Source = null;
                    if (source != null)
                        ((MediaPlaybackItem)source).Source.Dispose();
                });
            }
            appDisplayRequest?.RequestRelease();
            appDisplayRequest = null;
            
        }


        private void setPlayUrl()
        {
            if (ugcResourceItem is UgcVideoResource)
            {
                Task.Run(() =>
                {
                    var url = (ugcResourceItem as UgcVideoResource).PlayUrl;

                    var _mediaPlaybackItem = new MediaPlaybackItem(Windows.Media.Core.MediaSource.CreateFromUri(new Uri(url)));
                    var props = _mediaPlaybackItem.GetDisplayProperties();
                    props.Type = Windows.Media.MediaPlaybackType.Video;
                    props.VideoProperties.Title = ugcResourceItem.Title;
                    props.VideoProperties.Subtitle = ugcResourceItem.Description;
                    props.Thumbnail = RandomAccessStreamReference.CreateFromUri(new Uri(ugcResourceItem.DetailedCover));
                    _mediaPlaybackItem.ApplyDisplayProperties(props);
                    MediaPlayer.Source = _mediaPlaybackItem;

                });
            }
        }

        private async void LikeSymbolIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            (sender as UIElement).Tapped -= LikeSymbolIcon_Tapped;
            try
            {
                if (await ugcResourceItem.SetCollectedAsync(!ugcResourceItem.IsCollected))
                {
                    IsCollected = ugcResourceItem.IsCollected;
                    CollectionCount = ugcResourceItem.CollectionCount;
                }

            }
            finally
            {
                (sender as UIElement).Tapped += LikeSymbolIcon_Tapped;
            }

        }


        private async void Follow_Button_Click(object sender, RoutedEventArgs e)
        {
            (sender as Control).IsEnabled = false;
            try
            {
                if (await ugcResourceItem.Owner.SetIsFollowedAsync(!IsFollowed))
                {
                    IsFollowed = ugcResourceItem.Owner.IsFollowed;
                }
            }
            finally
            {
                (sender as Control).IsEnabled = true;
            }
        }

        

        private void ReplySmbolIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MainContentControl.Current.SwipeControlShowPage(typeof(ReplyPage), ugcResourceItem, ElementTheme.Dark);
        }

        public void GoBack()
        {
            videoPlayerConcrol.IsFullWindow = false;
        }
        bool lastPlaying = false;

        public void GotViewFocus()
        {
            if(MediaPlayer.Source == null)
                setPlayUrl();
            if (appDisplayRequest == null)
            {
                appDisplayRequest = new DisplayRequest();
                appDisplayRequest.RequestActive();
            }
            if (lastPlaying)
                MediaPlayer.Play();
        }

        public void LostViewFocus()
        {
            appDisplayRequest?.RequestRelease();
            appDisplayRequest = null;
            lastPlaying = MediaPlayer.PlaybackSession.PlaybackState == MediaPlaybackState.Playing;
            MediaPlayer.Pause();
        }

        public void ViewPageLaunched(object lastReleasedViewPageparam)
        {
            //throw new NotImplementedException();
        }

        public void ReleasingViewPage(out object saveParam)
        {
            saveParam = null;
            //throw new NotImplementedException();
        }

        private void AppBarButton_Add_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement parent = sender as FrameworkElement;
            for (var i = 3; i > 0; i--)
            {
                parent = parent.Parent as FrameworkElement;
            }
            var MyScrollViewer = parent.FindName("MyScrollViewer") as ScrollViewer;
            MyScrollViewer.ChangeView(null, null, MyScrollViewer.ZoomFactor + 0.1f);
        }

        private void AppBarButton_Remove_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement parent = sender as FrameworkElement;
            for (var i = 3; i > 0; i--)
            {
                parent = parent.Parent as FrameworkElement;
            }
            var MyScrollViewer = parent.FindName("MyScrollViewer") as ScrollViewer;

            MyScrollViewer.ChangeView(null, null, MyScrollViewer.ZoomFactor - 0.1f);
        }




        private void MyImage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var image = sender as Image;
            var MyScrollViewer = image.Parent as ScrollViewer;
            if (e.NewSize.Width == 0 || e.NewSize.Height == 0)
            {
                MyScrollViewer.ChangeView(null, null, 1, true);
            }
            else
            {
                if (e.NewSize.Width / this.ActualWidth > e.NewSize.Height / this.ActualHeight)
                {
                    MyScrollViewer.ChangeView(null, null, (float)(this.ActualWidth / image.ActualWidth - 0.0000005f), true);
                }
                else
                {
                    MyScrollViewer.ChangeView(null, null, (float)(this.ActualHeight / image.ActualHeight - 0.0000005f), true);
                }
            }
        }

        private void myFlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void PersonPicture_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MainContentControl.Current.SwipeControlShowPage(typeof(PageManagerPage), ugcResourceItem.Owner);
        }
    }
}
