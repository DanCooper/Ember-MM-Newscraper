using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.JSONRPC
{
   public class PermissionResponse
   {
       public bool controlgui { get; set; }
       public bool controlnotify { get; set; }
       public bool controlplayback { get; set; }
       public bool controlpower { get; set; }
       public bool controlpvr { get; set; }
       public bool controlsystem { get; set; }
       public bool executeaddon { get; set; }
       public bool manageaddon { get; set; }
       public bool navigate { get; set; }
       public bool readdata { get; set; }
       public bool removedata { get; set; }
       public bool updatedata { get; set; }
       public bool writefile { get; set; }
    }
}
