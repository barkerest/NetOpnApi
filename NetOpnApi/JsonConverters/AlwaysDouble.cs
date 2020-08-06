using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NetOpnApi.JsonConverters
{
    /// <summary>
    /// The value is always a floating point number.  Will read from string or number, will write a number.
    /// </summary>
    public class AlwaysDouble : JsonConverter<double>
    {
        public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => reader.TokenType == JsonTokenType.Number
                   ? (reader.TryGetDouble(out var d) ? d : 0.0)
                   : (double.TryParse(reader.GetString(), out var d2) ? d2 : 0.0);

        public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
            => writer.WriteNumberValue(value);
    }
}
