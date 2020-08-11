using AutoMapper;
using Backend.OpenWeathermap;
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
        private readonly ICitynamesIdsProvider citynamesIdsProvider;

        public WeatherService(ILogger<WeatherService> logger, IMapper mapper, 
            IOpenWeathermapService openWeathermapService, 
            IZipCodeToCitiesProvider zipCodeToCitiesProvider,
            ICitynamesIdsProvider citynamesIdsProvider)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.openWeathermapService = openWeathermapService ?? throw new ArgumentNullException(nameof(openWeathermapService));
            this.zipCodeToCitiesProvider = zipCodeToCitiesProvider ?? throw new ArgumentNullException(nameof(zipCodeToCitiesProvider));
            this.citynamesIdsProvider = citynamesIdsProvider ?? throw new ArgumentNullException(nameof(citynamesIdsProvider));
        }

        /// <summary>
        /// Retrieves the data for the weather
        /// </summary>
        /// <param name="cityId">Id of the city</param>
        /// <returns>Some(data) for the weather. None, when the cityId is unknown</returns>
        public async Task<Option<WeatherModel>> GetWeather(int cityId)
        {
            var currentTaskOption = openWeathermapService.GetCurrentWeather(cityId);
            var forecastTaskOption = openWeathermapService.GetWeatherforecast(cityId);

            Task.WaitAll(currentTaskOption, forecastTaskOption);

            Option<OpenWeathermapCurrent> openWeatherMapCurrentOption = await currentTaskOption;
            Option<OpenWeathermapForecast> openWeatherMapForecastOption = await forecastTaskOption;

            return openWeatherMapCurrentOption
                .Some(openWeatherMapCurrent =>
                {
                    Weather current = mapper.Map<OpenWeathermapCurrent, Weather>(openWeatherMapCurrent);
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
        /// Retrieves all cities and their Ids for the supplied zipCode
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns>all cities for the supplied zipCode or emtpy when the zipCode is unknown</returns>
        public async Task<IEnumerable<KeyValuePair<string, int>>> GetCitiesIdsForZipCode(int zipCode)
        {
            IEnumerable<string> cities;
            if (zipCodeToCitiesProvider.GetDictionary().TryGetValue(zipCode, out cities))
            {
                return await Task.FromResult(cities
                    .Select(city => citynamesIdsProvider.GetCityNameIdForCity(city)))
                    .BindT(cityOpt => cityOpt);
            }

            return await Task.FromResult(Enumerable.Empty<KeyValuePair<string, int>>());
        }

        /// <summary>
        /// Retrieves all pairs of cityname and cityid for all citynames starting with "start"
        /// </summary>
        /// <param name="start">start-string</param>
        /// <returns>all pairs of cityname and cityid for all citynames starting with "start". Empty, when city doesn't exist</returns>
        public async Task<IEnumerable<KeyValuePair<string, int>>> GetCitiesStartingWith(string start)
        {
            return await Task.FromResult(citynamesIdsProvider
                .GetCitynamesIdsStartingWith(start));
        }
    }
}
