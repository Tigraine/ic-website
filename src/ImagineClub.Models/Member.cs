namespace ImagineClub.Web.Models
{
    using System.Security.Principal;
    using Castle.ActiveRecord;
    using Castle.Components.Validator;
    using NHibernate.Criterion;
    using Services;

    [ActiveRecord("Members", DiscriminatorColumn = "type", DiscriminatorType = "String", DiscriminatorValue = "member")]
    public class Member : ValidatedActiveRecordEntity<Member>, IPrincipal
    {
        [ValidateIsUnique]
        [ValidateLength(6,int.MaxValue)]
        [Property(NotNull = true)]
        public string Username { get; set; }

        [ValidateEmail]
        [ValidateNonEmpty]
        [Property(NotNull = true)]
        public string Email { get; set; }

        [Property(NotNull = true)]
        public string Password { get; set; }

        [Nested]
        public Address Address { get; set; }

        [Nested]
        public PersonalInformation PersonalInformation { get; set; }

        [Nested]
        public ContactOptions ContactOptions { get; set; }

        public string Role
        {
            get
            {
                if (this is Administrator) return "Administrator";
                return "Member";
            }
        }

        public bool IsInRole(string role)
        {
            //TODO: Support this
            return false;
        }

        public IIdentity Identity
        {
            get
            {
                return new GenericIdentity(Username, "ImagineClub.Authentication");
            }
        }

        public static string HashPassword(string password)
        {
            var encryption = new SHA1HashAlgorithm();
            return encryption.Hash(password);
        }

        public static Member FindMemberByLogin(string username, string password)
        {
            var hash = HashPassword(password);
            var criteria = DetachedCriteria.For(typeof(Member))
                .Add(Restrictions.Eq("Username", username).IgnoreCase())
                .Add(Restrictions.Eq("Password", hash));

            return Member.FindFirst(criteria);
        }

        public bool Equals(Member other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Username, Username);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Member)) return false;
            return Equals((Member) obj);
        }

        public override int GetHashCode()
        {
            return (Username != null ? Username.GetHashCode() : 0);
        }

        public static bool operator ==(Member left, Member right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Member left, Member right)
        {
            return !Equals(left, right);
        }
    }
}