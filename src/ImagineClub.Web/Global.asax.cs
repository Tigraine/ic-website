using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Config;
using ImagineClub.Web.Models;

namespace ImagineClub.Web
{
    using System.Web;
    using System.Web.Security;
    using Castle.Core.Logging;
    using Castle.Windsor;

    public class Global : System.Web.HttpApplication, IContainerAccessor
    {
        private ILogger logger = NullLogger.Instance;
        private static IWindsorContainer container;

        protected void Application_Start(object sender, EventArgs e)
        {
            container = new ImagineClubContainer();
            HibernatingRhinos.NHibernate.Profiler.Appender.NHibernateProfiler.Initialize();

            ActiveRecordStarter.Initialize(
                typeof(Member).Assembly,
                ActiveRecordSectionHandler.Instance);

            // If you want to let ActiveRecord create the schema for you:
            /*ActiveRecordStarter.CreateSchema();*/
            ActiveRecordStarter.UpdateSchema();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies.Get(FormsAuthentication.FormsCookieName);
            if (cookie == null)
                return;
            var id = GetUserId(cookie);

            Member current = Member.Find(id);
            if (current == null)
            {
                //This means that we've a cookie for a user that has been removed, we'll
                //remove the cookie and redirect to the default page, if the user will
                //try to log in again, they will get the usual message, and then it is
                //the IT problem.
                RemoveAuthCookieAndRedirectToDefaultPage();
            }
            Context.User = current;
        }

        private long GetUserId(HttpCookie cookie)
        {
            try
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                return int.Parse(ticket.UserData);
            }
            catch (ArgumentException ae)
            {
                logger.ErrorFormat(
                    "A possible hack attempt, got an invalid cookie value: '{0}' from {1}",
                    cookie.Value, Request.UserHostAddress);
                //Somebody tried to mess with the cookie, could be a hacker or transimission
                //failure, we'll remove the cookie and send them to the default page.
                RemoveAuthCookieAndRedirectToDefaultPage();
                return -1;//will never reach here, the previous method will abort the thread
            }
        }

        private void RemoveAuthCookieAndRedirectToDefaultPage()
        {
            FormsAuthentication.SignOut();
            Response.Redirect("/home/default.rails", true);
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            container.Dispose();
        }

        public IWindsorContainer Container
        {
            get { return container; }
        }
    }
}