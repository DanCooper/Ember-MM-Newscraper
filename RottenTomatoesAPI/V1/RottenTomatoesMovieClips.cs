using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RottenTomatoes.V1
{
	public class MovieClipLinks
	{
		public string alternate { get; set; }
	}

	public class Clip
	{
		public string title { get; set; }
		public string duration { get; set; }
		public string thumbnail { get; set; }
		public MovieClipLinks links { get; set; }
	}

	public class Links2
	{
		public string self { get; set; }
		public string alternate { get; set; }
		public string rel { get; set; }
	}

	public class RottenTomatoesMovieClips
	{
		public List<Clip> clips { get; set; }
		public Links2 links { get; set; }
	}
}
