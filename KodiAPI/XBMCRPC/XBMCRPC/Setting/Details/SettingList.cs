using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Setting.Details
{
   public class SettingList : XBMCRPC.Setting.Details.SettingBase
   {
       [Newtonsoft.Json.JsonProperty("default")]
       public global::System.Collections.Generic.List<object> Default { get; set; }
       public object definition { get; set; }
       public string delimiter { get; set; }
       public XBMCRPC.Setting.Type elementtype { get; set; }
       public int maximumitems { get; set; }
       public int minimumitems { get; set; }
       public global::System.Collections.Generic.List<object> value { get; set; }
    }
}
