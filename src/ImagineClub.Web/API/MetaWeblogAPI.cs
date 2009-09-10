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
    using System;
    using CookComputing.XmlRpc;

    /// <summary>
    /// XmlRpc definitions for Metaweblog API methods and structures
    /// </summary>

    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct PostInfo
    {
        [XmlRpcMissingMapping(MappingAction.Error)]
        [XmlRpcMember]
        public DateTime dateCreated;
        [XmlRpcMissingMapping(MappingAction.Error)]
        [XmlRpcMember]
        public string description;
        [XmlRpcMissingMapping(MappingAction.Error)]
        [XmlRpcMember]
        public string title;

        public string[] categories;
        public string link;
        public string permalink;
        [XmlRpcMember]
        public object postid;
        public string userid;
        [XmlRpcMember]
        public string mt_text_more;
    }

    public struct CategoryInfo
    {
        public string description;
        public string htmlUrl;
        public string rssUrl;
        public string title;
        public string categoryid;
    }

    public struct FileData
    {
        public byte[] bits;
        public string name;
        public string type;
    }

    public struct UrlData
    {
        public string url;
    }

    public interface IMetaWeblog
    {
        [XmlRpcMethod("metaWeblog.editPost",
            Description = "Updates and existing post to a designated blog "
                          + "using the metaWeblog API. Returns true if completed.")]
        object editPost(
            string postid,
            string username,
            string password,
            PostInfo postInfo,
            bool publish);

        [XmlRpcMethod("metaWeblog.getCategories",
            Description = "Retrieves a list of valid categories for a post "
                          + "using the metaWeblog API. Returns the metaWeblog categories "
                          + "struct collection.")]
        CategoryInfo[] getCategories(
            string blogid,
            string username,
            string password);

        [XmlRpcMethod("metaWeblog.getPost",
            Description = "Retrieves an existing post using the metaWeblog "
                          + "API. Returns the metaWeblog struct.")]
        PostInfo getPost(
            string postid,
            string username,
            string password);

        [XmlRpcMethod("metaWeblog.getRecentPosts",
            Description = "Retrieves a list of the most recent existing post "
                          + "using the metaWeblog API. Returns the metaWeblog struct collection.")]
        PostInfo[] getRecentPosts(
            string blogid,
            string username,
            string password,
            int numberOfPosts);

        [XmlRpcMethod("metaWeblog.newPost",
            Description = "Makes a new post to a designated blog using the "
                          + "metaWeblog API. Returns postid as a string.")]
        string newPost(
            string blogid,
            string username,
            string password,
            PostInfo post,
            bool publish);

        [XmlRpcMethod("metaWeblog.newMediaObject",
            Description = "Makes a new file to a designated blog using the "
                          + "metaWeblog API. Returns url as a string of a struct.")]
        UrlData newMediaObject(
            string blogid,
            string username,
            string password,
            FileData file);
    }
}