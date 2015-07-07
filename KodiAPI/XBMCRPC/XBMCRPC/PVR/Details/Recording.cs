using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.PVR.Details
{
   public class Recording : XBMCRPC.Item.Details.Base
   {
       public XBMCRPC.Media.Artwork art { get; set; }
       public string channel { get; set; }
       public string directory { get; set; }
       public string endtime { get; set; }
       public string file { get; set; }
       public string genre { get; set; }
       public string icon { get; set; }
       public int lifetime { get; set; }
       public int playcount { get; set; }
       public string plot { get; set; }
       public string plotoutline { get; set; }
       public int recordingid { get; set; }
       public XBMCRPC.Video.Resume resume { get; set; }
       public int runtime { get; set; }
       public string starttime { get; set; }
       public string streamurl { get; set; }
       public string title { get; set; }
    }
}
