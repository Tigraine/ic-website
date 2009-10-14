using Castle.MonoRail.Framework;

namespace ImagineClub.Web.Controllers.admin
{
    using System.Collections.Generic;
    using System.Reflection;
    using Castle.ActiveRecord;
    using Castle.MonoRail.ActiveRecordSupport;
    using Castle.MonoRail.Framework.Helpers;
    using Models;
    using NHibernate.Criterion;

    [Filter(ExecuteWhen.BeforeAction, typeof(AdminAuthenticationFilter))]
    [ControllerDetails(Area = "admin"), Layout("admin")]
    public class UsersController : SmartDispatcherController
    {
        public const int PAGE_SIZE = 15;
        public void List([DefaultValue(1)] int page, [DefaultValue("Username")] string order)
        {
            Member[] members = Member.FindAll(Order.Asc(order));
            PropertyBag["members"] = PaginationHelper.CreatePagination<Member>(members, PAGE_SIZE, page);
        }

        public void Edit([ARFetch("id")] Member member)
        {
            PropertyBag["member"] = member;
        }

        public void ResetPassword([ARFetch("id")] Member member)
        {
            PropertyBag["member"] = member;
        }

        public void ResetPassword([ARFetch("id")] Member member, string password)
        {
            using (new SessionScope())
            {
                member.Password = Member.HashPassword(password);
                member.Save();
                Flash["info"] = "Password reset";
                RedirectToAction("Edit", new Dictionary<string, object> {{"id", member.Id}});
            }
        }
    }
}