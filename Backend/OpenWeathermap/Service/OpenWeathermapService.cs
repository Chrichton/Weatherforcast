using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Backend.OpenWeathermap.Service
{
    public class OpenWeathermapService
    {
        private HttpClient httpClient;

        public OpenWeathermapService(HttpClient httpClient)
        {
            if (httpClient == null)
                throw new ArgumentNullException($"{nameof(httpClient)} must not be null");

            this.httpClient = httpClient;
        }

        public async Task<Rootobject> GetWeatherforecast(string ort)
        {
            if (ort == null)
                throw new ArgumentNullException($"{nameof(ort)} must not be null");

            const string baseUrl = "http://api.openweathermap.org/data/2.5/forecast?APPID=fcadd28326c90c3262054e0e6ca599cd&lang=de&units=metric";

            int cityId = Cities.Dictionary.GetValueOrDefault(ort, -1);
            if (cityId == -1)
                throw new ArgumentException("no id found for ort", nameof(ort));

            HttpResponseMessage responseMessage = await httpClient.GetAsync($"{baseUrl}&id={cityId}");
            responseMessage.EnsureSuccessStatusCode();

            string json = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Rootobject>(json);
        }
    }
}

