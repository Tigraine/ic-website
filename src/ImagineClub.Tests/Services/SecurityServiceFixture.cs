namespace ImagineClub.Tests.Services
{
    using Web.Models;
    using Web.Models.Services;
    using Xunit;

    public class SecurityServiceFixture : ActiveRecordInMemoryTestBase
    {
        [Fact]
        public void CanAuthenticateUser()
        {
            Administrator administrator = ObjectMother.GetAdminAndSaveToDatabase();

            var service = new SecurityService();
            bool authenticationResult = service.AuthenticateUser(administrator.Username, administrator.Password);

            Assert.True(authenticationResult);
        }

        [Fact]
        public void WrongCredentialsFailAuthentication()
        {
            Administrator administrator = ObjectMother.GetAdminAndSaveToDatabase();

            var service = new SecurityService();
            bool authenticationResult = service.AuthenticateUser(administrator.Username, "wrong password");

            Assert.False(authenticationResult);
        }
    }
}