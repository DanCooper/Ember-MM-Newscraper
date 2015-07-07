using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.PVR
{
   public class GetChannelGroupDetails_channels
   {
       public XBMCRPC.List.Limits limits { get; set; }
       public XBMCRPC.PVR.Fields.Channel properties { get; set; }
    }
}
