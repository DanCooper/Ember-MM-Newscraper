using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatTmdb.V3
{
    public class CompanyResult
    {
        public int id { get; set; }
        public string logo_path { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return name;
        }
    }

    public class TmdbCompanySearch : TmdbSearchResultBase<CompanyResult>
    { }
}
