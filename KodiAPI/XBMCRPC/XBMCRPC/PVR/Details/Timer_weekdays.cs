using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.PVR.Details
{
   public class Timer_weekdays : global::System.Collections.Generic.List<XBMCRPC.Global.Weekday>
   {
         public static Timer_weekdays AllFields()
         {
             var items = Enum.GetValues(typeof (XBMCRPC.Global.Weekday));
             var list = new Timer_weekdays();
             list.AddRange(items.Cast<XBMCRPC.Global.Weekday>());
             return list;
         }
   }
}
