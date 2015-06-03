using System.Runtime.Serialization;

namespace Trakttv.TraktAPI.Model
{
    [DataContract]
    public class TraktCommentMovie
    {

        [DataMember(Name = "movie")]
        public TraktMovie Movie { get; set; }

        [DataMember(Name = "comment")]
        public string Text { get; set; }

        [DataMember(Name = "spoiler")]
        public bool IsSpoiler { get; set; }

    }
}
