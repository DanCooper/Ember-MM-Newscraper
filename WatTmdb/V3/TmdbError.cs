using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatTmdb.V3
{
    public class TmdbError
    {
        public int status_code { get; set; }
        public string status_message { get; set; }
    }
}
