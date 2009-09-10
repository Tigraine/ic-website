namespace ImagineClub.Web.ViewComponents
{
    using Castle.MonoRail.Framework;

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
                RenderView("LoggedIn");
        }
    }
}