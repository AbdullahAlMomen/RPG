
using DataBase.Models;
using DataBase.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.IServices
{
    public interface ICharacterService
    {
        Task<CommonResponse<List<CharacterDTO>>> GetCharacters();
        Task<CommonResponse<CharacterDTO>> GetCharacterById(int id);
        Task<CommonResponse<CharacterDTO>> AddCharacter(Character character);
        Task<CommonResponse<CharacterDTO>> UpdateCharacter(Character character);
        Task<CommonResponse<CharacterDTO>> DeleteCharacter(int id);
    }
}
