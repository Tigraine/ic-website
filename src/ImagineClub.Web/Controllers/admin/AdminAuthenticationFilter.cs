using System;
using Castle.MonoRail.Framework;
using ImagineClub.Web.Models;

namespace ImagineClub.Web.Controllers.admin
{
    public class AdminAuthenticationFilter : IFilter
    {
        public bool Perform(ExecuteWhen exec, IEngineContext context, IController controller, IControllerContext controllerContext)
        {
            if (context.CurrentUser == null || context.CurrentUser.Identity.IsAuthenticated == false
                || (context.CurrentUser is Administrator))
            {
                context.Flash["error"] = "Nur Administratoren haben Zugriff";
                context.Response.Redirect("", "home", "index");
                return false;
            }
            return true;
        }
    }
}