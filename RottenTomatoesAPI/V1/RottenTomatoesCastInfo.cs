using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RottenTomatoes.V1
{
	public class Cast
	{
		public string id { get; set; }
		public string name { get; set; }
		public List<object> characters { get; set; }
	}

	public class CastLinks
	{
		public string rel { get; set; }
	}

	public class RottenTomatoesCastInfo
	{
		public List<Cast> cast { get; set; }
		public CastLinks links { get; set; }
	}
}
