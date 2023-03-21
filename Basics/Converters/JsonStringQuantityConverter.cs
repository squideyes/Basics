// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text.Json;
using System.Text.Json.Serialization;

namespace SquidEyes.Basics;

public class JsonStringQuantityConverter : JsonConverter<Quantity>
{
    public override Quantity Read(ref Utf8JsonReader reader,
        Type _, JsonSerializerOptions options)
    {
        return Quantity.From(reader.GetInt32());
    }

    public override void Write(Utf8JsonWriter writer,
        Quantity value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.AsInt32());
    }
}