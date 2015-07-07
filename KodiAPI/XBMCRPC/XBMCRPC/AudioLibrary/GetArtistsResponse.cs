using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.AudioLibrary
{
   public class GetArtistsResponse
   {
       public global::System.Collections.Generic.List<XBMCRPC.Audio.Details.Artist> artists { get; set; }
       public XBMCRPC.List.LimitsReturned limits { get; set; }
    }
}
