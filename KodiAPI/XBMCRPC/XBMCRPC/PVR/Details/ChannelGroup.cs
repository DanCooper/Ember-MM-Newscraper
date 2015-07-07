using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.PVR.Details
{
   public class ChannelGroup : XBMCRPC.Item.Details.Base
   {
       public int channelgroupid { get; set; }
       public XBMCRPC.PVR.Channel.Type channeltype { get; set; }
   public class Extended : XBMCRPC.PVR.Details.ChannelGroup
   {
       public global::System.Collections.Generic.List<XBMCRPC.PVR.Details.Channel> channels { get; set; }
       public XBMCRPC.List.LimitsReturned limits { get; set; }
    }
    }
}
