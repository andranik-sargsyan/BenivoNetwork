using System.Collections.Generic;
using System.Linq;

namespace BenivoNetwork.DAL
{
    public class UserRepository
    {
        public static List<User> GetUsers()
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
