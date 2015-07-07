using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Playlist
{
   public class GetItemsResponse
   {
       public global::System.Collections.Generic.List<XBMCRPC.List.Item.All> items { get; set; }
       public XBMCRPC.List.LimitsReturned limits { get; set; }
    }
}
