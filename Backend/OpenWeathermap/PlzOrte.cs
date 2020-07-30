using System;
using System.Collections.Generic;

namespace Backend.OpenWeathermap
{
    /// <summary>
    /// Access to a German Ort by Plz
    /// Source of the "zuordnung_plz_ort.csv": https://www.suche-postleitzahl.org/downloads
    /// </summary>
    public static class PlzOrte
    {
        private static Lazy<Dictionary<string,string>> dictionary =
            new Lazy<Dictionary<string, string>>(() => ReadPlzOrteFromCsv());
        
        /// <summary>
        /// Mapping from German Plz to Ort 
        /// </summary>
        public static Dictionary<string, string> Dictionary => dictionary.Value;

        private static Dictionary<string, string> ReadPlzOrteFromCsv()
        {
            return new Dictionary<string, string>
            {
                {"21037", "Hamburg" }
            };
        }
    }
}
