using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatTmdb.V3
{
    public class CollectionBackdrop
    {
        public string file_path { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string iso_639_1 { get; set; }
        public double aspect_ratio { get; set; }
    }

    public class CollectionPoster
    {
        public string file_path { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string iso_639_1 { get; set; }
        public double aspect_ratio { get; set; }
    }

    public class TmdbCollectionImages
    {
        public List<CollectionBackdrop> backdrops { get; set; }
        public List<CollectionPoster> posters { get; set; }
    }
}
