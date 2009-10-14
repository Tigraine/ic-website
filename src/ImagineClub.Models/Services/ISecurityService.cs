namespace ImagineClub.Web.Models.Services
{
    public interface ISecurityService
    {
        bool AuthenticateUser(string username, string password);
        Administrator GetAdministrator(string username, string password);
        Member GetMember(string username, string password);
    }
}