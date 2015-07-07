using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Setting.Details
{
   public class ControlBase
   {
       public bool delayed { get; set; }
       public string format { get; set; }
       public string type { get; set; }
    }
}
