using KaiYan.Core.Page.Card;
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
using Windows.UI.Xaml.Media.Imaging;

namespace KaiYan.Templates.Models
{
    public abstract class ResourceCardListItem : CardListItem
    {
        public ResourceCardListItem(IPlayCardItem cardItem) : base(cardItem)
        {
            Title = cardItem.Title;
            Description = cardItem.Description;
            CoverUrl = cardItem.Cover;
            //CoverBitmapImage = new BitmapImage(new Uri(cardItem.Cover));
            if (cardItem.GetResource() is VideoResource videoResource)
                Category = videoResource.Category;
            if (cardItem.GetResource() is IVideoResource videoResource1)
            {
                DurationText = videoResource1.DurationText;
            }
            HeaderIconUrl = cardItem.GetHeader()?.GetIcon() ?? "https://home.eyepetizer.net/favicon.ico";
            //CoverBitmapImage.ImageFailed += Cover_ImageFailed;
            //CoverBitmapImage.ImageOpened += Cover_ImageOpened;
            //CoverBitmapImage.DownloadProgress += Cover_DownloadProgress;
            if (cardItem.GetResource() is IVideoResource)
            {
                PlaySymbolVisibiliy = Visibility.Visible;
            }
            else
            {
                PlaySymbolVisibiliy = Visibility.Collapsed;
            }
        }

        

        public string Title { get => (string)GetValue(TitleProperty); set { SetValue(TitleProperty, value); } }
        public static DependencyProperty TitleProperty { get; } = DependencyProperty.Register("Title", typeof(string), typeof(ResourceCardListItem), new PropertyMetadata(""));

        public string Description { get => (string)GetValue(DescriptionProperty); set { SetValue(DescriptionProperty, value); } }
        public static DependencyProperty DescriptionProperty { get; } = DependencyProperty.Register("Description", typeof(string), typeof(ResourceCardListItem), new PropertyMetadata(""));

        public string CoverUrl { get => (string)GetValue(CoverUrlProperty); set { SetValue(CoverUrlProperty, value); } }
        public static DependencyProperty CoverUrlProperty { get; } = DependencyProperty.Register("CoverUrl", typeof(string), typeof(ResourceCardListItem), new PropertyMetadata(""));

        public double CoverImageOpacity { get => (double)GetValue(CoverImageOpacityProperty); set { SetValue(CoverImageOpacityProperty, value); } }
        public static DependencyProperty CoverImageOpacityProperty { get; } = DependencyProperty.Register("CoverImageOpacity", typeof(double), typeof(ResourceCardListItem), new PropertyMetadata(0d));

        public Visibility PlaySymbolVisibiliy { get => (Visibility)GetValue(PlaySymbolVisibiliyProperty); set { SetValue(PlaySymbolVisibiliyProperty, value); } }
        public static DependencyProperty PlaySymbolVisibiliyProperty { get; } = DependencyProperty.Register("PlaySymbolVisibiliy", typeof(Visibility), typeof(VideoSmallCardListItem), new PropertyMetadata(Visibility.Visible));

        // public BitmapImage CoverBitmapImage { get => (BitmapImage)GetValue(CoverBitmapImageProperty); set { SetValue(CoverBitmapImageProperty, value); } }
        // public static DependencyProperty CoverBitmapImageProperty { get; } = DependencyProperty.Register("CoverBitmapImage", typeof(BitmapImage), typeof(PlayCardListItem), new PropertyMetadata(null));

        public string HeaderIconUrl { get => (string)GetValue(HeaderIconUrlProperty); set { SetValue(HeaderIconUrlProperty, value); } }
        public static DependencyProperty HeaderIconUrlProperty { get; } = DependencyProperty.Register("HeaderIconUrl", typeof(string), typeof(ResourceCardListItem), new PropertyMetadata(""));

        public string Category { get => (string)GetValue(CategoryProperty); set { SetValue(CategoryProperty, value); } }
        public static DependencyProperty CategoryProperty { get; } = DependencyProperty.Register("Category", typeof(string), typeof(ResourceCardListItem), new PropertyMetadata(""));

        public bool IsCoverImageLoading { get => (bool)GetValue(IsCoverImageLoadingProperty); set { SetValue(IsCoverImageLoadingProperty, value); } }
        public static DependencyProperty IsCoverImageLoadingProperty { get; } = DependencyProperty.Register("IsCoverImageLoading", typeof(bool), typeof(ResourceCardListItem), new PropertyMetadata(true));

        public bool IsCoverImageNotDownloading { get => (bool)GetValue(IsCoverImageNotDownloadingProperty); set { SetValue(IsCoverImageNotDownloadingProperty, value); } }
        public static DependencyProperty IsCoverImageNotDownloadingProperty { get; } = DependencyProperty.Register("IsCoverImageNotDownloading", typeof(bool), typeof(ResourceCardListItem), new PropertyMetadata(true));

        public double CoverDownloadProgress { get => (double)GetValue(CoverDownloadProgressProperty); set { SetValue(CoverDownloadProgressProperty, value); } }
        public static DependencyProperty CoverDownloadProgressProperty { get; } = DependencyProperty.Register("CoverDownloadProgress", typeof(double), typeof(ResourceCardListItem), new PropertyMetadata(0d));

        public string DurationText { get => (string)GetValue(DurationTextProperty); set { SetValue(DurationTextProperty, value); } }
        public static DependencyProperty DurationTextProperty { get; } = DependencyProperty.Register("DurationText", typeof(string), typeof(VideoSmallCardListItem), new PropertyMetadata(""));


        public void Cover_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            IsCoverImageLoading = false;
            CoverImageOpacity = 0;
        }

        public void Cover_ImageOpened(object sender, RoutedEventArgs e)
        {
            IsCoverImageLoading = false;
            CoverImageOpacity = 1;
        }
        public ResourceItem GetResource()=> (CardItem as IPlayCardItem).GetResource();

        public void Cover_DownloadProgress(object sender, Windows.UI.Xaml.Media.Imaging.DownloadProgressEventArgs e)
        {
            IsCoverImageNotDownloading = false;
            CoverDownloadProgress = e.Progress;
        }
        public override void CardListItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MainContentControl.Current.PlayResource(GetResource());

        }
    }
}
