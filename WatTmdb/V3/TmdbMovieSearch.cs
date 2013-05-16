using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatTmdb.V3
{
    public class MovieResult : TmdbMovieBase
    {
        public bool adult { get; set; }
        public double popularity { get; set; }
    }

    public class TmdbMovieSearch : TmdbSearchResultBase<MovieResult>
    { }
}
