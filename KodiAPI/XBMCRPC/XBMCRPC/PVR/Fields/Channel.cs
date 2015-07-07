using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.PVR.Fields
{
   public enum ChannelItem
   {
       thumbnail,
       channeltype,
       hidden,
       locked,
       channel,
       lastplayed,
   }
   public class Channel : List<ChannelItem>
   {
         public static Channel AllFields()
         {
             var items = Enum.GetValues(typeof (ChannelItem));
             var list = new Channel();
             list.AddRange(items.Cast<ChannelItem>());
             return list;
         }
   }
}
