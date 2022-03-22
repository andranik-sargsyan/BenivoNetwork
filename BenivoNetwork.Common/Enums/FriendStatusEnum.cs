using System.ComponentModel;

namespace BenivoNetwork.Common.Enums
{
    public enum FriendStatusEnum
    {
        [Description("Add Friend")]
        NotFriends = 0,
        [Description("Cancel Request")]
        RequestSent = 1,
        [Description("Accept Request")]
        RequestReceived = 2,
        [Description("Remove Friend")]
        Friends = 3
    }
}
