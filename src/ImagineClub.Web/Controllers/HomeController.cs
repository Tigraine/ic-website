namespace ImagineClub.Web.Controllers
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using Castle.ActiveRecord;
    using Castle.MonoRail.ActiveRecordSupport;
    using Castle.MonoRail.Framework;
    using Castle.MonoRail.Framework.Helpers;
    using Models;
    using Models.Commands;

    public class HomeController : ControllerBase
    {
        public const int PageSize = 5;
        public void Index([DefaultValue(1)]int page)
        {
            using(new SessionScope())
            {
                IList<NewsPost> posts = NewsPost.FindRecent(PageSize * 4);
                PropertyBag["news"] = PaginationHelper.CreatePagination(posts, PageSize, page);
            }
        }

        public void Detail([ARFetch("id")] NewsPost post)
        {
            PropertyBag["post"] = post;
        }

        //public void Detail([ARFetch("id")] NewsPost post, [DataBind("Comment")] Comment comment)
        //{
        //    if (comment.IsValid())
        //    {
        //        var addComment = new AddComment(post, comment, Context);
        //        addComment.Execute();
        //        RedirectToAction("Thanks", new NameValueCollection(){ {"Id", post.Id.ToString()}});
        //    }
        //    else
        //    {
        //        PropertyBag["post"] = post;
        //        PropertyBag["Comment"] = comment;
        //    }
        //}

        public void Thanks()
        {
        }

        public void Impressum()
        {
            
        }
    }
}