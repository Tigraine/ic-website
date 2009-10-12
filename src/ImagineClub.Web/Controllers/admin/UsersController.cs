using Castle.MonoRail.Framework;

namespace ImagineClub.Web.Controllers.admin
{
    [Filter(ExecuteWhen.BeforeAction, typeof(AdminAuthenticationFilter))]
    [ControllerDetails(Area = "admin")]
    public class UsersController : ControllerBase
    {
        public void List()
        {
        }
    }
}