using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.List.Items
{
   public class SourcesItem : XBMCRPC.Item.Details.Base
   {
       public string file { get; set; }
    }
}
