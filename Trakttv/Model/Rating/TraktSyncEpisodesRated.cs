using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TraktPlugin.TraktAPI.DataStructures
{
    [DataContract]
    public class TraktSyncEpisodesRated
    {
        [DataMember(Name = "episodes")]
        public List<TraktSyncEpisodeRated> Episodes { get; set; }
    }
}
