using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Body
{
    public class Tag:BodyBase
    {
        internal Tag(JObject jobject) : base(jobject)
        {
            Title = jobject.GetValue("title").ToObject<string>();
            Id = jobject.GetValue("id").ToObject<string>();
            Icon = jobject.GetValue("icon").ToObject<string>();
            IsFollowed = jobject.GetValue("follow").ToObject<JObject>().GetValue("followed").ToObject<bool>();
            IsPgc = jobject.GetValue("ifPgc")?.ToObject<bool>() ?? false;
            BodyType  = jobject.GetValue("follow").ToObject<JObject>().GetValue("itemType").ToObject<BodyType>();
        }

        public string Title { get; }

        public string Icon { get; }

        public override string Id { get; }

        public override BodyType BodyType { get; }

        public override bool IsFollowed { get; protected set; }
        public bool IsPgc { get; }

        PgcInfo PgcInfo;


        public override string GetIcon() => Icon;

        public override string GetTitle() => Title;
    }
}
