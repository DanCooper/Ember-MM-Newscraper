using System;
using System.Collections.Generic;
using System.Linq;
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
        specialsortseason,
        specialsortepisode,
        userrating,
        seasonid,
        ratings
    }
    public class Episode : List<EpisodeItem>
    {
        public static Episode AllFields()
        {
            var items = Enum.GetValues(typeof(EpisodeItem));
            var list = new Episode();
            list.AddRange(items.Cast<EpisodeItem>());
            return list;
        }
    }
}
