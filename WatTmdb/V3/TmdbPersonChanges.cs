using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatTmdb.V3
{
    public class PersonChangeItem
    {
        public string id { get; set; }
        public string action { get; set; }
        public string time { get; set; }
        public string value { get; set; }
    }

    public class PersonChange
    {
        public string key { get; set; }
        public List<PersonChangeItem> items { get; set; }
    }

    public class TmdbPersonChanges
    {
        public List<PersonChange> changes { get; set; }
    }
}
