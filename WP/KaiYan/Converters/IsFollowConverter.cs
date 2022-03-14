using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace KaiYan.Converters
{
    class IsFollowConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool follow = (bool)value;
            return follow ? "已关注":"+ 关注";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
