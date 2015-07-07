using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Settings
{
   public enum GetCategories_propertiesItem
   {
       settings,
   }
   public class GetCategories_properties : List<GetCategories_propertiesItem>
   {
         public static GetCategories_properties AllFields()
         {
             var items = Enum.GetValues(typeof (GetCategories_propertiesItem));
             var list = new GetCategories_properties();
             list.AddRange(items.Cast<GetCategories_propertiesItem>());
             return list;
         }
   }
}
