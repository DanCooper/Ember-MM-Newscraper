using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Video.Details
{
   public class Episode : XBMCRPC.Video.Details.File
   {
       public global::System.Collections.Generic.List<XBMCRPC.Video.CastItem> cast { get; set; }
       public int episode { get; set; }
       public int episodeid { get; set; }
       public string firstaired { get; set; }
       public string originaltitle { get; set; }
       public string productioncode { get; set; }
       public double rating { get; set; }
       public int season { get; set; }
       public string showtitle { get; set; }
       public int tvshowid { get; set; }
       public XBMCRPC.Video.Details.Episode_uniqueid uniqueid { get; set; }
       public string votes { get; set; }
       public global::System.Collections.Generic.List<string> writer { get; set; }
    }
}
