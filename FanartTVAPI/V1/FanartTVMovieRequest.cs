using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FanartTV.V1
{
	public partial class FanartTVMovieRequest
	{
		public string MovieID { get; set; }
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

		public FanartTVMovieRequest(string _MovieID, string _format, string _type, int _sort, int _limit)
		{
			MovieID = _MovieID;
			format = _format;
			type = _type;
			sort = _sort;
			limit = _limit;
		}
	}
}
