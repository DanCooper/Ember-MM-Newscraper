using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Setting.Details
{
   public class ControlCheckmark : XBMCRPC.Setting.Details.ControlBase
   {
       public XBMCRPC.Setting.Details.ControlCheckmark_format format { get; set; }
       public XBMCRPC.Setting.Details.ControlCheckmark_type type { get; set; }
    }
}
