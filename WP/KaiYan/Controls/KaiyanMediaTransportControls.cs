using KaiYan.Core.Page.Card.Resource;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace KaiYan.Controls
{
    public class KaiyanMediaTransportControls : MediaTransportControls
    {
        public KaiyanMediaTransportControls()
        {
            DefaultStyleKey = typeof(KaiyanMediaTransportControls);
            PlayInfos = new ObservableCollection<PlayInfo>();
        }

        private FrameworkElement PlayInfoButton;
        private ListView PlayInfosListView;

        public double PlaybackRate { get => (double)GetValue(PlaybackRateProperty); set { SetValue(PlaybackRateProperty, value); } }
        public static DependencyProperty PlaybackRateProperty { get; } = DependencyProperty.Register("PlaybackRate", typeof(double), typeof(KaiyanMediaTransportControls), new PropertyMetadata(1d));

        public ObservableCollection<PlayInfo> PlayInfos { get => (ObservableCollection<PlayInfo>)GetValue(PlayInfosProperty); set { SetValue(PlayInfosProperty, value); } }
        public static DependencyProperty PlayInfosProperty { get; } = DependencyProperty.Register("PlayInfos", typeof(double), typeof(KaiyanMediaTransportControls), new PropertyMetadata(null));

       

        public Visibility PlayInfoVisibility { get => (Visibility)GetValue(PlayInfoVisibilityProperty); set { SetValue(PlayInfoVisibilityProperty, value); } }
        public static DependencyProperty PlayInfoVisibilityProperty { get; } = DependencyProperty.Register("PlayInfoVisibility", typeof(double), typeof(KaiyanMediaTransportControls), new PropertyMetadata(Visibility.Collapsed, PlayInfoVisibilityPropertyChanged));
        static void PlayInfoVisibilityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((Visibility)e.OldValue != (Visibility)e.NewValue && (d as KaiyanMediaTransportControls).PlayInfoButton != null)
            {
                (d as KaiyanMediaTransportControls).PlayInfoButton.Visibility = (Visibility)e.NewValue;
            }
        }

        public PlayInfo SelectedPlayInfo { get => (PlayInfo)GetValue(SelectedPlayInfoProperty); set { SetValue(SelectedPlayInfoProperty, value); } }
        public static DependencyProperty SelectedPlayInfoProperty { get; } = DependencyProperty.Register("SelectedPlayInfo", typeof(PlayInfo), typeof(KaiyanMediaTransportControls), new PropertyMetadata(null, SelectedPlayInfoPropertyChanged));
        static void SelectedPlayInfoPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((PlayInfo)e.OldValue != (PlayInfo)e.NewValue && (d as KaiyanMediaTransportControls).PlayInfosListView != null)
            {
                (d as KaiyanMediaTransportControls).PlayInfosListView.SelectedItem = e.NewValue;
            }
        }
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var rootGrid = (FrameworkElement)Windows.UI.Xaml.Media.VisualTreeHelper.GetChild(this, 0);
            if (rootGrid != null )
            {
                
                PlayInfoButton = rootGrid.FindName("PlayInfoButton") as FrameworkElement;
                PlayInfoButton.Visibility = PlayInfoVisibility;
                PlayInfosListView = rootGrid.FindName("PlayInfosListView") as ListView;
                PlayInfosListView.SelectedItem = SelectedPlayInfo;
                if (PlayInfosListView != null)
                {
                    PlayInfosListView.SelectionChanged -= PlayInfosListView_SelectionChanged;
                }
                PlayInfosListView.SelectionChanged += PlayInfosListView_SelectionChanged;
            }
        }

        private void PlayInfosListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedPlayInfo = (PlayInfo)PlayInfosListView.SelectedItem;
        }
    }
}
