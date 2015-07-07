using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Application
{
   public class OnVolumeChanged_data
   {
       public bool muted { get; set; }
       public int volume { get; set; }
    }
}
