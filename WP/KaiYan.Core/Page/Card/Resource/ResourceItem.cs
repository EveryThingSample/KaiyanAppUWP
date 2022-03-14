using KaiYan.Core.API;
using KaiYan.Core.Exceptions;
using KaiYan.Core.Page.Body;
using KaiYan.Core.Page.Reply;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Card.Resource
{
    public abstract class ResourceItem : KaiYanBase, IResourceItem
    {
        internal ResourceItem(JObject jobject) : base(jobject)
        {

            Title = jobject.GetValue("title")?.ToString();
            Description = jobject.GetValue("description").ToString();
            library = jobject.GetValue("library").ToString();
            CreateTime = jobject.GetValue("createTime")?.ToObject<long>()/1000??0;
            ResourceType = jobject.GetValue("resourceType").ToObject<ResourceType>();
           
            IsCollected = jobject.GetValue("collected").ToObject<bool>();

            

            initCover(jobject.GetValue("cover").ToObject<JObject>());
            initConsumption(jobject.GetValue("consumption").ToObject<JObject>());
        }

        public string Title { get; }
        public string Description { get; }
        public string library { get; }

        public string DetailedCover { get; private set; }
        public string BlurredCover { get; private set; }
        public long CreateTime { get; }
        public ResourceType ResourceType { get; }
        public bool IsCollected { get; private set; }
        public int CollectionCount { get; private set; }
        public int ReplyCount { get; private set; }
        public int ShareCount { get; private set; }

        public abstract BodyBase Provider { get; }

        public ReplyTool GetReplyTool() => new ReplyTool(this);

        private void initCover(JObject jobject)
        {
            BlurredCover = jobject.GetValue("blurred")?.ToString();
            DetailedCover = jobject.GetValue("detail").ToString();
        }

        private void initConsumption(JObject jobject)
        {
            CollectionCount = jobject.GetValue("collectionCount").ToObject<int>();
            ShareCount = jobject.GetValue("shareCount").ToObject<int>();
            ReplyCount = jobject.GetValue("replyCount").ToObject<int>();
        }

        public async Task<bool> SetCollectedAsync(bool value)
        {
            checkAuth();
            try
            {
                if (value)
                {
                    await CollectionHttp.AddAsync(Id, ResourceType.ToString());
                    CollectionCount++;
                }
                else
                {
                    await CollectionHttp.CancelAsync(Id, ResourceType.ToString());
                    CollectionCount--;
                }
            }
            catch (HttpException ex)
            {
                if (!((ex.ErrorCode == -3 && value) || (ex.ErrorCode == -4 && value == false)))
                    return false;
            }
            IsCollected = value;
            return true;
        }

        public async Task<bool> ReplyAsync(string message)
        {
            checkAuth();
            await ReplyHttp.AddAsync(Id, ResourceType.ToString(), message);
            ReplyCount++;
            return true;
        }
    }
}
