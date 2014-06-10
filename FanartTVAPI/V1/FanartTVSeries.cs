using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FanartTV.V1
{
    public class Clearart
    {
        public string id { get; set; }
        public string url { get; set; }
        public string lang { get; set; }
        public string likes { get; set; }
    }

    public class Characterart
    {
        public string id { get; set; }
        public string url { get; set; }
        public string lang { get; set; }
        public string likes { get; set; }
    }

    public class Clearlogo
    {
        public string id { get; set; }
        public string url { get; set; }
        public string lang { get; set; }
        public string likes { get; set; }
    }

    public class Hdtvlogo
    {
        public string id { get; set; }
        public string url { get; set; }
        public string lang { get; set; }
        public string likes { get; set; }
    }

    public class Hdclearart
    {
        public string id { get; set; }
        public string url { get; set; }
        public string lang { get; set; }
        public string likes { get; set; }
    }

    public class Tvthumb
    {
        public string id { get; set; }
        public string url { get; set; }
        public string lang { get; set; }
        public string likes { get; set; }
    }

    public class Showbackground
    {
        public string id { get; set; }
        public string url { get; set; }
        public string lang { get; set; }
        public string likes { get; set; }
        public string season { get; set; }
    }

    public class Tvposter
    {
        public string id { get; set; }
        public string url { get; set; }
        public string lang { get; set; }
        public string likes { get; set; }
    }

    public class Seasonposter
    {
        public string id { get; set; }
        public string url { get; set; }
        public string lang { get; set; }
        public string likes { get; set; }
        public string season { get; set; }
    }

    public class Seasonthumb
    {
        public string id { get; set; }
        public string url { get; set; }
        public string lang { get; set; }
        public string likes { get; set; }
        public string season { get; set; }
    }

    public class Tvbanner
    {
        public string id { get; set; }
        public string url { get; set; }
        public string lang { get; set; }
        public string likes { get; set; }
    }

    public class FanartTVSeries_detail
    {
        public string thetvdb_id { get; set; }
        public List<Clearart> clearart { get; set; }
        public List<Characterart> characterart { get; set; }
        public List<Clearlogo> clearlogo { get; set; }
        public List<Hdtvlogo> hdtvlogo { get; set; }
        public List<Hdclearart> hdclearart { get; set; }
        public List<Tvthumb> tvthumb { get; set; }
        public List<Showbackground> showbackground { get; set; }
        public List<Tvposter> tvposter { get; set; }
        public List<Seasonposter> seasonposter { get; set; }
        public List<Seasonthumb> seasonthumb { get; set; }
        public List<Tvbanner> tvbanner { get; set; }
    }


	public class FanartTVSeries
	{
		public string title { get; set; }
		public  FanartTVSeries_detail showinfo { get; set; }
	}
}
