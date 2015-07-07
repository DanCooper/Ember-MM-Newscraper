using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Setting.Details
{
   public class ControlButton : XBMCRPC.Setting.Details.ControlHeading
   {
       public XBMCRPC.Setting.Details.ControlButton_type type { get; set; }
    }
}
