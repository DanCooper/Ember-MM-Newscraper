﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Trakttv.TraktAPI.Model
{
    [DataContract]
    public class TraktRatingMovies
    {
        [DataMember(Name = "username")]
        public string UserName { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "movies")]
        public List<TraktRatingMovies.Movie> Movies { get; set; }

        [DataContract]
        public class Movie
        {
            [DataMember(Name = "title")]
            public string Title { get; set; }

            [DataMember(Name = "year")]
            public int Year { get; set; }

            [DataMember(Name = "imdb_id")]
            public string IMDBID { get; set; }

            [DataMember(Name = "tmdb_id")]
            public string TMDBID { get; set; }

            [DataMember(Name = "rating")]
            public int Rating { get; set; }
        }
    }
}
