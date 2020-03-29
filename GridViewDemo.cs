using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace App1
{
    public class GridViewDemo : GridView
    {
        private Size newSize { get; set; }

        public GridViewDemo(object data)
        {
            this.SelectionMode = ListViewSelectionMode.None;
            ScrollViewer.SetHorizontalScrollMode(this, ScrollMode.Disabled);
            ScrollViewer.SetVerticalScrollMode(this, ScrollMode.Disabled);
            this.CanDragItems = false;
            newSize = new Size(300, 300 * 1.7 / 3);
            //ItemContainerStyle = Application.Current.Resources["GridViewItemNoneStyle"] as Style;
            // this.SizeChanged += GridViewDemo_SizeChanged;

            if (data is List<GridViewItemMode>)
            {
                AddItems((List<GridViewItemMode>)data);
            }
        }

        private async void GridViewDemo_SizeChanged(object sender, Windows.UI.Xaml.SizeChangedEventArgs e)
        {
         
            newSize = new Size(300, 300 * 1.7 / 3);

            for (var i = 0; i < this.Items.Count; i++)
            {
                await ((GridViewUIItem)this.Items[i]).Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    ((GridViewUIItem)this.Items[i]).ImageSize = newSize;
                });
            }
        }
      
        private void AddItems(List<GridViewItemMode> itemList)
        {
            foreach (var item in itemList)
            {
                Add(item);
            }
        }

        private void Add(GridViewItemMode item)
        {
            var it = new GridViewUIItem(item, newSize, false);
            it.ContentTapped += It_ContentTapped;
            this.Items.Add(it);
        }

        private void It_ContentTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var send = ((FrameworkElement)sender).DataContext as GridViewItemMode;
            var img = ((Grid)sender).Children[1] as Image;
            Windows.UI.Xaml.Media.Animation.ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("Player_postpic", (UIElement)sender);
          //  
        }
    }
    public class GridViewUIItem : DropShadowPanel
    {
        public event Windows.UI.Xaml.Input.TappedEventHandler ContentTapped;
        //   public event TypedEventHandler<ListItemsControl, ItemContentUnloadedEventArgs> ItemContentUnloaded;
        private UISettings uISettings { get; }
        private GridViewItemMode Data { get; }
        private Color DropShadowPanelColor { get; }
        private bool isDark { get; }
        private Size imageSize { get; set; }
        public Size ImageSize { get { return imageSize; } set { imageSize = value; SetSize(); } }

        public GridViewUIItem(GridViewItemMode Data, Size imageSize, bool isDark)
        {
            this.isDark = isDark;
            uISettings = new UISettings();
            this.Data = Data;
            DropShadowPanelColor = !isDark ? Color.FromArgb(255, 0, 0, 0) : Color.FromArgb(255, 255, 255, 255);
            this.imageSize = imageSize;
            this.Initialize();
        }
        private void Initialize()
        {
            this.BlurRadius = 11;
            this.ShadowOpacity = 0.6;
            this.OffsetX = 0;
            this.OffsetY = 0;
            Margin = new Windows.UI.Xaml.Thickness(8, 8, 4, 4);
            CanDrag = false;
            Color = DropShadowPanelColor;
            HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;

            this.Content = GetContent();
            SetSize();
        }
        private void SetSize()
        {
            this.Height = ImageSize.Height + 20;
            this.Width = ImageSize.Width;
            //var grid = (this.Content as Panel)?.Children[0] as Panel;
            //if (grid !=null)
            //{
            //    (this.Content as Panel).Width = ImageSize.Width;
            //    grid.Width = ImageSize.Width;
            //    grid.Height = 
            //   //((FrameworkElement)grid.Children[0]).Height = ImageSize.Height;
            //   // ((FrameworkElement)grid.Children[1]).Height = ImageSize.Height;
            //}
        }

        private UIElement GetContent()
        {
            var rootGrid = new Grid()
            {
                CornerRadius = new CornerRadius(8),
                Background = new SolidColorBrush(isDark ? Color.FromArgb(255, 0, 0, 0) : Color.FromArgb(255, 255, 255, 255)),
            };
            rootGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            rootGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });

            var grid = new Grid()
            {
                CornerRadius = new CornerRadius(7, 7, 0, 0),
            };
            grid.Tapped += Grid_Tapped;
            grid.DataContext = Data;
            grid.Children.Add(new Image() { Stretch = Stretch.UniformToFill, Source = new BitmapImage(new Uri("ms-Appx:///Assets/awaiting.jpg")) });

            var image = new Image()
            {
                Stretch = Stretch.UniformToFill,
                Source = new BitmapImage(new Uri(Data.pic)),
                Opacity = 0,
            };
            image.PointerEntered += Image_PointerEntered;
            image.PointerExited += Image_PointerExited;
            image.ImageOpened += Image_ImageOpened;
            grid.Children.Add(image);
            grid.Children.Add(GetTimeInfoUIElement());


            rootGrid.Children.Add(grid);
            rootGrid.Children.Add(GetAuthorInfoUIElement());

            return rootGrid;
        }

        private UIElement GetTimeInfoUIElement()
        {
            return new Border()
            {
                Width = 39,
                Height = 20,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                CornerRadius = new CornerRadius(4),
                Background = new SolidColorBrush(Color.FromArgb(255, (byte)~uISettings.GetColorValue(UIColorType.Accent).R, (byte)~uISettings.GetColorValue(UIColorType.Accent).G, (byte)~uISettings.GetColorValue(UIColorType.Accent).B)),
                Margin = new Thickness(13),
                Child = new TextBlock() { Foreground = new SolidColorBrush(Colors.White), Text = "08:30", TextAlignment = TextAlignment.Center, VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center },
            };
        }

        private UIElement GetAuthorInfoUIElement()
        {
            var grid = new Grid()
            {
                Margin = new Thickness(5),
                Height = 30,
            };
            Grid.SetRow(grid, 1);
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30, GridUnitType.Pixel) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            var pers = new PersonPicture()
            {
                VerticalAlignment = VerticalAlignment.Center,
                Width = 30,
                ProfilePicture = new BitmapImage(new Uri(Data.pic)),
            };
            Grid.SetRowSpan(pers, 2);


            var titleTxt = new TextBlock()
            {
                Text = Data.title,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                FontWeight = new Windows.UI.Text.FontWeight() { Weight = 700, },
                FontFamily = new FontFamily("Arial Black"),
                Margin = new Thickness(5, 0, 0, 0),
                TextTrimming = TextTrimming.CharacterEllipsis,
            };
            Grid.SetColumn(titleTxt, 1);


            var descriptionTxt = new TextBlock()
            {
                Text = Data.description,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 11,
                FontFamily = new FontFamily("Arial"),
                Margin = new Thickness(5, 0, 0, 0),
                TextTrimming = TextTrimming.CharacterEllipsis,
            };
            Grid.SetColumn(descriptionTxt, 1);
            Grid.SetRow(descriptionTxt, 1);

            grid.Children.Add(pers);
            grid.Children.Add(titleTxt);
            grid.Children.Add(descriptionTxt);
            return grid;
        }

        private void Image_ImageOpened(object sender, RoutedEventArgs e)
        {
            Windows.UI.Xaml.Media.Animation.DoubleAnimation opacityanimation = new Windows.UI.Xaml.Media.Animation.DoubleAnimation();
            opacityanimation.Duration = new TimeSpan(0, 0, 0, 0, 500);
            opacityanimation.From = 0;
            opacityanimation.To = 1;
            Windows.UI.Xaml.Media.Animation.Storyboard.SetTarget(opacityanimation, (DependencyObject)sender);
            Windows.UI.Xaml.Media.Animation.Storyboard.SetTargetProperty(opacityanimation, "Opacity");
            Windows.UI.Xaml.Media.Animation.Storyboard storyboard = new Windows.UI.Xaml.Media.Animation.Storyboard();
            storyboard.Children.Add(opacityanimation);
            storyboard.Begin();
        }

        private void Image_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            (sender as UIElement).Opacity = 1;
        }

        private void Image_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            (sender as UIElement).Opacity = 0.9;
        }

        private void Grid_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            ContentTapped?.Invoke(sender, e);
        }

    }
}

