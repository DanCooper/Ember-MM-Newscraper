using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.AudioLibrary
{
   public class GetGenresResponse
   {
       public global::System.Collections.Generic.List<XBMCRPC.Library.Details.Genre> genres { get; set; }
       public XBMCRPC.List.LimitsReturned limits { get; set; }
    }
}
