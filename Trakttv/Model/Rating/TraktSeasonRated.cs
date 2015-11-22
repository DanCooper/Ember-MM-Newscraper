using System.Runtime.Serialization;

namespace Trakttv.TraktAPI.Model
{
    [DataContract]
    public class TraktSeasonRated
    {
        [DataMember(Name = "rating")]
        public int Rating { get; set; }

        [DataMember(Name = "rated_at")]
        public string RatedAt { get; set; }

        [DataMember(Name = "show")]
        public TraktShow Show { get; set; }

        [DataMember(Name = "season")]
        public TraktSeason Season { get; set; }
    }
}