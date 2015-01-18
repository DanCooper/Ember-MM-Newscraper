using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Trakttv.TraktAPI.Model
{
    [DataContract]
    public class TraktImage
    {
        [DataMember(Name = "full")]
        public string FullSize { get; set; }

        [DataMember(Name = "medium")]
        public string MediumSize { get; set; }

        [DataMember(Name = "thumb")]
        public string ThumbSize { get; set; }
    }
}
