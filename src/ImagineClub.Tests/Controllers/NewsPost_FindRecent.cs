namespace ImagineClub.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Web.Models;
    using Xunit;

    public class NewsPost_FindRecent : ActiveRecordInMemoryTestBase
    {
        [Fact]
        public void NewsPost_FindRecent_ReturnsRightNumberOfPosts()
        {
            CreatePosts();

            ICollection<NewsPost> collection = NewsPost.FindRecent(3);
            Assert.Equal(3, collection.Count);
        }

        [Fact]
        public void FindRecent_ReturnsCorrectOrderOfPosts()
        {
            CreatePosts();

            var collection = NewsPost.FindRecent(3);
            Assert.Equal("Hello9", collection.First().Text); //Last should come first
            Assert.Equal("Hello8", collection.Second().Text);
        }

        private void CreatePosts()
        {
            IList<NewsPost> list = new List<NewsPost>();
            Administrator administrator = ObjectMother.GetAdminAndSaveToDatabase();
            for (int x = 0; x < 10; x++)
            {
                list.Add(new NewsPost()
                             {
                                 PostDate = DateTime.MinValue.AddDays(x),
                                 Text = "Hello" + x,
                                 Title = "Hello" + x,
                                 PostedBy = administrator
                             });
            }
            list.ToArray().SaveEach();
        }
    }
}