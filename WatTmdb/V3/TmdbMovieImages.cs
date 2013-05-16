using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatTmdb.V3
{
    public class Backdrop
    {
        public string file_path { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string iso_639_1 { get; set; }
        public double aspect_ratio { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
    }

    public class Poster
    {
        public string file_path { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string iso_639_1 { get; set; }
        public double aspect_ratio { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
    }

    public class TmdbMovieImages
    {
        public int id { get; set; }
        public List<Backdrop> backdrops { get; set; }
        public List<Poster> posters { get; set; }
    }
}
