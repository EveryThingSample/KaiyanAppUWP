using KaiYan.Core.API;
using KaiYan.Core.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core
{
    public class Account:IAccount
    {
        
        internal Account()
        {
            IsAnonymous = true;
            Current = this;
        }
        internal Account(AccountHttp account)
        {
            var member = account.member;
            //CookieContainer = new CookieContainer();
            ky_auth = account.ky_auth;
            uid = member.uid;
            name = member.nick;
            username = member.username;
            udid = member.devices[0].udid;
            avatar = member.avatar;
            IsAnonymous = false;

            Current = this;
        }

        public string uid { get; private set; }
        public static Account Current { get; private set; } = new Account();
        public static bool IsLogin => Current?.IsAnonymous == false;

        public string avatar { get; private set; }
        public string name { get; private set; }
        public string username { get; private set; }
        public string udid { get; private set; }

        private string ky_auth;


        public bool IsAnonymous { get; private set; }




        internal async Task initAsync(string ky_auth)
        {
            try
            {
                IsAnonymous = false;
                this.ky_auth = ky_auth;
                var account = await AccountHttp.GetProfileAsync();
                var member = account.member;
                uid = member.uid;
                name = member.nick;
                username = member.username;
                udid = member.devices[0].udid;
                avatar = member.avatar;
            }
            finally
            {
                if (uid == null)
                    IsAnonymous = true;
            }
        }



        public string GetAuthKey()
        {
            return ky_auth;
        }

        public void Logout()
        {
            IsAnonymous = true;
            uid = null;
            ky_auth = null;
        }

        public PageManager GetPageManager()
        {
            return new PageManager();
        }
        public PageTool GetColloctionPageTool() =>
            new PageTool("收藏", "https://baobab.kaiyanapp.com/api/v1/collection/list");
        public PageTool GetHistoryPageTool() =>
            new PageTool("观看记录", "http://baobab.kaiyanapp.com/api/v4/history");
        public TabListTool GetFollowTabListTool() => new TabListTool("https://baobab.kaiyanapp.com/api/v2/follow/newlist");
    }
}
