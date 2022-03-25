using BenivoNetwork.Common.Models;
using System.Collections.Generic;

namespace BenivoNetwork.BLL.Services
{
    public interface IPostService
    {
        IEnumerable<PostModel> Get();
        void Add(PostModel model);
    }
}
