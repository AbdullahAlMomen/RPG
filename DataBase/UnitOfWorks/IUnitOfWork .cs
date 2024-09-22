using DataBase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        ICharacterRepository CharacterRepository { get; }
        IAuthenticationRepository AuthenticationRepository { get; }

        int Save();
    }
}
