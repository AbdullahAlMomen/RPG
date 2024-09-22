using DataBase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public ICharacterRepository CharacterRepository { get; }
        public IAuthenticationRepository AuthenticationRepository { get; }



        public UnitOfWork(ApplicationDbContext dbContext,
                           ICharacterRepository characterRepository,
                           IAuthenticationRepository authenticationRepository)
        {
            _dbContext = dbContext;
            CharacterRepository = characterRepository;
            AuthenticationRepository = authenticationRepository;
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

    }
}
