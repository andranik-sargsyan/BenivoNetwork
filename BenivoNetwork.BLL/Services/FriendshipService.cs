using BenivoNetwork.Common.Helpers;
using BenivoNetwork.DAL;
using BenivoNetwork.DAL.Interfaces;
using System;
using System.Linq;

namespace BenivoNetwork.BLL.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FriendshipService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(int userID)
        {
            var claimID = ClaimHelper.ID;

            if (_unitOfWork.FriendshipRepository.Any(f => f.FirstUserID == userID && f.SecondUserID == claimID ||
                                                            f.SecondUserID == userID && f.FirstUserID == claimID))
            {
                throw new Exception("The friendship already exists.");
            }

            _unitOfWork.FriendshipRepository.Insert(new Friendship
            {
                FirstUserID = claimID,
                SecondUserID = userID
            });

            _unitOfWork.Commit();
        }

        public void Accept(int userID)
        {
            var claimID = ClaimHelper.ID;

            var friendship = _unitOfWork.FriendshipRepository
                .Get(f => f.FirstUserID == userID && f.SecondUserID == claimID ||
                            f.SecondUserID == userID && f.FirstUserID == claimID)
                .FirstOrDefault();

            if (friendship == null)
            {
                throw new Exception("The friendship is not found.");
            }

            friendship.IsAccepted = true;

            _unitOfWork.FriendshipRepository.Update(friendship);
            _unitOfWork.Commit();
        }

        public void Remove(int userID)
        {
            var claimID = ClaimHelper.ID;

            var friendship = _unitOfWork.FriendshipRepository
                .Get(f => f.FirstUserID == userID && f.SecondUserID == claimID ||
                            f.SecondUserID == userID && f.FirstUserID == claimID)
                .FirstOrDefault();

            if (friendship == null)
            {
                throw new Exception("The friendship is not found.");
            }

            _unitOfWork.FriendshipRepository.Delete(friendship);
            _unitOfWork.Commit();
        }
    }
}
