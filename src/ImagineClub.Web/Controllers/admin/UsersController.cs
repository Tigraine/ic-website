using Castle.MonoRail.Framework;

namespace ImagineClub.Web.Controllers.admin
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using Castle.ActiveRecord;
    using Castle.Components.Common.EmailSender;
    using Castle.MonoRail.ActiveRecordSupport;
    using Castle.MonoRail.Framework.Helpers;
    using Models;
    using NHibernate.Criterion;

    [Filter(ExecuteWhen.BeforeAction, typeof(AdminAuthenticationFilter))]
    [ControllerDetails(Area = "admin"), Layout("admin")]
    public class UsersController : SmartDispatcherController
    {
        public const int PAGE_SIZE = 15;
        public void List([DefaultValue(1)] int page, [DefaultValue("Username")] string order)
        {
            Member[] members = Member.FindAll(Order.Asc(order));
            PropertyBag["members"] = PaginationHelper.CreatePagination<Member>(members, PAGE_SIZE, page);
        }

        public void Edit([ARFetch("id")] Member member)
        {
            PropertyBag["member"] = member;
        }

        public void ResetPassword([ARFetch("id")] Member member)
        {
            PropertyBag["member"] = member;
            PropertyBag["password"] = GenerateRandomPassword();
        }

        public void ResetPassword([ARFetch("id")] Member member, string password, bool email)
        {
            using (new SessionScope())
            {
                member.Password = Member.HashPassword(password);
                member.Save();

                if (email)
                {
                    var dictionary = new Dictionary<string, object>{{"member", member}, {"password", password}};
                    var message = RenderMailMessage("admin-passwordreset", null, (IDictionary) dictionary);
                    message.Encoding = System.Text.Encoding.UTF8;
                    DeliverEmail(message);
                }

                Flash["info"] = "Password reset";
                RedirectToAction("Edit", new Dictionary<string, object> {{"id", member.Id}});
            }
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