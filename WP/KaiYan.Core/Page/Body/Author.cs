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
    public class Author: BodyBase
    {
        internal Author(JObject jobject):base(jobject)
        {
            Name = jobject.GetValue("name").ToObject<string>();
            Id = jobject.GetValue("id").ToObject<string>();
            Icon = jobject.GetValue("icon").ToObject<string>();
            IsFollowed = jobject.GetValue("follow").ToObject<JObject>().GetValue("followed").ToObject<bool>();
            IsPgc = jobject.GetValue("ifPgc")?.ToObject<bool>()??false;
        }
        public string Name { get; }
      
        public string Icon { get; }

        public override string Id { get; }

        public override BodyType BodyType => BodyType.author ;

        public override bool IsFollowed { get; protected set; }
        public bool IsPgc { get; }

        PgcInfo PgcInfo;


        public override string GetIcon() => Icon;

        public override string GetTitle() => Name;

        //public PgcInfo GetPgcInfo()
        //{
        //    if (IsPgc)
        //    {
        //        return PgcInfo;
        //    }
        //    else
        //        throw new Exception("is not pgc");
        //}

        
    }
}
