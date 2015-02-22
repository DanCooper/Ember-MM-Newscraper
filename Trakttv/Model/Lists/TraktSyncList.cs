using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Trakttv.TraktAPI.Model
{
    [DataContract]
    public class TraktSyncList
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "privacy")]
        public string Privacy { get; set; }

        [DataMember(Name = "display_numbers")]
        public bool DisplayNumbers { get; set; }

        [DataMember(Name = "allow_comments")]

        public bool AllowComments { get; set; }


        [DataMember(Name = "updated_at")]
        public string UpdatedAt { get; set; }

        [DataMember(Name = "item_count")]
        public int ItemCount { get; set; }

        [DataMember(Name = "likes")]
        public int Likes { get; set; }

        [DataMember(Name = "ids")]
        public TraktId Ids { get; set; }


        [DataMember(Name = "TraktListItems")]
        public List<TraktListItem> TraktListItems { get; set; }
        public List<TraktMovie> Movies { get; set; }

        [DataMember(Name = "list_modified")]

        public bool ListModified { get; set; }

        [DataMember(Name = "listitems_modified")]

        public bool ListItemsModified { get; set; }
        public bool ListDelete { get; set; }
        public bool NewList { get; set; }


    }
}
