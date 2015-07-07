using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Settings
{
   public enum GetSections_propertiesItem
   {
       categories,
   }
   public class GetSections_properties : List<GetSections_propertiesItem>
   {
         public static GetSections_properties AllFields()
         {
             var items = Enum.GetValues(typeof (GetSections_propertiesItem));
             var list = new GetSections_properties();
             list.AddRange(items.Cast<GetSections_propertiesItem>());
             return list;
         }
   }
}
