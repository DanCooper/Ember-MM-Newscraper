using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Trakttv.TraktAPI.Model
{
    [DataContract]
    public class TraktSeason
    {
        [DataMember(Name = "number")]
        public int Number { get; set; }

        [DataMember(Name = "ids")]
        public TraktSeasonId Ids { get; set; }
    }
}
