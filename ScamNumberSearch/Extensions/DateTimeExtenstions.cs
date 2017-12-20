namespace ScamNumberSearch.Extensions
{
    using System;

    public static class DateTimeExtenstions
    {
        public static double ToJulianDate(this DateTime date)
        {
            return date.ToOADate() + 2415018.5;
        }
    }
}
