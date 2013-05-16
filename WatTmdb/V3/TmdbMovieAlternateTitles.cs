using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatTmdb.V3
{
    public class AlternateTitle
    {
        public string iso_3166_1 { get; set; }
        public string title { get; set; }

        public override string ToString()
        {
            return title;
        }
    }

    public class TmdbMovieAlternateTitles
    {
        public int id { get; set; }
        public List<AlternateTitle> titles { get; set; }
    }
}
