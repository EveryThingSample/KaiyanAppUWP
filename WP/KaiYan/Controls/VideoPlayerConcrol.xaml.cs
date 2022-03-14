using EveryThingSampleTools.WP.UI.Controls;
using KaiYan.Core.Page.Card.Resource;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Media.Playback;
using Windows.UI.ViewManagement;
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
    public sealed partial class VideoPlayerConcrol : UserControl
    {
        public VideoPlayerConcrol()
        {
            this.InitializeComponent();
            mediaPlayerElement.SetMediaPlayer(new Windows.Media.Playback.MediaPlayer() { AutoPlay = true });
            PlayInfos = new ObservableCollection<PlayInfo>();
        }
        public string CoverUrl { get => (string)GetValue(CoverUrlProperty); set { SetValue(CoverUrlProperty, value); } }
        public static DependencyProperty CoverUrlProperty { get; } = DependencyProperty.Register("CoverUrl", typeof(string), typeof(VideoPlayerConcrol), new PropertyMetadata(""));

        public ObservableCollection<PlayInfo> PlayInfos { get => (ObservableCollection<PlayInfo>)GetValue(PlayInfosProperty); set { SetValue(PlayInfosProperty, value); } }
        public static DependencyProperty PlayInfosProperty { get; } = DependencyProperty.Register("PlayInfos", typeof(double), typeof(VideoPlayerConcrol), new PropertyMetadata(new ObservableCollection<PlayInfo>()));

        public Visibility PlayInfoVisibility { get => (Visibility)GetValue(PlayInfoVisibilityProperty); set { SetValue(PlayInfoVisibilityProperty, value); } }
        public static DependencyProperty PlayInfoVisibilityProperty { get; } = DependencyProperty.Register("PlayInfoVisibility", typeof(double), typeof(VideoPlayerConcrol), new PropertyMetadata(Visibility.Collapsed));

        public PlayInfo SelectedPlayInfo { get => (PlayInfo)GetValue(SelectedPlayInfoProperty); set { SetValue(SelectedPlayInfoProperty, value); } }
        public static DependencyProperty SelectedPlayInfoProperty { get; } = DependencyProperty.Register("SelectedPlayInfo", typeof(PlayInfo), typeof(VideoPlayerConcrol), new PropertyMetadata(null));

        public bool IsFullWindow { get => (bool)GetValue(IsFullWindowProperty); set { SetValue(IsFullWindowProperty, value); } }
        public static DependencyProperty IsFullWindowProperty { get; } = DependencyProperty.Register("IsFullWindow", typeof(bool), typeof(VideoPlayerConcrol), new PropertyMetadata(false, IsFullWindowPropertyChenged));
        private static void IsFullWindowPropertyChenged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.OldValue != (bool)e.NewValue)
            {
                (d as VideoPlayerConcrol).changeIsFullScreenMode((bool)e.NewValue);
            }
        }


        public MediaPlayerElement MediaPlayerElement => mediaPlayerElement;

        private void MediaControl_RootGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var RootGrid = sender as Grid;
            var BottomTransportContentControl = RootGrid.FindName("BottomTransportContentControl") as TransportContentControl;
            var position = e.GetPosition(RootGrid);
            if (BottomTransportContentControl.IsClosed || RootGrid.ActualHeight - position.Y > BottomTransportContentControl.ActualHeight)
            {
                if (BottomTransportContentControl.IsClosed)
                    BottomTransportContentControl.Show();
                else if (BottomTransportContentControl.IsOpened)
                    BottomTransportContentControl.Hide();
            }
        }
        private void FullWindowButton1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (sender as Control).IsEnabled = false;
                IsFullWindow = !IsFullWindow;
            }
            finally
            {
                (sender as Control).IsEnabled = true;
            }
        }
        private void changeIsFullScreenMode(bool isFullWindow)
        {

            if (ApplicationView.GetForCurrentView().IsFullScreenMode && isFullWindow == false)
            {
                ApplicationView.GetForCurrentView().ExitFullScreenMode();
                DisplayInformation.AutoRotationPreferences = DisplayOrientations.None;
                mediaPlayerElement.MinHeight = 0;
                mediaPlayerElement.MaxHeight = Double.PositiveInfinity;
            }
            else if (isFullWindow)
            {
                ApplicationView.GetForCurrentView().TryEnterFullScreenMode();
                if (ApplicationView.GetForCurrentView().Orientation == ApplicationViewOrientation.Portrait)
                {
                    DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;
                }
                var displayInformation = DisplayInformation.GetForCurrentView();
                var ScreenHeightInRawPixels = displayInformation.ScreenHeightInRawPixels;
                var ScreenWidthInRawPixels = displayInformation.ScreenWidthInRawPixels;
                var scale = displayInformation.RawPixelsPerViewPixel;
                mediaPlayerElement.MinHeight = Math.Min(ScreenWidthInRawPixels, ScreenHeightInRawPixels) / scale;
                mediaPlayerElement.MaxHeight = Math.Min(ScreenWidthInRawPixels, ScreenHeightInRawPixels) / scale;
            }
        }

        private void BottomTransportContentControl_Closing(FlyContent sender, ClosingEventArgs args)
        {
            if (MediaPlayerElement.MediaPlayer != null && MediaPlayerElement.MediaPlayer?.PlaybackSession.PlaybackState != MediaPlaybackState.Playing)
            {
                args.Cancel = true;
            }
        }
        ProgressBar progressBar = null; TransportContentControl progressBarContainer;
        private void ProgressSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (progressBar == null)
            {
                progressBarContainer = (((sender as FrameworkElement).Parent as FrameworkElement).Parent as FrameworkElement).FindName("ProgressBarContainer") as TransportContentControl;
                progressBar = progressBarContainer.FindName("ProgressBar") as ProgressBar;
            }
            progressBar.Value = e.NewValue;
        }

        private void BottomTransportContentControl_Closed(FlyContent sender, object args)
        {
            if (progressBar != null)
            {
                progressBarContainer.Show();
            }
        }

        private void BottomTransportContentControl_Opening(FlyContent sender, OpeningEventArgs args)
        {
            if (progressBarContainer != null)
            {
                progressBarContainer.Hide();
            }
        }
        

    }
}
