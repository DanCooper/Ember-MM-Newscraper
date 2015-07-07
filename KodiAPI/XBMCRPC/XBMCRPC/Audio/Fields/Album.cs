using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Audio.Fields
{
   public enum AlbumItem
   {
       title,
       description,
       artist,
       genre,
       theme,
       mood,
       style,
       type,
       albumlabel,
       rating,
       year,
       musicbrainzalbumid,
       musicbrainzalbumartistid,
       fanart,
       thumbnail,
       playcount,
       genreid,
       artistid,
       displayartist,
   }
   public class Album : List<AlbumItem>
   {
         public static Album AllFields()
         {
             var items = Enum.GetValues(typeof (AlbumItem));
             var list = new Album();
             list.AddRange(items.Cast<AlbumItem>());
             return list;
         }
   }
}
