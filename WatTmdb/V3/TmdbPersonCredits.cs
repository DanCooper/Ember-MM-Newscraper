using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatTmdb.V3
{
    public class CastCredit
    {
        public int id { get; set; }
        public string title { get; set; }
        public string character { get; set; }
        public string original_title { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }

        public override string ToString()
        {
            return title;
        }
    }

    public class CrewCredit
    {
        public int id { get; set; }
        public string title { get; set; }
        public string original_title { get; set; }
        public string department { get; set; }
        public string job { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }

        public override string ToString()
        {
            return title;
        }
    }

    public class TmdbPersonCredits
    {
        public List<CastCredit> cast { get; set; }
        public List<CrewCredit> crew { get; set; }
        public int id { get; set; }
    }
}
