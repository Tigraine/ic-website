namespace ImagineClub.Web.Models
{
    public interface INewsPostFactory
    {
        NewsPost Create(string title, string content, Administrator creator);
    }
}