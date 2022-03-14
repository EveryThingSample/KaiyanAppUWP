using KaiYan.Core.API;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Card.Cards
{
    public class SquareCardOfCommunityContent: SquareCardItem
    {
        public SquareCardOfCommunityContent(JObject jobject) : base(jobject)
        {
            JObject datajobject = jobject.GetValue("data").ToObject<JObject>();
            SubTitle = datajobject.GetValue("subTitle").ToObject<string>();
        }

        public string SubTitle { get; }

        public string BgPicture { get; }

        public string tabListUrl = "https://baobab.kaiyanapp.com/api/v7/tag/tabList";

        public IMainPageTool GetMainPageTool()
        {
            if (ActionUrl == "eyepetizer://community/tagSquare?tabIndex=0")
            {
                return new TabListTool(tabListUrl);
            }
            else
                throw new NotImplementedException();
        }

     

    }
}
