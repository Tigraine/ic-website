namespace ImagineClub.Web.Models
{
    using System;
    using System.Security.Principal;
    using Castle.ActiveRecord;
    using Castle.Components.Validator;
    using NHibernate.Criterion;

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

        public static Member FindMemberByLogin(string username, string password)
        {
            var criteria = DetachedCriteria.For(typeof(Member))
                .Add(Restrictions.Eq("Username", username).IgnoreCase())
                .Add(Restrictions.Eq("Password", password));

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
    }

        
    public class Address
    {
        [Property(NotNull = true)]
        public string Street { get; set; }
        [Property(NotNull = true)]
        public string ZipCode { get; set; }
        [Property(NotNull = true)]
        public string City { get; set; }
    }

    public class PersonalInformation
    {
        [ValidateNonEmpty]
        [Property(NotNull = true)]
        public string Firstname { get; set; }

        [ValidateNonEmpty]
        [Property(NotNull = true)]
        public string Lastname { get; set; }

        [Property]
        public string BirthPlace { get; set; }
        
        [Property(NotNull = true)]
        public string Nationality { get; set; }
        
        [Property]
        public string MatrNr { get; set; }

        [ValidateDateTime]
        [Property]
        public DateTime? BirthDay { get; set; }

        [Property(NotNull = true)]
        public string Salutation { get; set; }
        [Property]
        public string Title { get; set; }

        [Field]
        private string category;
        public Category Category
        {
            get { return Category.GetCategoryByName(category); }
            set { category = value.Name; }
        }
    }

    public class ContactOptions
    {
        [Property]
        public string MSN { get; set; }
        [Property]
        public string ICQ { get; set; }
        [Property]
        public string Skype { get; set; }

        [Property]
        public string Telephone { get; set; }

        [Property(NotNull = true)]
        public bool NewsLetterOptIn { get; set; }
    }
}