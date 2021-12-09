using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BenivoNetwork.Models
{
    public class PostModel
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserEmail { get; set; }
    }
}