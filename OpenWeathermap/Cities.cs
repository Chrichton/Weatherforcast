using System;
using System.Collections.Generic;

namespace Backend.OpenWeathermap
{
    public static class Cities
    {
        private static Lazy<IEnumerable<City>> cities = 
            new Lazy<IEnumerable<City>>(() => ReadCitiesFromJSON());
        public static IEnumerable<City> All => cities.Value;

        private static IEnumerable<City> ReadCitiesFromJSON()
        {
            yield return new City { Id = 42, Country = "DE" };
        }
    }
}
