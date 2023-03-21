// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text.Json;
using System.Text.Json.Serialization;

namespace SquidEyes.Basics;

public class JsonStringTokenConverter : JsonConverter<Token>
{
    public override Token Read(ref Utf8JsonReader reader,
        Type _, JsonSerializerOptions options)
    {
        return Token.From(reader.GetString()!);
    }

    public override void Write(Utf8JsonWriter writer,
        Token value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}