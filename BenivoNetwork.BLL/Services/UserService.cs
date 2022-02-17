using BenivoNetwork.Common.Models;
using BenivoNetwork.DAL;
using BenivoNetwork.DAL.Interfaces;
using BenivoNetwork.DAL.Repositories;
using System;
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

        public List<UserModel> GetUsers()
        {
            var users = _unitOfWork.UserRepository.GetUsers();

            return users.Select(u => new UserModel
            {
                ID = u.ID,
                FirstName = u.FirstName,
                Email = u.Email,
                DateOfBirth = u.DateOfBirth,
                Gender = u.Gender
            }).ToList();
        }

        public List<SearchResultModel> SearchUsers(string term)
        {
            var users = _unitOfWork.UserRepository.Get();

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
