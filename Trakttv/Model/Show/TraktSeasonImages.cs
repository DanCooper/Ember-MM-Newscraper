using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Trakttv.TraktAPI.Model
{
    [DataContract]
    public class TraktSeasonImages
    {
        [DataMember(Name = "poster")]
        public TraktImage Poster { get; set; }
        
        [DataMember(Name = "thumb")]
        public TraktImage Thumb { get; set; }
    }
}
