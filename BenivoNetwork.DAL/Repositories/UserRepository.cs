using BenivoNetwork.DAL.Interfaces;

namespace BenivoNetwork.DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BenivoNetworkEntities context) : base(context)
        {
        }
    }
}
