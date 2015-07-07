using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Addons
{
   public class GetAddonDetailsResponse
   {
       public XBMCRPC.Addon.Details addon { get; set; }
       public XBMCRPC.List.LimitsReturned limits { get; set; }
    }
}
