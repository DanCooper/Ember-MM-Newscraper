using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Player
{
   public class OnStop_data
   {
       public bool end { get; set; }
       public object item { get; set; }
    }
}
