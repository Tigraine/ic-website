namespace ImagineClub.Web.Models
{
    using Castle.ActiveRecord;

    public abstract class ValidatedActiveRecordEntity<T> : ActiveRecordValidationBase<T> where T : class 
    {
        [PrimaryKey(PrimaryKeyType.HiLo)]
        public long Id { get; set; }
    }
}