using Business.Services;
using Business.Services.IServices;
using DataBase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ILogger<AuthController> _logger;
        private IAuthenticationService _authenticationService;
        public AuthController(ILogger<AuthController> logger, IAuthenticationService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        [HttpPost("UserRegistration")]
        public async Task<ActionResult<CommonResponse<string>>> UserRegistration([FromBody]Login login)
        {
            _logger.LogInformation("UserRegistration api call");
            CommonResponse<string> response = await _authenticationService.RegisterUser(login);
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<CommonResponse<string>>> Login([FromBody] Login login)
        {
            _logger.LogInformation("UserRegistration api call");
            CommonResponse<string> response = await _authenticationService.Login(login);
            return Ok(response);
        }
    }
}