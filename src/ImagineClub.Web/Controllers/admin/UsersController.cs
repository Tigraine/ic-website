using Castle.MonoRail.Framework;

namespace ImagineClub.Web.Controllers.admin
{
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
    }
}