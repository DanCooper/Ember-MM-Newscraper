using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.List.Fields
{
   public enum AllItem
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
       channel,
       channeltype,
       hidden,
       locked,
       channelnumber,
       starttime,
       endtime,
   }
   public class All : List<AllItem>
   {
         public static All AllFields()
         {
             var items = Enum.GetValues(typeof (AllItem));
             var list = new All();
             list.AddRange(items.Cast<AllItem>());
             return list;
         }
   }
}
