using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RottenTomatoes.V1
{
	public partial class RottenTomatoesRequest
	{
		public string MovieID { get; set; }


		//public RottenTomatoesRequest(string _MovieID, string _format, string _type, int _sort, int _limit)
		//{
		//    MovieID = _MovieID;
		//    format = _format;
		//    type = _type;
		//    sort = _sort;
		//    limit = _limit;
		//}

		public RottenTomatoesRequest(string _MovieID)
		{
			MovieID = _MovieID;
		}
	}
}
