using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace secret_friend_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment environment;

        public TestController(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.configuration = configuration;
            this.environment = environment;
        }

        [HttpGet("status")]
        [AllowAnonymous]
        public IActionResult GetStatus()
        {
            return Ok("Online");
        }

        [HttpGet("environment")]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return Ok(new
            {
                Environment = environment.EnvironmentName,
                Audience = configuration["TokenConfiguration:Audience"]
            });
        }
    }
}
