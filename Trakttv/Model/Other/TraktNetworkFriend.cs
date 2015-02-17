using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Trakttv.TraktAPI.Model
{
    [DataContract]
    public class TraktNetworkFriend
    {
        [DataMember(Name = "friends_at")]
        public string FriendsAt { get; set; }

        [DataMember(Name = "user")]
        public TraktUserSummary User { get; set; }
    }
}
