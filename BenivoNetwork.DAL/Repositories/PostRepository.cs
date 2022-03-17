using BenivoNetwork.DAL.Interfaces;

namespace BenivoNetwork.DAL.Repositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(BenivoNetworkEntities context) : base(context)
        {
        }
    }
}
