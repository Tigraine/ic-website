namespace ImagineClub.Tests.Infrastructure
{
    using Web;
    using Web.Controllers;
    using Web.Models.Services;
    using Xunit;

    public class WindsorContainerTests
    {
        [Fact]
        public void CanResolveControllers()
        {
            var container = new ImagineClubContainer();

            Assert.DoesNotThrow(() =>
                                    {
                                        object resolvedType = container.Resolve("home.controller");
                                        Assert.IsType<HomeController>(resolvedType);
                                    });
        }

        [Fact]
        public void CanResolveViewComponent()
        {
            var container = new ImagineClubContainer();
            Assert.DoesNotThrow(() => container.Resolve("loginviewcomponent"));
        }

        [Fact]
        public void CanResolveSecurityService()
        {
            var container = new ImagineClubContainer();
            Assert.DoesNotThrow(() =>
                                    {
                                        var service = container.Resolve<ISecurityService>();
                                        Assert.IsType<SecurityService>(service);
                                    });
        }
    }
}