using Backend.OpenWeathermap;
using System.Collections.Generic;
using Xunit;

namespace BackendTest.OpenWeathermap
{
    public class CitynamesToIdsTest
    {
        [Fact]
        public void TestCityId()
        {
            Assert.Equal(TestUtilities.CityIdHamburg, CitynamesToIds.Dictionary["Hamburg"]);
        }

        [Fact]
        public void TestNoCityId()
        {
            Assert.Equal(-1, CitynamesToIds.Dictionary.GetValueOrDefault("Hambur", -1));
        }
    }
}
