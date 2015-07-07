using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.PVR
{
   public class GetTimerDetailsResponse
   {
       public XBMCRPC.PVR.Details.Timer timerdetails { get; set; }
    }
}
