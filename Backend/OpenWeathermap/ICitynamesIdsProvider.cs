using System.Collections.Generic;

namespace Backend.OpenWeathermap
{
    public interface ICitynamesIdsProvider
    {
        /// <summary>
        /// Returns all pairs of cityname and id where the citynames are starting with "start"
        /// </summary>
        /// <param name="start">start-string</param>
        /// <returns>all pairs of cityname and id where the citynames are starting with "start". Returns Empty, when city doesn't exist</ret
        public IEnumerable<KeyValuePair<string, int>> GetCitynamesStartingWith(string start);
    }
}