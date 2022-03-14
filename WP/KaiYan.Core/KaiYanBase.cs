using KaiYan.Core.Exceptions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core
{
    public class KaiYanBase:IKaiYanBase
    {
        internal KaiYanBase(JObject jobject)
        {
            this.jobject = jobject;
            Id = jobject.GetValue("id").ToString();
        }

        internal JObject jobject;
        public string Id { get; }

        internal void checkAuth()
        {
            if (!Account.IsLogin)
                throw new NotLoginException();
        }
    }
}
