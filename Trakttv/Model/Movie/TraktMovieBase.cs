using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Trakttv.TraktAPI.Model
{
    [DataContract]
    public class TraktMovieBase : TraktId
    {
        [DataMember(Name = "imdb")]
        public string Imdb { get; set; }

        [DataMember(Name = "tmdb")]
        public int? Tmdb { get; set; }
    }
}
