using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Video.Details
{
   public class Base : XBMCRPC.Media.Details.Base
   {
       public XBMCRPC.Media.Artwork art { get; set; }
       public int playcount { get; set; }
    }
}
