using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatTmdb.V3
{
    public class Cast
    {
        public int id { get; set; }
        public string name { get; set; }
        public string character { get; set; }
        public int order { get; set; }
        public string profile_path { get; set; }

        public override string ToString()
        {
            return name;
        }
    }

    public class Crew
    {
        public int id { get; set; }
        public string name { get; set; }
        public string department { get; set; }
        public string job { get; set; }
        public string profile_path { get; set; }

        public override string ToString()
        {
            return name;
        }
    }

    public class TmdbMovieCast
    {
        public int id { get; set; }
        public List<Cast> cast { get; set; }
        public List<Crew> crew { get; set; }
    }
}
