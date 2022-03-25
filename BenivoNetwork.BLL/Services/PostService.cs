using BenivoNetwork.BLL.Extensions;
using BenivoNetwork.Common.Helpers;
using BenivoNetwork.Common.Models;
using BenivoNetwork.DAL;
using BenivoNetwork.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BenivoNetwork.BLL.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PostModel> Get()
        {
            var claimID = ClaimHelper.ID;

            return _unitOfWork.PostRepository
                .Get(p => p.UserID == claimID, x => x.OrderByDescending(p => p.DateCreated))
                .MapTo<PostModel>();
        }

        public void Add(PostModel model)
        {
            var claimID = ClaimHelper.ID;

            var post = new Post
            {
                UserID = claimID,
                Text = model.Text,
                DateCreated = DateTime.UtcNow
            };

            _unitOfWork.PostRepository.Insert(post);
            _unitOfWork.Commit();
        }
    }
}
