using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Audio.Details
{
   public class Artist : XBMCRPC.Audio.Details.Base
   {
       public string artist { get; set; }
       public int artistid { get; set; }
       public string born { get; set; }
       public bool compilationartist { get; set; }
       public string description { get; set; }
       public string died { get; set; }
       public string disbanded { get; set; }
       public string formed { get; set; }
       public global::System.Collections.Generic.List<string> instrument { get; set; }
       public global::System.Collections.Generic.List<string> mood { get; set; }
       public string musicbrainzartistid { get; set; }
       public global::System.Collections.Generic.List<string> style { get; set; }
       public global::System.Collections.Generic.List<string> yearsactive { get; set; }
    }
}
