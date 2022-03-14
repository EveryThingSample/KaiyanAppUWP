using KaiYan.Core.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core
{
    public interface IAccount
    {
        string name { get; }
        string avatar { get; }
        string uid { get; }

        void Logout();

        PageManager GetPageManager();
    }
}
