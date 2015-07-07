using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Player
{
   public class GetItemResponse
   {
       public XBMCRPC.List.Item.All item { get; set; }
    }
}
