namespace ImagineClub.Web.Models.Commands
{
    using System;
    using Castle.MonoRail.Framework;

    public class AddComment : ICommand
    {
        private readonly NewsPost post;
        private readonly Comment comment;
        private readonly IEngineContext context;

        public AddComment(NewsPost post, Comment comment, IEngineContext context)
        {
            this.post = post;
            this.comment = comment;
            this.context = context;
            this.context = context;
        }

        public void Execute()
        {
            comment.Time = DateTime.Now;
            comment.ParentPost = post;
            comment.CommentIp = context.Request.UserHostAddress;;
            comment.SaveAndFlush();
        }
    }

    public interface ICommand
    {
        void Execute();
    }
}