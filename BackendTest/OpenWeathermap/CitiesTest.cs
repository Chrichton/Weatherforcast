using Backend.OpenWeathermap;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BackendTest.OpenWeathermap
{
    public class CitiesTest
    {
        [Fact]
        public void TestGetGermanCities()
        {
            Dictionary<string, int> cities = CitynamesToIds.Dictionary;
            Assert.True(cities.Count() > 0);

            Assert.Equal(2911298, cities["Hamburg"]);
        }
    }
}
