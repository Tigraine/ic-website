namespace ImagineClub.Web.Models.Services
{
    using System;

    public interface IDateProvider
    {
        DateTime GetNow();
        DateTime MinValue();
    }
}