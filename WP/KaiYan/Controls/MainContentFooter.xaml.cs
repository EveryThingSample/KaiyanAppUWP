using EveryThingSampleTools.WP.UI.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace KaiYan.Controls
{
    public sealed partial class MainContentFooter : UserControl
    {
        public MainContentFooter()
        {
            this.InitializeComponent();
            sliderTranslateTransformXDoubleAnimation = new DoubleAnimation()
            {
                Duration = new TimeSpan(0, 0, 0, 0, 650),
                From = -100,
                To = -100,
                EasingFunction = new PowerEase()
                {
                    Power = 5,
                    EasingMode = EasingMode.EaseOut,
                    
                }
            };
           
            Storyboard.SetTarget(sliderTranslateTransformXDoubleAnimation, slider.RenderTransform);
            Storyboard.SetTargetProperty(sliderTranslateTransformXDoubleAnimation, "X");
            storyboard.Children.Add(sliderTranslateTransformXDoubleAnimation);
            this.SizeChanged += MainContentFooter_SizeChanged;
        }

        public int SelectedIndex { get => (int)GetValue(SelectedIndexProperty); set { SetValue(SelectedIndexProperty, value); } }
        public static DependencyProperty SelectedIndexProperty { get; } = DependencyProperty.Register("SelectedIndex", typeof(int), typeof(MainContentFooter), new PropertyMetadata(-1, new PropertyChangedCallback(SelectedIndexPropertyChanged)));
        private static void SelectedIndexPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((int)e.OldValue != (int)e.NewValue)
            {
                (d as MainContentFooter).selectedIndexPropertyChanged((int)e.NewValue, (int)e.OldValue);
            }

        }

        private Storyboard storyboard { get; } = new Storyboard();
        private DoubleAnimation sliderTranslateTransformXDoubleAnimation;

        public event SelectionChangedEventHandler SelectionChanged;
        public event TypedEventHandler<MainContentFooter, int> DoubleSelected;


        private void Item_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var index = Child_Grid.Children.IndexOf(sender as UIElement);
            if (SelectedIndex != index)
                SelectedIndex = index;
            else
            {
                DoubleSelected?.Invoke(this, index);
            }
        }
        private void Item_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var index = Child_Grid.Children.IndexOf(sender as UIElement);
            if (SelectedIndex != index)
                SelectedIndex = index;
        }

        private void MainContentFooter_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            changeSelectIndex(SelectedIndex);
        }
        private void changeSelectIndex(int i)
        {
            if (i < 0 || i >= Child_Grid.Children.Count)
            {
                sliderTranslateTransformX(-100);
            }
            else
            {
                var element = Child_Grid.Children[i] as FrameworkElement;
                Rect elementBounds = element.TransformToVisual(this).TransformBounds(new Rect(0.0, 0.0, element.ActualWidth, element.ActualHeight));

                var x = (elementBounds.Left + elementBounds.Right) / 2;
                sliderTranslateTransformX(x - slider.ActualWidth / 2);
            }
        }

        private void sliderTranslateTransformX(double value)
        {
            try
            {
                storyboard.Pause();


                var animation = storyboard.Children[0] as DoubleAnimation;
                var from = (double)animation.From;
                var to = (double)animation.To;
                var total = animation.Duration.TimeSpan.TotalMilliseconds;
                var curentTime = storyboard.GetCurrentTime().TotalMilliseconds;
                var progressValue = curentTime / total;
                animation.EasingFunction.Ease(progressValue);
                animation.From = from + animation.EasingFunction.Ease(progressValue) * (to - from);





                animation.To = value;
                storyboard.Stop();
                storyboard.Children.Clear();
                storyboard.Seek(TimeSpan.Zero);
                storyboard.Children.Add(animation);
                storyboard.Begin();
            }
            catch
            { }
        }





       
        private void selectedIndexPropertyChanged(int newValue, int oldValue)
        {
            changeSelectIndex(newValue);
            SelectionChanged?.Invoke(this, new SelectionChangedEventArgs(new object[] { oldValue }, new object[] { newValue }));
        }

    }
}
