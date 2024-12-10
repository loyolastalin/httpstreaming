using httpstreaming.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System.Runtime.CompilerServices;

namespace IrxjsEnumerable.Controllers
{
    [ApiController]
    [Route("api/weather")]
    [Authorize]
    public class WeatherForecastController : ControllerBase
    {
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            this._logger = logger;
        }

        private static readonly string[] Summaries = new[]
            {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        private readonly ILogger<WeatherForecastController> _logger;

        [HttpGet]
        //public async IAsyncEnumerable<WeatherForecast> Get([EnumeratorCancellation] CancellationToken cancellationToken)
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes:Read")]
        public async IAsyncEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("API is called");
            HttpContext.Features.Get<IHttpResponseBodyFeature>().DisableBuffering();
            for (var index = 0; index < 5; index++)
            {
                await Task.Delay(1);
                //System.Console.WriteLine("yield callsed");

                yield return new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]

                };
            }
        }
    }
}