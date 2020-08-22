using Backend.OpenWeathermap;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Xunit;

namespace BackendTest.OpenWeathermap
{
    public class CitynamesToIdsTest
    {
        IOptions<CitiesSettings> citiesSettings = TestUtilities.GetCitiesSettingsOptions();

        [Fact]
        public void TestCityId()
        {
            Assert.Equal(TestUtilities.CityIdHamburg, TestUtilities.CitynameToId.Dictionary["Hamburg"]);
        }

        [Fact]
        public void TestNoCityId()
        {
            Assert.Equal(-1, TestUtilities.CitynameToId.Dictionary.GetValueOrDefault("Hambur", -1));
        }
    }
}
