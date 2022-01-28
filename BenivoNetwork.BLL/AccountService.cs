using BenivoNetwork.Common.Models;
using System;
using System.Web;
using System.Web.Security;

namespace BenivoNetwork.BLL
{
    public class AccountService
    {
        public static bool Login(LoginModel model)
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

        public static void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}
