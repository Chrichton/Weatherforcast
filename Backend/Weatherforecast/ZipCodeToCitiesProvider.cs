using System;
using System.Collections.Generic;

namespace Backend.Weatherforecast
{
    public class ZipCodeToCitiesProvider : IZipCodeToCitiesProvider
    {
        private readonly IDictionary<int, IEnumerable<string>> dictionary;

        /// <summary>
        /// Used by Dependency Injection
        /// </summary>
        public ZipCodeToCitiesProvider() => dictionary = ZipcodeCities.Dictionary;

        public ZipCodeToCitiesProvider(IDictionary<int, IEnumerable<string>> dictionary)
        {
            this.dictionary = dictionary ?? throw new ArgumentNullException($"{nameof(dictionary)} must not be null");
        }

        public IDictionary<int, IEnumerable<string>> GetDictionary() => dictionary;
    }
}
