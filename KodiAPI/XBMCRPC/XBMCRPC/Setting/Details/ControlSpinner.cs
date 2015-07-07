using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Setting.Details
{
   public class ControlSpinner : XBMCRPC.Setting.Details.ControlBase
   {
       public string formatlabel { get; set; }
       public string minimumlabel { get; set; }
       public XBMCRPC.Setting.Details.ControlSpinner_type type { get; set; }
    }
}
