using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.PVR
{
   public class GetChannelGroupsResponse
   {
       public global::System.Collections.Generic.List<XBMCRPC.PVR.Details.ChannelGroup> channelgroups { get; set; }
       public XBMCRPC.List.LimitsReturned limits { get; set; }
    }
}
