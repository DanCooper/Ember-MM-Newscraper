using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Trakttv.TraktAPI.Model
{
    [DataContract]
    public class TraktPersonSummary : TraktPerson
    {
        [DataMember(Name = "images")]
        public TraktPersonImages Images { get; set; }

        [DataMember(Name = "biography")]
        public string Biography { get; set; }

        [DataMember(Name = "birthday")]
        public string Birthday { get; set; }

        [DataMember(Name = "death")]
        public string Death { get; set; }

        [DataMember(Name = "birthplace")]
        public string Birthplace { get; set; }

        [DataMember(Name = "homepage")]
        public string Homepage { get; set; }
    }
}
