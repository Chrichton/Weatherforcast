using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace Backend.OpenWeathermap
{
    /// <summary>
    /// German cities used by the API
    /// </summary>
    public static class Cities
    {
        private static Lazy<IEnumerable<City>> cities =
            new Lazy<IEnumerable<City>>(() => ReadCitiesFromJSON());

        /// <summary>
        /// All cities used by the API
        /// </summary>
        public static IEnumerable<City> All => cities.Value;

        private static IEnumerable<City> ReadCitiesFromJSON()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), 
                @"OpenWeathermap/city.list.json");
            return JsonSerializer.Deserialize<IEnumerable<City>>(File.ReadAllText(path))
                .Where(city => city.Country == "DE");  // Only the German cities are required
        }
    }
}
