using LanguageExt;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Backend.OpenWeathermap
{
    /// <summary>
    /// All cityNames with their ids
    /// </summary>
    public class CitynamesIds : ICitynamesIds
    {
        private readonly IEnumerable<KeyValuePair<string,int>> citynamesIds;

        /// <summary>
        /// Used by Dependency Injection
        /// <param name="options">allows to configure the path of the cities.json via appsettings.json</param>
        /// <exception cref="ArgumentNullException">when options == null</exception>
        /// <exception cref="ArgumentException">when IsNullOrWhiteSpace(options.Value.Path)</exception>
        /// </summary>
        public CitynamesIds(IOptions<CitiesSettings> options)
        {
            if (options == null) 
                throw new ArgumentNullException(nameof(options));

            citynamesIds = new Cities(options.Value).All
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

            return citynamesIds.Where(cityname => cityname.Key.StartsWith(start));
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
                throw new ArgumentNullException(nameof(cityname));
            }

            KeyValuePair<string, int> result = citynamesIds
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
