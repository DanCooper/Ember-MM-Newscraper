using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Trakttv.TraktAPI.Model
{
    [DataContract]
    public class TraktSyncResponse : TraktResponse
    {
        [DataMember(Name = "inserted")]
        public string Inserted { get; set; }

        [DataMember(Name = "already_exist")]
        public string AlreadyExist { get; set; }

        [DataMember(Name = "skipped")]
        public string Skipped { get; set; }

        [DataMember(Name = "skipped_movies")]
        public List<TraktMovieSync.Movie> SkippedMovies { get; set; }

        [DataMember(Name = "already_exist_movies")]
        public List<TraktMovieSync.Movie> AlreadyExistMovies { get; set; }
    }

    /// <summary>
    /// Data structure for Syncing to Trakt
    /// </summary>
    [DataContract]
    public class TraktMovieSync
    {
        [DataMember(Name = "username")]
        public string UserName { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "movies")]
        public List<Movie> MovieList { get; set; }

        [DataContract]
        public class Movie : TraktMovieBase, IEquatable<Movie>
        {
            #region IEquatable
            public bool Equals(Movie other)
            {
                bool result = false;
                if (other != null)
                {
                    if (this.Title.Equals(other.Title) && this.Year.Equals(other.Year) && this.IMDBID.Equals(other.IMDBID))
                    {
                        result = true;
                    }
                }
                return result;
            }
            #endregion
        }
    }
}
