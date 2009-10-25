namespace ImagineClub.Web
{
    using System.Web;
    using ElmsConnector;
    using Models.Services;

    public class AuthenticationService : ISessionAuthenticationService
    {
        private readonly SecurityService _securityService;

        public AuthenticationService()
        {
            _securityService = new SecurityService();
        }

        public bool AuthenticateUser(string username, string password)
        {
            return _securityService.AuthenticateUser(username, password);
        }

        public bool IsAlreadyAuthenticated()
        {
            return HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated;
        }

        public string Username
        {
            get
            {
                return HttpContext.Current.User.Identity.Name;
            }
        }
    }
}