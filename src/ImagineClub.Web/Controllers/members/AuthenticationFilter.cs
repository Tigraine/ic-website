namespace ImagineClub.Web.Controllers
{
    using Castle.MonoRail.Framework;

    public class AuthenticationFilter : IFilter
    {
        public bool Perform(ExecuteWhen exec, IEngineContext context, IController controller, IControllerContext controllerContext)
        {
            if (context.CurrentUser == null || context.CurrentUser.Identity.IsAuthenticated == false)
            {
                context.Flash["error"] = "Du musst eingeloggt sein um den Mitglieder Bereich zu sehen";
                context.Response.Redirect("", "home", "index");
                return false;
            }
            return true;
        }
    }
}