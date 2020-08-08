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
            Assert.Equal(2911298, CitynamesToIds.Dictionary["Hamburg"]);
        }

        [Fact]
        public void TestNoCityId()
        {
            Assert.Equal(-1, CitynamesToIds.Dictionary.GetValueOrDefault("Hambur", -1));
        }
    }
}
