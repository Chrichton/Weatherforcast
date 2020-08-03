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
        /// <returns>Current Weather-Data</returns>
        /// <exception cref="ArgumentNullException">When city is null</exception>
        /// <exception cref="ArgumentException">When city is unknown</exception>
        Task<IEnumerable<string>> GetCitiesForZipCode(string zipCode);

        /// <summary>
        /// Retrieves all cities for the supplied zipCode
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns>all cities for the supplied zipCode or emtpy when zipCode is unknown</returns>
        Task<Model> GetWeather(string city);
    }
}