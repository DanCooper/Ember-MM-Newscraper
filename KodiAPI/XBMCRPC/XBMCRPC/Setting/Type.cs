using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Setting
{
   public enum Type
   {
       boolean,
       integer,
       number,
       [global::System.Runtime.Serialization.EnumMember(Value="string")]
       String,
       action,
       list,
       path,
       addon,
   }
}
