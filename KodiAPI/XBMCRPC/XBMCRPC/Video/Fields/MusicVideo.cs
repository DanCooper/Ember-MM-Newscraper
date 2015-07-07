using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Video.Fields
{
   public enum MusicVideoItem
   {
       title,
       playcount,
       runtime,
       director,
       studio,
       year,
       plot,
       album,
       artist,
       genre,
       track,
       streamdetails,
       lastplayed,
       fanart,
       thumbnail,
       file,
       resume,
       dateadded,
       tag,
       art,
   }
   public class MusicVideo : List<MusicVideoItem>
   {
         public static MusicVideo AllFields()
         {
             var items = Enum.GetValues(typeof (MusicVideoItem));
             var list = new MusicVideo();
             list.AddRange(items.Cast<MusicVideoItem>());
             return list;
         }
   }
}
