using BenivoNetwork.Common.Models;
using BenivoNetwork.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BenivoNetwork.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public UserModel GetUser(int id)
        {
            //TODO: check if user exists

            var user = _unitOfWork.UserRepository.GetByID(id);

            return new UserModel
            {
                ID = user.ID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                IsMarried = user.IsMarried
            };
        }

        //TODO: fix search
        public List<SearchResultModel> SearchUsers(string term)
        {
            var users = _unitOfWork.UserRepository.Get();

            return users
                .Where(u => (u.FirstName + " " + u.LastName + " " + u.Email).ToLower().Contains(term.ToLower()))
                .Select(u => new SearchResultModel
                {
                    //TODO: maybe fix Name prop
                    Name = u.Email
                })
                .ToList();
        }
    }
}
