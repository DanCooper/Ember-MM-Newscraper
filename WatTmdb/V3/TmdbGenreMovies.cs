using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatTmdb.V3
{
    public class GenreMovie : TmdbMovieBase { }

    public class TmdbGenreMovies : TmdbSearchResultBase<GenreMovie>
    {
        public int id { get; set; }
    }
}
