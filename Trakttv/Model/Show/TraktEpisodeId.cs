using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Trakttv.TraktAPI.Model
{
    [DataContract]
    public class TraktEpisodeId : TraktId
    {
        [DataMember(Name = "imdb")]
        public string Imdb { get; set; }

        [DataMember(Name = "tmdb")]
        public int? Tmdb { get; set; }

        [DataMember(Name = "tvdb")]
        public int? Tvdb { get; set; }

        [DataMember(Name = "tvrage")]
        public int? TvRage { get; set; }
    }
}
