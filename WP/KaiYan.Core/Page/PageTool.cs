using KaiYan.Core.API;
using KaiYan.Core.Exceptions;
using KaiYan.Core.Page.Card;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page
{
    public class PageTool :  IPageTool
    {
        public string Name { get; }
        internal PageTool(string name,string url)
        {
            this.Name = name;
            this.url = url;
            baseUrl = url;
        }

        string url, baseUrl;
        List<ICardItem> CardItems = new List<ICardItem>();
        public async Task<IList<ICardItem>> GetCardItemsAsync()
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
                var jobject = await VideoHttp.GetVideo(url);
                var itemList = jobject.GetValue("itemList").ToArray();
                if (itemList.Length > 0)
                {
                    url = jobject.GetValue("nextPageUrl").ToString();
                    if (url?.StartsWith("http://baobab.kaiyanapp.com/api/v5/index/tab/category/") == true)
                    {
                        url = url.Replace("http://baobab.kaiyanapp.com/api/v5/index/tab/category/", "https://baobab.kaiyanapp.com/api/v5/index/tab//category/");
                    }
                    foreach (var item in jobject.GetValue("itemList"))
                    {
                        var _cardItem = CardItemFactory.GetCardItem(item.ToObject<JObject>());
                        if (_cardItem != null)
                            _cardItems.Add(_cardItem);
                    }
                    CardItems.AddRange(_cardItems);
                    return _cardItems;
                }
                else
                    url = null;
            }
            throw new NoMoreItemException();
        }
        public bool HasMore()
        {
            return !string.IsNullOrEmpty(url);
        }
        public void Reset()
        {
            url = baseUrl;
            CardItems.Clear();
        }
    }
}
