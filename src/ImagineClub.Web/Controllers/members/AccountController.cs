namespace ImagineClub.Web.Controllers
{
    using Castle.ActiveRecord;
    using Castle.MonoRail.Framework;
    using Models;

    [Filter(ExecuteWhen.BeforeAction, typeof(AuthenticationFilter))]
    public class AccountController : MemberControllerBase
    {
        public void Edit()
        {
            using (new SessionScope())
            {
                PropertyBag["member"] = Context.CurrentUser;
            }
        }

        public void EditPassword(string password)
        {
            using (new SessionScope())
            {
                var member = (Member)Context.CurrentUser;
                member.Password = Member.HashPassword(password);
                member.Save();
                Flash["success"] = "Kennwort wurde erfolgreich geändert";
                RedirectToAction("Edit");
            }
        }
    }
}