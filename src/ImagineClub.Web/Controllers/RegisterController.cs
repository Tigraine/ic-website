namespace ImagineClub.Web.Controllers
{
    using System;
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

        public void PersonalInformation([DataBind("Personal")]PersonalInformationViewModel viewModel)
        {
            var runner = new ValidatorRunner(new CachedValidationRegistry());
            if (runner.IsValid(viewModel))
            {
                Session["PersonalInfo"] = viewModel;
            }
            else
            {
                PropertyBag["Personal"] = viewModel;
            }
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

    public class PersonalInformationViewModel
    {
        [ValidateNonEmpty]
        public string Category { get; set; }
        public string Title  { get; set; }
        [ValidateNonEmpty]
        public string Firstname { get; set; }
        [ValidateNonEmpty]
        public string Lastname { get; set; }
        [ValidateNonEmpty]
        public string Street { get; set; }
        [ValidateRegExp("[0-9]*")]
        public string Zip { get; set; }
        [ValidateNonEmpty]
        public string City { get; set; }
        [ValidateNonEmpty]
        [ValidateRegExp("[0-9]*")]
        public string MatrNr { get; set; }
        [ValidateNonEmpty]
        public string BirthPlace { get; set; }
        [ValidateDateTime]
        public DateTime Birthday { get; set; }
    }
}