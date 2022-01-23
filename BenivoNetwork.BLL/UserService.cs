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
            var users = UserRepository.GetUsers();

            return users.Select(u => new UserModel
            {
                ID = u.ID,
                FirstName = u.FirstName,
                Email = u.Email,
                DateOfBirth = u.DateOfBirth
            }).ToList();
        }

        public static List<SearchResultModel> SearchUsers(string term)
        {
            var users = UserRepository.GetUsers();

            return users
                .Where(u => (u.FirstName + " " + u.LastName + " " + u.Email).ToLower().Contains(term.ToLower()))
                .Select(u => new SearchResultModel
                {
                    Name = u.Email
                })
                .ToList();
        }
    }
}
