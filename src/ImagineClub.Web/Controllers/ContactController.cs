namespace ImagineClub.Web.Controllers
{
    using System.Collections;
    using System.Collections.Generic;
    using Castle.Components.Validator;
    using Castle.MonoRail.Framework;
    using Models;

    public class ContactController : ControllerBase
    {
        public void Index()
        {
        }

        public void Index([DataBind("ContactRequest")] ContactRequest request)
        {
            var runner = new ValidatorRunner(new CachedValidationRegistry());
            if (!runner.IsValid(request))
            {
                ErrorSummary summary = runner.GetErrorSummary(request);
                PropertyBag["ContactRequest"] = request;
                PropertyBag["error"] = summary;
            }
            else
            {
                var parameter = new Dictionary<string, object> { { "request", request } };
                var message = RenderMailMessage("contact", null, (IDictionary)parameter);
                message.Encoding = System.Text.Encoding.UTF8;
                DeliverEmail(message);
                RedirectToAction("Thanks");
            }
        }

        public void Thanks()
        {
        }
    }
}