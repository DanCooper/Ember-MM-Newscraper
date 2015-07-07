using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Video.Details
{
   public class MusicVideo : XBMCRPC.Video.Details.File
   {
       public string album { get; set; }
       public global::System.Collections.Generic.List<string> artist { get; set; }
       public global::System.Collections.Generic.List<string> genre { get; set; }
       public int musicvideoid { get; set; }
       public global::System.Collections.Generic.List<string> studio { get; set; }
       public global::System.Collections.Generic.List<string> tag { get; set; }
       public int track { get; set; }
       public int year { get; set; }
    }
}
