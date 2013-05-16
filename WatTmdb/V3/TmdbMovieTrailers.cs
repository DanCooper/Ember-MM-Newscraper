using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatTmdb.V3
{
    public class Youtube
    {
        public string name { get; set; }
        public string size { get; set; }
        public string source { get; set; }

        public override string ToString()
        {
            return name;
        }
    }

    public class TmdbMovieTrailers
    {
        public int id { get; set; }
        //public List<object> quicktime { get; set; }
        public List<Youtube> youtube { get; set; }
    }
}
