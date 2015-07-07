using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.GUI
{
   public class GetProperties_properties : global::System.Collections.Generic.List<XBMCRPC.GUI.Property.Name>
   {
         public static GetProperties_properties AllFields()
         {
             var items = Enum.GetValues(typeof (XBMCRPC.GUI.Property.Name));
             var list = new GetProperties_properties();
             list.AddRange(items.Cast<XBMCRPC.GUI.Property.Name>());
             return list;
         }
   }
}
