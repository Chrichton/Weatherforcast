using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.OpenWeathermap
{
    /// <summary>
    /// Mapping from German Citynames to their Id
    /// Id is used by OpenWeathermap API
    /// Source: OpenWeathermap (city.list.json)
    /// </summary>
    public class CitynameToId : ICitynameToId
    {
        /// <summary>
        /// User for Dependency Injection
        /// </summary>
        /// <param name="options">allows to configure the path of the cities.json via appsettings.json</param>
        /// <exception cref="ArgumentNullException">when options == null</exception>
        /// <exception cref="ArgumentException">when IsNullOrWhiteSpace(options.Value.Path)</exception>
        public CitynameToId(IOptions<CitiesSettings> options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            Dictionary = ReadCitiesFromJSON(options);
        }

        /// <summary>
        /// Mapping from Cityname to Id used by API
        /// </summary>
        public Dictionary<string, int> Dictionary { get; }

        private static Dictionary<string, int> ReadCitiesFromJSON(IOptions<CitiesSettings> options)
        {
            return new Cities(options.Value).All
                .GroupBy(city => city.Name)                 // There are more than one Cities with the same Name separated by exact location.
                .ToDictionary(g => g.Key, g => g.First().Id); // I pick the first one. Should be close enough. In real life, I would ask the customer
        }
    }
}
