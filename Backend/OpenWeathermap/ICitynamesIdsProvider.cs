using LanguageExt;
using System.Collections.Generic;

namespace Backend.OpenWeathermap
{
    /// <summary>
    /// Information about cityNames and their ids
    /// </summary>
    public interface ICitynamesIdsProvider
    {
        /// <summary>
        /// Returns all pairs of cityname and id where the citynames are starting with "start"
        /// </summary>
        /// <param name="start">start-string</param>
        /// <returns>all pairs of cityname and id where the citynames are starting with "start". Returns Empty, when city doesn't exist</ret
        public IEnumerable<KeyValuePair<string, int>> GetCitynamesIdsStartingWith(string start);

        /// <summary>
        /// Returns cityname and id for the cityname
        /// </summary>
        /// <param name="cityname">cityname</param>
        /// <returns>cityname and id for the citynames. Returns None, when city doesn't exist</ret
        public Option<KeyValuePair<string, int>> GetCityNameIdForCity(string cityname);
    }
}