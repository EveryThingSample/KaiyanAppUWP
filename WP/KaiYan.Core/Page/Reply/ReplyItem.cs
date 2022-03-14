using KaiYan.Core.API;
using KaiYan.Core.Exceptions;
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
    public class ReplyItem: CardItem, IReplyItem
    {
        internal ReplyItem(JObject jobject, IResourceItem resourceItem):base(jobject)
        {
            initData(jobject.GetValue("data").ToObject<JObject>());
            this.resourceItem = resourceItem;
        }

        IResourceItem resourceItem;
        public long CreateTime { get; private set; }
        public string Message { get; private set; }

        public bool IsLiked { get; private set; }

        public int LikeCount { get; private set; }
        public IReplyItem ParentReply { get; private set; }
        public string ReplyId { get; private set; }
        public User User { get; private set; }
        public int Sequence { get; private set; }
        private void initData(JObject jobject)
        {
            ReplyId = jobject.GetValue("id").ToObject<string>();
            Message = jobject.GetValue("message").ToObject<string>();
            CreateTime = jobject.GetValue("createTime").ToObject<long>() / 1000;
            IsLiked = jobject.GetValue("liked").ToObject<bool>();
            LikeCount = jobject.GetValue("likeCount").ToObject<int>();
            Sequence = jobject.GetValue("sequence").ToObject<int>();
            if (jobject.GetValue("user").ToObject<JObject>() != null)
            {
                User = new User(jobject.GetValue("user").ToObject<JObject>());
            }
            var parentReply = jobject.GetValue("parentReply").ToObject<JObject>();
            if (parentReply != null)
                ParentReply = new ParentReplyItem(parentReply);
        }
        public async Task<bool> SetIsLikedAsync(bool value)
        {
            checkAuth();
            try
            {
                if (value)
                {
                    await ReplyHttp.LikeAsync(value, ReplyId);
                    LikeCount++;
                }
                else
                {
                    await ReplyHttp.LikeAsync(value, ReplyId);
                    LikeCount--;
                }
            }
            catch (HttpException ex)
            {
                if (!((ex.ErrorCode == -3 && value) || (ex.ErrorCode == -4 && value == false)))
                    return false;
            }
            IsLiked = value;
            return true;
        }
        public async Task<bool> ReplyAsync(string message)
        {
            checkAuth();
            await ReplyHttp.AddAsync(resourceItem.Id, resourceItem.ResourceType.ToString(), message, ReplyId);
            return true;
        }
    }
}
