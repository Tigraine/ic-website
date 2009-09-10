namespace ImagineClub.Web.Controllers
{
    using System;
    using System.IO;
    using Castle.MonoRail.Framework;
    using Elmah;
    using Helpers;

    [Layout("default"), Helper(typeof(HtmlStringHelper)), Helper(typeof(FileHelper))]
    public class RescueController : SmartDispatcherController, IRescueController
    {
        public void Rescue(Exception exception, IController controller, IControllerContext controllerContext)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            RenderSharedView(Path.Combine("rescues", "generalerror"));
        }
    }
}
