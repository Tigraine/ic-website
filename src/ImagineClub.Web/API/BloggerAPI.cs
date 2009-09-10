// KenEgozi.com - my blog engine
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

namespace ImagineClub.Web.API
{
    using CookComputing.XmlRpc;

    /// <summary>
    /// XmlRpc definitions for Blogger API methods and structures
    /// </summary>

    public struct BlogInfo
    {
        public string blogid;
        public string url;
        public string blogName;
    }

    public interface IBlogger
    {
        [XmlRpcMethod("blogger.deletePost", Description = "Deletes a post.")]
        [return: XmlRpcReturnValue(Description = "Always returns true.")]
        bool blogger_deletePost(
            string appKey,
            string postid,
            string username,
            string password,
            [XmlRpcParameter(Description = "Where applicable, this specifies whether the blog should be republished after the post has been deleted.")]
                bool publish);

        [XmlRpcMethod("blogger.getUsersBlogs", Description = "Returns information on all the blogs a given user is a member.")]
        BlogInfo[] blogger_getUsersBlogs(
            string appKey,
            string username,
            string password);


        [XmlRpcMethod("blogger.getTemplate", Description = "Returns the main or archive index template of a given blog.")]
        string blogger_getTemplate(
            string appKey,
            string blogid,
            string username,
            string password,
            string templateType);


        [XmlRpcMethod("blogger.setTemplate", Description = "Edits the main or archive index template of a given blog.")]
        bool blogger_setTemplate(
            string appKey,
            string blogid,
            string username,
            string password,
            string template,
            string templateType);
    }
}