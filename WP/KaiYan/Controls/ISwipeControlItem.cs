using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Controls
{
    public interface ISwipeControlItem
    {

        bool CanGoback { get; }

        void GoBack();
        void GotViewFocus();
        void LostViewFocus();
        void ViewPageLaunched(object lastReleasedViewPageparam);
        void ReleasingViewPage(out object saveParam);
    }
}
