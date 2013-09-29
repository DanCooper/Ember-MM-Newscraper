﻿using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Trakttv.TraktAPI.Model
{
    [DataContract]
    public class TraktSeasonSeen : TraktShowSeen
    {
        [DataMember(Name = "season")]
        public int Season { get; set; }
    }

    [DataContract]
    public class TraktSeasonLibrary : TraktSeasonSeen { }
}
