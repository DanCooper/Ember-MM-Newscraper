using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Media.Details
{
   public class Base : XBMCRPC.Item.Details.Base
   {
       public string fanart { get; set; }
       public string thumbnail { get; set; }
    }
}
