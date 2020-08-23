using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Backend.Weatherforecast
{
    public class ZipcodeToCitiesProvider : IZipcodeToCitiesProvider
    {
        /// <summary>
        /// Used by Dependency Injection
        /// </summary>
        /// <param name="options">allows to configure the path of the zuordnung_plz_ort.csv via appsettings.json</param>
        /// <exception cref="ArgumentNullException">when options == null</exception>
        /// <exception cref="ArgumentException">when IsNullOrWhiteSpace(options.Value.Path)</exception>
        public ZipcodeToCitiesProvider(IOptions<ZipcodeToCitiesSetting> options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            Dictionary = new ZipcodeToCities(options.Value).Dictionary;
        }

        public IDictionary<int, IEnumerable<string>> Dictionary { get; }
    }
}
