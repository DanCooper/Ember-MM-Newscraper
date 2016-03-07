using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Trakttv.TraktAPI.Model
{
    [DataContract]
    public class TraktSyncHistoryID
    {
        [DataMember(Name = "ids")]
        public int? Ids { get; set; }
    }
}
