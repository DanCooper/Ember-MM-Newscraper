using System.Runtime.Serialization;

namespace Trakttv.TraktAPI.Model
{
    [DataContract]
    public class TraktCommentBase
    {

        [DataMember(Name = "comment")]
        public string Text { get; set; }

        [DataMember(Name = "spoiler")]
        public bool IsSpoiler { get; set; }

    }
}
