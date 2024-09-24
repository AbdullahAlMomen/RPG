using Business.Services.IServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.UnitOfWorks;
using DataBase;
using DataBase.Models;
using DataBase.Models.DTO;


namespace Business.Services
{
    public class CharacterService : ICharacterService
    {
        private ILogger<CharacterService> _logger;
        private IUnitOfWork _unitOfWork;
        public CharacterService(ILogger<CharacterService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;  
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResponse<CharacterDTO>> AddCharacter(Character character)
        {
            CommonResponse<CharacterDTO> response = new CommonResponse<CharacterDTO>();
            try
            {
                await _unitOfWork.CharacterRepository.Add(character);
                _unitOfWork.Save();
                response.Data = new CharacterDTO()
                {
                    Name= character.Name,
                    HitPoints = character.HitPoints,
                    Strength = character.Strength,
                    Defence = character.Defence,
                    Class = character.Class,
                };
                response.Success = true;
                response.Message = "Add Character Successfully";
                _logger.LogInformation(response.Message);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                throw new Exception(ex.Message);
            }

        }

        public async Task<CommonResponse<CharacterDTO>> DeleteCharacter(int id)
        {
            CommonResponse<CharacterDTO> response = new CommonResponse<CharacterDTO>();
            try
            {
                Character character = await _unitOfWork.CharacterRepository.GetById(id);
                if (character != null)
                {
                    await _unitOfWork.CharacterRepository.Delete(character);
                    _unitOfWork.Save();
                    response.Data = new CharacterDTO()
                    {
                        Name = character.Name,
                        HitPoints = character.HitPoints,
                        Strength = character.Strength,
                        Defence = character.Defence,
                        Class = character.Class,
                    };
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
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                throw new Exception(ex.Message);
            }
          
        }

        public async Task<CommonResponse<CharacterDTO>> GetCharacterById(int id)
        {
            CommonResponse<CharacterDTO> response = new CommonResponse<CharacterDTO>();
            try
            {
                Character character = await _unitOfWork.CharacterRepository.GetById(id);
                if (character != null)
                {
                    response.Data = new CharacterDTO() { Name = character.Name, HitPoints = character.HitPoints, Strength = character.Strength, Defence = character.Defence, Class = character.Class };
                    response.Success = true;
                    response.Message = "Character found";
                    _logger.LogInformation(response.Message);
                    return response;
                }
                response.Success = false;
                response.Message = "Found no character for this Id";
                _logger.LogError(response.Message);
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                throw new Exception(ex.Message);
            }
           

        }

        public async Task<CommonResponse<List<CharacterDTO>>> GetCharacters()
        {
            try
            {
                CommonResponse<List<CharacterDTO>> response = new CommonResponse<List<CharacterDTO>>();

                List<CharacterDTO> characterDTOs = new List<CharacterDTO>();
                var characters= (List<Character>?)await _unitOfWork.CharacterRepository.GetAll();
                foreach(var character in characters)
                {
                    CharacterDTO characterDTO = new CharacterDTO()
                    {
                        Name = character.Name,
                        HitPoints = character.HitPoints,
                        Strength = character.Strength,
                        Defence = character.Defence,
                        Class = character.Class,

                    };
                    characterDTOs.Add(characterDTO);
                }
                response.Data = characterDTOs;
                if(response.Data.Count > 0)
                {
                   response.Success = true;
                   response.Message = "Get all Character";
                   _logger.LogInformation(response.Message);
                }
                else
                {
                    response.Success = false;
                    response.Message = "No Characters found";
                    _logger.LogInformation(response.Message);
                }
             
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                throw new Exception(ex.Message);
            }
           
        }

        public async Task<CommonResponse<CharacterDTO>> UpdateCharacter(Character updatedcharacter)
        {
            CommonResponse<CharacterDTO> response = new CommonResponse<CharacterDTO>();
            try
            {
                Character character = await _unitOfWork.CharacterRepository.GetById(updatedcharacter.Id);
                if (character != null)
                {
                    character.Strength = updatedcharacter.Strength;
                    character.Defence = updatedcharacter.Defence;
                    character.Class = updatedcharacter.Class;
                    character.Name = updatedcharacter.Name;
                    character.HitPoints = updatedcharacter.HitPoints;
                   await _unitOfWork.CharacterRepository.Update(character);
                    _unitOfWork.Save();

                    response.Data = new CharacterDTO() { Name = character.Name, HitPoints = character.HitPoints, Strength = character.Strength, Defence = character.Defence, Class = character.Class };
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
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                throw new Exception(ex.Message);
            }
           
        }
    }
}
