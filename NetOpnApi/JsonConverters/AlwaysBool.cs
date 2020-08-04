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

        private static readonly string[] FalseValues =
        {
            "false",
            "f",
            "no",
            "n",
            "0",
            "off",
            "n/a",
            ""
        };

        private static bool ToBool(string s)
        {
            s = (s?.Trim().ToLower()) ?? "";
            if (TrueValues.Contains(s)) return true;
            if (FalseValues.Contains(s)) return false;
            throw new InvalidCastException($"The value \"{s}\" is not a recognized boolean value.");
        }
        
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => reader.TokenType switch
            {
                JsonTokenType.True   => true,
                JsonTokenType.False  => false,
                JsonTokenType.Null   => false,
                JsonTokenType.Number => (reader.GetInt32() != 0),
                _                    => ToBool(reader.GetString())
            };
        
        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
            => writer.WriteBooleanValue(value);
    }
}
