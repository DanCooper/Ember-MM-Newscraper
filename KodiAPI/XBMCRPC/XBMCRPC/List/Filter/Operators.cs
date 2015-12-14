using System;
using System.Linq;
using Newtonsoft.Json;

namespace XBMCRPC.List.Filter
{
    [JsonConverter(typeof(OperatorEnumConverter))]
    public enum Operators
   {
       contains,
       doesnotcontain,
       Is,
       isnot,
       startswith,
       endswith,
       greaterthan,
       lessthan,
       after,
       before,
       inthelast,
       notinthelast,
       True,
       False,
       between,
   }
}

public class OperatorEnumConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        XBMCRPC.List.Filter.Operators op = (XBMCRPC.List.Filter.Operators)value;
        writer.WriteValue(op.ToString().ToLower());
    }
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var values = Enum.GetNames(typeof(XBMCRPC.List.Filter.Operators));
        var enumValue = values.FirstOrDefault(v => v.ToLower().Equals(reader.Value.ToString()));
        XBMCRPC.List.Filter.Operators op;
        if (Enum.TryParse(enumValue, out op))
        {
            return op;
        }
        return null;
    }
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(string);
    }
}
