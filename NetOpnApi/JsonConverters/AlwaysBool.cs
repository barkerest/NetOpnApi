using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NetOpnApi.JsonConverters
{
    public class AlwaysBool : JsonConverter<bool>
    {
        private static readonly string[] TrueValues =
        {
            "true",
            "t",
            "yes",
            "y",
            "1",
            "-1",
            "on"
        };

        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => reader.TokenType switch
            {
                JsonTokenType.True   => true,
                JsonTokenType.False  => false,
                JsonTokenType.Null   => false,
                JsonTokenType.Number => (reader.GetInt32() != 0),
                _                    => TrueValues.Contains(reader.GetString()?.ToLower())
            };
        
        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
            => writer.WriteBooleanValue(value);
    }
}
