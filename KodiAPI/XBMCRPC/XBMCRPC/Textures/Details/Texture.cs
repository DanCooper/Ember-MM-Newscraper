using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Textures.Details
{
   public class Texture
   {
       public string cachedurl { get; set; }
       public string imagehash { get; set; }
       public string lasthashcheck { get; set; }
       public global::System.Collections.Generic.List<XBMCRPC.Textures.Details.Size> sizes { get; set; }
       public int textureid { get; set; }
       public string url { get; set; }
    }
}
