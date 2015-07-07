using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.List
{
   public class Sort
   {
       public bool ignorearticle { get; set; }
       public XBMCRPC.List.Sort_method method { get; set; }
       public XBMCRPC.List.Sort_order order { get; set; }
    }
}
