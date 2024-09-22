
using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.IServices
{
    public interface ICharacterService
    {
        Task<CommonResponse<List<Character>>> GetCharacters();
        Task<CommonResponse<Character>> GetCharacterById(int id);
        Task<CommonResponse<Character>> AddCharacter(Character character);
        Task<CommonResponse<Character>> UpdateCharacter(Character character);
        Task<CommonResponse<Character>> DeleteCharacter(int id);
    }
}
