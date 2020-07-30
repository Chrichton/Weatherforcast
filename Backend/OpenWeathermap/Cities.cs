using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Backend.OpenWeathermap
{
    /// <summary>
    /// Access to all Cities and Locations of OpenWeathermap (city.list.json)
    /// used by their API
    /// </summary>
    public static class Cities
    {
        private static Lazy<IEnumerable<City>> cities = 
            new Lazy<IEnumerable<City>>(() => ReadCitiesFromJSON());
        
        /// <summary>
        /// Access to all cities
        /// </summary>
        public static IEnumerable<City> All => cities.Value;

        private static IEnumerable<City> ReadCitiesFromJSON()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"OpenWeathermap\city.list.json");
            List<City> cities = JsonConvert.DeserializeObject<List<City>>(File.ReadAllText(path));
            return cities;
        }
    }
}
