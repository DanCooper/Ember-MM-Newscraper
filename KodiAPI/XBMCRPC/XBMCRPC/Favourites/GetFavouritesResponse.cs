using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Favourites
{
   public class GetFavouritesResponse
   {
       public global::System.Collections.Generic.List<XBMCRPC.Favourite.Details.Favourite> favourites { get; set; }
       public XBMCRPC.List.LimitsReturned limits { get; set; }
    }
}
