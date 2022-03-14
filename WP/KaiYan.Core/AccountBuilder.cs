using KaiYan.Core.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core
{
    public class AccountBuilder
    {
        private string password = null,username = null,ky_auth = null;
        public AccountBuilder WithPassword(string password)
        {
            this.password = password;
            return this;
        }
        public AccountBuilder WithUsername(string username)
        {
            this.username = username;
            return this;
        }
        public AccountBuilder WithKyAuth(string ky_auth)
        {
            this.ky_auth = ky_auth;
            return this;
        }
        

        public async Task<IAccount> BuildAsync()
        {
            if (ky_auth == null)
            {
                if (username != null && password != null)
                {
                    var _account = await AccountHttp.Loginasync(username, password);
                    Windows.Storage.ApplicationData.Current.LocalSettings.Values["ky_auth"] = _account.ky_auth;
                    return new Account(_account);
                }
                else
                {
                    ky_auth = Windows.Storage.ApplicationData.Current.LocalSettings.Values["ky_auth"] as string;
                }
            }
            if (ky_auth == null)
                return new Account();
            var account = new Account();
            await account.initAsync(ky_auth);
            return account;
        }
    }
}
