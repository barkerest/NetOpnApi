using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NetOpnApi.JsonConverters
{
    /// <summary>
    /// The value is always a long integer.  Will read from string or number, will write a number.
    /// </summary>
    public class AlwaysLong : JsonConverter<long>
    {
        private static long Parse(string s) => long.TryParse(s, out var i) ? i : (decimal.TryParse(s, out var d) ? (long) d : 0);

        public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => reader.TokenType == JsonTokenType.Number
                   ? (reader.TryGetInt64(out var i)
                          ? i
                          : (reader.TryGetDecimal(out var d)
                                 ? (long) d
                                 : 0))
                   : Parse(reader.GetString());

        public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
            => writer.WriteNumberValue(value);
    }
}
