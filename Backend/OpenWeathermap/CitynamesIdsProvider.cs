using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Backend.OpenWeathermap
{
    public class CitynamesIdsProvider : ICitynamesIdsProvider
    {
        private readonly IEnumerable<KeyValuePair<string,int>> citynames;

        /// <summary>
        /// Used by Dependency Injection
        /// </summary>
        public CitynamesIdsProvider()
        {
            citynames = Cities.All
                .Distinct(new Comparer()) // There are Citynames (Berlin) with two different ids
                .Select(city => new KeyValuePair<string, int>(city.Name, city.Id));
        }
        /// <summary>
        /// Returns all pairs of cityname and id where the citynames are starting with "start"
        /// </summary>
        /// <param name="start">start-string</param>
        /// <returns>all pairs of cityname and id where the citynames are starting with "start". Returns Empty, when city doesn't exist</ret
        public IEnumerable<KeyValuePair<string, int>> GetCitynamesStartingWith(string start)
        {
            if (string.IsNullOrWhiteSpace(start))
            {
                return Enumerable.Empty<KeyValuePair<string, int>>();
            }

            return citynames.Where(cityname => cityname.Key.StartsWith(start));
        }

        private class Comparer : IEqualityComparer<City>
        {
            public bool Equals([AllowNull] City x, [AllowNull] City y) => Equals(x.Name, y.Name);

            public int GetHashCode([DisallowNull] City obj) => obj.Name.GetHashCode();
        }
    }
}
