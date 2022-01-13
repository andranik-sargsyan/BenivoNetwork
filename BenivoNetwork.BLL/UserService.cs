using BenivoNetwork.Common.Models;
using BenivoNetwork.DAL;
using System.Collections.Generic;
using System.Linq;

namespace BenivoNetwork.BLL
{
    public class UserService
    {
        public static List<UserModel> GetUsers()
        {
            var user = UserRepository.GetUsers();

            return user.Select(u => new UserModel
            {
                ID = u.ID,
                FirstName = u.FirstName,
                Email = u.Email
            }).ToList();
        }
    }
}
