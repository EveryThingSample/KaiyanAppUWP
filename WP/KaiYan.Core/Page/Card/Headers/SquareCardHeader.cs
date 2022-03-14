using KaiYan.Core.Page.Card.Headers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Card.Headers
{
    public class SquareCardHeader:Header
    {
        internal SquareCardHeader(JObject jobject):base(jobject)
        {
            ActionUrl = jobject.GetValue("actionUrl").ToObject<string>();
            Font = jobject.GetValue("font").ToObject<string>();
            RightText = jobject.GetValue("rightText").ToObject<string>();
            SubTitle = jobject.GetValue("subTitle").ToObject<string>();
            SubTitleFont = jobject.GetValue("subTitleFont").ToObject<string>();
        }
        public string Font { get; }
        public string ActionUrl { get; }
        public string RightText { get; }
        public string SubTitle { get; }
        public string SubTitleFont { get; }
    }
}
