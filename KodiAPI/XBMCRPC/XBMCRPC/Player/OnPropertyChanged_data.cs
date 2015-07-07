using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Player
{
   public class OnPropertyChanged_data
   {
       public XBMCRPC.Player.Notifications.Player player { get; set; }
       public XBMCRPC.Player.Property.Value property { get; set; }
    }
}
