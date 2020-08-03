using System;

namespace Backend.Weatherforecast.Common
{
    /// <summary>
    /// Code from: https://stackoverflow.com/questions/249760/how-can-i-convert-a-unix-timestamp-to-datetime-and-vice-versa
    /// </summary>
    public static class DateTimeUTC
    {
        public static DateTime FromSecondsSinceUnixEpoch(long seconds)
        {
            return DateTimeOffset
                .FromUnixTimeSeconds(seconds)
                .UtcDateTime;
        }
    }
}
