using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Audio.Details
{
   public class Media : XBMCRPC.Audio.Details.Base
   {
       public global::System.Collections.Generic.List<string> artist { get; set; }
       public global::System.Collections.Generic.List<int> artistid { get; set; }
       public string displayartist { get; set; }
       public global::System.Collections.Generic.List<int> genreid { get; set; }
       public string musicbrainzalbumartistid { get; set; }
       public string musicbrainzalbumid { get; set; }
       public int rating { get; set; }
       public string title { get; set; }
       public int year { get; set; }
    }
}
