﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Exceptions
{
    public class NoMoreItemException:Exception
    {
        public NoMoreItemException():base("加载完了")
        {

        }
        
    }
}
