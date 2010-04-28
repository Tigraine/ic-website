namespace ImagineClub.Web.ViewComponents
{
    using Castle.MonoRail.Framework;
    using Models;

    public class Login : ViewComponent
    {
        public override void Initialize()
        {
            Flash["user"] = EngineContext.CurrentUser.Identity;
        }

        public override void Render()
        {
            if (!EngineContext.CurrentUser.Identity.IsAuthenticated)
                RenderView("Login");
            else
            {
                if (EngineContext.CurrentUser as Administrator != null)
                    PropertyBag["UserIsAdmin"] = true;
                RenderView("LoggedIn");
            }
        }
    }
}