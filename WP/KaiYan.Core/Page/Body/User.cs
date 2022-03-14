using KaiYan.Core.Page.Card;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Body
{
    public class User : BodyBase
    {
        internal User(JObject jobject):base(jobject)
        {
            UId = jobject.GetValue("uid").ToObject<string>();
            Name = jobject.GetValue("nickname").ToObject<string>();
            Avatar = jobject.GetValue("avatar").ToObject<string>();
            IsFollowed = jobject.GetValue("followed").ToObject<bool>();
            City= jobject.GetValue("city").ToObject<string>();
            Area = jobject.GetValue("area").ToObject<string>();
        }
        public string City { get; }
        public string Area { get; }
        public string Name { get; }

        public string Avatar { get; }

        public string UId { get; }

        public override string Id =>UId;

        public override BodyType BodyType =>  BodyType.user;

        public override bool IsFollowed { get; protected set; }

        public override string GetIcon() => Avatar;

        public override string GetTitle() => Name;
    }
}
