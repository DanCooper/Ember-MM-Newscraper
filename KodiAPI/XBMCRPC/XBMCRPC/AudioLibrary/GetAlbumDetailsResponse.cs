using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.AudioLibrary
{
   public class GetAlbumDetailsResponse
   {
       public XBMCRPC.Audio.Details.Album albumdetails { get; set; }
    }
}
