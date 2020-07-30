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
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => reader.TokenType == JsonTokenType.Number
                   ? reader.GetInt32()
                   : (int.TryParse(reader.GetString(), out var i) ? i : 0);

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
            => writer.WriteNumberValue(value);
    }
}
