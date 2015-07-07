using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Player
{
   public class SeekResponse
   {
       public double percentage { get; set; }
       public XBMCRPC.Global.Time time { get; set; }
       public XBMCRPC.Global.Time totaltime { get; set; }
    }
}
