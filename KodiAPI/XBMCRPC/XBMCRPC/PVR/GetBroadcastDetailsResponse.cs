using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.PVR
{
   public class GetBroadcastDetailsResponse
   {
       public XBMCRPC.PVR.Details.Broadcast broadcastdetails { get; set; }
    }
}
