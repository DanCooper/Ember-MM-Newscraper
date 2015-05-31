using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Trakttv.TraktAPI.Model
{
    public class TraktShowWatchedProgress
    {
        public string ShowTitle { get; set; }
        public string ShowID { get; set; }
        public int EpisodesAired { get; set; }
        public int EpisodesWatched { get; set; }
        public int EpisodePlaycount { get; set; }
        public string LastWatchedEpisode { get; set; }
      
    }
}
