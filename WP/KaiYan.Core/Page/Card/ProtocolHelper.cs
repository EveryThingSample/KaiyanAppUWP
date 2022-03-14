using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Card
{
    public static class ProtocolHelper
    {
        public static object CreateTool(string ActionUrl)
        {
            if (ActionUrl != null)
            {
                var uri = new Uri(ActionUrl);
                string url;
                switch (uri.Host)
                {
                    case "ranklist":

                        url = "https://baobab.kaiyanapp.com/api/v4/rankList";
                        return new TabListTool(url);
                    case "tag":

                        var tagId = uri.AbsolutePath.Substring(1, uri.AbsolutePath.Length - 1);
                        if (tagId.EndsWith("/"))
                            tagId = tagId.Substring(0, tagId.Length - 1);
                        url = "https://baobab.kaiyanapp.com/api/v6/tag/index?id=" + tagId;
                        return new TabListTool(url);
                        break;
                    case "userfollow":

                        var uId = uri.AbsolutePath.Substring(1, uri.AbsolutePath.Length - 1);
                        url = "https://baobab.kaiyanapp.com/api/v2/follow/newlist?uid=" + uId;
                        return new TabListTool(url);
                    case "webview":
                    case "common":
                        url = uri.Query.Substring(uri.Query.IndexOf("url=") + 4);
                        url = Uri.UnescapeDataString(url);
                        return new Uri(url);
                        break;
                    case "ugc":
                        if (uri.AbsolutePath.StartsWith("/detail/"))
                        {
                            var id = uri.AbsolutePath.Substring("/detail/".Length);
                            id = id.Substring(0,id.IndexOf("/"));

                            return Body.BodyBase.CreateTabListTool(id, Body.BodyType.author);
                        }
                        break;
                    default:
                        var action = uri.Host;
                        break;

                }
            }
            return null;
        }
    }
}
