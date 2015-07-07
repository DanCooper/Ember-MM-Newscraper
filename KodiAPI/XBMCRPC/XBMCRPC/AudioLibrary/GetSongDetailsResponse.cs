using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.AudioLibrary
{
   public class GetSongDetailsResponse
   {
       public XBMCRPC.Audio.Details.Song songdetails { get; set; }
    }
}
