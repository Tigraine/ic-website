namespace ImagineClub.Tests.Controllers
{
    using Web.Controllers;
    using Web.Models;
    using Xunit;

    public class CommentsBehaviorTests : GenericDatabaseDependantControllerTest<HomeController>
    {
        //[Fact]
        //public void Detail_Comment_CanBeInserted()
        //{
        //    var admin = ObjectMother.GetAdminAndSaveToDatabase();
        //    var post = ObjectMother.CreateManyPosts(1, admin)[0];

        //    var homeController = new HomeController();
        //    PrepareController(homeController);

        //    var comment = new Comment
        //                      {
        //                          CommentIp = "127.0.0.1",
        //                          Name = "Testcomment",
        //                          Text = "Hello"
        //                      };

        //    homeController.Detail(post, comment);
        //    post = NewsPost.Find(post.Id);
        //    Assert.Equal(1, post.Comments.Count);
        //    Assert.Equal(comment.Id, post.Comments[0].Id);
        //}

        [Fact]
        public void Detail_FetchesComments()
        {
            var homeController = new HomeController();
            PrepareController(homeController);

            var administrator = ObjectMother.GetAdminAndSaveToDatabase();
            var post = ObjectMother.CreateManyPosts(1, administrator)[0];
            ObjectMother.CreateManyComments(1, post);
            /*homeController.Detail();*/
        }
    }
}