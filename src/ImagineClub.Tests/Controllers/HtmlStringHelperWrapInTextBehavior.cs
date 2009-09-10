namespace ImagineClub.Tests.Controllers
{
    using Web.Helpers;
    using Xunit;

    public class HtmlStringHelperWrapInTextBehavior
    {
        private readonly HtmlStringHelper helper = new HtmlStringHelper();

        [Fact]
        public void HelperAddsPTagtoTextIfNotAlreadyPresent()
        {
            var text = "Test";
            string output = helper.WrapInParagraph(text);

            Assert.Equal("<p>Test</p>", output);
        }

        [Fact]
        public void WrapInParagraph_OnlyStartPIsPresent_NoWrap()
        {
            var text = "<p>Test";
            string output = helper.WrapInParagraph(text);

            Assert.Equal(text, output);
        }

        [Fact]
        public void WrapInParagraph_OnlyEndPIsPresent_NoWrap()
        {
            var text = "Test</p>";
            string output = helper.WrapInParagraph(text);

            Assert.Equal(text, output);
        }
    }
}