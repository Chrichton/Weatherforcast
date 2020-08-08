using System;
using System.Collections.Generic;

namespace Backend.OpenWeathermap
{
    public class CityNameToIdProvider : ICitynameToIdProvider
    {
        private readonly IDictionary<string, int> dictionary;

        /// <summary>
        /// Used by Dependency Injection
        /// </summary>
        public CityNameToIdProvider() => dictionary = CitynamesToIds.Dictionary;

        public CityNameToIdProvider(IDictionary<string, int> dictionary)
        {
            this.dictionary = dictionary ?? throw new ArgumentNullException($"{nameof(dictionary)} must not be null");
        }

        public IDictionary<string, int> GetDictionary() => dictionary;
    }
}
