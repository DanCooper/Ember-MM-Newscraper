using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Notifications
{
   public class Item7
   {
       public XBMCRPC.PVR.Channel.Type channeltype { get; set; }
       public int id { get; set; }
       public string title { get; set; }
       public XBMCRPC.Notifications.Item.Type type { get; set; }
    }
}
