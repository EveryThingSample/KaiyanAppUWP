using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Card.Resource
{
    public interface IVideoResource: IResourceItem
    {
        string Category { get; }
        int Duration { get; }
        string DurationText { get; }
    }
}
