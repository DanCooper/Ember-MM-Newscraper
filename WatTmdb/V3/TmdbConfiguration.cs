using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatTmdb.V3
{
    public class ImageConfiguration
    {
        public string base_url { get; set; }
        public string secure_base_url { get; set; }
        public List<string> poster_sizes { get; set; }
        public List<string> backdrop_sizes { get; set; }
        public List<string> profile_sizes { get; set; }
        public List<string> logo_sizes { get; set; }
    }

    public class TmdbConfiguration
    {
        public ImageConfiguration images { get; set; }
        public List<string> change_keys { get; set; }
    }
}
