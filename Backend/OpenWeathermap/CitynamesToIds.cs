using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.OpenWeathermap
{
    /// <summary>
    /// Mapping from German Citynames to their Id
    /// Id is used by their API
    /// Source: OpenWeathermap (city.list.json)
    /// </summary>
    public static class CitynamesToIds
    {
        private static Lazy<Dictionary<string,int>> dictionary = 
            new Lazy<Dictionary<string, int>>(() => ReadCitiesFromJSON());
        
        /// <summary>
        /// Mapping from Cityname to Id used by API
        /// </summary>
        public static Dictionary<string, int> Dictionary => dictionary.Value;

        private static Dictionary<string, int> ReadCitiesFromJSON()
        {
            return Cities.All
                .GroupBy(city => city.Name)                 // There are more than one Cities with the same Name separated by exact location.
                .ToDictionary(g => g.Key, g => g.First().Id); // I pick the first one. Should be close enough. In real life, I would ask the customer
        }
    }
}
