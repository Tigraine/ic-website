namespace ImagineClub.Web.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web.Security;
    using Castle.ActiveRecord;
    using Castle.Components.Common.EmailSender;
    using Castle.MonoRail.Framework;
    using Models;
    using Models.Services;
    using NHibernate.Criterion;


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

        public void RecoverPassword()
        {
        }

        public void RecoverPassword(string email)
        {
            var criteria = DetachedCriteria.For(typeof (Member))
                .Add(Restrictions.Eq("Email", email))
                .SetMaxResults(1);
            var member = Member.FindOne(criteria);
            if (member == null)
            {
                Flash["recovery_error"] = "Die angegebene Email adresse ist uns nicht bekannt";
                Flash["email"] = email;
            }
            else
            {
                string password = GenerateRandomPassword();
                using (new SessionScope())
                {
                    member.Password = Member.HashPassword(password);
                    member.Save();
                }
                var parameter = new Dictionary<string, object> { { "password", password }, {"member", member} };
                Message message = RenderMailMessage("PasswordRecovery", null, (IDictionary)parameter);
                message.Encoding = System.Text.Encoding.UTF8;
                DeliverEmail(message);
                RedirectToAction("RecoveryComplete");
            }
        }

        public void RecoveryComplete()
        {
        }

        private string GenerateRandomPassword()
        {
            var random = new Random();
            double d = random.NextDouble();
            string randomPwd =
                Member.HashPassword(DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString() +
                                    d);
            return randomPwd.Substring(0, 6);
        }
    }
}