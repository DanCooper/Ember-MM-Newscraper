using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Trakttv.TraktAPI.Model
{
    [DataContract]
    public class TraktSyncEpisodes
    {
        [DataMember(Name = "episodes")]
        public List<TraktEpisode> Episodes { get; set; }
    }
}
