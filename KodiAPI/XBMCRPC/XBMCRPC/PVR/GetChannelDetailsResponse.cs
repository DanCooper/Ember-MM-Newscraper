using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.PVR
{
   public class GetChannelDetailsResponse
   {
       public XBMCRPC.PVR.Details.Channel channeldetails { get; set; }
    }
}
