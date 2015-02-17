using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Trakttv.TraktAPI.Model
{
    [DataContract]
    public class TraktUserSummary : TraktUser
    {
        [DataMember(Name = "joined_at")]
        public string JoinedAt { get; set; }

        [DataMember(Name = "location")]
        public string Location { get; set; }

        [DataMember(Name = "about")]
        public string About { get; set; }

        [DataMember(Name = "gender")]
        public string Gender { get; set; }

        [DataMember(Name = "age")]
        public int? Age { get; set; }

        [DataMember(Name = "images")]
        public TraktUserImages Images { get; set; }
    }
}
