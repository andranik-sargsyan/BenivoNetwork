using BenivoNetwork.DAL.Interfaces;

namespace BenivoNetwork.DAL.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(BenivoNetworkEntities context) : base(context)
        {
        }
    }
}
