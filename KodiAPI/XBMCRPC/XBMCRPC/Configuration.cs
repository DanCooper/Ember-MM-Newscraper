using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC
{
   public class Configuration
   {
       public XBMCRPC.Configuration.Notifications notifications { get; set; }
   public class Notifications
   {
       public bool application { get; set; }
       public bool audiolibrary { get; set; }
       public bool gui { get; set; }
       public bool input { get; set; }
       public bool other { get; set; }
       public bool player { get; set; }
       public bool playlist { get; set; }
       public bool pvr { get; set; }
       public bool system { get; set; }
       public bool videolibrary { get; set; }
    }
    }
}
