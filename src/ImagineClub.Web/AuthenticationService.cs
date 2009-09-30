namespace ImagineClub.Web
{
    using ElmsConnector;
    using Models.Services;

    public class AuthenticationService : IAuthenticatonService
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
    }
}