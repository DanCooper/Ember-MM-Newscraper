using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Playlist
{
   public class Item1
   {
       public string directory { get; set; }
       public XBMCRPC.Files.Media media { get; set; }
       public bool recursive { get; set; }
    }
}
