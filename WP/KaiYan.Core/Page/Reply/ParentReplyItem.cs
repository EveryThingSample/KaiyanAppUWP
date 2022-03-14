using KaiYan.Core.Page.Body;
using KaiYan.Core.Page.Card;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Reply
{
    internal class ParentReplyItem: IReplyItem
    {
        internal ParentReplyItem(JObject jobject)
        {
            initData(jobject);
        }
        public string Message { get; private set; }

        public IReplyItem ParentReply { get; private set; }
        public string ReplyId { get; private set; }
        public User User { get; private set; }

        public string Id => "0";

        public string ActionUrl => null;

        public CardType GetCardType() => CardType.reply;

        private void initData(JObject jobject)
        {
            ReplyId = jobject.GetValue("id").ToObject<string>();
            Message = jobject.GetValue("message").ToObject<string>();
            User = new User(jobject.GetValue("user").ToObject<JObject>());
        }
    }
}
