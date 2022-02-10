using BenivoNetwork.Common.Models;
using System.Collections.Generic;

namespace BenivoNetwork.BLL.Services
{
    public interface IUserService
    {
        List<UserModel> GetUsers();
        List<SearchResultModel> SearchUsers(string term);
    }
}
