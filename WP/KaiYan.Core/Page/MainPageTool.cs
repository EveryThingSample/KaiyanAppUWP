using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page
{
    public class MainPageTool:IMainPageTool
    {
        internal MainPageTool(MainPageType mainPageType)
        {
            this.mainPageType = mainPageType;
        }
        MainPageType mainPageType;
        private static IList<PageTool> recommendMainPageTool = new PageTool[3]
       {
            new PageTool("推荐", "https://baobab.kaiyanapp.com/api/v5/index/tab/allRec?page=0"),
            new PageTool("日报", "https://baobab.kaiyanapp.com/api/v5/index/tab/feed"),
            new PageTool("发现", "https://baobab.kaiyanapp.com/api/v7/index/tab/discovery"),
       };
        private static IList<PageTool> categoryMainPageTool = new PageTool[]
       {
            new PageTool("广告", "https://baobab.kaiyanapp.com/api/v5/index/tab//category/14"),
            new PageTool("生活", "https://baobab.kaiyanapp.com/api/v5/index/tab//category/36"),
            new PageTool("动画", "https://baobab.kaiyanapp.com/api/v5/index/tab//category/10"),
            new PageTool("搞笑", "https://baobab.kaiyanapp.com/api/v5/index/tab//category/28"),
            new PageTool("开胃", "https://baobab.kaiyanapp.com/api/v5/index/tab//category/4"),
            new PageTool("创意", "https://baobab.kaiyanapp.com/api/v5/index/tab//category/2"),
            new PageTool("运动", "https://baobab.kaiyanapp.com/api/v5/index/tab//category/18"),
            new PageTool("音乐", "https://baobab.kaiyanapp.com/api/v5/index/tab//category/20"),
            new PageTool("萌宠", "https://baobab.kaiyanapp.com/api/v5/index/tab//category/26"),
            new PageTool("剧情", "https://baobab.kaiyanapp.com/api/v5/index/tab//category/12"),
            new PageTool("科技", "https://baobab.kaiyanapp.com/api/v5/index/tab//category/32"),
            new PageTool("旅行", "https://baobab.kaiyanapp.com/api/v5/index/tab//category/6"),
            new PageTool("记录", "https://baobab.kaiyanapp.com/api/v5/index/tab//category/22"),
            new PageTool("游戏", "https://baobab.kaiyanapp.com/api/v5/index/tab//category/30"),
            new PageTool("综艺", "https://baobab.kaiyanapp.com/api/v5/index/tab//category/38"),
            new PageTool("时尚", "https://baobab.kaiyanapp.com/api/v5/index/tab//category/24"),
            new PageTool("综艺", "https://baobab.kaiyanapp.com/api/v5/index/tab//category/38"),
       };
        private static IList<PageTool> ugcMainPageTool = new PageTool[]
       {
            new PageTool("推荐", "https://baobab.kaiyanapp.com/api/v5/index/tab/ugcSelected"),
            new PageTool("关注","https://baobab.kaiyanapp.com/api/v6/community/tab/follow")
       };
        private IList<PageTool> GetPageTools()
        {
            switch(mainPageType)
            {
                case MainPageType.Recommend:
                    return recommendMainPageTool;
                    break;
                case MainPageType.Category:
                    return categoryMainPageTool;
                    break;
                case MainPageType.Ugc:
                    return ugcMainPageTool;
                    break;
                case MainPageType.none:

                    break;
            }
            throw new NotImplementedException();
        }
        public Task<IList<PageTool>> GetPageToolsAsync()
        {
            return Task.FromResult(GetPageTools());
        }
    }
}
