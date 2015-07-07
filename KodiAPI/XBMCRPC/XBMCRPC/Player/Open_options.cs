using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Player
{
   public class Open_options
   {
       public object repeat { get; set; }
       public object resume { get; set; }
       public bool? shuffled { get; set; }
    }
}
