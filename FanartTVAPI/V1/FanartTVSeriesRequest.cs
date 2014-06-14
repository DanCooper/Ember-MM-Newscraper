using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FanartTV.V1
{
	public partial class FanartTVSeriesRequest
	{
		public string SeriesID { get; set; }
		public string format { get; set; }
		public string type { get; set; }
		public int sort { get; set; }
		public int limit { get; set; }

		//public FanartTVRequest(string _MovieID, string _format, string _type, int _sort, int _limit)
		//{
		//    MovieID = _MovieID;
		//    format = _format;
		//    type = _type;
		//    sort = _sort;
		//    limit = _limit;
		//}

		public FanartTVSeriesRequest(string _SeriesID, string _format, string _type, int _sort, int _limit)
		{
			SeriesID = _SeriesID;
			format = _format;
			type = _type;
			sort = _sort;
			limit = _limit;
		}
	}
}
