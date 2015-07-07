using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Video.Details
{
   public class File : XBMCRPC.Video.Details.Item
   {
       public global::System.Collections.Generic.List<string> director { get; set; }
       public XBMCRPC.Video.Resume resume { get; set; }
       public int runtime { get; set; }
       public XBMCRPC.Video.Streams streamdetails { get; set; }
    }
}
