using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Trakttv.TraktAPI.Model
{
    [DataContract]
    public class TraktMovieWatched
    {
        [DataMember(Name = "plays")]
        public int Plays { get; set; }

        [DataMember(Name = "last_watched_at")]
        public string LastWatchedAt { get; set; }

        [DataMember(Name = "movie")]
        public TraktMovie Movie { get; set; }
    }
}
