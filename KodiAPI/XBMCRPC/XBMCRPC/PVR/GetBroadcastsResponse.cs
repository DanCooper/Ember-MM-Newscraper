using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.PVR
{
   public class GetBroadcastsResponse
   {
       public global::System.Collections.Generic.List<XBMCRPC.PVR.Details.Broadcast> broadcasts { get; set; }
       public XBMCRPC.List.LimitsReturned limits { get; set; }
    }
}
