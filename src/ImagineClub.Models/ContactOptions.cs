namespace ImagineClub.Web.Models
{
    using Castle.ActiveRecord;

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