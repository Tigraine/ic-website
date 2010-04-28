namespace ImagineClub.Web.Models.Services
{
    using System;
    using System.Data.SqlTypes;

    public class DateProvider : IDateProvider
    {
        public DateTime GetNow()
        {
            return DateTime.UtcNow;
        }

        public DateTime MinValue()
        {
            return SqlDateTime.MinValue.Value;
        }
    }
}