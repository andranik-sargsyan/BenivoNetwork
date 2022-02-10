using System.Collections.Generic;

namespace BenivoNetwork.DAL.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        List<User> GetUsers();
    }
}
