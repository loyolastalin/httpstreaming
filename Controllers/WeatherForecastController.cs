using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace IrxjsEnumerable.Controllers
{
    [ApiController]
    [Route("api/weather")]
    public class WeatherForecastController : ControllerBase
    {

        private static readonly string[] Summaries = new[]
            {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        [HttpGet]
        //public async IAsyncEnumerable<WeatherForecast> Get([EnumeratorCancellation] CancellationToken cancellationToken)

        public async IAsyncEnumerable<WeatherForecast> Get()
        {
            HttpContext.Features.Get<IHttpResponseBodyFeature>().DisableBuffering();
            for (var index = 0; index < 5; index++)
            {
                await Task.Delay(1000);
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