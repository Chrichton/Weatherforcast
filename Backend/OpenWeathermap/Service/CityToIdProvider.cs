using System;
using System.Collections.Generic;

namespace Backend.OpenWeathermap.Service
{
    public class CityToIdProvider : ICityToIdProvider
    {
        private readonly IDictionary<string, int> dictionary;

        /// <summary>
        /// Used by Dependency Injection
        /// </summary>
        public CityToIdProvider() => dictionary = Cities.Dictionary;

        public CityToIdProvider(IDictionary<string, int> dictionary)
        {
            this.dictionary = dictionary ?? throw new ArgumentNullException($"{nameof(dictionary)} must not be null");
        }

        public IDictionary<string, int> GetDictionary() => dictionary;
    }
}
