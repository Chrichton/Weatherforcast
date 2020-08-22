using Backend.OpenWeathermap;
using LanguageExt;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BackendTest.OpenWeathermap
{
    public class CitynamesIdsTest
    {
        [Fact]
        public void TestBerlin()
        {
            IEnumerable<KeyValuePair<string, int>> result = TestUtilities.CitynamesIds
                .GetCitynamesIdsStartingWith("Berlin");

            Assert.Equal(10, result.Count());
            Assert.Single(result.Where(pair => pair.Key == "Berlin")); // Berlin was there two times, with different ids
        }

        [Fact]
        public void TestNull()
        {
            IEnumerable<KeyValuePair<string, int>> result = TestUtilities.CitynamesIds
                .GetCitynamesIdsStartingWith(null);

            Assert.Empty(result);
        }

        [Fact]
        public void TestWhitespaces()
        {
            IEnumerable<KeyValuePair<string, int>> result = TestUtilities.CitynamesIds
                .GetCitynamesIdsStartingWith("  ");

            Assert.Empty(result);
        }

        [Fact]
        public void TestGetCityNameIdForCity()
        {
            Option<KeyValuePair<string, int>> idOpt =
                TestUtilities.CitynamesIds.GetCityNameIdForCity("Hamburg");

            var expected = new KeyValuePair<string, int>("Hamburg", TestUtilities.CityIdHamburg);

            idOpt
                .Some(id => Assert.Equal(expected, id))
                .None(() => Assert.False(true, "Test Failed"));
        }

        [Fact]
        public void TestGetCityNameIdForCityNoCity()
        {
            Option<KeyValuePair<string, int>> idOpt =
                TestUtilities.CitynamesIds.GetCityNameIdForCity("Hambur");

            Assert.True(idOpt.IsNone);
        }

        [Fact]
        public void TestGetCitynamesIdsStartingWith()
        {
            IEnumerable<KeyValuePair<string, int>> result =
                TestUtilities.CitynamesIds.GetCitynamesIdsStartingWith("Berlin");

            Assert.Equal(10, result.Count());
            var expected = new KeyValuePair<string, int>("Berlin Köpenick", 2885657);
            Assert.Equal(expected, result.First());
        }
    }
}
