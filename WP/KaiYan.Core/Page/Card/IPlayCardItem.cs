using KaiYan.Core.Page.Card.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Card
{
   public interface IPlayCardItem:ICardItem
    {
        string Title { get; }
        string Description { get; }
        string Cover { get; }
        ResourceItem GetResource();
        ICover GetHeader();
    }
}
