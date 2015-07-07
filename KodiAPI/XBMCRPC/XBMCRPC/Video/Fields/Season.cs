using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Video.Fields
{
   public enum SeasonItem
   {
       season,
       showtitle,
       playcount,
       episode,
       fanart,
       thumbnail,
       tvshowid,
       watchedepisodes,
       art,
   }
   public class Season : List<SeasonItem>
   {
         public static Season AllFields()
         {
             var items = Enum.GetValues(typeof (SeasonItem));
             var list = new Season();
             list.AddRange(items.Cast<SeasonItem>());
             return list;
         }
   }
}
