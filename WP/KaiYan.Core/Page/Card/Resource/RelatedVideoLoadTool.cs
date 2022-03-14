using KaiYan.Core.API;
using KaiYan.Core.Exceptions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Card.Resource
{
    public class RelatedVideoLoadTool : IPageTool
    {
        internal RelatedVideoLoadTool(string videoId)
        {
            this.videoId = videoId;
        }
        string videoId;
        bool hasMore = true;
        public async Task<IList<ICardItem>> GetNextItemsAsync()
        {
            if (HasMore())
            {
                hasMore = false;
                var _cardItems = new List<ICardItem>();
                var jobject = await VideoHttp.GetRelatedVideo(videoId);
                var itemList = jobject.GetValue("itemList").ToArray();
                if (itemList.Length > 0)
                {

                    foreach (var item in jobject.GetValue("itemList"))
                    {
                        var jobjectitem = item.ToObject<JObject>();
                        var it = CardItemFactory.GetCardItem(jobjectitem);
                        if (it != null)
                            _cardItems.Add(it);
                    }
                }
                else
                {
                    throw new NoMoreItemException();
                }
                
                return _cardItems;
            }
            else
                throw new NoMoreItemException();
        }

        public bool HasMore() => hasMore;
        public void Reset()
        {
            hasMore = true;
        }
    }
}
