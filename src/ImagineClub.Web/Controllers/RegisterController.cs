namespace ImagineClub.Web.Controllers
{
    using Castle.Components.Validator;
    using Castle.MonoRail.Framework;
    using Models;
    using Validators;

    [Layout("default"), Rescue(typeof (RescueController))]
    public class RegisterController : ControllerBase
    {
        public void AccountInformation()
        {
            
        }

        public void AccountInformation([DataBind("Register")]AccountInformationViewModel viewModel)
        {
            ValidatorRunner runner = new ValidatorRunner(new CachedValidationRegistry());
            if (runner.IsValid(viewModel))
            {
                Session["AccountInfo"] = viewModel;
                RedirectToAction("PersonalInformation");
            }
            else
            {
                PropertyBag["Register"] = viewModel;
            }
        }

        public void PersonalInformation()
        {
            PropertyBag["categories"] = Category.All;
        }
    }

    public class AccountInformationViewModel
    {
        [ValidateNonEmpty]
        [UsernameUniqueValidator]
        public string Username { get; set; }
        [ValidateNonEmpty]
        public string Password { get; set; }
        [ValidateNonEmpty]
        [ValidateSameAs("Password")]
        public string PasswordConfirmation { get; set; }
        [ValidateNonEmpty]
        [ValidateEmail]
        public string Email { get; set; }
    }
}