using EveryThingSampleTools.WP.UI.Controls;
using KaiYan.Core.Page.Card.Resource;
using KaiYan.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Media.Playback;
using Windows.Storage.Streams;
using Windows.System.Display;
using Windows.UI.ViewManagement;
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
    public sealed partial class PlayerPage : Page, ISwipeControlItem
    {
        MediaPlayer mediaPlayer { get; }
        public PlayerPage()
        {
            this.InitializeComponent();
            mediaPlayer = videoPlayerConcrol.MediaPlayerElement.MediaPlayer;
            mediaPlayer.MediaOpened += MediaPlayer_MediaOpened;
        }


        private DisplayRequest appDisplayRequest = null;
        VideoResource videoResource;

        public Uri BlurredCoverUri { get => (Uri)GetValue(BlurredCoverUriProperty); set { SetValue(BlurredCoverUriProperty, value); } }
        public static DependencyProperty BlurredCoverUriProperty { get; } = DependencyProperty.Register("BlurredCoverUri", typeof(Uri), typeof(PlayerPage), new PropertyMetadata(null));

        public string Title { get => (string)GetValue(TitleProperty); set { SetValue(TitleProperty, value); } }
        public static DependencyProperty TitleProperty { get; } = DependencyProperty.Register("Title", typeof(string), typeof(PlayerPage), new PropertyMetadata(""));

        public string Description { get => (string)GetValue(DescriptionProperty); set { SetValue(DescriptionProperty, value); } }
        public static DependencyProperty DescriptionProperty { get; } = DependencyProperty.Register("Description", typeof(string), typeof(PlayerPage), new PropertyMetadata(""));

        public string CoverUrl { get => (string)GetValue(CoverUrlProperty); set { SetValue(CoverUrlProperty, value); } }
        public static DependencyProperty CoverUrlProperty { get; } = DependencyProperty.Register("CoverUrl", typeof(string), typeof(PlayerPage), new PropertyMetadata(""));


        public string HeaderName { get => (string)GetValue(HeaderNameProperty); set { SetValue(HeaderNameProperty, value); } }
        public static DependencyProperty HeaderNameProperty { get; } = DependencyProperty.Register("HeaderName", typeof(string), typeof(PlayerPage), new PropertyMetadata(""));

        public string HeaderDescription { get => (string)GetValue(HeaderDescriptionProperty); set { SetValue(HeaderDescriptionProperty, value); } }
        public static DependencyProperty HeaderDescriptionProperty { get; } = DependencyProperty.Register("HeaderDescription", typeof(string), typeof(PlayerPage), new PropertyMetadata(""));

        public string HeaderIconUrl { get => (string)GetValue(HeaderIconUrlProperty); set { SetValue(HeaderIconUrlProperty, value); } }
        public static DependencyProperty HeaderIconUrlProperty { get; } = DependencyProperty.Register("HeaderIconUrl", typeof(string), typeof(PlayerPage), new PropertyMetadata(""));

        public bool IsFollowed { get => (bool)GetValue(IsFollowedProperty); set { SetValue(IsFollowedProperty, value); } }
        public static DependencyProperty IsFollowedProperty { get; } = DependencyProperty.Register("IsFollowed", typeof(bool), typeof(PlayerPage), new PropertyMetadata(false));


        public string Category { get => (string)GetValue(CategoryProperty); set { SetValue(CategoryProperty, value); } }
        public static DependencyProperty CategoryProperty { get; } = DependencyProperty.Register("Category", typeof(string), typeof(PlayerPage), new PropertyMetadata(""));

        public string DurationText { get => (string)GetValue(DurationTextProperty); set { SetValue(DurationTextProperty, value); } }
        public static DependencyProperty DurationTextProperty { get; } = DependencyProperty.Register("DurationText", typeof(string), typeof(PlayerPage), new PropertyMetadata(""));

        public bool IsCollected { get => (bool)GetValue(IsCollectedProperty); set { SetValue(IsCollectedProperty, value); } }
        public static DependencyProperty IsCollectedProperty { get; } = DependencyProperty.Register("IsCollected", typeof(bool), typeof(PlayerPage), new PropertyMetadata(false));

        public int CollectionCount { get => (int)GetValue(CollectionCountProperty); set { SetValue(CollectionCountProperty, value); } }
        public static DependencyProperty CollectionCountProperty { get; } = DependencyProperty.Register("CollectionCount", typeof(int), typeof(PlayerPage), new PropertyMetadata(0));

        public int ShareCount { get => (int)GetValue(ShareCountProperty); set { SetValue(ShareCountProperty, value); } }
        public static DependencyProperty ShareCountProperty { get; } = DependencyProperty.Register("ShareCount", typeof(int), typeof(PlayerPage), new PropertyMetadata(0));

        private bool isViewCurrent = false, isViewLaunched = false;
        public bool CanGoback => videoPlayerConcrol.IsFullWindow;
        private bool isLastTimePlaying = false;
        private bool AutoPlay = true;
        PlayInfo currentPlayInfo;
        long registerPropertyChangedCallbackToken;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is VideoResource videoResource)
            {
                this.videoResource = videoResource;
                start();
            }
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            unload();
            videoPlayerConcrol.UnregisterPropertyChangedCallback(VideoPlayerConcrol.SelectedPlayInfoProperty, registerPropertyChangedCallbackToken);
        }

        private void start()
        {
            setCover();
        }
        private void setCover()
        {
            BlurredCoverUri = new Uri(videoResource.BlurredCover);

            Title = videoResource.Title;
            Description = videoResource.Description;
            CoverUrl = videoResource.DetailedCover;
            DurationText = videoResource.DurationText;
            Category = videoResource.Category;

            HeaderName = videoResource.Author.GetTitle();
            HeaderDescription = videoResource.Author.GetDescription();
            HeaderIconUrl = videoResource.Author.GetIcon();
            IsCollected = videoResource.IsCollected;
            CollectionCount = videoResource.CollectionCount;
            ShareCount = videoResource.ShareCount;
            IsFollowed = videoResource.Author.IsFollowed;
            videoPlayerConcrol.PlayInfos.Clear();

            foreach (var item in videoResource.GetPlayInfos())
                videoPlayerConcrol.PlayInfos.Add(item);
            videoPlayerConcrol.SelectedPlayInfo = videoResource.GetPlayInfos().First();
            registerPropertyChangedCallbackToken = videoPlayerConcrol.RegisterPropertyChangedCallback(VideoPlayerConcrol.SelectedPlayInfoProperty, (d, e) => {
                selectPlayInfo(videoPlayerConcrol.SelectedPlayInfo);
            });
            ReplySuggestBox.Text = "";
        }


        private void unload()
        {
            BlurredCoverUri = null;
            if (mediaPlayer.Source != null)
            {
                Task.Run(() =>
                {
                    videoResource.ReportViewTime((int)mediaPlayer.PlaybackSession.Position.TotalSeconds);
                    if (mediaPlayer.PlaybackSession.CanPause)
                        mediaPlayer.Pause();
                    mediaPlayer.MediaOpened -= MediaPlayer_MediaOpened;
                    var source = mediaPlayer.Source;
                    mediaPlayer.Source = null;
                    if (source != null)
                        ((MediaPlaybackItem)source).Source.Dispose();
                });
            }
            appDisplayRequest?.RequestRelease();
            appDisplayRequest = null;
            relatedFrame.Navigate(typeof(Page), null, new Windows.UI.Xaml.Media.Animation.SuppressNavigationTransitionInfo());
            replyFrame.Navigate(typeof(Page), null, new Windows.UI.Xaml.Media.Animation.SuppressNavigationTransitionInfo());
        }


        private void MediaPlayer_MediaOpened(MediaPlayer sender, object args)
        {
            var time = videoResource.GetViewTime();
            if (time > 0)
                sender.PlaybackSession.Position = TimeSpan.FromSeconds(time);
        }

        private void selectPlayInfo(PlayInfo item)
        {if (item == null) return;
            Task.Run(() =>
            {

                var _mediaPlaybackItem = new MediaPlaybackItem(Windows.Media.Core.MediaSource.CreateFromUri(new Uri(item.url)));
                var props = _mediaPlaybackItem.GetDisplayProperties();
                props.Type = Windows.Media.MediaPlaybackType.Video;
                props.VideoProperties.Title = videoResource.Title;
                props.VideoProperties.Subtitle = videoResource.Description;
                props.Thumbnail = RandomAccessStreamReference.CreateFromUri(new Uri(videoResource.DetailedCover));
                _mediaPlaybackItem.ApplyDisplayProperties(props);
                mediaPlayer.Source = _mediaPlaybackItem;

            });
            if (BlurredCoverUri == null)
                BlurredCoverUri = new Uri(videoResource.BlurredCover);
        }

        private void setReplyAndRelatedVideo()
        {
            relatedFrame.Navigate(typeof(ListPage), videoResource.GetRelatedVideoLoadTool(), new Windows.UI.Xaml.Media.Animation.SuppressNavigationTransitionInfo());
            replyFrame.Navigate(typeof(ListPage), videoResource.GetReplyTool(), new Windows.UI.Xaml.Media.Animation.SuppressNavigationTransitionInfo());

        }

        //private void MediaControl_RootGrid_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    var RootGrid = sender as Grid;
        //    var BottomTransportContentControl= RootGrid.FindName("BottomTransportContentControl") as TransportContentControl;
        //    var position = e.GetPosition(RootGrid);
        //    if (BottomTransportContentControl.IsClosed || RootGrid.ActualHeight - position.Y > BottomTransportContentControl.ActualHeight)
        //    {
        //        if (BottomTransportContentControl.IsClosed)
        //            BottomTransportContentControl.Show();
        //        else if (BottomTransportContentControl.IsOpened)
        //            BottomTransportContentControl.Hide();
        //    }
        //}
       
        private void BottomTransportContentControl_Closing(FlyContent sender, ClosingEventArgs args)
        {
            if (mediaPlayer?.PlaybackSession.PlaybackState != MediaPlaybackState.Playing)
            {
                args.Cancel = true;
            }
        }
        public void GoBack()
        {
            videoPlayerConcrol.IsFullWindow = false;
        }
        public void GotViewFocus()
        {
            isViewCurrent = true;
            if (mediaPlayer.Source == null)
            {
                if (isViewLaunched)
                {
                    mediaPlayer.AutoPlay = AutoPlay;
                    selectPlayInfo(videoResource.GetPlayInfos().First());
                    setReplyAndRelatedVideo();
                }
            }
            else
            {
                if (isLastTimePlaying)
                    mediaPlayer.Play();
            }
            if (appDisplayRequest == null)
            {
                appDisplayRequest = new DisplayRequest();
                appDisplayRequest.RequestActive();
            }
           
            
            
        }
        public void LostViewFocus()
        {
            appDisplayRequest?.RequestRelease();
            appDisplayRequest = null;
            isViewCurrent = false;
            Task.Run(() => {
                isLastTimePlaying = mediaPlayer.PlaybackSession.PlaybackState == MediaPlaybackState.Playing;
                mediaPlayer.Pause();
            });
        }

        public void ViewPageLaunched(object lastReleasedViewPageparam)
        {
            isViewLaunched = true;
            if (lastReleasedViewPageparam != null)
            {
                AutoPlay = (bool)lastReleasedViewPageparam;
            }
            if (isViewCurrent)
            {
                mediaPlayer.AutoPlay = AutoPlay;
                if (mediaPlayer?.Source == null)
                {
                    selectPlayInfo(videoResource.GetPlayInfos().First());
                    setReplyAndRelatedVideo();
                }
            }
        }
        public void ReleasingViewPage(out object saveParam)
        {
            saveParam = isLastTimePlaying;
            unload();
           
        }


        private void PlayerPage_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            sender.DataRequested -= PlayerPage_DataRequested;
            if (videoResource.WebUrl == null)
                return;
            DataRequest request = args.Request;
            //创建一个数据包
            DataPackage dataPackage = new DataPackage();
            
            dataPackage.SetText(string.Format("【开眼视频UWP】\n  ◼{0}◼\n  {1}\n【观看地址】:{2}\n【电脑观看】: https://www.microsoft.com/store/productId/9P1C882TQNZR", Title, Description, videoResource.WebUrl));
            //把要分享的链接放到数据包里
            dataPackage.SetWebLink(new Uri(videoResource.WebUrl));
            if (videoResource.DetailedCover != null)
            {
                dataPackage.SetBitmap(RandomAccessStreamReference.CreateFromUri(new Uri(videoResource.DetailedCover)));
            }
     
            //数据包的标题（内容和标题必须提供）
            dataPackage.Properties.Title = Title;
            //数据包的描述
            dataPackage.Properties.Description = Description;
            //给dataRequest对象赋值
            request.Data = dataPackage;
        }
        private void ShareSymbolIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DataTransferManager.GetForCurrentView().DataRequested += PlayerPage_DataRequested;
            DataTransferManager.ShowShareUI();
        }

        private async void LikeSymbolIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            (sender as UIElement).Tapped -= LikeSymbolIcon_Tapped;
            try
            {
                if (await videoResource.SetCollectedAsync(!videoResource.IsCollected))
                {
                    IsCollected = videoResource.IsCollected;
                    CollectionCount = videoResource.CollectionCount;
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
                if (await videoResource.Author.SetIsFollowedAsync(!IsFollowed))
                {
                    IsFollowed = videoResource.Author.IsFollowed;
                }
            }
            finally
            {
                (sender as Control).IsEnabled = true;
            }
        }

        private void PersonPicture_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MainContentControl.Current.SwipeControlShowPage(typeof(PageManagerPage), videoResource.Author);
        }

        private async void ReplySuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            try
            {
                sender.IsEnabled = false;
                await videoResource.ReplyAsync(sender.Text);
                sender.Text = "";
                (replyFrame.Content as IRefresh)?.Refresh();

            }
            finally
            {
                sender.IsEnabled = true;
            }
        }
    }
}
