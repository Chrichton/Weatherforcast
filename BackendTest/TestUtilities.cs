using Backend.OpenWeathermap;
using Microsoft.Extensions.Options;
using NSubstitute;
using System.IO;

namespace BackendTest
{
    public static class TestUtilities
    {
        public static Cities Cities = new Cities(GetCitiesSettings());

        public static CitynamesIds CitynamesIds = new CitynamesIds(GetCitiesSettingsOptions());

        public static CitynameToId CitynameToId = new CitynameToId(GetCitiesSettingsOptions());

        public static string GetOpenWeathermapForcastJson() => 
            File.ReadAllText(@"./OpenWeathermap/service/weatherforecast.json");

        public static string GetOpenWeathermapCurrentWeatherJson() =>
            File.ReadAllText(@"./OpenWeathermap/service/currentweather.json");

        public static int CityIdHamburg => 2911298;

        public static CitiesSettings GetCitiesSettings() => 
            new CitiesSettings { Path = "OpenWeathermap/city.list.json" };

        public static IOptions<CitiesSettings> GetCitiesSettingsOptions()
        {
            IOptions<CitiesSettings> settings = Substitute.For<IOptions<CitiesSettings>>();
            settings.Value.Returns(GetCitiesSettings());

            return settings;
        }
    }
}