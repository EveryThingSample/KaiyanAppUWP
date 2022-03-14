using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace KaiYan.Class
{
    public sealed class ImageViewItem : DependencyObject
    {
        public ImageViewItem()
        {

        }
        public object Data { get; set; }
        public string IndexStr { get => (string)GetValue(IndexStrProperty); set { SetValue(IndexStrProperty, value); } }
        public ImageSource ImageSource { get => (ImageSource)GetValue(ImageSourceProperty); set { SetValue(ImageSourceProperty, value); } }
        public static DependencyProperty ImageSourceProperty { get; } = DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ImageViewItem), new PropertyMetadata(null));
        public static DependencyProperty IndexStrProperty { get; } = DependencyProperty.Register("IndexStr", typeof(string), typeof(ImageViewItem), new PropertyMetadata(""));
    }
}
