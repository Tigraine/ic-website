namespace ImagineClub.Web.Validators
{
    using Castle.Components.Validator;

    public class UsernameUniqueValidatorAttribute : AbstractValidationAttribute
    {
        private readonly string _message;

        public UsernameUniqueValidatorAttribute(string message)
        {
            _message = message;
        }

        public override IValidator Build()
        {
            var validator = new UsernameUniqueValidator(_message);
            ConfigureValidatorMessage(validator);
            return validator;
        }
    }
}