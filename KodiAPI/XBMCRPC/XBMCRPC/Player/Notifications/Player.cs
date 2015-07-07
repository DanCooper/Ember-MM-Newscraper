using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Player.Notifications
{
   public class Player
   {
       public int playerid { get; set; }
       public int speed { get; set; }
   public class Seek : XBMCRPC.Player.Notifications.Player
   {
       public XBMCRPC.Global.Time seekoffset { get; set; }
       public XBMCRPC.Global.Time time { get; set; }
    }
    }
}
