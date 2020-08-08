using LanguageExt;
using System;
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
        public IEnumerable<KeyValuePair<string, int>> GetCitynamesIdsStartingWith(string start)
        {
            if (string.IsNullOrWhiteSpace(start))
            {
                return Enumerable.Empty<KeyValuePair<string, int>>();
            }

            return citynames.Where(cityname => cityname.Key.StartsWith(start));
        }

        /// <summary>
        /// Returns cityname and id for the cityname
        /// </summary>
        /// <param name="cityname">cityname</param>
        /// <returns>cityname and id for the citynames. Returns None, when city doesn't exist</ret
        public Option<KeyValuePair<string, int>> GetCityNameIdForCity(string cityname)
        {
            if (string.IsNullOrWhiteSpace(cityname))
            {
                throw new ArgumentNullException($"{nameof(cityname)} must not be null");
            }

            KeyValuePair<string, int> result = citynames
                .SingleOrDefault(name => name.Key == cityname);

            if (result.Equals(default(KeyValuePair<string, int>)))
            {
                return Option<KeyValuePair<string, int>>.None;
            }

            return Option<KeyValuePair<string, int>>.Some(result);
        }


        private class Comparer : IEqualityComparer<City>
        {
            public bool Equals([AllowNull] City x, [AllowNull] City y) => Equals(x.Name, y.Name);

            public int GetHashCode([DisallowNull] City obj) => obj.Name.GetHashCode();
        }
    }
}
