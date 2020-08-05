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
        /// <param name="city">German City</param>
        /// <returns>Some(data) for the weather. None, when the city is unknown</returns>
        /// <exception cref="ArgumentNullException">When city is null</exception>
        public Task<Option<WeatherModel>> GetWeather(string city);

        /// <summary>
        /// Retrieves all cities for the supplied zipCode
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns>all cities for the supplied zipCode or emtpy when zipCode is unknown</returns>
        /// <summary>
        public Task<IEnumerable<string>> GetCitiesForZipCode(int zipCode);
    }
}