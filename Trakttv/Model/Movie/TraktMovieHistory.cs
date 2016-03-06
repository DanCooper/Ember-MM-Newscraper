using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Globalization;

namespace Trakttv.TraktAPI.Model
{

        [DataContract]
        public class TraktMovieHistory
        {
            [DataMember(Name = "id")]
            public string HistoryID { get; set; }

            [DataMember(Name = "action")]
            public string Action { get; set; }

            private string _watchedAt;
            [DataMember(Name = "watched_at")]
            public string WatchedAt
            {
              get
                {
                    return _watchedAt;
                }
              set
                {
                    _watchedAt = value;
                    DateTime tmpdate;
                    //traktv date is ISO 8601 format, convert to .NET date object first. i.e: 2014-08-15T21:45:00.000Z
                    DateTime.TryParseExact(value, @"yyyy-MM-dd\THH:mm:ss.fff\Z", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out tmpdate);
                    WatchedAt_DateTime = tmpdate;
                }
            }
          
            [DataMember(Name = "movie")]
            public TraktMovieSummary Movie { get; set; }

            public DateTime WatchedAt_DateTime { get; set; }

        }
}
