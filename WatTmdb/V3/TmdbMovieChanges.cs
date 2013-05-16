using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatTmdb.V3
{
    public class MovieChangeItem
    {
        public string id { get; set; }
        public string action { get; set; }
        public string time { get; set; }
        public string value { get; set; }
        public string iso_639_1 { get; set; }
        public string original_value { get; set; }
    }

    public class MovieChange
    {
        public string key { get; set; }
        public List<MovieChangeItem> items { get; set; }
    }

    public class TmdbMovieChanges
    {
        public List<MovieChange> changes { get; set; }
    }
}
