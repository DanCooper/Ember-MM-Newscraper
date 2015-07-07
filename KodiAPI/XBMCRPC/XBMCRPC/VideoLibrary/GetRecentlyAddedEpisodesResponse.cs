using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.VideoLibrary
{
   public class GetRecentlyAddedEpisodesResponse
   {
       public global::System.Collections.Generic.List<XBMCRPC.Video.Details.Episode> episodes { get; set; }
       public XBMCRPC.List.LimitsReturned limits { get; set; }
    }
}
