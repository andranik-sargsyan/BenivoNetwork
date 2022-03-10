using BenivoNetwork.BLL.Extensions;
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

        public UserModel GetUser(string id)
        {
            var isInteger = int.TryParse(id, out int userId);

            var user = isInteger
                ? _unitOfWork.UserRepository.GetByID(userId)
                : _unitOfWork.UserRepository.Get(u => u.UserName == id).FirstOrDefault();

            return user.MapTo<UserModel>();
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
