using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.GUI
{
   public enum SetStereoscopicMode_mode
   {
       toggle,
       tomono,
       next,
       previous,
       select,
       off,
       split_vertical,
       split_horizontal,
       row_interleaved,
       hardware_based,
       anaglyph_cyan_red,
       anaglyph_green_magenta,
       monoscopic,
   }
}
