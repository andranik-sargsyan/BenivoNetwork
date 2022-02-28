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

        public UserModel GetUser(int id)
        {
            var user = _unitOfWork.UserRepository.GetByID(id);

            //TODO: check if user exists

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
