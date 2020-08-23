using Backend.OpenWeathermap;
using Backend.Weatherforecast;
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

        public static ZipcodeToCities ZipcodeToCities = new ZipcodeToCities(GetZipcodeToCitiesSettings());

        public static ZipcodeToCitiesProvider ZipcodeToCitiesProvider =
            new ZipcodeToCitiesProvider(GetZipcodeToCitiesSettingsOptions());

        public static string GetOpenWeathermapForcastJson() => 
            File.ReadAllText(@"./OpenWeathermap/service/weatherforecast.json");

        public static string GetOpenWeathermapCurrentWeatherJson() =>
            File.ReadAllText(@"./OpenWeathermap/service/currentweather.json");

        public static int CityIdHamburg => 2911298;

        public static int PlzWith20Cities => 55767;

        public static CitiesSettings GetCitiesSettings() => 
            new CitiesSettings { Path = "OpenWeathermap/city.list.json" };

        public static IOptions<CitiesSettings> GetCitiesSettingsOptions()
        {
            IOptions<CitiesSettings> settings = Substitute.For<IOptions<CitiesSettings>>();
            settings.Value.Returns(GetCitiesSettings());

            return settings;
        }

        public static ZipcodeToCitiesSetting GetZipcodeToCitiesSettings() =>
           new ZipcodeToCitiesSetting { Path = "Weatherforecast/zuordnung_plz_ort.csv" };

        public static IOptions<ZipcodeToCitiesSetting> GetZipcodeToCitiesSettingsOptions()
        {
            IOptions<ZipcodeToCitiesSetting> settings = Substitute.For<IOptions<ZipcodeToCitiesSetting>>();
            settings.Value.Returns(GetZipcodeToCitiesSettings());

            return settings;
        }
    }
}