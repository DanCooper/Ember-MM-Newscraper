using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Audio.Fields
{
   public enum SongItem
   {
       title,
       artist,
       albumartist,
       genre,
       year,
       rating,
       album,
       track,
       duration,
       comment,
       lyrics,
       musicbrainztrackid,
       musicbrainzartistid,
       musicbrainzalbumid,
       musicbrainzalbumartistid,
       playcount,
       fanart,
       thumbnail,
       file,
       albumid,
       lastplayed,
       disc,
       genreid,
       artistid,
       displayartist,
       albumartistid,
   }
   public class Song : List<SongItem>
   {
         public static Song AllFields()
         {
             var items = Enum.GetValues(typeof (SongItem));
             var list = new Song();
             list.AddRange(items.Cast<SongItem>());
             return list;
         }
   }
}
