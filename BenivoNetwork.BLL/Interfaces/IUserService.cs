using BenivoNetwork.Common.Models;
using System.Collections.Generic;

namespace BenivoNetwork.BLL.Services
{
    public interface IUserService
    {
        UserModel GetUser(string id);
        List<SearchResultModel> SearchUsers(string term);
        void Update(UserModel model);
    }
}
