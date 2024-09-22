using Business.Services.IServices;
using DataBase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private ILogger<CharacterController> _logger;
        private ICharacterService _characterService;
        public CharacterController(ILogger<CharacterController> logger,ICharacterService characterService)
        {
            _logger = logger;
            _characterService = characterService;
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
            CommonResponse<Character> response = await _characterService.AddCharacter(character);
            return response;
        }
        [HttpPut("UpdateCharacter")]
        public async Task<ActionResult<CommonResponse<Character>>> UpdateCharacter([FromBody] Character character)
        {
            _logger.LogInformation("UpdateCharacter api call");
            CommonResponse<Character> response = await _characterService.UpdateCharacter(character);
            return response;
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
