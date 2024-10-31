using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace netcore_webapp_felickz.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public enum RecommendationEngineType

        {
            Account401kToIRA,
            SDIRAInvestment,
        }

        public enum RegistrationEventType
        {
            AccountCreated = 1,
            AccountCompleted = 2,
            TrustedContactAdded = 3
        }


        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;

        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {

            _logger.LogInformation($"HandleAsync called for trusted contact added event: {RegistrationEventType.TrustedContactAdded}");


            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //Add post that takes in an enum value type of weather forcast
        [HttpPost("PostWeatherForecast/{type}/{assessmentId}")]
        public IActionResult Post([FromRoute] RecommendationEngineType type, [FromRoute] int assessmentId )
        {
            _logger.LogInformation("Generating {engineType} recommendation for {assessmentId}.", type, assessmentId);

            return Ok();
        }

    }
}
