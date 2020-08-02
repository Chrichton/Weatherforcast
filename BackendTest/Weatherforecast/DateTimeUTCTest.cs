using Backend.Weatherforecast.Service;
using System;
using Xunit;

namespace BackendTest.Weatherforecast
{
    public class DateTimeUTCTest
    {
        const int secondsPerDay = 1000 * 60 * 60 * 24;

        [Fact]
        public void TestBeginOfTime()
        {
            DateTime result = DateTimeUTC.FromMillisecondsSinceUnixEpoch(0);

            Assert.Equal(new DateTime(1970, 1, 1), result);
        }

        [Fact]
        public void TestNegativeUTC()
        {
            DateTime result = DateTimeUTC.FromMillisecondsSinceUnixEpoch(-secondsPerDay);

            Assert.Equal(new DateTime(1970, 1, 1).AddDays(-1), result);
        }

        [Fact]
        public void TestPositiveUTC()
        {
            DateTime result = DateTimeUTC.FromMillisecondsSinceUnixEpoch(secondsPerDay);

            Assert.Equal(new DateTime(1970, 1, 1).AddDays(1), result);
        }

        [Fact]
        //[Theory]
        public void TestUTCFromOpenWeathermap()
        {
            DateTime result = DateTimeUTC.FromMillisecondsSinceUnixEpoch(1596358800);
            Assert.Equal(new DateTime(2020, 8, 2, 9, 0, 0), result);

            result = DateTimeUTC.FromMillisecondsSinceUnixEpoch(1596369600);
            Assert.Equal(new DateTime(2020, 8, 2, 12, 0, 0), result);

            result = DateTimeUTC.FromMillisecondsSinceUnixEpoch(1596380400);
            Assert.Equal(new DateTime(2020, 8, 2, 15, 0, 0), result);
        }
    }
}
