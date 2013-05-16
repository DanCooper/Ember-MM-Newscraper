using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatTmdb.V3
{
    public class PersonResult
    {
        public bool adult { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string profile_path { get; set; }

        public override string ToString()
        {
            return name;
        }
    }

    public class TmdbPersonSearch : TmdbSearchResultBase<PersonResult>
    { }
}
