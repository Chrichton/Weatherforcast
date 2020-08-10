using Backend.OpenWeathermap;
using LanguageExt;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BackendTest.OpenWeathermap
{
    public class CitynamesIdsProviderTest
    {
        [Fact]
        public void TestGetCityNameIdForCity()
        {
            Option<KeyValuePair<string, int>> idOpt =
                new CitynamesIdsProvider().GetCityNameIdForCity("Hamburg");

            var expected = new KeyValuePair<string, int>("Hamburg", 2911298);

            idOpt
                .Some(id => Assert.Equal(expected, id))
                .None(() => Assert.False(true, "Test Failed"));
        }

        [Fact]
        public void TestGetCityNameIdForCityNoCity()
        {
            Option<KeyValuePair<string, int>> idOpt =
                new CitynamesIdsProvider().GetCityNameIdForCity("Hambur");

            Assert.True(idOpt.IsNone);
        }

        [Fact]
        public void TestGetCitynamesIdsStartingWith()
        {
            IEnumerable<KeyValuePair<string, int>> result = 
                new CitynamesIdsProvider().GetCitynamesIdsStartingWith("Berlin");

            Assert.Equal(10, result.Count());
            var expected = new KeyValuePair<string, int>("Berlin Köpenick", 2885657);
            Assert.Equal(expected, result.First());
        }

        [Fact]
        public void TestGetCityNameIdForCityWithExternalList()
        {
            var list = new[] { new KeyValuePair<string, int>("Hamburg", 11111) };
            Option<KeyValuePair<string, int>> idOpt =
                new CitynamesIdsProvider(list).GetCityNameIdForCity("Hamburg");

            var expected = new KeyValuePair<string, int>("Hamburg", 11111);

            idOpt
                .Some(id => Assert.Equal(expected, id))
                .None(() => Assert.False(true, "Test Failed"));
        }

        [Fact]
        public void TestGetCityNameIdForCityNoCityWithExternalList()
        {
            var list = new KeyValuePair<string, int>[] { };
            Option<KeyValuePair<string, int>> idOpt =
                new CitynamesIdsProvider(list).GetCityNameIdForCity("Hamburg");

            Assert.True(idOpt.IsNone);
        }

        [Fact]
        public void TestGetCitynamesIdsStartingWithExternalList()
        {
            var list = new[] { new KeyValuePair<string, int>("Berlin", 11111) };
            
            IEnumerable<KeyValuePair<string, int>> result =
                new CitynamesIdsProvider(list)
                    .GetCitynamesIdsStartingWith("Berlin");

            Assert.Single(result);
            var expected = new KeyValuePair<string, int>("Berlin", 11111);
            Assert.Equal(expected, result.Single());
        }
    }
}
