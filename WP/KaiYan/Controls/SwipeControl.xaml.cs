using EveryThingSampleTools.WP.Tools;
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
    public sealed partial class SwipeControl : UserControl
    {
        public SwipeControl()
        {
            this.InitializeComponent();
            swipeCardStack = new Stack<StackItem> ();
            this.SizeChanged += FlyPageCollection_SizeChanged;
            //this.ManipulationMode = ManipulationModes.TranslateX;
            //this.ManipulationDelta += SwipeControl_ManipulationDelta;
        }
        double maxD;
        private void SwipeControl_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (maxD < e.Delta.Translation.X)
                maxD = e.Delta.Translation.X;
        }

        public int MaxCachedPagesCount { get; set; } = 2;
        public int MinCachedPagesCount { get; set; } = 2;
        public int CurrentCachedPagesCount { get; private set; } = 0;

        Stack<StackItem> swipeCardStack;
        SwipeCard previousSwipeCard;

        public bool CanGoBack => swipeCardStack.Count > 0;

        public void ShowPage(Type pageType, object parameter, ElementTheme requestedTheme = ElementTheme.Default)
        {
            SwipeCard card;
            if (previousSwipeCard == null)
            {
                card = createSwipeCard();
            }
            else
            {
                card = previousSwipeCard;
       
                previousSwipeCard = null;
            }
            card.Closed += SwipeCard_Closed;
            card.Opened += SwipeCard_Opened;
            card.SwipeStarted += SwipeCard_SwipeStarted;
            card.SwipeCompleted += SwipeCard_SwipeCompleted;
            card.ShowLength = new ShowLength(ActualWidth, ShowUnitType.Pixel);
            (card.Content as Frame).RequestedTheme = requestedTheme;
            (card.Content as Frame).Navigate(pageType, parameter, NavigationTransitionInfo);
            RootGrid.Children.Add(card);

            stackPush(new StackItem(card, pageType, parameter));

            card.IsOpen = true;
        }


        public void GoBack()
        {
            if (CanGoBack == false)
                throw new Exception("不能回退了");
            StackItem stackItem = swipeCardStack.Peek();
            SwipeCard card = stackItem.SwipeCard;
            if ((card.Content as Frame).Content is ISwipeControlItem swipeControlItem && swipeControlItem.CanGoback)
            {
                swipeControlItem.GoBack();
            }
            else
            {
                card.IsOpen = false;
                stackPop();
            }
        }


        private void SwipeCard_Opened(SwipeCard sender, object args)
        {
            sender.Opened -= SwipeCard_Opened;
            if ((sender.Content as Frame).Content is ISwipeControlItem swipeControlItem)
            {
                swipeControlItem.ViewPageLaunched(null);
            }
        }

        private void SwipeCard_SwipeStarted(SwipeCard sender, object args)
        {
            FocusedCurrent = null;
        }
        private void SwipeCard_SwipeCompleted(SwipeCard sender, object args)
        {
            if (FocusedCurrent == null)
            {
                if (swipeCardStack.Peek().SwipeCard == sender)
                    FocusedCurrent = swipeCardStack.Peek();
            }
        }


        private void SwipeCard_Closed(SwipeCard sender, object args)
        {
            sender.Closed -= SwipeCard_Closed;
            if (swipeCardStack.Count > 0 && swipeCardStack.Peek().SwipeCard == sender)
            {
                stackPop();
            }
            if ((sender.Content as Frame).Content is ISwipeControlItem swipeControlItem)
            {
                object param = null;
                swipeControlItem.ReleasingViewPage(out param);
            }

            (sender.Content as Frame).Navigate(typeof(Page),null, NavigationTransitionInfo);
            previousSwipeCard = sender;

            RootGrid.Children.Remove(sender);
        }
        private void FlyPageCollection_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach(SwipeCard child in RootGrid.Children)
            {
                child.ShowLength = new ShowLength(e.NewSize.Width, ShowUnitType.Pixel);
            }
        }
        private SwipeCard createSwipeCard()
        {
            Frame frame = new Frame();
            return new SwipeCard() { Content = frame };
        }
        private void stackPush(StackItem item)
        {
            swipeCardStack.Push(item);
            CurrentCachedPagesCount++;
            FocusedCurrent = item;
            if (CurrentCachedPagesCount > MaxCachedPagesCount)
            {
                unloadCachedPage();
            }
        }
        private void stackPop()
        {
            StackItem item =  swipeCardStack.Pop();
            CurrentCachedPagesCount--;
            if (CurrentCachedPagesCount < MinCachedPagesCount && swipeCardStack.Count >= MinCachedPagesCount)
            {
                loadCachedPage();
            }
           
            FocusedCurrent = swipeCardStack.Count > 0 ? swipeCardStack.Peek() : null;
        }
        private void loadCachedPage()
        {
            CurrentCachedPagesCount++;
            int i = MinCachedPagesCount - 1;
            StackItem stackItem = swipeCardStack.ToArray()[i];
            SwipeCard SwipeCard = stackItem.SwipeCard;
            (SwipeCard.Content as Frame).Navigate(stackItem.pagetype, stackItem.parameter, NavigationTransitionInfo);
            if ((SwipeCard.Content as Frame).Content is ISwipeControlItem swipeControlItem)
            {
                swipeControlItem.ViewPageLaunched(stackItem.viewClosedParameter);
            }
        }
        static NavigationTransitionInfo NavigationTransitionInfo = new SuppressNavigationTransitionInfo();
        private void unloadCachedPage()
        {
            CurrentCachedPagesCount--;

            int i = MaxCachedPagesCount;
            StackItem stackItem = swipeCardStack.ToArray()[i];
            SwipeCard SwipeCard = stackItem.SwipeCard;

            if ((SwipeCard.Content as Frame).Content is ISwipeControlItem swipeControlItem)
            {
                object param;
                swipeControlItem.ReleasingViewPage(out param);
                stackItem.viewClosedParameter = param;
            }
            (SwipeCard.Content as Frame).Navigate(typeof(Page),null, NavigationTransitionInfo);
        }
        private StackItem _focusedCurrent;
        private StackItem FocusedCurrent { get => _focusedCurrent;
            set
            {
                if ((_focusedCurrent?.SwipeCard.Content as Frame)?.Content is ISwipeControlItem swipeControlItem)
                {
                    swipeControlItem.LostViewFocus();
                }
                _focusedCurrent = value;
                if ((_focusedCurrent?.SwipeCard.Content as Frame)?.Content is ISwipeControlItem _swipeControlItem)
                {
                    _swipeControlItem.GotViewFocus();
                }
            }
        }
        private class StackItem
        {
            public StackItem(SwipeCard SwipeCard, Type pagetype, object parameter)
            {
                this.SwipeCard = SwipeCard;
                this.pagetype = pagetype;
                this.parameter = parameter;
            }
            public SwipeCard SwipeCard { get; }
            public Type pagetype { get; }
            public object parameter { get; }
            public object viewClosedParameter { get; set; }
        }
    }
}
