using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Trakttv.TraktAPI.Model
{
    [DataContract]
    public class TraktSynchronize
    {
        [DataMember(Name = "movies", EmitDefaultValue = false)]
        public List<TraktMovie> Movies { get; set; }

        [DataMember(Name = "shows", EmitDefaultValue = false)]
        public List<TraktShow> Shows { get; set; }

        [DataMember(Name = "seasons", EmitDefaultValue = false)]
        public List<TraktSeason> Seasons { get; set; }

        [DataMember(Name = "episodes", EmitDefaultValue = false)]
        public List<TraktEpisode> Episodes { get; set; }

        [DataMember(Name = "people", EmitDefaultValue = false)]
        public List<TraktPerson> People { get; set; }
    }
}