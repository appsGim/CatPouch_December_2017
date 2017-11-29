using System;


public static class UtilsDateTime
{

    private static TimeZoneInfo IsraelTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Israel Standard Time");

    public static DateTime UTC_To_Israel_Time()
    {
        return TimeZoneInfo.ConvertTime(DateTime.UtcNow, IsraelTimeZone);
    }

    public static DateTime Israel_Time_To_UTC(DateTime dt)
    {

        return TimeZoneInfo.ConvertTimeToUtc(dt, IsraelTimeZone);
    }
}