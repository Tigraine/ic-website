namespace ImagineClub.Web.Controllers
{
    using System;
    using System.Web.Security;
    using Models;
    using Models.Services;


    public class ProfileController : MemberControllerBase
    {
        private readonly ISecurityService securityService;

        public ProfileController(ISecurityService securityService)
        {
            this.securityService = securityService;
        }

        public void AuthenticateUser(string username, string password)
        {
            if (!securityService.AuthenticateUser(username, password))
            {
                Flash["failure"] = "Benutzername / Kennwort unbekannt";
                RenderText("Back to home page", "default");
                return;
            }
            AddAuthenticationTicket(securityService.GetAdministrator(username, password));
            Redirect("", "Home", "Index");
        }

        private void AddAuthenticationTicket(Member member)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, member.Username,
                DateTime.Now, DateTime.Now.AddMinutes(20), false, member.Id.ToString());
            string cookieValue = FormsAuthentication.Encrypt(ticket);
            Context.Response.CreateCookie(FormsAuthentication.FormsCookieName, cookieValue,
                DateTime.Now.AddMinutes(20));
            Context.CurrentUser = member;
        }

        public void Logout()
        {
            Context.Response.RemoveCookie(FormsAuthentication.FormsCookieName);
            Redirect("", "Home", "Index");
        }
    }
}