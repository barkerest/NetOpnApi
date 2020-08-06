using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NetOpnApi.JsonConverters
{
    /// <summary>
    /// The value is always a dictionary.  Will read from JSON objects, empty arrays, empty strings, and null.  Will write JSON objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AlwaysDictionary<T> : JsonConverter<Dictionary<string, T>>
    {
        public override Dictionary<string, T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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

            var ret = new Dictionary<string, T>();

            if (reader.TokenType == JsonTokenType.StartArray)
            {
                reader.Read();
                if (reader.TokenType != JsonTokenType.EndArray)
                {
                    throw new JsonException("Non-empty array where dictionary expected.");
                }

                return ret;
            }

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected object.");
            }

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return ret;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Expected property name.");
                }

                var key = reader.GetString();

                if (!reader.Read())
                {
                    throw new JsonException("Expected value.");
                }

                var val = JsonSerializer.Deserialize<T>(ref reader, options);

                ret.Add(key, val);
            }

            throw new JsonException("Missing end of object.");
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<string, T> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}
