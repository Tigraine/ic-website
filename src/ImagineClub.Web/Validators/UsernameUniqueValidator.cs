namespace ImagineClub.Web.Validators
{
    using Castle.ActiveRecord;
    using Castle.Components.Validator;
    using Models;
    using NHibernate.Criterion;

    public class UsernameUniqueValidator : AbstractValidator
    {
        public override bool IsValid(object instance, object fieldValue)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(Member))
                .Add(Restrictions.Eq("Username", fieldValue.ToString()));
            bool valid = !ActiveRecordBase<Member>.Exists(criteria);
            return valid;
        }

        public override bool SupportsBrowserValidation
        {
            get { return false; }
        }
    }
}