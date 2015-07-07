using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Video
{
   public class CastItem
   {
       public string name { get; set; }
       public int order { get; set; }
       public string role { get; set; }
       public string thumbnail { get; set; }
    }
}
