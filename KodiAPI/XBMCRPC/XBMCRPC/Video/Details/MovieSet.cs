using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Video.Details
{
   public class MovieSet : XBMCRPC.Video.Details.Media
   {
       public int setid { get; set; }
   public class Extended : XBMCRPC.Video.Details.MovieSet
   {
       public XBMCRPC.List.LimitsReturned limits { get; set; }
       public global::System.Collections.Generic.List<XBMCRPC.Video.Details.Movie> movies { get; set; }
    }
    }
}
