 using Microsoft.AspNetCore.Mvc;

namespace DAFTech.DriverLicenseSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login()
        {
            return Ok(new { message = "Login endpoint working" });
        }
    }
}

