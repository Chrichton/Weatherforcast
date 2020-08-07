using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Weatherforecast.Service;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Backend.Controllers
{
    [ApiController]
    [Route("")]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherController> logger;
        private readonly IWeatherService weatherService;

        public WeatherController(ILogger<WeatherController> logger, IWeatherService weatherService)
        {
            this.logger = logger;
            this.weatherService = weatherService;
        }


        // I decided not to use the query route "forecast?city=[city], 
        // because I really like the minlength(3)-feature, 
        // which I could not get to work with a query
        [HttpGet("forecast/city/{city:minlength(3)}")]
        public IActionResult GetForecastByCity(string city)
        {
            logger.LogInformation("GetForecastByCity", city);

            // Can I do this, without waiting for the result?
            // TODO should be: 
            /*
            return weatherService.GetWeather(city).Result
                .Match(weather => Ok(weather), () => NotFound());
            */

            Option<WeatherModel> weatherOpt = weatherService.GetWeather(city).Result;
            WeatherModel weather = weatherOpt.MatchUnsafe(
                weather => weather, () => null);

            if (weather != null)
                return Ok(weather);

            return NotFound();
        }

        // I decided not to use GetForecastByZipCode,
        // because I don't want the user to be able to supply
        // invalid-zip-codes
        [HttpGet("forecast/zipcode/{zipcode:length(5)}")]
        public IActionResult GetCitiesByZipCode(int zipCode)
        {
            logger.LogInformation("GetForecastByZipCode", zipCode);

            Task<IEnumerable<string>> result = weatherService.GetCitiesForZipCode(zipCode);
            if (result.Result.Any())
                return Ok(result);

            return NotFound();
        }


        // That is the way I found to use a query-string
        /*
        [HttpGet("forecast")]
        public IActionResult ZipCode([FromQuery] int zipCode)
        {
            Task<IEnumerable<string>> result = weatherService.GetCitiesForZipCode(zipCode);
            if (result.Result.Any())
                return Ok(result);

            return NotFound();
        }
        */

        // My Test-Route for checking, if the controller is able to produce results
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            logger.LogInformation("Get");

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
