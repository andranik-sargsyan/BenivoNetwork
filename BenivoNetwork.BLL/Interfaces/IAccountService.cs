using BenivoNetwork.Common.Models;

namespace BenivoNetwork.BLL.Services
{
    public interface IAccountService
    {
        bool Login(LoginModel model);
        void Logout();
        RegisterResultModel Register(RegisterModel model);
    }
}
