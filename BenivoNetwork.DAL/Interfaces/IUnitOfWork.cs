namespace BenivoNetwork.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; set; }
        IPostRepository PostRepository { get; set; }
        IMessageRepository MessageRepository { get; set; }
        IFriendshipRepository FriendshipRepository { get; set; }

        void Commit();
    }
}
