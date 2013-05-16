using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatTmdb.V3
{
    public class TmdbAuthToken
    {
        public string expires_at { get; set; }
        public string request_token { get; set; }
        public bool success { get; set; }
    }
}
