using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page
{
    public class PageManager
    {
        internal PageManager()
        {

        }

        public MainPageTool GetRecomentMainPageTool() => new MainPageTool(MainPageType.Recommend);
        public MainPageTool GetCategoryPageTool() => new MainPageTool(MainPageType.Category);
        public MainPageTool GetUgcPageTool() => new MainPageTool(MainPageType.Ugc);

        
    }
}
