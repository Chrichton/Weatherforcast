using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Backend.Weatherforecast
{
    /// <summary>
    /// Access to a German city (Ort) by zipcode (Plz)
    /// Source of "zuordnung_plz_ort.csv": https://www.suche-postleitzahl.org/downloads
    /// </summary>
    public class ZipcodeToCities
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="zipcodeCitiesSetting">allows to configure the path to the zuordnung_plz_ort.csv</param>
        /// <exception cref="ArgumentNullException">when zipcodeCitiesSetting == null</exception>
        /// <exception cref="ArgumentException">when IsNullOrWhiteSpace(zipcodeCitiesSetting.Path)</exception>
        public ZipcodeToCities(ZipcodeToCitiesSetting zipcodeCitiesSetting)
        {
            if (zipcodeCitiesSetting == null)
                throw new ArgumentNullException(nameof(zipcodeCitiesSetting));

            if (string.IsNullOrWhiteSpace(zipcodeCitiesSetting.Path))
                throw new ArgumentException("Path must not be nullOrWhitespace");

            Dictionary = ReadZipcodeCitiesFromCsv(zipcodeCitiesSetting);
        }

        /// <summary>
        /// Mapping from German zipcode to cities 
        /// </summary>
        public Dictionary<int, IEnumerable<string>> Dictionary { get; }

        private static Dictionary<int, IEnumerable<string>> ReadZipcodeCitiesFromCsv(
            ZipcodeToCitiesSetting settings)
        {
            const int zipcodeIndex = 2;
            const int cityIndex = 1;

            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                settings.Path);

            return File.ReadAllText(path)
                .Split(Environment.NewLine)
                .Skip(1) // Skip Header
                .Where(line => line != "") // Skip empty lines
                .Select(csvLine =>         // Select zipcode and city
                {
                    var column = csvLine.Split(",");
                    return new KeyValuePair<int, string>(int.Parse(column[zipcodeIndex]), column[cityIndex]);
                })
                .GroupBy(k => k.Key)
                .ToDictionary(k => k.Key, k => k.Select(g => g.Value));
        }
    }
}
