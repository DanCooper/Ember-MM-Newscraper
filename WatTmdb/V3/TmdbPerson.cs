using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatTmdb.V3
{
    public class TmdbPerson
    {
        public bool adult { get; set; }
        //public List<object> also_known_as { get; set; }
        public string biography { get; set; }
        public string birthday { get; set; }
        public string deathday { get; set; }
        public string homepage { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string place_of_birth { get; set; }
        public string profile_path { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
}
