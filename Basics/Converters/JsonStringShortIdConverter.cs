// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text.Json;
using System.Text.Json.Serialization;

namespace SquidEyes.Basics;

public class JsonStringShortIdConverter : JsonConverter<ShortId>
{
    public override ShortId Read(ref Utf8JsonReader reader,
        Type _, JsonSerializerOptions options)
    {
        return ShortId.From(reader.GetString()!);
    }

    public override void Write(Utf8JsonWriter writer,
        ShortId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}