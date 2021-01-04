using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        [Authorize]
        public IActionResult Get()
        {
            var apiClaim = User.Claims.Where(c => c.Type == "scope" && c.Value == "api1.read").FirstOrDefault();  //Checks if the request contains the claims with scope=api1
            if (apiClaim == null)
                return BadRequest();
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        [HttpGet("[action]")]
        [Authorize]
        public IActionResult GetWrite()
        {
            var apiClaim = User.Claims.Where(c => c.Type == "scope" && c.Value == "api1.write").FirstOrDefault();  //Checks if the request contains the claims with scope=api1
            if (apiClaim == null)
                return BadRequest();
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}
