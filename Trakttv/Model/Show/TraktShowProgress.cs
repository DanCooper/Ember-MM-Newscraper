using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Trakttv.TraktAPI.Model
{
    [DataContract]
    public class TraktShowProgress
    {

        [DataMember(Name = "aired")]
        public int Aired { get; set; }


        [DataMember(Name = "completed")]
        public int Completed { get; set; }


        [DataMember(Name = "seasons")]
        public Seasons SeasonsProgress { get; set; }

        [DataContract]
        public class Seasons
        {
            [DataMember(Name = "number")]
            public int Number { get; set; }

            [DataMember(Name = "aired")]
            public int Aired { get; set; }

            [DataMember(Name = "completed")]
            public int Completed { get; set; }

            [DataMember(Name = "episodes")]
            public List<Episode> Episodes { get; set; }

            [DataContract]
            public class Episode
            {
                [DataMember(Name = "number")]
                public int Number { get; set; }

                [DataMember(Name = "completed")]
                public bool Completed { get; set; }
            }
        }
    }
}
