using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Audio.Details
{
   public class Base : XBMCRPC.Media.Details.Base
   {
       public global::System.Collections.Generic.List<string> genre { get; set; }
    }
}
