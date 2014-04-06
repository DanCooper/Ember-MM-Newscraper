using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FanartTV.V1
{
	public class MovieLogo
	{
		public string id { get; set; }
		public string url { get; set; }
		public string lang { get; set; }
		public string likes { get; set; }
	}

	public class MovieArt
	{
		public string id { get; set; }
		public string url { get; set; }
		public string lang { get; set; }
		public string likes { get; set; }
	}

    public class MoviePoster
    {
        public string id { get; set; }
        public string url { get; set; }
        public string lang { get; set; }
        public string likes { get; set; }
    }

    public class MovieBanner
    {
        public string id { get; set; }
        public string url { get; set; }
        public string lang { get; set; }
        public string likes { get; set; }
    }

	public class MovieBackground
	{
		public string id { get; set; }
		public string url { get; set; }
		public string lang { get; set; }
		public string likes { get; set; }
	}

	public class MovieDisc
	{
		public string id { get; set; }
		public string url { get; set; }
		public string lang { get; set; }
		public string likes { get; set; }
		public string disc { get; set; }
		public string disc_type { get; set; }
	}

	public class MovieThumb
	{
		public string id { get; set; }
		public string url { get; set; }
		public string lang { get; set; }
		public string likes { get; set; }
	}

	public class HDMovieLogo
	{
		public string id { get; set; }
		public string url { get; set; }
		public string lang { get; set; }
		public string likes { get; set; }
	}

	public class HDMovieClearArt
	{
		public string id { get; set; }
		public string url { get; set; }
		public string lang { get; set; }
		public string likes { get; set; }
	}

	public class FanartTVMovie_detail
	{
		public string tmdb_id { get; set; }
		public string imdb_id { get; set; }
		public List<MovieLogo> movielogo { get; set; }
        public List<MovieArt> movieart { get; set; }
        public List<MoviePoster> movieposter { get; set; }
        public List<MovieBanner> moviebanner { get; set; }
		public List<MovieBackground> moviebackground { get; set; }
		public List<MovieDisc> moviedisc { get; set; }
		public List<MovieThumb> moviethumb { get; set; }
		public List<HDMovieLogo> hdmovielogo { get; set; }
		public List<HDMovieClearArt> hdmovieclearart { get; set; }
	}

	public class FanartTVMovie
	{
		public string title { get; set; }
		public  FanartTVMovie_detail movieinfo { get; set; }
	}
}
