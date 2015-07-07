using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Player.Notifications
{
   public class Data
   {
       public object item { get; set; }
       public XBMCRPC.Player.Notifications.Player player { get; set; }
    }
}
