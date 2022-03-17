namespace BenivoNetwork.Common.Models
{
    public class ConversationModel
    {
        public UserModel User { get; set; }
        public MessageModel LastMessage { get; set; }
        public bool IsSelected { get; set; }
    }
}
