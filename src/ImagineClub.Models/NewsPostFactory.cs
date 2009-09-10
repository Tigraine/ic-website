namespace ImagineClub.Web.Models
{
    using System;

    public class NewsPostFactory : INewsPostFactory
    {
        public NewsPost Create(string title, string content, Administrator creator)
        {
            var post = new NewsPost()
                           {
                               Title = title,
                               Text = content,
                               PostDate = DateTime.Now,
                               PostedBy = creator
                           };
            return post;
        }
    }
}