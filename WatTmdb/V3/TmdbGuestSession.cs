using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatTmdb.V3
{
    public class TmdbGuestSession
    {
        public string guest_session_id { get; set; }
        public string expires_at { get; set; }
        public bool success { get; set; }
    }
}
