// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text.Json;
using System.Text.Json.Serialization;

namespace SquidEyes.Basics;

public class JsonStringArgSetConverter : JsonConverter<ArgSet>
{
    private readonly JsonSerializerOptions options;

    public JsonStringArgSetConverter()
    {
        options = new JsonSerializerOptions()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            NumberHandling = JsonNumberHandling.Strict,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            WriteIndented = false
        };

        options.Converters.Add(new JsonStringEnumConverter());
    }

    public override ArgSet Read(ref Utf8JsonReader reader,
        Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException();

        var args = new ArgSet();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
                return args;

            var (key, arg) = ReadArg(ref reader);

            args.Add(key!, arg);

            continue;
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer,
        ArgSet argSet, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        foreach (var (key, value) in argSet
            .Select(kv => (kv.Key, kv.Value.Value)))
        {
            writer.WriteStartObject();

            writer.WriteString("key", key.AsString());

            var type = value.GetType();

            writer.WriteString("type", type.ToTypeName());

            writer.WritePropertyName("value");

            if (type == typeof(ClientId))
            {
                writer.WriteStringValue(
                    ((ClientId)value).AsString());
            }
            else if (type == typeof(Email))
            {
                writer.WriteStringValue(
                    ((Email)value).AsString());
            }
            else if (type == typeof(Phone))
            {
                writer.WriteStringValue(((Phone)value).AsString());
            }
            else if (type == typeof(Quantity))
            {
                writer.WriteNumberValue(((Quantity)value).AsInt32());
            }
            else if (type == typeof(ShortId))
            {
                writer.WriteStringValue(
                    ((ShortId)value).AsString());
            }
            else if (type == typeof(Token))
            {
                writer.WriteStringValue(((Token)value).AsString());
            }
            else
            {
                writer.WriteRawValue(
                    JsonSerializer.Serialize(value, this.options));
            }

            writer.WriteEndObject();
        }

        writer.WriteEndArray();
    }

    private static (string Key, Arg Arg) ReadArg(ref Utf8JsonReader reader)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();

        static string ReadStringValue(
            ref Utf8JsonReader reader, string propertyName)
        {
            reader.Read();

            if (reader.GetString() != propertyName)
                throw new JsonException();

            reader.Read();

            return reader.GetString()!;
        }

        var key = ReadStringValue(ref reader, "key");

        var argType = Type.GetType(ReadStringValue(ref reader, "type"));

        reader.Read();

        if (reader.GetString() != "value")
            throw new JsonException();

        reader.Read();

        Arg arg;

        if (argType!.IsEnum)
        {
            arg = new Arg(Enum.Parse(argType, reader.GetString()!, true));
        }
        else
        {
            arg = argType!.FullName switch
            {
                "System.Boolean" 
                    => new Arg(reader.GetBoolean()),
                "System.Byte" => new Arg(reader.GetByte()),
                "SquidEyes.Basics.ClientId" =>
                    new Arg(ClientId.From(reader.GetString()!)),
                "System.DateTime" => new Arg(reader.GetDateTime()),
                "System.DateOnly" =>
                    new Arg(DateOnly.Parse(reader.GetString()!)),
                "System.Decimal" => new Arg(reader.GetDecimal()),
                "System.Double" => new Arg(reader.GetDouble()),
                "SquidEyes.Basics.Email" =>
                    new Arg(Email.From(reader.GetString()!)),
                "System.Guid" => new Arg(reader.GetGuid()),
                "System.Int16" => new Arg(reader.GetInt16()),
                "System.Int32" => new Arg(reader.GetInt32()),
                "System.Int64" => new Arg(reader.GetInt64()),
                "SquidEyes.Basics.Phone" =>
                    new Arg(Phone.From(reader.GetString()!)),
                "SquidEyes.Basics.Quantity" =>
                    new Arg(Quantity.From(reader.GetInt32())),
                "System.Single" => new Arg(reader.GetSingle()),
                "SquidEyes.Basics.ShortId" =>
                    new Arg(ShortId.From(reader.GetString()!)),
                "System.String" => new Arg(reader.GetString()!),
                "System.TimeOnly" =>
                    new Arg(TimeOnly.Parse(reader.GetString()!)),
                "System.TimeSpan" =>
                    new Arg(TimeSpan.Parse(reader.GetString()!)),
                "SquidEyes.Basics.Token" =>
                    new Arg(Token.From(reader.GetString()!)),
                "System.Uri" =>
                    new Arg(new Uri(reader.GetString()!)),
                _ => throw new JsonException()
            };
        }

        reader.Read();

        if (reader.TokenType != JsonTokenType.EndObject)
            throw new JsonException();

        return (key, arg);
    }
}