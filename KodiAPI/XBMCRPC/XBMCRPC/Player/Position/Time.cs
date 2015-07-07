using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Player.Position
{
   public class Time
   {
       public int hours { get; set; }
       public int milliseconds { get; set; }
       public int minutes { get; set; }
       public int seconds { get; set; }
    }
}
