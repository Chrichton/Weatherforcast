using System;
using System.Collections.Generic;

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
            yield return new City { Id = 42, Country = "DE" };
        }
    }
}
