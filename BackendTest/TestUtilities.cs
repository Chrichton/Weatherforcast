using System.IO;
using System.Reflection;

namespace BackendTest
{
    public static class TestUtilities
    {
        public static string GetOpenWeathermapForcastJson()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"OpenWeathermap\service\weatherforecast.json");

            return File.ReadAllText(path);
        }

        public static string GetOpenWeathermapCurrentWeatherJson()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"OpenWeathermap\service\currentweather.json");

            return File.ReadAllText(path);
        }
    }
}
