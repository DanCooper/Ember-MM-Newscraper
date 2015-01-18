using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Trakttv.TraktAPI.Model
{
    [DataContract]
    public class TraktShowImages
    {
        [DataMember(Name = "fanart")]
        public TraktImage Fanart { get; set; }

        [DataMember(Name = "poster")]
        public TraktImage Poster { get; set; }

        [DataMember(Name = "logo")]
        public TraktImage Logo { get; set; }

        [DataMember(Name = "clearart")]
        public TraktImage ClearArt { get; set; }

        [DataMember(Name = "banner")]
        public TraktImage Banner { get; set; }

        [DataMember(Name = "thumb")]
        public TraktImage Thumb { get; set; }
    }
}
