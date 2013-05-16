using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatTmdb.V3
{
    public class ListItem
    {
        public string description { get; set; }
        public int favorite_count { get; set; }
        public string id { get; set; }
        public int item_count { get; set; }
        public string iso_639_1 { get; set; }
        public string list_type { get; set; }
        public string name { get; set; }
        public string poster_path { get; set; }

        public override string ToString()
        {
            return name;
        }
    }

    public class TmdbList : TmdbSearchResultBase<ListItem>
    { }

    public class ListMovie
    {
        public string backdrop_path { get; set; }
        public int id { get; set; }
        public string original_title { get; set; }
        public string release_date { get; set; }
        public string poster_path { get; set; }
        public string title { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }

        public override string ToString()
        {
            return title;
        }
    }

    public class TmdbListItem
    {
        public string created_by { get; set; }
        public string description { get; set; }
        public int favorite_count { get; set; }
        public string id { get; set; }
        public List<ListMovie> items { get; set; }
    }
}
