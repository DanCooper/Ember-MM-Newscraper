using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Notifications.Item
{
   public enum Type
   {
       unknown,
       movie,
       episode,
       musicvideo,
       song,
       picture,
       channel,
   }
}
