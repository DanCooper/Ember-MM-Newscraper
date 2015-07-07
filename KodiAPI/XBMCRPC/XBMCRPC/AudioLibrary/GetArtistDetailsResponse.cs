using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.AudioLibrary
{
   public class GetArtistDetailsResponse
   {
       public XBMCRPC.Audio.Details.Artist artistdetails { get; set; }
    }
}
