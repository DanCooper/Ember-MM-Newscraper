using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.List
{
   public enum Sort_method
   {
       none,
       label,
       date,
       size,
       file,
       path,
       drivetype,
       title,
       track,
       time,
       artist,
       album,
       albumtype,
       genre,
       country,
       year,
       rating,
       votes,
       top250,
       programcount,
       playlist,
       episode,
       season,
       totalepisodes,
       watchedepisodes,
       tvshowstatus,
       tvshowtitle,
       sorttitle,
       productioncode,
       mpaa,
       studio,
       dateadded,
       lastplayed,
       playcount,
       listeners,
       bitrate,
       random,
   }
}
