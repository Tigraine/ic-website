using Castle.MonoRail.Framework;

namespace ImagineClub.Web.Controllers.admin
{
    using Castle.MonoRail.Framework.Helpers;
    using Models;
    using NHibernate.Criterion;

    [Filter(ExecuteWhen.BeforeAction, typeof(AdminAuthenticationFilter))]
    [ControllerDetails(Area = "admin")]
    public class UsersController : ControllerBase
    {
        public const int PAGE_SIZE = 2;
        public void List([DefaultValue(1)] int page, [DefaultValue("Username")] string order)
        {
            Member[] members = Member.FindAll(Order.Asc(order));
            PropertyBag["members"] = PaginationHelper.CreatePagination<Member>(members, PAGE_SIZE, page);
        }
    }
}