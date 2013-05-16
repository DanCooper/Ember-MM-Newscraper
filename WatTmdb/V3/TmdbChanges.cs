using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatTmdb.V3
{
    public class ChangesItem
    {
        public int id { get; set; }
        public bool adult { get; set; }
    }

    public class TmdbChanges : TmdbSearchResultBase<ChangesItem>
    { }
}
