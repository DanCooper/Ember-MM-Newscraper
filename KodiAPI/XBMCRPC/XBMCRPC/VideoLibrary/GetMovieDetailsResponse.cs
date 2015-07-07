using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.VideoLibrary
{
   public class GetMovieDetailsResponse
   {
       public XBMCRPC.Video.Details.Movie moviedetails { get; set; }
    }
}
