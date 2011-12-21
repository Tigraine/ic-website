using System.Globalization;

namespace ImagineClub.Web.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Castle.Components.Validator;
    using Castle.MonoRail.Framework;
    using Helpers;
    using Models;
    using Models.Services;
    using Validators;

    [Layout("default"), Rescue(typeof(RescueController)), Helper(typeof(ValidationHelper))]
    public class RegisterController : ControllerBase
    {
        public void AccountInformation()
        {
        }

        public void AccountInformation([DataBind("Register")] AccountInformationViewModel viewModel)
        {
            ValidatorRunner runner = new ValidatorRunner(new CachedValidationRegistry());
            if (runner.IsValid(viewModel))
            {
                Session["AccountInfo"] = viewModel;
                RedirectToAction("PersonalInformation");
            }
            else
            {
                var summary = runner.GetErrorSummary(viewModel);
                Flash["summary"] = summary;
                Flash["Register"] = viewModel;
            }
        }

        public void PersonalInformation()
        {
            Flash["categories"] = Category.All;
        }

        public void PersonalInformation([DataBind("Personal")] PersonalInformationViewModel viewModel)
        {
            Flash["categories"] = Category.All;
            var runner = new ValidatorRunner(new CachedValidationRegistry());
            if (runner.IsValid(viewModel))
            {
                Session["PersonalInfo"] = viewModel;
                RedirectToAction("Thanks");
            }
            else
            {
                Flash["Personal"] = viewModel;
                Flash["summary"] = runner.GetErrorSummary(viewModel);
            }
        }

        public void Thanks()
        {
            var personalInfo = (PersonalInformationViewModel) Session["PersonalInfo"];
            var accountInfo = (AccountInformationViewModel) Session["AccountInfo"];

            //TODO: Fix Nationality and newletteropt-in defaults
            var member = new Member
                             {
                                 Username = accountInfo.Username,
                                 Address =
                                     new Address
                                         {
                                             City = personalInfo.City,
                                             Street = personalInfo.Street,
                                             ZipCode = personalInfo.Zip
                                         },
                                 ContactOptions = new ContactOptions {NewsLetterOptIn = true},
                                 Email = accountInfo.Email,
                                 Password = Member.HashPassword(accountInfo.Password),
                                 AccountExpiration = DateProviderFactory.Provider.MinValue(),
                                 PersonalInformation = new PersonalInformation
                                                           {
                                                               BirthDay = GetOptionalDateTime(personalInfo.Birthday),
                                                               BirthPlace = personalInfo.BirthPlace,
                                                               Category =
                                                                   Category.GetCategoryByName(personalInfo.Category),
                                                               Firstname = personalInfo.Firstname,
                                                               Lastname = personalInfo.Lastname,
                                                               MatrNr = personalInfo.MatrNr,
                                                               Nationality = "AT",
                                                               Title = personalInfo.Title
                                                           }
                             };
            if (member.IsValid())
            {
                member.Save();
                var parameter = new Dictionary<string, object> {{"user", member}};
                var message = RenderMailMessage("Registration", null, (IDictionary) parameter);
                message.Encoding = System.Text.Encoding.UTF8;
                DeliverEmail(message);
            }
        }

        private static DateTime? GetOptionalDateTime(string str)
        {
            return String.IsNullOrEmpty(str) ? null : (DateTime?) DateTime.Parse(str);
        }
    }

    public class AccountInformationViewModel
    {
        [ValidateNonEmpty("Erforderliches Feld - muss ausgefüllt werden.")]
        [UsernameUniqueValidator("Der Benutzername ist bereits registriert.")]
        [ValidateLength(6, int.MaxValue, "Der Benutzername muss mindestens 6 Zeichen lang sein.")]
        public string Username { get; set; }

        [ValidateNonEmpty("Erforderliches Feld - muss ausgefüllt werden.")]
        public string Password { get; set; }

        [ValidateNonEmpty("Erforderliches Feld - muss ausgefüllt werden.")]
        [ValidateSameAs("Password", "Stimmt nicht mit Kennwort überein")]
        public string PasswordConfirmation { get; set; }

        [ValidateNonEmpty("Erforderliches Feld - muss ausgefüllt werden.")]
        [ValidateEmail("Bitte gib eine valide Email Adresse ein.")]
        public string Email { get; set; }
    }

    public class PersonalInformationViewModel
    {
        [ValidateNonEmpty("Erforderliches Feld - muss ausgefüllt werden.")]
        public string Category { get; set; }

        public string Title { get; set; }

        [ValidateNonEmpty("Erforderliches Feld - muss ausgefüllt werden.")]
        public string Firstname { get; set; }

        [ValidateNonEmpty("Erforderliches Feld - muss ausgefüllt werden.")]
        public string Lastname { get; set; }

        [ValidateNonEmpty("Erforderliches Feld - muss ausgefüllt werden.")]
        public string Street { get; set; }

        [ValidateRegExp("[0-9]*", "Numerisches Feld - Bitte gib nur Zahlen ein.")]
        public string Zip { get; set; }

        [ValidateNonEmpty("Erforderliches Feld - muss ausgefüllt werden.")]
        public string City { get; set; }

        [ValidateNonEmpty("Erforderliches Feld - muss ausgefüllt werden.")]
        [ValidateRegExp("[0-9]*", "Numerisches Feld - Bitte gib nur Zahlen ein.")]
        public string MatrNr { get; set; }

        [ValidateNonEmpty("Erforderliches Feld - muss ausgefüllt werden.")]
        public string BirthPlace { get; set; }

        [ValidateDateTime("Bitte gib ein gültiges Datum ein")]
        [ValidateRegExp("([0-9]{1,2}[\\.-][0-9]{1,2}[\\.-][0-9]{4})?", "Datumsfeld - Bitte gib ein Datum im Format TT.MM.YYYY ein.")]
        public string Birthday { get; set; }
    }
}