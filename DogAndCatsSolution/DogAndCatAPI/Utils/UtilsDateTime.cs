using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DogAndCatAPI.Utils
{
    public static class UtilsDateTime
    {
        private static TimeZoneInfo IsraelTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Israel Standard Time");
        public static DateTime UTC_To_Israel_Time()
        {
            return TimeZoneInfo.ConvertTime(DateTime.UtcNow, IsraelTimeZone);//.AddHours(correctionhours);
        }
    }
}