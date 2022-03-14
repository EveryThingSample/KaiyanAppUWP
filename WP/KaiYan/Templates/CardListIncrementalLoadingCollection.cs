using KaiYan.Core.Page;
using Microsoft.Toolkit.Uwp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Templates
{
    public class CardListIncrementalLoadingCollection : IncrementalLoadingCollection<CardListItemSource, CardListItem>
    {
        public CardListIncrementalLoadingCollection()
        {
        }
        internal new CardListItemSource Source => base.Source;

        public void SetPageTool(IPageTool pageTool)
        {
            this.Source.PageTool = pageTool;
        }
    }
}
