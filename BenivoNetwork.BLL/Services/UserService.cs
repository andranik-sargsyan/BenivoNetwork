using BenivoNetwork.BLL.Extensions;
using BenivoNetwork.Common.Enums;
using BenivoNetwork.Common.Helpers;
using BenivoNetwork.Common.Models;
using BenivoNetwork.DAL.Interfaces;
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

        public UserModel GetUser(string id)
        {
            bool isInteger = int.TryParse(id, out int userId);

            var user = isInteger
                ? _unitOfWork.UserRepository.GetByID(userId)
                : _unitOfWork.UserRepository.Get(u => u.UserName == id).FirstOrDefault();

            return user.MapTo<UserModel>();
        }

        //TODO: fix search, it should not only search for users, so move to other service
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

        public void Update(UserModel model)
        {
            var user = _unitOfWork.UserRepository.GetByID(model.ID);

            if (user == null)
            {
                throw new Exception("User is not found.");
            }

            var claimID = ClaimHelper.ID;
            var claimRole = ClaimHelper.Role;

            if (claimRole != RoleEnum.Admin && claimID != model.ID)
            {
                throw new Exception("You are not allowed to perform this action.");
            }

            if (_unitOfWork.UserRepository.Any(u => u.ID != model.ID && (u.UserName == model.UserName || u.Email == model.UserName)))
            {
                throw new Exception("The user name must be unique and not match with someone's email address.");
            }

            if (claimRole == RoleEnum.Admin && user.Role != RoleEnum.Admin)
            {
                user.Role = model.Role;
            }

            user.UserName = model.UserName;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Gender = model.Gender;
            user.DateOfBirth = model.DateOfBirth;
            user.IsMarried = model.IsMarried;

            //TODO: perform image saving/deleting

            _unitOfWork.Commit();
        }
    }
}
