using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Video
{
   public class Streams_videoItem
   {
       public double aspect { get; set; }
       public string codec { get; set; }
       public int duration { get; set; }
       public int height { get; set; }
       public int width { get; set; }
    }
}
