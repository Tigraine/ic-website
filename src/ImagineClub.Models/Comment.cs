namespace ImagineClub.Web.Models
{
    using System;
    using Castle.ActiveRecord;
    using Castle.Components.Validator;

    [ActiveRecord("Comments")]
    public class Comment : ValidatedActiveRecordEntity<Comment>
    {
        [Property(NotNull = true)]
        public string CommentIp { get; set; }
        
        [Property(NotNull = true)]
        public DateTime Time { get; set; }
        
        [ValidateNonEmpty]
        [Property(NotNull = true)]
        public string Name { get; set; }
        
        [Property(SqlType = "NTEXT", NotNull = true)]
        [ValidateNonEmpty]
        public string Text { get; set; }

        [BelongsTo]
        public NewsPost ParentPost { get; set; }
    }
}