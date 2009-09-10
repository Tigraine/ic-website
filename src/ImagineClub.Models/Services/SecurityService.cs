namespace ImagineClub.Web.Models.Services
{
    public class SecurityService : ISecurityService
    {
        public bool AuthenticateUser(string username, string password)
        {
            var member = Member.FindMemberByLogin(username, password);
            return member != null;
        }

        public Administrator GetAdministrator(string username, string password)
        {
            Member member = Member.FindMemberByLogin(username, password);
            if (member is Administrator)
                return (Administrator) member;
            return null;
        }
    }
}