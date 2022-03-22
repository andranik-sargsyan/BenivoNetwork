namespace BenivoNetwork.BLL.Services
{
    public interface IFriendshipService
    {
        void Add(int userID);
        void Accept(int userID);
        void Remove(int userID);
    }
}
