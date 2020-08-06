using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NetOpnApi.JsonConverters
{
    /// <summary>
    /// The value is always an integer.  Will read from string or number, will write a number.
    /// </summary>
    public class AlwaysInt : JsonConverter<int>
    {
        private static int Parse(string s) => int.TryParse(s, out var i) ? i : (decimal.TryParse(s, out var d) ? (int) d : 0);

        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => reader.TokenType == JsonTokenType.Number
                   ? (reader.TryGetInt32(out var i)
                          ? i
                          : (reader.TryGetDecimal(out var d)
                                 ? (int) d
                                 : 0))
                   : Parse(reader.GetString());

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
            => writer.WriteNumberValue(value);
    }
}
