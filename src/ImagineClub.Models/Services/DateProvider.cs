namespace ImagineClub.Web.Models.Services
{
    using System;

    public class DateProvider : IDateProvider
    {
        public DateTime GetNow()
        {
            return DateTime.UtcNow;
        }
    }
}