using KaiYan.Core.API;
using KaiYan.Core.Exceptions;
using KaiYan.Core.Page.Card;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Reply
{
    public class ReplyTool : IPageTool
    {
        internal ReplyTool(IResourceItem resourceItem)
        {
            this.resourceItem = resourceItem;
        }

        IResourceItem resourceItem;

        int sequence = -1;
        List<ICardItem> CardItems = new List<ICardItem>();
        public async Task<IList<ICardItem>> GetReplayItemsAsync()
        {
            if (CardItems.Count == 0)
            {
                return await GetNextItemsAsync();
            }
            return CardItems;
        }
        public async Task<IList<ICardItem>> GetNextItemsAsync()
        {
            if (HasMore())
            {
                var _cardItems = new List<ICardItem>();
                var jobject = await ReplyHttp.GetRepliesAsync(resourceItem.Id, resourceItem.ResourceType.ToString(), 20, sequence);
                var itemList = jobject.GetValue("itemList").ToArray();

                sequence = 1;
                if (itemList.Length > 0)
                {
                    foreach (var item in jobject.GetValue("itemList"))
                    {
                        var jobjectitem = item.ToObject<JObject>();
                        var _cardItem = CardItemFactory.GetCardItem(jobjectitem, resourceItem);
                        if(_cardItem!=null)
                            _cardItems.Add(_cardItem);
                        if (_cardItem is ReplyItem replyItem)
                            sequence = replyItem.Sequence;
                    }
                    CardItems.AddRange(_cardItems);
                    return _cardItems;
                }
            }
            throw new NoMoreItemException();
        }
        public bool HasMore()
        {
            return sequence != 1;
        }
        public void Reset()
        {
            sequence = -0;
            CardItems.Clear();
        }
    }
}
