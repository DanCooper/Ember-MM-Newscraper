using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Setting.Details
{
   public class SettingAddon : XBMCRPC.Setting.Details.SettingString
   {
       public XBMCRPC.Addon.Types addontype { get; set; }
    }
}
