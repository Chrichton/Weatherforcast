using LanguageExt;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Weatherforecast.Service
{
    public interface IWeatherService
    {
        /// <summary>
        /// Retrieves the data for the weather
        /// </summary>
        /// <param name="cityId">Id of the city</param>
        /// <returns>Some(data) for the weather. None, when the cityId is unknown</returns>
        public Task<Option<WeatherModel>> GetWeather(int cityId);

        /// <summary>
        /// Retrieves all cities and their Ids for the supplied zipCode
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns>all cities for the supplied zipCode or emtpy when the zipCode is unknown</returns>
        public Task<IEnumerable<KeyValuePair<string, int>>> GetCitiesIdsForZipCode(int zipCode);

        /// <summary>
        /// Retrieves all pairs of cityname and cityid for all citynames starting with "start"
        /// </summary>
        /// <param name="start">start-string</param>
        /// <returns>all pairs of cityname and cityid for all citynames starting with "start". Empty, when city doesn't exist</returns>
        public Task<IEnumerable<KeyValuePair<string,int>>> GetCitiesStartingWith(string start);
    }
}