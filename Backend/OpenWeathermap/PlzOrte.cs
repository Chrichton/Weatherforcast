using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Backend.OpenWeathermap
{
    /// <summary>
    /// Access to a German Ort by Plz
    /// Source of the "zuordnung_plz_ort.csv": https://www.suche-postleitzahl.org/downloads
    /// </summary>
    public static class PlzOrte
    {
        private static Lazy<Dictionary<string,IEnumerable<string>>> dictionary =
            new Lazy<Dictionary<string, IEnumerable<string>>>(() => ReadPlzOrteFromCsv());
        
        /// <summary>
        /// Mapping from German Plz to Orte 
        /// </summary>
        public static Dictionary<string, IEnumerable<string>> Dictionary => dictionary.Value;

        private static Dictionary<string, IEnumerable<string>> ReadPlzOrteFromCsv()
        {
            const int plzIndex = 2;
            const int ortIndex = 1;

            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), 
                @"OpenWeathermap\zuordnung_plz_ort.csv");
            
            return File.ReadAllText(path)
                .Split(Environment.NewLine)
                .Skip(1) // Skip Header
                .Where(line => line != "") // Skip empty lines
                .Select(csvLine =>         // Select plz and ort
                {
                    var column = csvLine.Split(",");
                    return new KeyValuePair<string, string>(column[plzIndex], column[ortIndex]);
                })
                .GroupBy(k => k.Key)
                .ToDictionary(k => k.Key, k => k.Select(g => g.Value));
        }
    }
}
