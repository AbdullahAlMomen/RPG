using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository
{
    public class CharacterRepository:RepositoryBase<Character>,ICharacterRepository
    {
        public CharacterRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
