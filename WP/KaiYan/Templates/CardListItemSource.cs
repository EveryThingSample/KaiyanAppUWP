using KaiYan.Core.Exceptions;
using KaiYan.Core.Page;
using KaiYan.Core.Page.Card;
using KaiYan.Templates.Models;
using Microsoft.Toolkit.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;

namespace KaiYan.Templates
{
    public class CardListItemSource : IIncrementalSource<CardListItem>
    {
        internal IPageTool PageTool;
     
        public CardListItemSource()
        {
           
        }
        bool blocked = false;
        public bool Blocked
        {
            get => blocked; set
            {
                if (blocked != value)
                {
                    blocked = value;
                    if (value)
                        blockSlim.WaitAsync();
                    else
                        blockSlim.Release();
                }
            }
        }
        private void Block()
        {
            blockSlim.WaitAsync();
        }
        private void Release()
        {
            if (blockSlim.CurrentCount == 0)
                blockSlim.Release();
        }
        SemaphoreSlim blockSlim = new SemaphoreSlim(1, 1);
        public async Task<IEnumerable<CardListItem>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            if (blockSlim.CurrentCount == 0)
            {
                await blockSlim.WaitAsync();
                blockSlim.Release();
            }
            try
            {
                var _CardListItems = new List<CardListItem>();
                
                try
                {
                    Loading?.Invoke(this, true);
                }catch(Exception)
                { }
               var result = await PageTool.GetNextItemsAsync();
                foreach (var item in result)
                {
                    _CardListItems.Add(CardListItemFactory.Create(item));
                    if (item is ICollectionCard collectionCard)
                    {
                        foreach(var _item in collectionCard.Items)
                        {
                            _CardListItems.Add(CardListItemFactory.Create(_item));
                        }
                    }
                }
                try
                {
                    Loading?.Invoke(this, false);
                }
                catch (Exception)
                { }
                return _CardListItems;
            }
            catch(NoMoreItemException)
            {
                try
                {
                    Loading?.Invoke(this, false);
                    NoMore?.Invoke(this, true);
                }
                catch (Exception)
                { }
            }
            return null;
        }

        public event TypedEventHandler<CardListItemSource, bool> Loading;
        public event TypedEventHandler<CardListItemSource, object> NoMore;
    }
    
}
