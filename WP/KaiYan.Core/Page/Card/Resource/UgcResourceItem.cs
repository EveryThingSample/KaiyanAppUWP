using KaiYan.Core.Page.Body;
using KaiYan.Core.Page.Card.Headers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Card.Resource
{
    public class UgcResourceItem: ResourceItem
    {
        internal UgcResourceItem(JObject jobject):base(jobject)
        {
            Owner = new User(jobject.GetValue("owner").ToObject<JObject>());
            Area = jobject.GetValue("area")?.ToString()??"";
        }
        public User Owner { get; }
        public string Area { get; }
        public override BodyBase Provider => Owner;
    }
}
