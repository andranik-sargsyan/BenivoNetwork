using BenivoNetwork.Common.Models;

namespace BenivoNetwork.BLL.Services
{
    public interface IAccountService
    {
        LoginResultModel Login(LoginModel model);
        void Logout();
        RegisterResultModel Register(RegisterModel model);
    }
}
