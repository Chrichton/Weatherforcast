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
    public class Cities
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="citiesSettings">allows to configure the path to the cities.json</param>
        /// <exception cref="ArgumentNullException">when citiesSettings == null</exception>
        /// <exception cref="ArgumentException">when IsNullOrWhiteSpace(citiesSettings.Path)</exception>
        public Cities(CitiesSettings citiesSettings)
        {
            if (citiesSettings == null) 
                throw new ArgumentNullException(nameof(citiesSettings));

            if (string.IsNullOrWhiteSpace(citiesSettings.Path))
                throw new ArgumentException("Path must not be nullOrWhitespace");

            All = ReadCitiesFromJSON(citiesSettings);
        }

        /// <summary>
        /// All German cities used by the API
        /// </summary>
        public IEnumerable<City> All { get; }

        private static IEnumerable<City> ReadCitiesFromJSON(CitiesSettings settings)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), 
                settings.Path);
            return JsonSerializer.Deserialize<IEnumerable<City>>(File.ReadAllText(path))
                .Where(city => city.Country == "DE");  // Only the German cities are required
        }
    }
}
