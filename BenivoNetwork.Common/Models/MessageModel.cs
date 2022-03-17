using System;
using System.ComponentModel.DataAnnotations;

namespace BenivoNetwork.Common.Models
{
    public class MessageModel
    {
        public int ID { get; set; }
        public int FromUserID { get; set; }
        public int ToUserID { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime DateSent { get; set; }
        public bool IsFromUser { get; set; }
    }
}
