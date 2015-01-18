using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Trakttv.TraktAPI.Model
{
    [DataContract]
    public class TraktSyncMoviesWatched
    {
        [DataMember(Name = "movies")]
        public List<TraktSyncMovieWatched> Movies { get; set; }
    }
}
