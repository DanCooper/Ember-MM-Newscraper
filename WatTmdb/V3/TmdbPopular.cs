using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatTmdb.V3
{
    public class PopularMovie : TmdbMovieBase { }

    public class TmdbPopular : TmdbSearchResultBase<PopularMovie>
    { }
}
