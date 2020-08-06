using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NetOpnApi.JsonConverters
{
    /// <summary>
    /// The value is always an array.  Will read from JSON arrays, empty strings, and null.  Will write JSON arrays.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AlwaysArray<T> : JsonConverter<T[]>
    {
        public override T[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            if (reader.TokenType == JsonTokenType.String &&
                reader.GetString() == "")
            {
                return null;
            }

            var ret = new List<T>();

            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException("Expected array.");
            }

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    return ret.ToArray();
                }

                var val = JsonSerializer.Deserialize<T>(ref reader, options);
                ret.Add(val);
            }

            throw new JsonException("Missing end of array.");
        }

        public override void Write(Utf8JsonWriter writer, T[] value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}
