namespace ImagineClub.Tests
{
    using System;
    using System.Collections.Generic;
    using Castle.ActiveRecord;
    using Web.Models;

    public class ObjectMother
    {
        public static Administrator GetAdministrator()
        {
            var administrator = new Administrator
                                    {
                                        Email = "tigraine@tigraine.at",
                                        Username = "Tigraine",
                                        Password = "random-password",
                                        Address = new Address
                                                      {
                                                          City = "Klagenfurt",
                                                          Street = "Somestreet",
                                                          ZipCode = "9020",
                                                      },
                                        PersonalInformation = new PersonalInformation
                                                                  {
                                                                      Firstname = "Daniel",
                                                                      Lastname = "Hölbling",
                                                                      Nationality = "AT",
                                                                      Salutation = Salutation.Mr,
                                                                      BirthDay = new DateTime(1985, 7, 24),
                                                                      Category = Category.Student
                                                                  },
                                        ContactOptions = new ContactOptions()
                                    };
            return administrator;
        }

        public static Administrator GetAdminAndSaveToDatabase()
        {
            Administrator administrator = GetAdministrator();
            using (new SessionScope())
            {
                administrator.SaveAndFlush();
            }
            return administrator;
        }

        public static NewsPost[] CreateManyPosts(int numberOfPosts, Administrator poster)
        {
            var posts = new List<NewsPost>();
            for (int i = 0; i < numberOfPosts; i++)
            {
                var post = new NewsPost
                               {
                                   PostDate = DateTime.MinValue,
                                   PostedBy = poster,
                                   Text = String.Format("Test {0}", i),
                                   Title = String.Format("Test {0}", i)
                               };
                post.SaveAndFlush();
                posts.Add(post);
            }
            return posts.ToArray();
        }

        public static Comment[] CreateManyComments(int numberOfComments, NewsPost post)
        {
            var comments = new List<Comment>();
            for (int i = 0; i < numberOfComments; i++)
            {
                var comment = new Comment()
                                  {
                                      CommentIp = "127.0.0.1",
                                      Name = "Daniel" + i,
                                      Text = "Hello World " + i,
                                      ParentPost = post,
                                      Time = DateTime.MinValue
                                  };
                comment.SaveAndFlush();
                comments.Add(comment);
            }
            return comments.ToArray();
        }
    }
}