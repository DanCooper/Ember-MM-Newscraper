using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Video
{
   public class Streams_audioItem
   {
       public int channels { get; set; }
       public string codec { get; set; }
       public string language { get; set; }
    }
}
