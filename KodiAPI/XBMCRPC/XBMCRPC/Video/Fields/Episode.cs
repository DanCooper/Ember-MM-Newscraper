using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Video.Fields
{
   public enum EpisodeItem
   {
       title,
       plot,
       votes,
       rating,
       writer,
       firstaired,
       playcount,
       runtime,
       director,
       productioncode,
       season,
       episode,
       originaltitle,
       showtitle,
       cast,
       streamdetails,
       lastplayed,
       fanart,
       thumbnail,
       file,
       resume,
       tvshowid,
       dateadded,
       uniqueid,
       art,
   }
   public class Episode : List<EpisodeItem>
   {
         public static Episode AllFields()
         {
             var items = Enum.GetValues(typeof (EpisodeItem));
             var list = new Episode();
             list.AddRange(items.Cast<EpisodeItem>());
             return list;
         }
   }
}
