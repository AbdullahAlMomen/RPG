using Business.Services.IServices;
using DataBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CharacterController : ControllerBase
    {
        private ILogger<CharacterController> _logger;
        private ICharacterService _characterService;
        private IAuthenticationService _authenticationService;
        public CharacterController(ILogger<CharacterController> logger,ICharacterService characterService, IAuthenticationService authenticationService)
        {
            _logger = logger;
            _characterService = characterService;
            _authenticationService = authenticationService;
        }

        [HttpGet("GetCharacters")]
        public async Task<ActionResult<CommonResponse<List<Character>>>> GetCharacters()
        {
            _logger.LogInformation("GetCharacters api call");
            CommonResponse<List<Character>> response = await _characterService.GetCharacters();
            return Ok(response) ;
        }

        [HttpGet("GetCharacterById")]
        public async Task<ActionResult<CommonResponse<Character>>> GetCharacterById([FromQuery]int Id)
        {
            _logger.LogInformation("GetCharacterById api call");
            CommonResponse<Character> response = await _characterService.GetCharacterById(Id);
            return Ok(response);
        }

        [HttpPost("AddCharacter")]
        public async Task<ActionResult<CommonResponse<Character>>> AddCharacter([FromBody] Character character)
        {
            _logger.LogInformation("AddCharacter api call");
            var username = User.Claims.FirstOrDefault(x => x.Type == "UserName")?.Value;
            var user = await _authenticationService.GetUserByUserName(username);
            character.User = user.Data;
            CommonResponse<Character> response = await _characterService.AddCharacter(character);
            return Ok(response);
        }
        [HttpPut("UpdateCharacter")]
        public async Task<ActionResult<CommonResponse<Character>>> UpdateCharacter([FromBody] Character character)
        {
            _logger.LogInformation("UpdateCharacter api call");
            CommonResponse<Character> response = await _characterService.UpdateCharacter(character);
            return Ok(response);
        }

        [HttpDelete("DeleteCharacter")]
        public async Task<ActionResult<CommonResponse<Character>>> DeleteCharacter([FromQuery] int Id)
        {
            _logger.LogInformation("DeleteCharacter api call");
            CommonResponse<Character> response = await _characterService.DeleteCharacter(Id);
            return Ok(response);
        }

    }
}
