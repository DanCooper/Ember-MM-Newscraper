using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.GUI.Property
{
   public class Value
   {
       public XBMCRPC.GUI.Property.Value_currentcontrol currentcontrol { get; set; }
       public XBMCRPC.GUI.Property.Value_currentwindow currentwindow { get; set; }
       public bool fullscreen { get; set; }
       public XBMCRPC.GUI.Property.Value_skin skin { get; set; }
       public XBMCRPC.GUI.Stereoscopy.Mode stereoscopicmode { get; set; }
    }
}
