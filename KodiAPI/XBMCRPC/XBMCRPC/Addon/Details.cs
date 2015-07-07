using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Addon
{
   public class Details : XBMCRPC.Item.Details.Base
   {
       public string addonid { get; set; }
       public string author { get; set; }
       public string broken { get; set; }
       public global::System.Collections.Generic.List<XBMCRPC.Addon.Details_dependenciesItem> dependencies { get; set; }
       public string description { get; set; }
       public string disclaimer { get; set; }
       public bool enabled { get; set; }
       public global::System.Collections.Generic.List<XBMCRPC.Addon.Details_extrainfoItem> extrainfo { get; set; }
       public string fanart { get; set; }
       public string name { get; set; }
       public string path { get; set; }
       public int rating { get; set; }
       public string summary { get; set; }
       public string thumbnail { get; set; }
       public XBMCRPC.Addon.Types type { get; set; }
       public string version { get; set; }
    }
}
