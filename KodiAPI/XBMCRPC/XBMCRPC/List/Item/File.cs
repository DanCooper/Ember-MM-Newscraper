using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.List.Item
{
   public class File : XBMCRPC.List.Item.Base
   {
       public string file { get; set; }
       public XBMCRPC.List.Item.File_filetype filetype { get; set; }
       public string lastmodified { get; set; }
       public string mimetype { get; set; }
       public int size { get; set; }
    }
}
