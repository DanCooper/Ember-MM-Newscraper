using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Setting.Details
{
   public class ControlEdit : XBMCRPC.Setting.Details.ControlHeading
   {
       public bool hidden { get; set; }
       public XBMCRPC.Setting.Details.ControlEdit_type type { get; set; }
       public bool verifynewvalue { get; set; }
    }
}
