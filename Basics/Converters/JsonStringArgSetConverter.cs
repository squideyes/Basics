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

            if (type == typeof(Ratchet))
            {
                var ratchet = value.GetAs<Ratchet>();

                writer.WriteStartObject();
                writer.WriteString("Trigger",
                    ratchet.Trigger.ToString());
                writer.WriteString("MoveStopTo",
                    ratchet.MoveStopTo.ToString());
                writer.WriteEndObject();
            }
            else if (type == typeof(EmailAddress))
            {
                writer.WriteStringValue(
                    ((EmailAddress)value).AsString());
            }
            else if (type == typeof(Name))
            {
                writer.WriteStringValue(((Name)value).AsString());
            }
            else if (type == typeof(PhoneNumber))
            {
                writer.WriteStringValue(((PhoneNumber)value).AsString());
            }
            else if (type == typeof(Quantity))
            {
                writer.WriteNumberValue(((Quantity)value).AsInt32());
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

        string ReadStringValue(ref Utf8JsonReader reader, string propertyName)
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
        else if (argType == typeof(Ratchet))
        {
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException();

            static Offset ReadOffset(
                ref Utf8JsonReader reader, string propertyName)
            {
                reader.Read();

                if (reader.GetString() != propertyName)
                    throw new JsonException();

                reader.Read();

                return Offset.Parse(reader.GetString()!);
            }

            var trigger = ReadOffset(ref reader, "Trigger");

            var moveStopTo = ReadOffset(ref reader, "MoveStopTo");

            arg = new Arg(Ratchet.From(trigger, moveStopTo));

            reader.Read();

            if (reader.TokenType != JsonTokenType.EndObject)
                throw new JsonException();
        }
        else
        {
            arg = argType!.FullName switch
            {
                "SquidEyes.Basics.EmailAddress" =>
                    new Arg(EmailAddress.From(reader.GetString()!)),
                "SquidEyes.Basics.Name" =>
                    new Arg(Name.From(reader.GetString()!)),
                "SquidEyes.Basics.PhoneNumber" =>
                    new Arg(PhoneNumber.From(reader.GetString()!)),
                "SquidEyes.Basics.Quantity" =>
                    new Arg(Quantity.From(reader.GetInt32())),
                "System.Boolean" => new Arg(reader.GetBoolean()),
                "System.Byte" => new Arg(reader.GetByte()),
                "System.DateTime" => new Arg(reader.GetDateTime()),
                "System.DateOnly" =>
                    new Arg(DateOnly.Parse(reader.GetString()!)),
                "System.Decimal" => new Arg(reader.GetDecimal()),
                "System.Double" => new Arg(reader.GetDouble()),
                "System.Guid" => new Arg(reader.GetGuid()),
                "System.Int16" => new Arg(reader.GetInt16()),
                "System.Int32" => new Arg(reader.GetInt32()),
                "System.Int64" => new Arg(reader.GetInt64()),
                "System.Single" => new Arg(reader.GetSingle()),
                "System.String" => new Arg(reader.GetString()!),
                "System.TimeOnly" =>
                    new Arg(TimeOnly.Parse(reader.GetString()!)),
                "System.TimeSpan" =>
                    new Arg(TimeSpan.Parse(reader.GetString()!)),
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