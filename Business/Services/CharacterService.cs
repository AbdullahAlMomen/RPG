using Business.Models;
using Business.Response;
using Business.Services.IServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class CharacterService : ICharacterService
    {
        private List<Character> characters = new List<Character> {
            new Character(){Id=1,Name="Momen"},
            new Character(){Id=2,Name="Shovon"}
            };
        private ILogger<CharacterService> _logger;
        public CharacterService(ILogger<CharacterService> logger)
        {
            _logger = logger;   
        }

        public async Task<CommonResponse<Character>> AddCharacter(Character character)
        {
            CommonResponse<Character> response = new CommonResponse<Character>();
            characters.Add(character);
            response.Data = character;
            response.Success = true;
            response.Message = "Add Character Successfully";
            _logger.LogInformation(response.Message);
            return  response;
            
        }

        public async Task<CommonResponse<Character>> DeleteCharacter(int id)
        {
            CommonResponse<Character> response = new CommonResponse<Character>();
            Character character = characters.FirstOrDefault(x => x.Id == id);
            if (character != null)
            {
                characters.Remove(character);
                response.Data = character;
                response.Success = true;
                response.Message = "Character Removed";
                _logger.LogInformation(response.Message);
                return response;
            }
            response.Success = false;
            response.Message = "Found no character for this Id";
            _logger.LogError(response.Message);
            return response;
        }

        public async Task<CommonResponse<Character>> GetCharacterById(int id)
        {
            CommonResponse<Character> response = new CommonResponse<Character>();
            Character character = characters.FirstOrDefault(x=>x.Id==id);
            if (character != null)
            {
                response.Data = character;
                response.Success=true;
                response.Message = "Character found";
                _logger.LogInformation(response.Message);
                return response;
            }
            response.Success = false;
            response.Message = "Found no character for this Id";
            _logger.LogError(response.Message);
            return response;

        }

        public async Task<CommonResponse<List<Character>>> GetCharacters()
        {
            CommonResponse<List<Character>> response = new CommonResponse<List<Character>>
            {
                Data = characters,
                Success = true,
                Message = "Get all Character"

            };
            _logger.LogInformation(response.Message);
            return response;
        }

        public async Task<CommonResponse<Character>> UpdateCharacter(Character updatedcharacter)
        {
            CommonResponse<Character> response = new CommonResponse<Character>();
            Character character = characters.FirstOrDefault(x => x.Id == updatedcharacter.Id);
            if (character != null)
            {
                character.Strength = updatedcharacter.Strength;
                character.Defence = updatedcharacter.Defence;
                character.Class = updatedcharacter.Class;
                character.Name = updatedcharacter.Name;
                character.HitPoints= updatedcharacter.HitPoints;
                response.Data = character;
                response.Success = true;
                response.Message = "Character updated";
                _logger.LogInformation(response.Message);
                return response;
            }
            response.Success = false;
            response.Message = "Found no character to update";
            _logger.LogError(response.Message);
            return response;
        }
    }
}
