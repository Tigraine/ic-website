using Castle.MonoRail.Framework;

namespace ImagineClub.Web.Controllers.admin
{
    using Castle.Components.Pagination;
    using Models;
    using NHibernate.Criterion;

    [Filter(ExecuteWhen.BeforeAction, typeof(AdminAuthenticationFilter))]
    [ControllerDetails(Area = "admin")]
    public class UsersController : ControllerBase
    {
        public const int PAGE_SIZE = 20;
        public void List([DefaultValue(1)] int page, [DefaultValue("Username")] string order)
        {
            Member[] members = Member.FindAll(Order.Asc(order));
            var genericPage = new GenericPage<Member>(members, page, PAGE_SIZE);
            PropertyBag["members"] = genericPage;
        }
    }
}