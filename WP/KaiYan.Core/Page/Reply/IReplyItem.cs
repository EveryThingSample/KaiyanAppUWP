using KaiYan.Core.Page.Body;
using KaiYan.Core.Page.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Reply
{
    public interface IReplyItem: ICardItem
    {
        string ReplyId { get; }
        string Message { get; }
        IReplyItem ParentReply { get; }
        User User { get; }
    }
}
