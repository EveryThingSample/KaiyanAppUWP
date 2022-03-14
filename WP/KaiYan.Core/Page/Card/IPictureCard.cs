using KaiYan.Core.Page.Card.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Card
{
    public interface IPictureCard
    {
        string Title { get; }
        string Description { get; }
        ICover Header { get; }
        UgcPictureResource GetPictureResource();
    }
}
