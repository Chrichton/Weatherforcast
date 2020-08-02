using System;

namespace Backend.Weatherforecast.Service
{
    /// <summary>
    /// Code from: https://stackoverflow.com/questions/249760/how-can-i-convert-a-unix-timestamp-to-datetime-and-vice-versa
    /// </summary>
    public static class DateTimeUTC
    {
        public static DateTime FromMillisecondsSinceUnixEpoch(long seconds)
        {
            return DateTimeOffset
                .FromUnixTimeSeconds(seconds)
                .UtcDateTime;
        }
    }
}
