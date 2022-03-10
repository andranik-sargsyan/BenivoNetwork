using BenivoNetwork.BLL.Extensions;
using BenivoNetwork.Common.Enums;
using BenivoNetwork.Common.Models;
using BenivoNetwork.DAL;
using BenivoNetwork.DAL.Interfaces;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace BenivoNetwork.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHashingService _hashingService;

        public AccountService(IUnitOfWork unitOfWork,
            IHashingService hashingService)
        {
            _unitOfWork = unitOfWork;
            _hashingService = hashingService;
        }

        public LoginResultModel Login(LoginModel model)
        {
            var user = _unitOfWork.UserRepository
                .Get(u => u.UserName == model.Login || u.Email == model.Login)
                .FirstOrDefault();

            if (user == null)
            {
                return new LoginResultModel
                {
                    IsSuccessful = false,
                    Message = "User is not found."
                };
            }

            string passwordHash = _hashingService.GetHash($"{model.Password}{user.Salt}");
            if (passwordHash != user.Password)
            {
                return new LoginResultModel
                {
                    IsSuccessful = false,
                    Message = "Password is not correct."
                };
            }

            if (!user.IsActive)
            {
                return new LoginResultModel
                {
                    IsSuccessful = false,
                    Message = "User is not active."
                };
            }

            var userData = JsonConvert.SerializeObject(user.MapTo<AccountModel>());
            var authTicket = new FormsAuthenticationTicket(1, user.UserName, DateTime.Now, DateTime.Now.AddMinutes(10080), model.RememberMe, userData);
            var encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            HttpContext.Current.Response.Cookies.Add(authCookie);

            return new LoginResultModel
            {
                IsSuccessful = true
            };
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }

        public RegisterResultModel Register(RegisterModel model)
        {
            if (_unitOfWork.UserRepository.Any(u => u.Email == model.Email))
            {
                return new RegisterResultModel
                {
                    IsSuccessful = false,
                    Message = "User is already registered."
                };
            }

            var random = new Random();
            var salt = random.Next(10000, 100000).ToString();
            string passwordHash = _hashingService.GetHash($"{model.Password}{salt}");

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                IsActive = true,
                Email = model.Email,
                UserName = model.Email,
                Password = passwordHash,
                Salt = salt,
                Role = RoleEnum.User
            };

            _unitOfWork.UserRepository.Insert(user);

            _unitOfWork.Commit();

            return new RegisterResultModel
            {
                IsSuccessful = true,
                ID = user.ID
            };
        }
    }
}
