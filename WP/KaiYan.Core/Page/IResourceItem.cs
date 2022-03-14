using KaiYan.Core.Page.Body;
using KaiYan.Core.Page.Card;
using KaiYan.Core.Page.Card.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page
{
    public interface IResourceItem:IKaiYanBase
    {
        ResourceType ResourceType { get; }
        BodyBase Provider { get; }
    }
}
