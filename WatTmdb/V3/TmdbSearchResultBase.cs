using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatTmdb.V3
{
    public abstract class TmdbSearchResultBase<T>
    {
        public int page { get; set; }
        public List<T> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
}
