using Business.Services.IServices;
using DataBase.Models;
using DataBase.Repository;
using DataBase.UnitOfWorks;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private ILogger<CharacterService> _logger;
        private IUnitOfWork _unitOfWork;
        public AuthenticationService(ILogger<CharacterService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<CommonResponse<string>> RegisterUser(Login loginUser)
        {
            var existUser = await _unitOfWork.AuthenticationRepository.Find(x => x.UserName == loginUser.UserName);
            CommonResponse<string> response = new CommonResponse<string>();
            if (existUser == null)
            {
                try
                {
                    User user = new User();

                    CreatePasswordHash(loginUser.Password, out byte[] passwordHash, out byte[] passwordSalt);
                    user.UserName = loginUser.UserName;
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    await _unitOfWork.AuthenticationRepository.Add(user);
                    _unitOfWork.Save();
                    response.Success = true;
                    response.Data = user.UserName;
                    response.Message = "User Created Successfully";
                    _logger.LogInformation("User Registration Done");
                    return response;
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex.Message);
                    throw new Exception(ex.Message, ex);
                }

            }
            response.Success = false;
            response.Data = null;
            response.Message = "User already exist";
            return response;
        }

        public async Task<CommonResponse<string>> Login(Login loginUser)
        {
            var existUser = await _unitOfWork.AuthenticationRepository.Find(x => x.UserName == loginUser.UserName);
            CommonResponse<string> response = new CommonResponse<string>();
            if (existUser == null)
            {
                response.Success = false;
                response.Data = null;
                response.Message = "No User Found for this Username";
                return response;
            }
            if(VerifyPasswordHash(loginUser.Password,existUser.PasswordHash,existUser.PasswordSalt))
            {
                response.Success = true;
                response.Data = null;
                response.Message = "Login Succesfull";
                return response;
            }
            response.Success = false;
            response.Data = null;
            response.Message = "Password don't match";
            return response;
        }

        private void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            using(var hmc =new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmc.Key;
                passwordHash = hmc.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password,  byte[] passwordHash,  byte[] passwordSalt)
        {
            using (var hmc = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmc.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }
    }
}
