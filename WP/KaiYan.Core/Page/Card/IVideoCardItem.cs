
using KaiYan.Core.Page.Card.Headers;
using KaiYan.Core.Page.Card.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Card
{
    public interface IVideoCardItem: IPlayCardItem
    {
        string DurationText { get; }
        string Category { get; }
        VideoResource GetVideoResource();
    }
}
