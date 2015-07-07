using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.VideoLibrary
{
   public class GetTVShowDetailsResponse
   {
       public XBMCRPC.Video.Details.TVShow tvshowdetails { get; set; }
    }
}
