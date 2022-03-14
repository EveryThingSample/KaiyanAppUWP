using KaiYan.Core.Page.Body;
using KaiYan.Core.Page.Card.Resource;
using KaiYan.Controls;
using KaiYan.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace KaiYan.Templates.Models
{
    public class FolllowHeader: BodyBaseItem
    {
        public FolllowHeader(ResourceItem resourceItem):base(resourceItem.Provider)
        {
            Area = (resourceItem as UgcResourceItem)?.Area;
            CreateTime = resourceItem.CreateTime;

            
            IsCollected = resourceItem.IsCollected;
            
            CollectionCount = resourceItem.CollectionCount;
            
            ReplyCount = resourceItem.ReplyCount;
            
            Description = resourceItem.Description;

            DescriptionMaxHeight = 60;

            this.resourceItem = resourceItem;
            ViewMoreText = "展开";

            if (resourceItem.IsCollected)
            {
                LikeButtonVisibility = Visibility.Collapsed;
                DisLikeButtonVisibility = Visibility.Visible;
            }
            else
            {
                DisLikeButtonVisibility = Visibility.Collapsed;
                LikeButtonVisibility = Visibility.Visible;
            }
        }

        

        private ResourceItem resourceItem;



      
        public string Area { get => (string)GetValue(AreaProperty); set { SetValue(AreaProperty, value); } }
        public static DependencyProperty AreaProperty { get; } = DependencyProperty.Register("Area", typeof(string), typeof(FolllowHeader), new PropertyMetadata(""));

        public string Description { get => (string)GetValue(DescriptionProperty); set { SetValue(DescriptionProperty, value); } }
        public static DependencyProperty DescriptionProperty { get; } = DependencyProperty.Register("Description", typeof(string), typeof(FolllowHeader), new PropertyMetadata(""));

  
        public long CreateTime { get => (long)GetValue(CreaateTimeProperty); set { SetValue(CreaateTimeProperty, value); } }
        public static DependencyProperty CreaateTimeProperty { get; } = DependencyProperty.Register("CreateTime", typeof(long), typeof(FolllowHeader), new PropertyMetadata(0));

        public bool IsCollected { get => (bool)GetValue(IsCollectedProperty); set { SetValue(IsCollectedProperty, value); } }
        public static DependencyProperty IsCollectedProperty { get; } = DependencyProperty.Register("IsCollected", typeof(bool), typeof(BodyBaseItem), new PropertyMetadata(false));

        public int CollectionCount { get => (int)GetValue(CollectionCountProperty); set { SetValue(CollectionCountProperty, value); } }
        public static DependencyProperty CollectionCountProperty { get; } = DependencyProperty.Register("CollectionCount", typeof(int), typeof(FolllowHeader), new PropertyMetadata(0));

        public int ReplyCount { get => (int)GetValue(ReplyCountProperty); set { SetValue(ReplyCountProperty, value); } }
        public static DependencyProperty ReplyCountProperty { get; } = DependencyProperty.Register("ReplyCount", typeof(int), typeof(FolllowHeader), new PropertyMetadata(0));

        public double DescriptionMaxHeight { get => (double)GetValue(DescriptionMaxHeightProperty); set { SetValue(DescriptionMaxHeightProperty, value); } }
        public static DependencyProperty DescriptionMaxHeightProperty { get; } = DependencyProperty.Register("DescriptionMaxHeight", typeof(double), typeof(FolllowHeader), new PropertyMetadata(0d));

        public Visibility ViewMoreVisibility { get => (Visibility)GetValue(ViewMoreVisibilityProperty); set { SetValue(ViewMoreVisibilityProperty, value); } }
        public static DependencyProperty ViewMoreVisibilityProperty { get; } = DependencyProperty.Register("ViewMoreVisibility", typeof(Visibility), typeof(FolllowHeader), new PropertyMetadata(Visibility.Collapsed));
        
        public string ViewMoreText { get => (string)GetValue(ViewMoreTextProperty); set { SetValue(ViewMoreTextProperty, value); } }
        public static DependencyProperty ViewMoreTextProperty { get; } = DependencyProperty.Register("ViewMoreText", typeof(string), typeof(FolllowHeader), new PropertyMetadata(""));

        public Visibility LikeButtonVisibility { get => (Visibility)GetValue(LikeButtonVisibilityProperty); set { SetValue(LikeButtonVisibilityProperty, value); } }
        public static DependencyProperty LikeButtonVisibilityProperty { get; } = DependencyProperty.Register("LikeButtonVisibility", typeof(Visibility), typeof(FolllowHeader), new PropertyMetadata(Visibility.Collapsed));

        public Visibility DisLikeButtonVisibility { get => (Visibility)GetValue(DisLikeButtonVisibilityProperty); set { SetValue(DisLikeButtonVisibilityProperty, value); } }
        public static DependencyProperty DisLikeButtonVisibilityProperty { get; } = DependencyProperty.Register("DisLikeButtonVisibility", typeof(Visibility), typeof(FolllowHeader), new PropertyMetadata(Visibility.Collapsed));

        public void Description_TextBlock_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Height > DescriptionMaxHeight)
            {
                ViewMoreVisibility = Visibility.Visible;
            }
            else
            {
                ViewMoreVisibility = Visibility.Collapsed;
            }
        }

        public void ViewMore_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (DescriptionMaxHeight == double.PositiveInfinity)
            {
                ViewMoreText = "展开";
                DescriptionMaxHeight = 60;
            }
            else
            {
                ViewMoreText = "收起";
                DescriptionMaxHeight = Double.PositiveInfinity;
            }
           
        }

        public void ReplySmbolIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MainContentControl.Current.SwipeControlShowPage(typeof(ReplyPage), resourceItem);
        }
        public async void LikeSymbolIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            (sender as UIElement).Tapped -= LikeSymbolIcon_Tapped;
            try
            {
                if (await resourceItem.SetCollectedAsync(!resourceItem.IsCollected))
                {
                    IsCollected = resourceItem.IsCollected;
                    CollectionCount = resourceItem.CollectionCount;
                    if (resourceItem.IsCollected)
                    {
                        LikeButtonVisibility = Visibility.Collapsed;
                        DisLikeButtonVisibility = Visibility.Visible;
                    }
                    else
                    {
                        DisLikeButtonVisibility = Visibility.Collapsed;
                        LikeButtonVisibility = Visibility.Visible;
                    }
                }

            }
            finally
            {
                (sender as UIElement).Tapped += LikeSymbolIcon_Tapped;
            }
        }
    }
}
