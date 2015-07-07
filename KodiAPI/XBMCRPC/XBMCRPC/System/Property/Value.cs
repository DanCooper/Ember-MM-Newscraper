using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.System.Property
{
   public class Value
   {
       public bool canhibernate { get; set; }
       public bool canreboot { get; set; }
       public bool canshutdown { get; set; }
       public bool cansuspend { get; set; }
    }
}
