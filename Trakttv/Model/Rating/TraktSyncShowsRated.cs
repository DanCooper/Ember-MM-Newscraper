using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TraktPlugin.TraktAPI.DataStructures
{
    [DataContract]
    public class TraktSyncShowsRated
    {
        [DataMember(Name = "shows")]
        public List<TraktSyncShowRated> Shows { get; set; }
    }
}
