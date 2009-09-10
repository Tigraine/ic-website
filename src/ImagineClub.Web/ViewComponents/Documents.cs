namespace ImagineClub.Web.ViewComponents
{
    using Castle.ActiveRecord;
    using Castle.ActiveRecord.Queries.Modifiers;
    using Castle.MonoRail.Framework;
    using Models;
    using NHibernate.Criterion;

    public class Documents : ViewComponent
    {
        public override void Initialize()
        {
        }

        public override void Render()
        {
            using (new SessionScope())
            {
                PropertyBag["documents"] = Document.FindAll(DetachedCriteria.For(typeof(Document)).SetMaxResults(5), Order.Desc("UploadedOn"));
                RenderView("Documents");
            }
        }
    }
}