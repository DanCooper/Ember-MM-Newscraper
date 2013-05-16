using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatTmdb.V3
{
    public class SimilarMovie : TmdbMovieBase { }

    public class TmdbSimilarMovies : TmdbSearchResultBase<SimilarMovie>
    { }
}
