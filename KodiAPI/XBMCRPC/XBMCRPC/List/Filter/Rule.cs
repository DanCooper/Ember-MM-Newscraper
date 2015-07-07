using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.List.Filter
{
   public class Rule
   {
       [Newtonsoft.Json.JsonProperty("operator")]
       public XBMCRPC.List.Filter.Operators Operator { get; set; }
       public object value { get; set; }
   public class Albums : XBMCRPC.List.Filter.Rule
   {
       public XBMCRPC.List.Filter.Fields.Albums field { get; set; }
    }
   public class Artists : XBMCRPC.List.Filter.Rule
   {
       public XBMCRPC.List.Filter.Fields.Artists field { get; set; }
    }
   public class Episodes : XBMCRPC.List.Filter.Rule
   {
       public XBMCRPC.List.Filter.Fields.Episodes field { get; set; }
    }
   public class Movies : XBMCRPC.List.Filter.Rule
   {
       public XBMCRPC.List.Filter.Fields.Movies field { get; set; }
    }
   public class MusicVideos : XBMCRPC.List.Filter.Rule
   {
       public XBMCRPC.List.Filter.Fields.MusicVideos field { get; set; }
    }
   public class Songs : XBMCRPC.List.Filter.Rule
   {
       public XBMCRPC.List.Filter.Fields.Songs field { get; set; }
    }
   public class TVShows : XBMCRPC.List.Filter.Rule
   {
       public XBMCRPC.List.Filter.Fields.TVShows field { get; set; }
    }
   public class Textures : XBMCRPC.List.Filter.Rule
   {
       public XBMCRPC.List.Filter.Fields.Textures field { get; set; }
    }
    }
}
