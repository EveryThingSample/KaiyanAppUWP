using KaiYan.Core.API;
using KaiYan.Core.Page.Body;
using KaiYan.Core.Page.Reply;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Card.Resource
{
    public class VideoResource: ResourceItem,IVideoResource
    {
        internal VideoResource(JObject jobject):base(jobject)
        {
            Category = jobject.GetValue("category").ToString();
            Author = new Author(jobject.GetValue("author").ToObject<JObject>());
            WebUrl = jobject.GetValue("webUrl").ToObject<JObject>()?.GetValue("raw").ToString();
            Duration = jobject.GetValue("duration").ToObject<int>();
            initPlayInfo();
        }
        public string Category { get; }
        public int Duration { get; }
        public string DurationText => (Duration / 60).ToString("d2") + ":" + (Duration % 60).ToString("d2");

        public string WebUrl { get; private set; }
        public Author Author { get; }

        public override BodyBase Provider => Author;

        IList<PlayInfo> playInfos;
        public IList<PlayInfo> GetPlayInfos() => playInfos;




        private void initPlayInfo()
        {
            playInfos = new List<PlayInfo>();
            var jobjects = jobject.GetValue("playInfo")?.ToArray();
            if (jobjects?.Length > 0)
            {
                foreach (var item in jobjects)
                {
                    playInfos.Add(new PlayInfo(item.ToObject<JObject>()));
                }
            }
            else
            {
                playInfos.Add(new PlayInfo("默认", jobject.GetValue("playUrl")?.ToString()));
            }
        }


        public RelatedVideoLoadTool GetRelatedVideoLoadTool() => new RelatedVideoLoadTool(Id);

        public async void ReportViewTime(int timePoint)
        {
            try
            {
                if (timePoint > 0)
                { 
                    ViewTimeDic[Id] = timePoint;
                    if (Account.IsLogin)
                    {
                        if (this.ResourceType == ResourceType.video)
                        {
                            await ConsumptionHttp.ReportTimeAsync(Id, timePoint);
                            await ConsumptionHttp.AddAsync(Id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public int GetViewTime()
        {
            if (ViewTimeDic.ContainsKey(Id))
                return ViewTimeDic[Id];
            return 0;
        }

        private static Dictionary<string, int> ViewTimeDic = new Dictionary<string, int>();
    }
}
