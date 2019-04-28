using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.List.Item
{
    using Newtonsoft.Json;
    [Newtonsoft.Json.JsonConverter(typeof(ListItemBaseConverter))]
   public class Base
   {
       public string album { get; set; }
       public global::System.Collections.Generic.List<string> albumartist { get; set; }
       public global::System.Collections.Generic.List<int> albumartistid { get; set; }
       public int albumid { get; set; }
       public string albumlabel { get; set; }
       public global::System.Collections.Generic.List<XBMCRPC.Video.Cast> cast { get; set; }
       public string comment { get; set; }
       public global::System.Collections.Generic.List<string> country { get; set; }
       public string description { get; set; }
       public int disc { get; set; }
       public int duration { get; set; }
       public int episode { get; set; }
       public string episodeguide { get; set; }
       public string firstaired { get; set; }
       public int id { get; set; }
       public string imdbnumber { get; set; }
       public string lyrics { get; set; }
       public global::System.Collections.Generic.List<string> mood { get; set; }
       public string mpaa { get; set; }
       public string musicbrainzartistid { get; set; }
       public string musicbrainztrackid { get; set; }
       public string originaltitle { get; set; }
       public string plotoutline { get; set; }
       public string premiered { get; set; }
       public string productioncode { get; set; }
       public int season { get; set; }
       public string set { get; set; }
       public int setid { get; set; }
       public global::System.Collections.Generic.List<string> showlink { get; set; }
       public string showtitle { get; set; }
       public string sorttitle { get; set; }
       public global::System.Collections.Generic.List<string> studio { get; set; }
       public global::System.Collections.Generic.List<string> style { get; set; }
       public global::System.Collections.Generic.List<string> tag { get; set; }
       public string tagline { get; set; }
       public global::System.Collections.Generic.List<string> theme { get; set; }
       public int top250 { get; set; }
       public int track { get; set; }
       public string trailer { get; set; }
       public int tvshowid { get; set; }
       public XBMCRPC.List.Item.Base_type type { get; set; }
       public XBMCRPC.List.Item.Base_uniqueid uniqueid { get; set; }
       public string votes { get; set; }
       public int watchedepisodes { get; set; }
       public global::System.Collections.Generic.List<string> writer { get; set; }
       [Newtonsoft.Json.JsonIgnore]
       public XBMCRPC.Video.Details.File AsVideoDetailsFile  { get; set; }
       [Newtonsoft.Json.JsonIgnore]
       public XBMCRPC.Audio.Details.Media AsAudioDetailsMedia  { get; set; }
       [Newtonsoft.Json.JsonIgnore]
       public XBMCRPC.Media.Details.Base AsMediaDetailsBase  { get; set; }
    }
    internal class ListItemBaseConverter : Newtonsoft.Json.Converters.CustomCreationConverter<XBMCRPC.List.Item.Base>
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var jObject = (JObject)serializer.Deserialize(reader);
            var localReader = new JTokenReader(jObject);
            var val = (XBMCRPC.List.Item.Base)base.ReadJson(localReader, objectType, existingValue, serializer);

            localReader = new JTokenReader(jObject);
            val.AsVideoDetailsFile = serializer.Deserialize<XBMCRPC.Video.Details.File>(localReader);
            localReader = new JTokenReader(jObject);
            val.AsAudioDetailsMedia = serializer.Deserialize<XBMCRPC.Audio.Details.Media>(localReader);
            localReader = new JTokenReader(jObject);
            val.AsMediaDetailsBase = serializer.Deserialize<XBMCRPC.Media.Details.Base>(localReader);

            return val;
        }

		  public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(XBMCRPC.List.Item.Base);
        }

        public override XBMCRPC.List.Item.Base Create(Type objectType)
        {
            return (XBMCRPC.List.Item.Base) Activator.CreateInstance(objectType);
        }
    }
}
