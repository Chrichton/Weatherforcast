using Backend.Weatherforecast.Common;
using System;
using Xunit;

namespace BackendTest.Common
{
    public class DateTimeUTCTest
    {
        const int secondsPerDay = 60 * 60 * 24;

        [Fact]
        public void TestBeginOfTime()
        {
            DateTime result = DateTimeUTC.FromSecondsSinceUnixEpoch(0);

            Assert.Equal(new DateTime(1970, 1, 1), result);
        }

        [Fact]
        public void TestNegativeUTC()
        {
            DateTime result = DateTimeUTC.FromSecondsSinceUnixEpoch(-secondsPerDay);

            Assert.Equal(new DateTime(1970, 1, 1).AddDays(-1), result);
        }

        [Fact]
        public void TestPositiveUTC()
        {
            DateTime result = DateTimeUTC.FromSecondsSinceUnixEpoch(secondsPerDay);

            Assert.Equal(new DateTime(1970, 1, 1).AddDays(1), result);
        }

        [Fact]
        public void TestUTCFromOpenWeathermap()
        {
            DateTime result = DateTimeUTC.FromSecondsSinceUnixEpoch(1596358800);
            Assert.Equal(new DateTime(2020, 8, 2, 9, 0, 0), result);

            result = DateTimeUTC.FromSecondsSinceUnixEpoch(1596369600);
            Assert.Equal(new DateTime(2020, 8, 2, 12, 0, 0), result);

            result = DateTimeUTC.FromSecondsSinceUnixEpoch(1596380400);
            Assert.Equal(new DateTime(2020, 8, 2, 15, 0, 0), result);
        }
    }
}
