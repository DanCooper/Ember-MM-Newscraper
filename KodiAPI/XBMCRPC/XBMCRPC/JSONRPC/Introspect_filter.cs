using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.JSONRPC
{
   public class Introspect_filter
   {
       public bool getreferences { get; set; }
       public string id { get; set; }
       public XBMCRPC.JSONRPC.Introspect_filter_type type { get; set; }
    }
}
