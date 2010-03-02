namespace ImagineClub.Web.Models
{
    using System;
    using System.Collections.Generic;
    using Castle.ActiveRecord;
    using Castle.Components.Validator;
    using NHibernate.Criterion;


    [ActiveRecord("NewsPosts")]
    public class NewsPost : ValidatedActiveRecordEntity<NewsPost>
    {
        [BelongsTo(NotNull = true)]
        public Administrator PostedBy { get; set; }

        [Property(NotNull = true)]
        [ValidateNonEmpty]
        public string Title { get; set; }

        [ValidateNonEmpty]
        [Property(NotNull = true)]
        public DateTime PostDate { get; set; }

        [Property(SqlType = "NTEXT", NotNull = true)]
        [ValidateNonEmpty]
        public string Text { get; set; }

        [HasMany]
        public IList<Comment> Comments { get; set;}

        public static NewsPost[] FindRecent(int posts)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(NewsPost))
                .AddOrder(Order.Desc("PostDate"))
                .SetMaxResults(posts);
            return FindAll(criteria);
        }
    }
}