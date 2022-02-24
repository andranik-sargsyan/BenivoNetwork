using BenivoNetwork.Common.Models;
using System.Collections.Generic;

namespace BenivoNetwork.BLL.Services
{
    public interface IUserService
    {
        UserModel GetUser(int id);
        List<SearchResultModel> SearchUsers(string term);
    }
}
