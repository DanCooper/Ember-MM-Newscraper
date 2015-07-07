using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Player.Audio
{
   public class Stream
   {
       public int bitrate { get; set; }
       public int channels { get; set; }
       public string codec { get; set; }
       public int index { get; set; }
       public string language { get; set; }
       public string name { get; set; }
    }
}
