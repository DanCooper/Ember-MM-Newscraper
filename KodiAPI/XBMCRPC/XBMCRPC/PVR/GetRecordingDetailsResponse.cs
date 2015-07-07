using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.PVR
{
   public class GetRecordingDetailsResponse
   {
       public XBMCRPC.PVR.Details.Recording recordingdetails { get; set; }
    }
}
