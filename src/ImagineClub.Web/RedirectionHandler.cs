namespace ImagineClub.Web
{
    using System.Web;

    public class RedirectionHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Redirect("/Home/Index.aspx");
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}