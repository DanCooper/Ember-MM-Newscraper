using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Trakttv.TraktAPI.Model
{
    [DataContract]
    public class TraktSyncMovies
    {
        [DataMember(Name = "movies")]
        public List<TraktMovie> Movies { get; set; }
    }
}
