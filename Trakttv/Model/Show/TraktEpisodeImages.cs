using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Trakttv.TraktAPI.Model
{
    [DataContract]
    public class TraktEpisodeImages
    {
        [DataMember(Name = "screenshot")]
        public TraktImage ScreenShot { get; set; }
    }
}
