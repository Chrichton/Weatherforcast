using System;
using System.Collections.Generic;

namespace Backend.OpenWeathermap
{
    public class CitynameToIdProvider : ICitynameToIdProvider
    {
        private readonly IDictionary<string, int> dictionary;

        /// <summary>
        /// Used by Dependency Injection
        /// </summary>
        public CitynameToIdProvider() => dictionary = CitynamesToIds.Dictionary;

        public CitynameToIdProvider(IDictionary<string, int> dictionary)
        {
            this.dictionary = dictionary ?? throw new ArgumentNullException($"{nameof(dictionary)} must not be null");
        }

        public IDictionary<string, int> GetDictionary() => dictionary;
    }
}
