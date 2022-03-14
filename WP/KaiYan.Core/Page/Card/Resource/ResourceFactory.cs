using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Card.Resource
{
    static class ResourceFactory
    {
        public static ResourceItem Create(JObject jobject)
        {
            switch(jobject.GetValue("resourceType").ToObject<ResourceType>())
            {
                case ResourceType.ugc_picture:
                    return new UgcPictureResource(jobject);
                    break;
                case ResourceType.ugc_video:
                    return new UgcVideoResource(jobject);
                    break;
                case ResourceType.video:
                    return new VideoResource(jobject);
                    break;
                default:
                    throw new Exception();
            }
        }
    }
}
