using Backend.OpenWeathermap;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BackendTest.OpenWeathermap
{
    public class CitynamesIdProviderTest
    {
        [Fact]
        public void TestBerlin()
        {
            IEnumerable<KeyValuePair<string, int>> result = new CitynamesIdsProvider()
                .GetCitynamesIdsStartingWith("Berlin");

            Assert.Equal(10, result.Count());
            Assert.Single(result.Where(pair => pair.Key == "Berlin")); // Berlin was there two times, with different ids
        }

        [Fact]
        public void TestNull()
        {
            IEnumerable<KeyValuePair<string, int>> result = new CitynamesIdsProvider()
                .GetCitynamesIdsStartingWith(null);

            Assert.Empty(result);
        }

        [Fact]
        public void TestWhitespaces()
        {
            IEnumerable<KeyValuePair<string, int>> result = new CitynamesIdsProvider()
                .GetCitynamesIdsStartingWith("  ");

            Assert.Empty(result);
        }
    }
}
