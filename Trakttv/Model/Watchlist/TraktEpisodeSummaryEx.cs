using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Trakttv.TraktAPI.Model
{
    [DataContract]
    public class TraktEpisodeSummaryEx
    {
        [DataMember(Name = "episode")]
        public TraktEpisodeSummary Episode { get; set; }

        [DataMember(Name = "show")]
        public TraktShowSummary Show { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}x{2} - {3}", this.Show.Title, Episode.Season, Episode.Number, Episode.Title ?? "TBA");
        }
    }
}
