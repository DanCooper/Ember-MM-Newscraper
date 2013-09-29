using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Trakttv.TraktAPI.Model
{
    [DataContract]
    public class TraktWatchedEpisode
    {

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "year")]
        public int Year { get; set; }

        [DataMember(Name = "imdb_id")]
        public string Imdb { get; set; }

        [DataMember(Name = "tvdb_id")]
        public string SeriesId { get; set; }

        [DataMember(Name = "tvrage_id")]
        public string TvRage { get; set; }

        [DataMember(Name = "seasons")]
        public List<SeasonsWatched> Seasons { get; set; }

        [DataContract]
        public class SeasonsWatched
        {
            [DataMember(Name = "season")]
            public int Season { get; set; }

            [DataMember(Name = "episodes")]
            public List<int> Episodes { get; set; }
        }


        [DataMember(Name = "genres")]
        public List<string> Genres { get; set; }

        [DataMember(Name = "images")]
        public ShowImages Images { get; set; }

        [DataContract]
        public class ShowImages
        {
            [DataMember(Name = "fanart")]
            public string Fanart { get; set; }

            [DataMember(Name = "poster")]
            public string Poster { get; set; }

            [DataMember(Name = "banner")]
            public string Banner { get; set; }

        }


        public override string ToString()
        {
            return this.Title;
        }


        //[DataMember(Name = "episode")]
        //public TraktEpisode Episode { get; set; }

        //[DataMember(Name = "show")]
        //public TraktShow Show { get; set; }

        //[DataMember(Name = "watched")]
        //public long WatchedDate { get; set; }

        //public override string ToString()
        //{
        //    return string.Format("{0} - {1}x{2}{3}", Show.Title, Episode.Season.ToString(), Episode.Number.ToString(), string.IsNullOrEmpty(Episode.Title) ? string.Empty : " - " + Episode.Title);
        //}
    }

    [DataContract]
    public class TraktWatchedMovie
    {

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "year")]
        public string Year { get; set; }

        [DataMember(Name = "imdb_id")]
        public string IMDBID { get; set; }

        [DataMember(Name = "tmdb_id")]
        public string TMDBID { get; set; }

        [DataMember(Name = "genres")]
        public List<string> Genres { get; set; }

        [DataMember(Name = "images")]
        public MovieImages Images { get; set; }

        [DataMember(Name = "plays")]
        public int Plays { get; set; }

        [DataContract]
        public class MovieImages
        {
            [DataMember(Name = "fanart")]
            public string Fanart { get; set; }

            [DataMember(Name = "poster")]
            public string Poster { get; set; }
        }
        //Does not work right now, cause Trakt API changed!!
        //[DataMember(Name = "movie")]
        //public TraktMovie Movie { get; set; }

        //[DataMember(Name = "watched")]
        //public long WatchedDate { get; set; }

        //public override string ToString()
        //{
        //    return Movie.Title;
        //}
    }
}
