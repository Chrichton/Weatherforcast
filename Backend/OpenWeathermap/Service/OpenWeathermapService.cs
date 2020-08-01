using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Backend.OpenWeathermap.Service
{
    public class OpenWeathermapService
    {
        private readonly ILogger<OpenWeathermapService> logger;
        private readonly HttpClient httpClient;
        private readonly IDictionary<string, int> ortToIdMapping;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="httpClient"></param>
        /// <param name="ortToIdMapping">The API needs a mapping from an ort to its id</param>
        public OpenWeathermapService(ILogger<OpenWeathermapService> logger, HttpClient httpClient, IDictionary<string,int> ortToIdMapping)
        {
            this.logger = logger ?? throw new ArgumentNullException($"{nameof(logger)} must not be null");
            this.httpClient = httpClient ?? throw new ArgumentNullException($"{nameof(httpClient)} must not be null");
            this.ortToIdMapping = ortToIdMapping ?? throw new ArgumentNullException($"{nameof(ortToIdMapping)} must not be null");
        }

        /// <summary>
        /// Retrieves the Data for the 5 day weatherforecast
        /// </summary>
        /// <param name="ort">German City</param>
        /// <returns>Forecast-Data</returns>
        /// <exception cref="ArgumentNullException">When ort is null</exception>
        /// <exception cref="ArgumentException">When ort is unknown</exception>
        public async Task<Rootobject> GetWeatherforecast(string ort)
        {
            if (ort == null)
            {
                logger.LogError("{nameof(ort)} was null");
                throw new ArgumentNullException($"{nameof(ort)} must not be null");
            }

            const string baseUrl = "http://api.openweathermap.org/data/2.5/forecast";
            const string appId = "fcadd28326c90c3262054e0e6ca599cd";
            const string language = "de";
            const string units = "metric";

            int ortId;
            if (!ortToIdMapping.TryGetValue(ort, out ortId))
            {
                logger.LogError($"unknown {nameof(ort)} requested", ort);
                throw new ArgumentException("Der Ort ist unbekannt", nameof(ort));
            }

            string requestUri = $"{baseUrl}?APPID={appId}&lang={language}&units={units}&id={ortId}";
            HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUri);
            responseMessage.EnsureSuccessStatusCode();

            return await JsonSerializer.DeserializeAsync<Rootobject>(
                await responseMessage.Content.ReadAsStreamAsync());
        }

        public async Task<Rootobject> GetFutureWeatherforecast(string ort)
        {
            if (ort == null)
            {
                logger.LogError("{nameof(ort)} was null");
                throw new ArgumentNullException($"{nameof(ort)} must not be null");
            }

            const string baseUrl = "http://api.openweathermap.org/data/2.5/forecast";
            const string appId = "fcadd28326c90c3262054e0e6ca599cd";
            const string language = "de";
            const string units = "metric";

            int ortId;
            if (!ortToIdMapping.TryGetValue(ort, out ortId))
            {
                logger.LogError($"unknown {nameof(ort)} requested", ort);
                throw new ArgumentException("ort unknown", nameof(ort));
            }

            string requestUri = $"{baseUrl}?APPID={appId}&lang={language}&units={units}&id={ortId}";
            HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUri);
            responseMessage.EnsureSuccessStatusCode();

            return await JsonSerializer.DeserializeAsync<Rootobject>(
                await responseMessage.Content.ReadAsStreamAsync());
        }
    }
}