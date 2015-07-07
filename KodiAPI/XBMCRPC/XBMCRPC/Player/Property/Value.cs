using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Player.Property
{
   public class Value
   {
       public global::System.Collections.Generic.List<XBMCRPC.Player.Audio.Stream> audiostreams { get; set; }
       public bool canchangespeed { get; set; }
       public bool canmove { get; set; }
       public bool canrepeat { get; set; }
       public bool canrotate { get; set; }
       public bool canseek { get; set; }
       public bool canshuffle { get; set; }
       public bool canzoom { get; set; }
       public XBMCRPC.Player.Audio.Stream currentaudiostream { get; set; }
       public XBMCRPC.Player.Subtitle currentsubtitle { get; set; }
       public bool live { get; set; }
       public bool partymode { get; set; }
       public double percentage { get; set; }
       public int playlistid { get; set; }
       public int position { get; set; }
       public XBMCRPC.Player.Repeat repeat { get; set; }
       public bool shuffled { get; set; }
       public int speed { get; set; }
       public bool subtitleenabled { get; set; }
       public global::System.Collections.Generic.List<XBMCRPC.Player.Subtitle> subtitles { get; set; }
       public XBMCRPC.Global.Time time { get; set; }
       public XBMCRPC.Global.Time totaltime { get; set; }
       public XBMCRPC.Player.Type type { get; set; }
    }
}
