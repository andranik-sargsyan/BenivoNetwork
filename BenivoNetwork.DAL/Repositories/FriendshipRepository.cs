using BenivoNetwork.DAL.Interfaces;

namespace BenivoNetwork.DAL.Repositories
{
    public class FriendshipRepository : Repository<Friendship>, IFriendshipRepository
    {
        public FriendshipRepository(BenivoNetworkEntities context) : base(context)
        {
        }
    }
}
