using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Setting.Details
{
   public class SettingPath : XBMCRPC.Setting.Details.SettingString
   {
       public global::System.Collections.Generic.List<string> sources { get; set; }
       public bool writable { get; set; }
    }
}
