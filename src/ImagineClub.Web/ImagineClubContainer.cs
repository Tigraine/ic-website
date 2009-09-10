namespace ImagineClub.Web
{
    using Castle.MicroKernel.Registration;
    using Castle.MonoRail.WindsorExtension;
    using Castle.Windsor;
    using Models;

    public class ImagineClubContainer : WindsorContainer
    {
        public ImagineClubContainer()
        {
            AddFacility("rails", new MonoRailFacility());

            var assembly = typeof (Global).Assembly;
            var modelAssembly = typeof (Document).Assembly;

            Register(
                AllTypes.FromAssembly(assembly)
                    .Where(p => p.Name.EndsWith("Controller"))
                    .Configure(p => p.Named(p.Implementation.Name.ToLower().Replace("controller", ".controller"))),
                AllTypes.FromAssembly(assembly)
                    .Where(p => p.Namespace.EndsWith("Web.ViewComponents"))
                    .Configure(p => p.Named(p.Implementation.Name.ToLower() + "viewcomponent"))
                    .WithService.FromInterface(),
                AllTypes.FromAssembly(modelAssembly)
                    .Where(p => p.Namespace.EndsWith("Services"))
                    .WithService.FirstInterface()
                );
        }
    }
}