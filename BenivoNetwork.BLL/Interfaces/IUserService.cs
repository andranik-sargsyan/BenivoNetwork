using BenivoNetwork.Common.Models;
using System.Collections.Generic;
using System.Web;

namespace BenivoNetwork.BLL.Services
{
    public interface IUserService
    {
        UserModel GetUser(string id);
        IEnumerable<UserModel> GetFriendUsers();
        IEnumerable<UserModel> GetOtherUsers();
        List<SearchResultModel> SearchUsers(string term);
        void Update(UserModel model);
        void UpdateImage(HttpPostedFile imageFile, int userID);
        void RemoveImage(int userID);
    }
}
