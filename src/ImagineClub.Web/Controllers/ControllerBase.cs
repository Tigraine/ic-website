namespace ImagineClub.Web.Controllers
{
    using Castle.MonoRail.Framework;
    using Helpers;

    [Layout("default"), Rescue(typeof(RescueController)), Helper(typeof(HtmlStringHelper)), Helper(typeof(FileHelper))]
    public abstract class ControllerBase : SmartDispatcherController
    {
        
    }
}