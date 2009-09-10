namespace ImagineClub.Tests.Controllers
{
    using Castle.MonoRail.TestSupport;
    using Rhino.Mocks;
    using Web.Controllers;
    using Web.Models.Services;
    using Xunit;

    public class LoginBehavior : BaseControllerTest
    {
        [Fact]
        public void AuthenticateUser_DisplaysFlash_When_Login_Failed()
        {
            var service = MockRepository.GenerateStub<ISecurityService>();
            var controller = new ProfileController(service);
            service.Stub(p => p.AuthenticateUser(null, null)).IgnoreArguments().Return(false);
            PrepareController(controller);

            controller.AuthenticateUser("John", "Doe");

            Assert.NotNull(controller.Flash["failure"]);
        }

        [Fact]
        public void AuthenticateUser_RedirectsToHome_When_Login_Successful()
        {
            var service = MockRepository.GenerateStub<ISecurityService>();
            service.Stub(p => p.AuthenticateUser(null, null)).IgnoreArguments().Return(true);
            service.Stub(p => p.GetAdministrator(null, null)).IgnoreArguments().Return(ObjectMother.GetAdministrator());
            var controller = new ProfileController(service);
            PrepareController(controller);

            controller.AuthenticateUser(null, null); //Doesn't matter. The Stub returns true

            Assert.Equal("/Home/Index.castle", Response.RedirectedTo);
        }
    }
}