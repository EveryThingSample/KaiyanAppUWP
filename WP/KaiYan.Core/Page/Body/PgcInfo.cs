using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Body
{
    public class PgcInfo: Author
    {
        internal PgcInfo(JObject jobject) : base(jobject)
        {
            // cover = http://img.kaiyanapp.com/000d96c25e74024a64a04b596af3b7e4.png

            Cover = jobject.GetValue("cover").ToObject<string>();
        }

        public string Cover { get; }
    }
}
