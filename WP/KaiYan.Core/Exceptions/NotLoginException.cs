using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Exceptions
{
    public class NotLoginException:Exception
    {
        internal NotLoginException() :base("没有登录")
        { }
    }
}
