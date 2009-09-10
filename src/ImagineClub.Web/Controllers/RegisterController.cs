namespace ImagineClub.Web.Controllers
{
    using System;
    using Castle.MonoRail.Framework;
    using Helpers;
    using Models;

    [DynamicActionProvider(typeof (WizardActionProvider))]
    [Layout("default"), Rescue(typeof (RescueController))]
    public class RegisterController : ControllerBase, IWizardController
    {
        public void OnWizardStart()
        {
        }

        public bool OnBeforeStep(string wizardName, string stepName, IWizardStepPage step)
        {
            return true;
        }

        public void OnAfterStep(string wizardName, string stepName, IWizardStepPage step)
        {
        }

        public IWizardStepPage[] GetSteps(IEngineContext context)
        {
            return new IWizardStepPage[]
                       {
                           new IntroductionStep(),
                           new DetailStep()
                       };
        }

        public bool UseCurrentRouteForRedirects
        {
            get { return true; }
        }
    }

    [Helper(typeof(HtmlStringHelper)), Helper(typeof(FileHelper))]
    public class IntroductionStep : WizardStepPage
    {
        public void Next()
        {
            try
            {
                DoNavigate();
            }
            catch(Exception ex)
            {
                Flash["error"] = ex;
                RedirectToAction(ActionName);
            }
        }
    }

    [Helper(typeof(HtmlStringHelper)), Helper(typeof(FileHelper))]
    public class DetailStep : WizardStepPage
    {
        protected override void RenderWizardView()
        {
            PropertyBag["categories"] = Category.All;
            base.RenderWizardView();
        }
    }
}