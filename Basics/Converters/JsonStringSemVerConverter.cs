// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text.Json;
using System.Text.Json.Serialization;

namespace SquidEyes.Basics;

public class JsonStringSemVerConverter : JsonConverter<SemVer>
{
    public override SemVer Read(ref Utf8JsonReader reader,
        Type _, JsonSerializerOptions options)
    {
        return SemVer.Parse(reader.GetString()!);
    }

    public override void Write(Utf8JsonWriter writer,
        SemVer value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}