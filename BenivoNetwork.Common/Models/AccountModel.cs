﻿using BenivoNetwork.Common.Enums;

namespace BenivoNetwork.Common.Models
{
    public class AccountModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ImageURL { get; set; }
        public RoleEnum Role { get; set; }
    }
}
