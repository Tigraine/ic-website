namespace ImagineClub.Tests.Controllers
{
    using Castle.MonoRail.Framework.Test;
    using Castle.MonoRail.TestSupport;
    using Web.Controllers;
    using Web.Models;
    using Xunit;

    public class ContactTestcase : BaseControllerTest
    {
        private ContactRequest validContactRequest = new ContactRequest
                                                         {
                                                             Email = "test@test.com",
                                                             Name = "Daniel",
                                                             Subject = "Hello World",
                                                             Text = "Body"
                                                         };
        [Fact]
        public void Thanks_RendersTemplatedEmail()
        {
            var controller = new ContactController();
            PrepareController(controller);

            controller.Index(validContactRequest);

            Assert.True(HasRenderedEmailTemplateNamed("contact"));
        }

        [Fact]
        public void Thanks_MailRendering_ParametersGetPassed()
        {
            var controller = new ContactController();
            PrepareController(controller);

            controller.Index(validContactRequest);

            var parameters = RenderedEmailTemplates[0].Parameters["request"];
            Assert.Same(validContactRequest, parameters);
        }

        [Fact]
        public void Thanks_EmailSending_SendsOutOneEmail()
        {
            var controller = new ContactController();
            PrepareController(controller);
            var context = (StubEngineContext)Context;

            controller.Index(validContactRequest);

            Assert.Equal(1, context.MessagesSent.Count);
        }

        [Fact]
        public void Send_InvalidRequest_PopulatesError()
        {
            var controller = new ContactController();
            PrepareController(controller);

            var invalidRequest = new ContactRequest();
            controller.Index(invalidRequest);

            Assert.NotNull(controller.PropertyBag["error"]);
        }

        [Fact]
        public void Send_InvalidRequest_EmailIsNotSent()
        {
            var controller = new ContactController();
            PrepareController(controller);

            var invalidRequest = new ContactRequest();
            controller.Index(invalidRequest);

            Assert.Equal(0, ((StubEngineContext)Context).MessagesSent.Count);
        }
    }
}