namespace ImagineClub.Web.Models
{
    using Castle.ActiveRecord;

    public class Address
    {
        [Property(NotNull = true)]
        public string Street { get; set; }
        [Property(NotNull = true)]
        public string ZipCode { get; set; }
        [Property(NotNull = true)]
        public string City { get; set; }
    }
}