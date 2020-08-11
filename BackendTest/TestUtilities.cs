using System.IO;

namespace BackendTest
{
    public static class TestUtilities
    {
        public static string GetOpenWeathermapForcastJson() => 
            File.ReadAllText(@"./OpenWeathermap/service/weatherforecast.json");

        public static string GetOpenWeathermapCurrentWeatherJson() =>
            File.ReadAllText(@"./OpenWeathermap/service/currentweather.json");

        public static int CityIdHamburg => 2911298;
    }
}