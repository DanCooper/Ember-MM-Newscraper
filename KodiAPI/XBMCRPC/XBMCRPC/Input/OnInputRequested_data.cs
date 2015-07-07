using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Input
{
   public class OnInputRequested_data
   {
       public string title { get; set; }
       public string type { get; set; }
       public string value { get; set; }
    }
}
