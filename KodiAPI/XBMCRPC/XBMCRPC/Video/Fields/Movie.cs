using System;
using System.Collections.Generic;
using System.Linq;
namespace XBMCRPC.Video.Fields
{
    public enum MovieItem
    {
        title,
        genre,
        year,
        rating,
        director,
        trailer,
        tagline,
        plot,
        plotoutline,
        originaltitle,
        lastplayed,
        playcount,
        writer,
        studio,
        mpaa,
        cast,
        country,
        imdbnumber,
        runtime,
        set,
        showlink,
        streamdetails,
        top250,
        votes,
        fanart,
        thumbnail,
        file,
        sorttitle,
        resume,
        setid,
        dateadded,
        tag,
        art,
        userrating,
        ratings,
        premiered,
        uniqueid
    }
    public class Movie : List<MovieItem>
    {
        public static Movie AllFields()
        {
            var items = Enum.GetValues(typeof(MovieItem));
            var list = new Movie();
            list.AddRange(items.Cast<MovieItem>());
            return list;
        }
    }
}
