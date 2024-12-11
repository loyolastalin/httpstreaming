using httpstreaming.Data;
using httpstreaming.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace httpstreaming.Controllers
{

    [ApiController]
    [Route("api/platform")]
    public class PlatformController : Controller
    {
        private readonly ILogger<PlatformController> _logger;
        private readonly IDbContextFactory<AppDbContext> _factory;

        public PlatformController(ILogger<PlatformController> logger, IDbContextFactory<AppDbContext> factory)
        {
            _logger = logger;
            this._factory = factory;
        }

        [HttpGet]
        public async IAsyncEnumerable<Platform> Get()
        {
            await using var context = _factory.CreateDbContext();
            await foreach (var platform in context.Platforms.AsAsyncEnumerable())
            {
                await Task.Delay(300);
                yield return platform;
            }
        }
    }
}