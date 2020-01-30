using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using QPublisher.Services;
using System.Linq;

namespace QPublisher.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationService _authService;

        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpGet("authenticate")]
        //[HttpGet]
        public IActionResult Authenticate(string clientId, string clientSecret)
        {
            var token = _authService.Authenticate(clientId, clientSecret);

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "ClientId and/or Client Secret is incorrect" });

            return Ok(token);
        }
    }
}