using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Video.Details
{
   public class Item : XBMCRPC.Video.Details.Media
   {
       public string dateadded { get; set; }
       public string file { get; set; }
       public string lastplayed { get; set; }
       public string plot { get; set; }
    }
}
