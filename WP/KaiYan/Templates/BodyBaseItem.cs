using KaiYan.Core.Page.Body;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace KaiYan.Templates
{
    public class BodyBaseItem : DependencyObject
    {
        public BodyBaseItem(BodyBase bodyBase)
        {
            this.bodyBase = bodyBase;
            HeaderName = bodyBase.GetTitle();
            HeaderIconUrl = bodyBase.GetIcon();
            IsFollowed = bodyBase.IsFollowed;
            
        }
        BodyBase bodyBase;
        public string HeaderName { get => (string)GetValue(HeaderNameProperty); set { SetValue(HeaderNameProperty, value); } }
        public static DependencyProperty HeaderNameProperty { get; } = DependencyProperty.Register("HeaderName", typeof(string), typeof(BodyBaseItem), new PropertyMetadata(""));

        public string HeaderIconUrl { get => (string)GetValue(HeaderIconUrlProperty); set { SetValue(HeaderIconUrlProperty, value); } }
        public static DependencyProperty HeaderIconUrlProperty { get; } = DependencyProperty.Register("HeaderIconUrl", typeof(string), typeof(BodyBaseItem), new PropertyMetadata(""));

        public bool IsFollowed { get => (bool)GetValue(IsFollowedProperty); set { SetValue(IsFollowedProperty, value); } }
        public static DependencyProperty IsFollowedProperty { get; } = DependencyProperty.Register("IsFollowed", typeof(bool), typeof(BodyBaseItem), new PropertyMetadata(false));

        public async void Follow_Button_Click(object sender, RoutedEventArgs e)
        {
            (sender as Control).IsEnabled = false;
            try
            {
                if (await bodyBase.SetIsFollowedAsync(!IsFollowed))
                {
                    IsFollowed = bodyBase.IsFollowed;
                }
            }
            finally
            {
                (sender as Control).IsEnabled = true;
            }
        }
        

    }
}
