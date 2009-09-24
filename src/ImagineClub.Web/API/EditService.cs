// Original code taken from KenEgozi.com under the MIT license - Adapted by Daniel Hölbling (http://www.tigraine.at)
// This project is licensed under the MIT License
//
//Copyright (c) 2007, Ken Egozi
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in
//all copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Web;
using CookComputing.XmlRpc;
using System.IO;

namespace ImagineClub.Web.API
{
    using Castle.ActiveRecord;
    using Models;
    using Models.Services;

    public class EditService : XmlRpcService, IMetaWeblog, IBlogger
    {
        private const string MoreDelimiter = "<!--more-->";
        readonly INewsPostFactory postFactory;
        readonly ISecurityService securityService;

        public EditService()
        {
            postFactory = new NewsPostFactory();
            securityService = new SecurityService();
        }

        #region IMetaWeblog Members

        object IMetaWeblog.editPost(string postid, string username, string password, PostInfo postInfo, bool publish)
        {
            AssertCredentials(username, password);
            long postId = long.Parse(postid);

            using(new SessionScope())
            {
                NewsPost post = NewsPost.Find(postId);
                if (post == null)
                    throw new ApplicationException("Post not found");

                post.Title = postInfo.title;
                post.Text = CreatePostText(postInfo.description, postInfo.mt_text_more);
                post.SaveAndFlush();
            }
            return true;
        }

        public string CreatePostText(string description, string more)
        {
            return String.Format("{0}{2}{1}", description, more, MoreDelimiter);
        }

        CategoryInfo[] IMetaWeblog.getCategories(string blogid, string username, string password)
        {
            AssertCredentials(username, password);
            List<CategoryInfo> categories = new List<CategoryInfo>();
            return categories.ToArray();
        }

        PostInfo IMetaWeblog.getPost(string postid, string username, string password)
        {
            AssertCredentials(username, password);
            long postId = long.Parse(postid);
            NewsPost post = NewsPost.Find(postId);
            if (post == null)
                return new PostInfo();
            PostInfo postInfo = PostToPostInfo(post);
            return postInfo;
        }

        PostInfo[] IMetaWeblog.getRecentPosts(string blogid, string username, string password, int numberOfPosts)
        {
            AssertCredentials(username, password);

            IEnumerable<NewsPost> posts = NewsPost.FindRecent(numberOfPosts);

            List<PostInfo> postInfos = new List<PostInfo>();
            foreach (NewsPost post in posts)
                postInfos.Add(PostToPostInfo(post));
            return postInfos.ToArray();
        }

        string IMetaWeblog.newPost(string blogid, string username, string password, PostInfo postInfo, bool publish)
        {
            AssertCredentials(username, password);
            var user = securityService.GetAdministrator(username, password);

            var post = postFactory.Create(postInfo.title, CreatePostText(postInfo.description, postInfo.mt_text_more), user);
            post.SaveAndFlush();
            return post.Id.ToString();
        }

        UrlData IMetaWeblog.newMediaObject(string blogid, string username, string password, FileData file)
        {
            throw new NotImplementedException();
            AssertCredentials(username, password);

            string extension = Path.GetExtension(file.name);

            string[] fileNameParts = file.name.Split('/');

//			string originalFileName = Path.GetFileNameWithoutExtension(fileNameParts[fileNameParts.Length - 1]);
//			string fileName = HttpUtility.UrlEncode(originalFileName);

            StringBuilder url = new StringBuilder();
            for (int i = 0; i < fileNameParts.Length - 1; ++i)
                url.Append(fileNameParts[i]).Append("/");
            url.Append(Guid.NewGuid().ToString()).Append(extension);

            string path = "/Content/Binary/" + url;

            string serverPath = Context.Server.MapPath("~" + path);

            string directory = Path.GetDirectoryName(serverPath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            File.WriteAllBytes(serverPath, file.bits);

            UrlData urlData = new UrlData();
            urlData.url = GetBlogUrl() + path;
            return urlData;
        }

        bool IBlogger.blogger_deletePost(string appKey, string postid, string username, string password, bool publish)
        {
            AssertCredentials(username, password);
            long postId = long.Parse(postid);

            using (new SessionScope())
            {
                NewsPost post = NewsPost.Find(postId);
                post.DeleteAndFlush();
            }
            return true;
        }

        BlogInfo[] IBlogger.blogger_getUsersBlogs(string appKey, string username, string password)
        {
            AssertCredentials(username, password);
            BlogInfo b = new BlogInfo();
            b.blogid = "1";
            b.blogName = "imagineClub";
            b.url = GetBlogUrl();
            return new BlogInfo[] { b };
        }
        /*
		string getTemplate(string appKey, string blogid, string username, string password, string templateType)
		{
			return "";
		}
		 * */
        #endregion

        private static PostInfo PostToPostInfo(NewsPost post)
        {
            PostInfo postInfo = new PostInfo();
            List<string> categories = new List<string>();
            postInfo.categories = categories.ToArray();
            postInfo.dateCreated = post.PostDate;
            postInfo.description = post.Text.Split(MoreDelimiter.ToCharArray())[0];
            if (post.Text.IndexOf(MoreDelimiter) > -1)
                postInfo.mt_text_more = post.Text.Split(MoreDelimiter.ToCharArray())[1];
            postInfo.title = post.Title;
            postInfo.permalink = GetBlogUrl() + "/Home/Detail.aspx?Id=" + post.Id;
            postInfo.postid = post.Id.ToString();
            return postInfo;
        }

        private void AssertCredentials(string username, string password)
        {
            if (!securityService.AuthenticateUser(username, password))
                throw new SecurityException("Invalid credentials");
        }

        private static string GetBlogUrl()
        {
            HttpRequest request = HttpContext.Current.Request;
            return
                request.Url.Scheme + "://" +
                request.Url.Authority +
                request.ApplicationPath + "/Home/Index.aspx";
        }
        #region IBlogger Members


        string IBlogger.blogger_getTemplate(string appKey, string blogid, string username, string password, string templateType)
        {
            throw new NotImplementedException("IBlogger.blogger_getTemplate");
        }


        bool IBlogger.blogger_setTemplate(string appKey, string blogid, string username, string password, string template, string templateType)
        {
            return true;
        }

        #endregion

    }
}