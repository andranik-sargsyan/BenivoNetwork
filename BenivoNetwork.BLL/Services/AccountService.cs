using BenivoNetwork.Common.Models;
using BenivoNetwork.DAL.Interfaces;
using System;
using System.Web;
using System.Web.Security;

namespace BenivoNetwork.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Login(LoginModel model)
        {
            if (model.Password == "test")
            {
                var authTicket = new FormsAuthenticationTicket(1, model.Login, DateTime.Now, DateTime.Now.AddMinutes(10080), model.RememberMe, model.Login == "andranik" ? "Admin,User" : "User");
                var encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                HttpContext.Current.Response.Cookies.Add(authCookie);

                return true;
            }

            return false;
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}
