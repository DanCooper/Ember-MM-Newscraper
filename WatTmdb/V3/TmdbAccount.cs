using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatTmdb.V3
{
    public class TmdbAccount
    {
        public int id { get; set; }
        public bool include_adult { get; set; }
        public string iso_3166_1 { get; set; }
        public string iso_639_1 { get; set; }
        public string name { get; set; }
        public string username { get; set; }
    }
}
