namespace ImagineClub.Web.Models
{
    using Castle.ActiveRecord;

    [ActiveRecord(DiscriminatorValue = "administrator")]
    public class Administrator : Member
    {
        
    }
}