using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Profiles
{
   public class Password
   {
       public XBMCRPC.Profiles.Password_encryption encryption { get; set; }
       public string value { get; set; }
    }
}
