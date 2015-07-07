using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.PVR.Details
{
   public class Channel : XBMCRPC.Item.Details.Base
   {
       public string channel { get; set; }
       public int channelid { get; set; }
       public XBMCRPC.PVR.Channel.Type channeltype { get; set; }
       public bool hidden { get; set; }
       public string lastplayed { get; set; }
       public bool locked { get; set; }
       public string thumbnail { get; set; }
    }
}
