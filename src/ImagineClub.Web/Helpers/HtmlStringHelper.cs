namespace ImagineClub.Web.Helpers
{
    using System;
    using System.Text;

    public class HtmlStringHelper
    {
        public string WrapInParagraph(string text)
        {
            if (!text.StartsWith("<p") && !text.EndsWith("</p>"))
            {
                var builder = new StringBuilder();
                builder.AppendFormat("<p>{0}</p>", text);
                return builder.ToString();
            }
            return text;
        }

        public string ShortenText(string text)
        {
            var splitter = new[] { "<!--more-->" };
            return text.Split(splitter, StringSplitOptions.None)[0];
        }
    }
}