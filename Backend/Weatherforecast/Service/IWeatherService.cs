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

        /// TODO Löschen
        /// <summary>
        /// Retrieves the data for the weather
        /// </summary>
        /// <param name="city">German City</param>
        /// <returns>Some(data) for the weather. None, when the city is unknown</returns>
        /// <exception cref="ArgumentNullException">When city is null</exception>
        public Task<Option<WeatherModel>> GetWeather(string city);

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