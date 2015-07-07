using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Player
{
   public class GetActivePlayersResponseItem
   {
       public int playerid { get; set; }
       public XBMCRPC.Player.Type type { get; set; }
    }
}
