using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository
{
    public class AuthenticationRepository : RepositoryBase<User>, IAuthenticationRepository
    {
        public AuthenticationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        
    }
}
