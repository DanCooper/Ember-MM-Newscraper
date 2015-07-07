using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Setting.Details
{
   public class Category : XBMCRPC.Setting.Details.Base
   {
       public global::System.Collections.Generic.List<XBMCRPC.Setting.Details.Group> groups { get; set; }
    }
}
