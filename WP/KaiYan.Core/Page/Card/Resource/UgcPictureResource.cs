using KaiYan.Core.Page.Body;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Card.Resource
{
    public class UgcPictureResource : UgcResourceItem
    {
        internal UgcPictureResource(JObject jobject) : base(jobject)
        {
           
            initPictureUrls();
        }

        IList<string> urls;
        public IList<string> GetPictureUrls() => urls;


        private void initPictureUrls()
        {
            urls = new List<string>();
            var jobjects = jobject.GetValue("urls").ToArray();

            foreach (var item in jobjects)
            {
                urls.Add(item.ToObject<string>());
            }
        }
    }
}
