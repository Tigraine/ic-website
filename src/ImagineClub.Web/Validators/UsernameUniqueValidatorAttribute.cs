namespace ImagineClub.Web.Validators
{
    using Castle.Components.Validator;

    public class UsernameUniqueValidatorAttribute : AbstractValidationAttribute
    {
        public override IValidator Build()
        {
            var validator = new UsernameUniqueValidator();
            ConfigureValidatorMessage(validator);
            return validator;
        }
    }
}