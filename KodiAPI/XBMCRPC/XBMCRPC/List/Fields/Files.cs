using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.List.Fields
{
   public enum FilesItem
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
       director,
       trailer,
       tagline,
       plot,
       plotoutline,
       originaltitle,
       lastplayed,
       writer,
       studio,
       mpaa,
       cast,
       country,
       imdbnumber,
       premiered,
       productioncode,
       runtime,
       set,
       showlink,
       streamdetails,
       top250,
       votes,
       firstaired,
       season,
       episode,
       showtitle,
       thumbnail,
       file,
       resume,
       artistid,
       albumid,
       tvshowid,
       setid,
       watchedepisodes,
       disc,
       tag,
       art,
       genreid,
       displayartist,
       albumartistid,
       description,
       theme,
       mood,
       style,
       albumlabel,
       sorttitle,
       episodeguide,
       uniqueid,
       dateadded,
       size,
       lastmodified,
       mimetype,
   }
   public class Files : List<FilesItem>
   {
         public static Files AllFields()
         {
             var items = Enum.GetValues(typeof (FilesItem));
             var list = new Files();
             list.AddRange(items.Cast<FilesItem>());
             return list;
         }
   }
}
