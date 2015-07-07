using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.PVR.Property
{
   public class Value
   {
       public bool available { get; set; }
       public bool recording { get; set; }
       public bool scanning { get; set; }
    }
}
