using AutoMapper;
using Backend.OpenWeathermap.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Weatherforecast.Service
{
    public class WeatherService : IWeatherService
    {
        private readonly ILogger<WeatherService> logger;
        private IMapper mapper;
        private IOpenWeathermapService openWeathermapService;

        public WeatherService(ILogger<WeatherService> logger, IMapper mapper, 
            IOpenWeathermapService openWeathermapService)
        {
            this.logger = logger ?? throw new ArgumentNullException($"{nameof(logger)} must not be null");
            this.mapper = mapper ?? throw new ArgumentNullException($"{nameof(mapper)} must not be null");
            this.openWeathermapService = openWeathermapService ?? throw new ArgumentNullException($"{nameof(openWeathermapService)} must not be null");
        }

        /// <summary>
        /// Retrieves the data for the weather
        /// </summary>
        /// <param name="city">German City</param>
        /// <returns>Current Weather-Data</returns>
        /// <exception cref="ArgumentNullException">When city is null</exception>
        /// <exception cref="ArgumentException">When city is unknown</exception>
        public async Task<Model> GetWeather(string city)
        {
            return await Task.FromResult(new Model());
        }

        /// <summary>
        /// Retrieves all cities for the supplied zipCode
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns>all cities for the supplied zipCode or emtpy when zipCode is unknown</returns>
        public async Task<IEnumerable<string>> GetCitiesForZipCode(string zipCode)
        {
            return await Task.FromResult(Enumerable.Empty<string>());
        }
    }
}
