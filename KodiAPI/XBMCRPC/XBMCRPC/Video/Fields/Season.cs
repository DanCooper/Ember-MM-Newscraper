using System;
using System.Collections.Generic;
using System.Linq;
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
        userrating,
        title
    }
    public class Season : List<SeasonItem>
    {
        public static Season AllFields()
        {
            var items = Enum.GetValues(typeof(SeasonItem));
            var list = new Season();
            list.AddRange(items.Cast<SeasonItem>());
            return list;
        }
    }
}
