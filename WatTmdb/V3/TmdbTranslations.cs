using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatTmdb.V3
{
    public class Translation
    {
        public string iso_639_1 { get; set; }
        public string name { get; set; }
        public string english_name { get; set; }
    }

    public class TmdbTranslations
    {
        public int id { get; set; }
        public List<Translation> translations { get; set; }
    }
}
