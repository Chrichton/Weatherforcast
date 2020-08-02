using System;

namespace Backend.Weatherforecast.Service
{
    /// <summary>
    /// Code from: https://stackoverflow.com/questions/6565023/c-sharp-convert-utc-int-to-datetime-object
    /// </summary>
    public static class DateTimeUTC
    {
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0,
                                                          DateTimeKind.Utc);
        public static DateTime FromMillisecondsSinceUnixEpoch(int milliseconds)
        {
            return UnixEpoch + TimeSpan.FromMilliseconds(milliseconds);
        }
    }
}
