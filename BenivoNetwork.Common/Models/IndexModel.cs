using System.Collections.Generic;

namespace BenivoNetwork.Common.Models
{
    public class IndexModel
    {
        public IEnumerable<UserModel> FriendUsers { get; set; } = new List<UserModel>();
        public IEnumerable<UserModel> OtherUsers { get; set; } = new List<UserModel>();
    }
}
