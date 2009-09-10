namespace ImagineClub.Web.Controllers
{
    using System;
    using Castle.MonoRail.Framework;
    using Models;

    public class XmlRpc : SmartDispatcherController
    {
        public void Manifest()
        {
            Console.WriteLine("Test)");
        }

        [Layout("default")]
        public void WebLayout()
        {
            PropertyBag["post"] = new NewsPost
                                      {
                                          PostDate = DateTime.Now,
                                          Text = "{post-body}",
                                          Title = "{post-title}",
                                          PostedBy = null,
                                      };
            RenderView("Home", "Detail");
        }

        [Layout("default")]
        public void WebPreview()
        {
        }
    }
}