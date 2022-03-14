using EveryThingSampleTools.WP.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace KaiYan.Converters
{
    public class TimestampToTimeComverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            long timeStamp = (long)value;
            if (timeStamp <= 0)
                return "";
            if (TimeTool.NowTimeStamp/ 86400 == timeStamp / 86400)
            {
                return TimeTool.ConvertToDateTime(timeStamp).ToString("HH:mm");
            }
            return TimeTool.ConvertToDateTime(timeStamp).ToString("yyyy-MM-dd");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
