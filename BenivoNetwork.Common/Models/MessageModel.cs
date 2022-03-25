using System;

namespace BenivoNetwork.Common.Models
{
    public class MessageModel : TextFormModel
    {
        public int FromUserID { get; set; }
        public int ToUserID { get; set; }
        public DateTime DateSent { get; set; }
        public bool IsFromUser { get; set; }
    }
}
