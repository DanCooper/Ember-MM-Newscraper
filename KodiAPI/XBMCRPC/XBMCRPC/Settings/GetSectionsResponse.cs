using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Settings
{
   public class GetSectionsResponse
   {
       public global::System.Collections.Generic.List<XBMCRPC.Setting.Details.Section> sections { get; set; }
    }
}
