namespace ImagineClub.Tests.Controllers
{
    using System.Linq;
    using Castle.Components.Pagination;
    using Web.Controllers;
    using Web.Models;
    using Xunit;

    public class HomeTests : GenericDatabaseDependantControllerTest<HomeController>
    {
        private Administrator admin;
        private NewsPost[] posts;

        private void PreparePostsCollectionAndController(int numberOfPosts)
        {
            admin = ObjectMother.GetAdminAndSaveToDatabase();
            posts = ObjectMother.CreateManyPosts(numberOfPosts, admin);

            controller = new HomeController();
            PrepareController(controller);
        }

        [Fact]
        public void HomeLoadsNewsPosts()
        {
            PreparePostsCollectionAndController(4);

            controller.Index(1);

            var news = (IPaginatedPage<NewsPost>)controller.PropertyBag["news"];
            Assert.Equal(4, news.TotalItems);
        }

        [Fact]
        public void Index_ManyPosts_Pagination()
        {
            PreparePostsCollectionAndController(20);

            controller.Index(1);

            var news = (IPaginatedPage<NewsPost>)controller.PropertyBag["news"];
            Assert.Equal(HomeController.PageSize, news.CurrentPageSize);
        }

        [Fact]
        public void Index_PaginationGetsSortedByDate()
        {
            PreparePostsCollectionAndController(7);
            controller.Index(2);

            var dictionary = (IPaginatedPage<NewsPost>)controller.PropertyBag["news"];
            Assert.Equal(2, dictionary.Count());
        }

        [Fact]
        public void Index_News_AreSortedByDateDesc()
        {
            controller = new HomeController();
            PrepareController(controller);

            admin = ObjectMother.GetAdminAndSaveToDatabase();

            NewsPost[] newsPosts = ObjectMother.CreateManyPosts(2, admin);

            newsPosts.Each(p => p.PostDate = p.PostDate.AddDays(p.Id));
            newsPosts.SaveEach();

            controller.Index(1);

            var result = (IPaginatedPage<NewsPost>)controller.PropertyBag["news"];
            Assert.True(result.ElementAt(0).PostDate > result.ElementAt(1).PostDate);
        }

        [Fact]
        public void Detail_PutsPostInPropertyBag()
        {
            controller = new HomeController();
            PrepareController(controller);

            admin = ObjectMother.GetAdminAndSaveToDatabase();

            NewsPost post = ObjectMother.CreateManyPosts(1, admin)[0];

            controller.Detail(post);

            Assert.Same(post, controller.PropertyBag["post"]);
        }
    }
}