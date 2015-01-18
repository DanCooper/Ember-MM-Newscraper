using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TraktPlugin.TraktAPI.DataStructures
{
    [DataContract]
    public class TraktSyncShowRated : TraktShow
    {
        [DataMember(Name = "rated_at")]
        public string RatedAt { get; set; }

        [DataMember(Name = "rating")]
        public int Rating { get; set; }
    }
}
