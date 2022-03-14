using KaiYan.Core.API;
using KaiYan.Core.Exceptions;
using KaiYan.Core.Page.Card;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Body
{
    public abstract class BodyBase: ICover, IMainPageTool
    {
        public BodyBase(JObject jobject)
        {
            Description = jobject.GetValue("description").ToObject<string>();
        }
        
        public string Description { get; }

        public abstract string Id { get; }

        public abstract BodyType BodyType { get; }

        public abstract bool IsFollowed { get; protected set; }

        public async Task<bool> SetIsFollowedAsync(bool value)
        {
            checkAuth();
            try
            {
                if (value)
                {
                    await FollowHttp.AddAsync(Id, BodyType.ToString());
                }
                else
                {
                    await FollowHttp.CancelAsync(Id, BodyType.ToString());
                }
            }
            catch (HttpException ex)
            {
                return false;
            }
            IsFollowed = value;
            return true;
        }
        public string GetDescription() => Description;

        public abstract string GetIcon();

        public abstract string GetTitle();



        internal void checkAuth()
        {
            if (!Account.IsLogin)
                throw new NotLoginException();
        }
        public async Task<IList<PageTool>> GetPageToolsAsync()
        {
            string url;
            if (BodyType == BodyType.tag)
            {
                url = "https://baobab.kaiyanapp.com/api/v6/tag/index?&id="+Id;
            }
            else
            {
                url = "https://baobab.kaiyanapp.com/api/v5/userInfo/tab?&id="+Id+"&userType=" + (BodyType == BodyType.author ? "PGC" : "NORMAL");
            }
            var table = new TabListTool(url);
            var PageTools = await table.GetPageToolsAsync();
            return PageTools;
        }
        public static TabListTool CreateTabListTool(string id, BodyType bodyType)
        {
            string url;
            if (bodyType == BodyType.tag)
            {
                url = "https://baobab.kaiyanapp.com/api/v6/tag/index?&id=" + id;
            }
            else
            {
                url = "https://baobab.kaiyanapp.com/api/v5/userInfo/tab?&id=" + id + "&userType=" + (bodyType == BodyType.author ? "PGC" : "NORMAL");
            }
            return new TabListTool(url);
        }
    }
}
