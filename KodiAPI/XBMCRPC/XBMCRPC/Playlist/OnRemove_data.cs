using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Playlist
{
   public class OnRemove_data
   {
       public int playlistid { get; set; }
       public int position { get; set; }
    }
}
