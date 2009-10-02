namespace ImagineClub.Web.Helpers
{
    using System.Text;
    using Castle.Components.Validator;

    public class ValidationHelper
    {
        public string GetErrorMessages(ErrorSummary summary, string property)
        {
            if (summary == null) return string.Empty;

            StringBuilder builder = new StringBuilder();
            builder.Append("<ul class=\"validationerror\">");
            foreach(var x in summary.GetErrorsForProperty(property))
            {
                builder.AppendFormat("<li>{0}</li>", x);
            }
            builder.Append("</ul>");
            return builder.ToString();
        }
    }
}