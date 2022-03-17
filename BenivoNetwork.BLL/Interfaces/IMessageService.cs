using BenivoNetwork.Common.Models;
using System.Collections.Generic;

namespace BenivoNetwork.BLL.Services
{
    public interface IMessageService
    {
        IEnumerable<ConversationModel> GetConversations(int? userID);
        IEnumerable<MessageModel> GetByUserID(int id);
        void Add(MessageModel model);
        bool HasNewMessages(int fromUserID, int lastMessageID);
    }
}
