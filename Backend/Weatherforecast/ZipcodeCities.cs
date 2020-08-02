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
    public static class ZipcodeCities
    {
        private static Lazy<Dictionary<string,IEnumerable<string>>> dictionary =
            new Lazy<Dictionary<string, IEnumerable<string>>>(() => ReadZipcodeCitiesFromCsv());
        
        /// <summary>
        /// Mapping from German zipcode to cities 
        /// </summary>
        public static Dictionary<string, IEnumerable<string>> Dictionary => dictionary.Value;

        private static Dictionary<string, IEnumerable<string>> ReadZipcodeCitiesFromCsv()
        {
            const int zipcodeIndex = 2;
            const int cityIndex = 1;

            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Weatherforecast\zuordnung_plz_ort.csv");
            
            return File.ReadAllText(path)
                .Split(Environment.NewLine)
                .Skip(1) // Skip Header
                .Where(line => line != "") // Skip empty lines
                .Select(csvLine =>         // Select zipcode and city
                {
                    var column = csvLine.Split(",");
                    return new KeyValuePair<string, string>(column[zipcodeIndex], column[cityIndex]);
                })
                .GroupBy(k => k.Key)
                .ToDictionary(k => k.Key, k => k.Select(g => g.Value));
        }
    }
}
