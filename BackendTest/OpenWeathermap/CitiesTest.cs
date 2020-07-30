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
            IEnumerable<City> cities = Cities.All.Where(city => city.Country == "DE");
            Assert.True(cities.Count() > 0);
        }
    }
}
