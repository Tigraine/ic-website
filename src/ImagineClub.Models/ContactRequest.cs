namespace ImagineClub.Web.Models
{
    using Castle.Components.Validator;

    public class ContactRequest
    {
        [ValidateNonEmpty]
        public string Name { get; set; }
        [ValidateEmail]
        public string Email { get; set;}
        [ValidateNonEmpty]
        public string Subject { get; set; }
        [ValidateNonEmpty]
        public string Text { get; set; }
    }
}