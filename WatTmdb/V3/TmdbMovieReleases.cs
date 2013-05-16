using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatTmdb.V3
{
    public class ReleaseCountry
    {
        public string iso_3166_1 { get; set; }
        public string certification { get; set; }
        public string release_date { get; set; }
    }

    public class TmdbMovieReleases
    {
        public int id { get; set; }
        public List<ReleaseCountry> countries { get; set; }
    }
}
