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

        // When I took the the programming task,  
        // I first thought, I would go with it and 
        // retrieve the weather by suppying city and zipcode as string

        // But while working on it, I realized, 
        // that there can be more than one city for a zipcode
        // zipcode 55767 hat 20 cities
        // I didn't like to select just the first city.

        // And after that I realized, that there are Weather informations for parts of a city.
        // But you cannot query them, because you don't know, how they are called

        // The Openmap-WeatherAPI strongly suggest to use the unique id for each location when requesting data.
        // So my main service function became:
        //
        // GetForecastById(int id)
        //
        // The user of course doesn't know these ids
        // So the remaining two service functions became:
        //
        // GetCitiesStartingWith(string cities)
        // and
        // GetCitiesForZipCode(int zipCode)
        //
        // That enables the frontend to let the user search for a city by providing a partial cityname-string.
        // The backend supplies all cities and their ids, that start with this string.
        // When the user selects one city/id, the front-end requests the weather by supplying the id
        //
        // I think, a user knows the exact zipcode.
        // The frontend calls the backend via GetCitiesForZipCode and receives all cities/ids for the zipcode.
        // When the user selects one city/id, the front-end requests the weather by supplying the id

        [HttpGet("forecast/id/{id}")]
        public async Task<IActionResult> GetForecastById(int id)
        {
            logger.LogInformation(nameof(GetForecastById), id);

            return (await weatherService
                .GetWeather(id))
                .Match<ActionResult>(weather => Ok(weather), () => NotFound());
        }

        // I decided not to implement GetForecastByZipCode(string zipCode),
        // because I don't want the user to be able to supply
        // invalid-zip-codes
        [HttpGet("forecast/zipcode/{zipcode:length(5)}")]
        public IActionResult GetCitiesForZipCode(int zipCode)
        {
            logger.LogInformation(nameof(GetCitiesForZipCode), zipCode);

            Task<IEnumerable<KeyValuePair<string, int>>> 
                result = weatherService.GetCitiesIdsForZipCode(zipCode);

            if (result.Result.Any())
                return Ok(result);

            return NotFound();
        }

        // I decided not to use the query routes, 
        // because I really like the minlength(1) and length(5) -feature, 
        // which I could not get to work with a query
        [HttpGet("forecast/cities/{cities:minlength(1)}")]
        public IActionResult GetCitiesStartingWith(string cities)
        {
            logger.LogInformation(nameof(GetCitiesStartingWith), cities);

            Task<IEnumerable<KeyValuePair<string, int>>> result = 
                weatherService.GetCitiesStartingWith(cities);
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

        // My Test-Route for checking, if the controller is able to produce results without calling any services
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
