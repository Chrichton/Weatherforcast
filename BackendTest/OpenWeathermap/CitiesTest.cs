using Backend.OpenWeathermap;
using System.Linq;
using Xunit;

namespace BackendTest.OpenWeathermap
{
    public class CitiesTest
    {
        [Fact]
        public void TestCitiesCount()
        {
            Assert.Equal(28786, Cities.All.Count());
        }

        [Fact]
        public void TestGermanyCitiesOnly()
        {
            Assert.Empty(Cities.All.Where(city => city.Country != "DE"));
        }
    }
}
