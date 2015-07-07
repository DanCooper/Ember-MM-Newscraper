using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Settings
{
   public class GetCategoriesResponse
   {
       public global::System.Collections.Generic.List<XBMCRPC.Setting.Details.Category> categories { get; set; }
    }
}
