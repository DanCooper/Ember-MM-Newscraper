namespace XBMCRPC.Video.Details
{
    public class Movie : File
    {
        public global::System.Collections.Generic.List<Cast> cast { get; set; }
        public global::System.Collections.Generic.List<string> country { get; set; }
        public global::System.Collections.Generic.List<string> genre { get; set; }
        public string imdbnumber { get; set; }
        public int movieid { get; set; }
        public string mpaa { get; set; }
        public string originaltitle { get; set; }
        public string plotoutline { get; set; }
        public string premiered { get; set; }
        public double rating { get; set; }
        public global::System.Collections.Generic.Dictionary<string, Rating> ratings { get; set; }
        public string set { get; set; }
        public int setid { get; set; }
        public global::System.Collections.Generic.List<string> showlink { get; set; }
        public string sorttitle { get; set; }
        public global::System.Collections.Generic.List<string> studio { get; set; }
        public global::System.Collections.Generic.List<string> tag { get; set; }
        public string tagline { get; set; }
        public int top250 { get; set; }
        public string trailer { get; set; }
        public global::System.Collections.Generic.Dictionary<string, string> uniqueid { get; set; }
        public int userrating { get; set; }
        public string votes { get; set; }
        public global::System.Collections.Generic.List<string> writer { get; set; }
        public int year { get; set; }
    }
}
