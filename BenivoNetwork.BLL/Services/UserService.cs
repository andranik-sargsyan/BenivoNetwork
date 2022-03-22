using BenivoNetwork.BLL.Extensions;
using BenivoNetwork.Common.Enums;
using BenivoNetwork.Common.Helpers;
using BenivoNetwork.Common.Models;
using BenivoNetwork.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

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
            var claimID = ClaimHelper.ID;

            bool isInteger = int.TryParse(id, out int userId);

            var user = isInteger
                ? _unitOfWork.UserRepository.GetByID(userId)
                : _unitOfWork.UserRepository.Get(u => u.UserName == id).FirstOrDefault();

            var userModel = user.MapTo<UserModel>();

            if (user != null && user.ID != claimID)
            {
                if (user.SentFriendships.Any(f => f.SecondUserID == claimID && f.IsAccepted) ||
                    user.ReceivedFriendships.Any(f => f.FirstUserID == claimID && f.IsAccepted))
                {
                    userModel.FriendStatus = FriendStatusEnum.Friends;
                }
                else if (!user.SentFriendships.Any(f => f.SecondUserID == claimID) &&
                        !user.ReceivedFriendships.Any(f => f.FirstUserID == claimID))
                {
                    userModel.FriendStatus = FriendStatusEnum.NotFriends;
                }
                else if (user.SentFriendships.Any(f => f.SecondUserID == claimID && !f.IsAccepted))
                {
                    userModel.FriendStatus = FriendStatusEnum.RequestReceived;
                }
                else if (user.ReceivedFriendships.Any(f => f.FirstUserID == claimID && !f.IsAccepted))
                {
                    userModel.FriendStatus = FriendStatusEnum.RequestSent;
                }
            }

            return userModel;
        }

        public IEnumerable<UserModel> GetFriendUsers()
        {
            var claimID = ClaimHelper.ID;

            var users = _unitOfWork.UserRepository.Get(u => (u.SentFriendships.Any(f => f.SecondUserID == claimID && f.IsAccepted) ||
                                                            u.ReceivedFriendships.Any(f => f.FirstUserID == claimID && f.IsAccepted)) &&
                                                            u.ID != claimID);

            return users.MapTo<UserModel>();
        }

        public IEnumerable<UserModel> GetOtherUsers()
        {
            var claimID = ClaimHelper.ID;

            var users = _unitOfWork.UserRepository.Get(u => !u.SentFriendships.Any(f => f.SecondUserID == claimID && f.IsAccepted) &&
                                                            !u.ReceivedFriendships.Any(f => f.FirstUserID == claimID && f.IsAccepted) &&
                                                            u.ID != claimID);

            return users.MapTo<UserModel>();
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
            user.ImageURL = model.ImageURL;

            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Commit();
        }

        public void UpdateImage(HttpPostedFile imageFile, int userID)
        {
            var user = _unitOfWork.UserRepository.GetByID(userID);

            if (user == null)
            {
                throw new Exception("User is not found.");
            }

            var claimID = ClaimHelper.ID;
            var claimRole = ClaimHelper.Role;

            if (claimRole != RoleEnum.Admin && claimID != userID)
            {
                throw new Exception("You are not allowed to perform this action.");
            }

            //TODO: fix (bring from settings)
            var directory = HostingEnvironment.MapPath("~/Content/Images/Uploads");
            var extension = Path.GetExtension(imageFile.FileName);
            var fileName = $"profile-image-{userID}{extension}";
            var path = Path.Combine(directory, fileName);

            File.Delete(path);
            imageFile?.SaveAs(path);

            user.ImageURL = $"/Content/Images/Uploads/{fileName}";

            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Commit();
        }

        public void RemoveImage(int userID)
        {
            var user = _unitOfWork.UserRepository.GetByID(userID);

            if (user == null)
            {
                throw new Exception("User is not found.");
            }

            var claimID = ClaimHelper.ID;
            var claimRole = ClaimHelper.Role;

            if (claimRole != RoleEnum.Admin && claimID != userID)
            {
                throw new Exception("You are not allowed to perform this action.");
            }

            user.ImageURL = null;

            //TODO: fix (bring from settings)
            var directory = HostingEnvironment.MapPath("~/Content/Images/Uploads");
            var paths = Directory.GetFiles(directory, $"profile-image-{userID}.*");
            foreach (var path in paths)
            {
                File.Delete(path);
            }

            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Commit();
        }
    }
}
