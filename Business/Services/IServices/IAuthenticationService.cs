using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.IServices
{
    public interface IAuthenticationService
    {
        Task<CommonResponse<string>>RegisterUser(Login loginUser);
        Task<CommonResponse<string>> Login(Login loginUser);
        Task<CommonResponse<User>>GetUserByUserName(string userName);
    }
}
