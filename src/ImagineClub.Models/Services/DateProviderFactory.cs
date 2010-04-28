namespace ImagineClub.Web.Models.Services
{
    public class DateProviderFactory
    {
        private static IDateProvider _provider;
        
        public static void SetProvider(IDateProvider provider)
        {
            _provider = provider;
        }
        public static IDateProvider Provider
        {
            get
            {
                if (_provider == null)
                    _provider = new DateProvider();
                return _provider;
            }
        }
    }
}