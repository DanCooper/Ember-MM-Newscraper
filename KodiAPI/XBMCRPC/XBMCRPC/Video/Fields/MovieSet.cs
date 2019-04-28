using System;
using System.Collections.Generic;
using System.Linq;
namespace XBMCRPC.Video.Fields
{
    public enum MovieSetItem
    {
        title,
        playcount,
        fanart,
        thumbnail,
        art,
        plot
    }
    public class MovieSet : List<MovieSetItem>
    {
        public static MovieSet AllFields()
        {
            var items = Enum.GetValues(typeof(MovieSetItem));
            var list = new MovieSet();
            list.AddRange(items.Cast<MovieSetItem>());
            return list;
        }
    }
}
