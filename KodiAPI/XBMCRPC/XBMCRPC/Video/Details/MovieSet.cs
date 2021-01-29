namespace XBMCRPC.Video.Details
{
    public class MovieSet : Media
    {
        public string plot { get; set; }
        public int setid { get; set; }
        public class Extended : MovieSet
        {
            public List.LimitsReturned limits { get; set; }
            public global::System.Collections.Generic.List<Movie> movies { get; set; }
        }
    }
}
