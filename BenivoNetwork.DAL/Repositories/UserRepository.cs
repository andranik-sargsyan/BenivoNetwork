using BenivoNetwork.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BenivoNetwork.DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BenivoNetworkEntities context) : base(context)
        {
        }

        public void Create(User ob)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<User> GetUsers()
        {
            List<User> users;

            using (var context = new BenivoNetworkEntities())
            {
                users = context.Users.AsNoTracking().ToList();
            }

            return users;
        }
    }
}
