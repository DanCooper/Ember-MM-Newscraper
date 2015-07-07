using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Setting.Details
{
   public class ControlList : XBMCRPC.Setting.Details.ControlHeading
   {
       public bool multiselect { get; set; }
       public XBMCRPC.Setting.Details.ControlList_type type { get; set; }
    }
}
