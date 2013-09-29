﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Trakttv.TraktAPI.Model
{
    [DataContract]
    public class TraktRatingEpisodes
    {
        [DataMember(Name = "username")]
        public string UserName { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "episodes")]
        public List<TraktRatingEpisodes.Episode> Episodes { get; set; }

        [DataContract]
        public class Episode
        {
            [DataMember(Name = "title")]
            public string Title { get; set; }

            [DataMember(Name = "year")]
            public int Year { get; set; }

            [DataMember(Name = "tvdb_id")]
            public string TVDBID { get; set; }

            [DataMember(Name = "imdb_id")]
            public string IMDBID { get; set; }

            [DataMember(Name = "season")]
            public int SeasonNumber { get; set; }

            [DataMember(Name = "episode")]
            public int EpisodeNumber { get; set; }

            [DataMember(Name = "rating")]
            public int Rating { get; set; }
        }
    }
}
