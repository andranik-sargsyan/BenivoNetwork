using BenivoNetwork.Common.Enums;
using System;

namespace BenivoNetwork.Common.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public RoleEnum Role { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderEnum? Gender { get; set; }
        public bool? IsMarried { get; set; }
    }
}
