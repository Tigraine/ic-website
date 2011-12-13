using Castle.MonoRail.Framework;

namespace ImagineClub.Web.Controllers.admin
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Reflection;
    using Castle.ActiveRecord;
    using Castle.Components.Common.EmailSender;
    using Castle.MonoRail.ActiveRecordSupport;
    using Castle.MonoRail.Framework.Helpers;
    using Models;
    using Models.Services;
    using NHibernate.Criterion;

    public class Entry
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime AccountExpiration { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string MatrNr { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Category { get; set; }
        public string Birthplace { get; set; }
        public string Nationality { get; set; }
        public DateTime Birthday { get; set; }
        public string Title { get; set; }
        public string Telephone { get; set; }
    }

    [Filter(ExecuteWhen.BeforeAction, typeof (AdminAuthenticationFilter))]
    [ControllerDetails(Area = "admin"), Layout("admin")]
    public class UsersController : SmartDispatcherController
    {
        public const int PAGE_SIZE = 30;

        public void List([DefaultValue(1)] int page, [DefaultValue("Username")] string order)
        {
            PropertyBag["order"] = order;
            Member[] members = Member.FindAll(Order.Asc(order));
            PropertyBag["members"] = PaginationHelper.CreatePagination<Member>(members, PAGE_SIZE, page);
        }

        public void List([DefaultValue(1)] int page, [DefaultValue("Username")] string order, string search)
        {
            search = String.Format("%{0}%", search);
            PropertyBag["order"] = order;
            var crit = DetachedCriteria.For(typeof (Member))
                .Add(
                Restrictions.Or(
                    Restrictions.Or(
                        Restrictions.Or(
                            Restrictions.Like("Username", search),
                            Restrictions.Like("PersonalInformation.Firstname", search)),
                        Restrictions.Like("PersonalInformation.Lastname", search)),
                    Restrictions.Like("Email", search)))
                .AddOrder(Order.Asc(order));
            var members = Member.FindAll(crit);
            PropertyBag["members"] = PaginationHelper.CreatePagination<Member>(members, PAGE_SIZE, page);
        }
        private T Cast<T>(DataRow row, string property) where T : class 
        {
            if (row[property] is DBNull)
                return null;
            return (T)row[property];
        }
        [Layout("none")]
        public void Dump()
        {
            var sqlConnection = new SqlConnection("Server=.;Initial Catalog=ic;Integrated Security=True;");
            sqlConnection.Open();
            var cmd = sqlConnection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Members";
            var reader = new SqlDataAdapter(cmd);
            var ds = new DataSet();
            reader.Fill(ds);
            var results = new List<Entry>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var entry = new Entry()
                                {
                                    Id = (long) row["Id"],
                                    Type = Cast<string>(row, "Type"),
                                    Username = Cast<string>(row, "Username"),
                                    Email = Cast<string>(row, "Email"),
                                    Password = Cast<string>(row, "Password"),
                                    AccountExpiration = (DateTime) row["AccountExpiration"],
                                    Firstname = Cast<string>(row, "Firstname"),
                                    Lastname = Cast<string>(row, "Lastname"),
                                    MatrNr = Cast<string>(row, "MatrNr"),
                                    Street = Cast<string>(row, "Street"),
                                    ZipCode = Cast<string>(row, "ZipCode"),
                                    City = Cast<string>(row, "City"),
                                    Category = Cast<string>(row, "Category"),
                                    Birthplace = Cast<string>(row, "Birthplace"),
                                    Nationality = Cast<string>(row, "Nationality"),
                                    Birthday = (DateTime) row["Birthday"],
                                    Title = Cast<string>(row, "Title"),
                                    Telephone = Cast<string>(row, "Telephone"),
                                };

                results.Add(entry);
            }

            PropertyBag["ds"] = results;
            sqlConnection.Close();
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
                    var dictionary = new Dictionary<string, object> {{"member", member}, {"password", password}};
                    var message = RenderMailMessage("admin-passwordreset", null, (IDictionary) dictionary);
                    message.Encoding = System.Text.Encoding.UTF8;
                    DeliverEmail(message);
                }

                Flash["info"] = "Password reset";
                RedirectToAction("Edit", new Dictionary<string, object> {{"id", member.Id}});
            }
        }

        public void Unlock([ARFetch("id")] Member member)
        {
            PropertyBag["member"] = member;
        }

        public void Unlock([ARFetch("id")] Member member, string expiration)
        {
            DateTime parsedExpiration;
            if (!DateTime.TryParse(expiration, out parsedExpiration))
            {
                Flash["error"] = "Date is invalid";
                return;
            }
            using (new SessionScope())
            {
                member.AccountExpiration = parsedExpiration;
                member.Save();

                Flash["info"] = String.Format("Account unlocked until {0}", parsedExpiration.ToShortDateString());
                RedirectToAction("Edit", new Dictionary<string, object> {{"id", member.Id}});
            }
        }

        public void Lockout([ARFetch("id")] Member member)
        {
            using (new SessionScope())
            {
                member.AccountExpiration = DateProviderFactory.Provider.MinValue();
                member.Save();

                Flash["info"] = String.Format("Account {0} has been locked", member.Username);
                RedirectToAction("List");
            }
        }

        public void Delete([ARFetch("id")] Member member)
        {
            PropertyBag["member"] = member;
        }

        public void Delete([ARFetch("id")] Member member, bool confirmation)
        {
            if (confirmation)
            {
                using (new SessionScope())
                {
                    member.DeleteAndFlush();
                    Flash["info"] = String.Format("Account {0} has been deleted", member.Username);
                    RedirectToAction("List");
                }
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
