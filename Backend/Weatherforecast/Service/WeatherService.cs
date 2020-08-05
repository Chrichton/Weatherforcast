using AutoMapper;
using Backend.OpenWeathermap.Service;
using LanguageExt;
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
        private readonly IZipCodeToCitiesProvider zipCodeToCitiesProvider;

        public WeatherService(ILogger<WeatherService> logger, IMapper mapper, 
            IOpenWeathermapService openWeathermapService, IZipCodeToCitiesProvider zipCodeToCitiesProvider)
        {
            this.logger = logger ?? throw new ArgumentNullException($"{nameof(logger)} must not be null");
            this.mapper = mapper ?? throw new ArgumentNullException($"{nameof(mapper)} must not be null");
            this.openWeathermapService = openWeathermapService ?? throw new ArgumentNullException($"{nameof(openWeathermapService)} must not be null");
            this.zipCodeToCitiesProvider = zipCodeToCitiesProvider ?? throw new ArgumentNullException($"{nameof(zipCodeToCitiesProvider)} must not be null");

        }

        /// <summary>
        /// Retrieves the data for the weather
        /// </summary>
        /// <param name="city">German City</param>
        /// <returns>Current Weather-Data</returns>
        /// <exception cref="ArgumentNullException">When city is null</exception>
        /// <exception cref="ArgumentException">When city is unknown</exception>
        public async Task<Option<WeatherModel>> GetWeather(string city)
        {
            var currentTaskOption = openWeathermapService.GetCurrentWeather(city);
            var forecastTaskOption = openWeathermapService.GetWeatherforecast(city);

            Task.WaitAll(currentTaskOption, forecastTaskOption);

            Option<OpenWeatherMapCurrent> openWeatherMapCurrentOption = await currentTaskOption;
            Option<OpenWeathermapForecast> openWeatherMapForecastOption = await forecastTaskOption;

            return openWeatherMapCurrentOption
                .Some(openWeatherMapCurrent =>
                {
                    Weather current = mapper.Map<OpenWeatherMapCurrent, Weather>(openWeatherMapCurrent);
                    return openWeatherMapForecastOption
                        .Some(openWeatherMapForecast =>
                        {
                            Weather[] forecast = mapper.Map<WeatherList[], Weather[]>(openWeatherMapForecast.list);

                            var model = new WeatherModel(current, forecast);
                            model.AverageHumidity = model.CalculateAverageHumidity();
                            model.AverageTemperature = model.CalculateAverageTemperature();

                            return Option<WeatherModel>.Some(model);
                        })
                        .None(() => Option<WeatherModel>.None);
                })
                .None(() => Option<WeatherModel>.None);
        }

        /// <summary>
        /// Retrieves all cities for the supplied zipCode
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns>all cities for the supplied zipCode or emtpy when the zipCode is unknown</returns>
        public async Task<IEnumerable<string>> GetCitiesForZipCode(int zipCode)
        {
            IEnumerable<string> cities;
            if (zipCodeToCitiesProvider.GetDictionary().TryGetValue(zipCode, out cities))
                return await Task.FromResult(cities);

            return await Task.FromResult(Enumerable.Empty<string>());
        }
    }
}
