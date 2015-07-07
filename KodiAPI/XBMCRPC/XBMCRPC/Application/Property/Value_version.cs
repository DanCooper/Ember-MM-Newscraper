using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Application.Property
{
   public class Value_version
   {
       public int major { get; set; }
       public int minor { get; set; }
       public string revision { get; set; }
       public XBMCRPC.Application.Property.Value_version_tag tag { get; set; }
    }
}
