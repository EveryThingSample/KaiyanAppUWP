using KaiYan.Core.Page.Card.Cards;
using KaiYan.Core.Page.Reply;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page.Card
{
    internal static class CardItemFactory
    {
        public static ICardItem GetCardItem(JObject jobject,IKaiYanBase parent = null)
        {
            ICardItem result = null;
            switch (jobject.GetValue("type").ToString())
            {
                case "squareCardCollection":
                    result = new SquareCardCollection(jobject);
                    break;
                case "followCard":
                    try
                    {
                        result = new FollowCard(jobject);
                    }catch (Exception) { }
                    break;
                case "videoSmallCard":
                    try
                    {
                        result = new VideoSmallCard(jobject);
                    }catch(Exception ex)
                    {

                    }
                    break;
                case "pictureFollowCard":
                    result = new PictureFollowCard(jobject);
                    break;
                case "autoPlayFollowCard":
                    result = new AutoPlayFollowCard(jobject);
                    break;
                case "textCard":
                    result = new TextCard(jobject);
                    break;
                case "reply":
                    var _result = new ReplyItem(jobject, (IResourceItem)parent);
                    if (_result.User != null)
                        result = _result;
                    break;
                case "horizontalScrollCard":
                    result = new HorizontalScrollCard(jobject);
                    break;
                case "specialSquareCardCollection":
                    break;
                case "columnCardList":
                    break;
                case "briefCard":
                    result = new BriefCard(jobject);
                    break;
                case "simpleHotReplyScrollCard":
                    break;
                case "banner":
                    result = new BannerCard(jobject);
                    break;
                case "squareCardOfCommunityContent":
                    result = new SquareCardOfCommunityContent(jobject);
                    break;
                case "userListCard":
                    break;
                case "tagSquareCardCollection":
                    break;
                case "videoCollectionOfHorizontalScrollCard":
                    break;
                case "videoCollectionWithBrief":
                    break;
                case "DynamicInfoCard":
                    break;
                default:
                    var cardType = jobject.GetValue("type").ToString();
                    //throw new NotImplementedException();
                    break;
            }
            return result;
        }
    }
}
