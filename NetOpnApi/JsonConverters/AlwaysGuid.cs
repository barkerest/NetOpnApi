using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NetOpnApi.JsonConverters
{
    /// <summary>
    /// The value is always a GUID.  Will read from string and write to string.
    /// </summary>
    public class AlwaysGuid : JsonConverter<Guid>
    {
        public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null) return Guid.Empty;
            if (reader.TokenType == JsonTokenType.Number &&
                reader.GetInt32() == 0) return Guid.Empty;
            
            var s = reader.GetString();
            if (string.IsNullOrEmpty(s)) return Guid.Empty;
            if (s == "0") return Guid.Empty;

            return Guid.TryParse(s, out var g) ? g : Guid.Empty;
        }
            

        public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString("D"));
    }
}
