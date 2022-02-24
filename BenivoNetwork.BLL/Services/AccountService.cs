using BenivoNetwork.Common.Enums;
using BenivoNetwork.Common.Models;
using BenivoNetwork.DAL;
using BenivoNetwork.DAL.Interfaces;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

        public bool Login(LoginModel model)
        {
            //TODO: login with email

            var user = _unitOfWork.UserRepository
                .Get(u => u.UserName == model.Login)
                .FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            string passwordHash = _hashingService.GetHash($"{model.Password}{user.Salt}");
            if (passwordHash != user.Password)
            {
                return false;
            }

            var authTicket = new FormsAuthenticationTicket(1, user.UserName, DateTime.Now, DateTime.Now.AddMinutes(10080), model.RememberMe, model.Login == "andranik" ? "Admin,User" : "User");
            var encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            HttpContext.Current.Response.Cookies.Add(authCookie);

            return true;
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }

        public RegisterResultModel Register(RegisterModel model)
        {
            var random = new Random();
            var salt = random.Next(10000, 100000).ToString();

            //TODO: check if user already exists

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
