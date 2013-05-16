using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatTmdb.V3
{
    public class ParentCompany
    {
        public string name { get; set; }
        public int id { get; set; }
        public string logo_path { get; set; }

        public override string ToString()
        {
            return name;
        }
    }

    public class TmdbCompany
    {
        public string description { get; set; }
        public string headquarters { get; set; }
        public string homepage { get; set; }
        public int id { get; set; }
        public string logo_path { get; set; }
        public string name { get; set; }
        public ParentCompany parent_company { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
}
