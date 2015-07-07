using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Media
{
   public class Artwork
   {
       public string banner { get; set; }
       public string fanart { get; set; }
       public string poster { get; set; }
       public string thumb { get; set; }

       //2015/06/26 Cocotus, Added support for additional image types which are supported in KODI skins but not yet by INTROSPECT call (=current JSON format)
       public string clearart { get; set; }
       public string clearlogo { get; set; }
       public string discart { get; set; }
       public string landscape { get; set; }
       public string extrafanart { get; set; }
       public string extrathumbs { get; set; }
       public string characterart { get; set; }
      
   public class Set
   {
       public object banner { get; set; }
       public object fanart { get; set; }
       public object poster { get; set; }
       public object thumb { get; set; }

       //2015/06/26 Cocotus, Added support for additional image types which are supported in KODI skins but not yet by INTROSPECT call (=current JSON format)
       public string clearart { get; set; }
       public string clearlogo { get; set; }
       public string discart { get; set; }
       public string landscape { get; set; }
       public string extrafanart { get; set; }
       public string extrathumbs { get; set; }
       public string characterart { get; set; }
    }
    }
}
