using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Device.API.Controllers.Health
{
    [ApiController]
    [Route("v1/[controller]")]
    [Produces("application/json")]
    public class HealthController(HealthCheckService healthCheck) : Controller
    {
        private readonly HealthCheckService _healthCheck = healthCheck;

        [HttpGet()]
        public async Task<IActionResult> Get() => Ok(await _healthCheck.CheckHealthAsync());
    }
}
