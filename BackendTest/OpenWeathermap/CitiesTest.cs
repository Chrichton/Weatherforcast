using System.Linq;
using Xunit;

namespace BackendTest.OpenWeathermap
{
    public class CitiesTest
    {
        [Fact]
        public void TestCitiesCount()
        {
            Assert.Equal(28786, TestUtilities.Cities.All.Count());
        }

        [Fact]
        public void TestGermanCitiesOnly()
        {
            Assert.Empty(TestUtilities.Cities.All.Where(city => city.Country != "DE"));
        }
    }
}
