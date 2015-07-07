using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Notifications
{
   public class Item3
   {
       public int episode { get; set; }
       public int season { get; set; }
       public string showtitle { get; set; }
       public string title { get; set; }
       public XBMCRPC.Notifications.Item.Type type { get; set; }
    }
}
