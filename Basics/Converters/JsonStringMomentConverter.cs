// ********************************************************
// Copyright (C) 2021 Louis S. Berman (louis@squideyes.com)
//
// This file is part of SquidEyes.Basics
//
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text.Json;
using System.Text.Json.Serialization;

namespace SquidEyes.Basics;

public class JsonStringMomentConverter : JsonConverter<Moment>
{
    public override Moment Read(ref Utf8JsonReader reader,
        Type typeToConvert, JsonSerializerOptions options)
    {
        return Moment.Parse(reader.GetString()!);
    }

    public override void Write(Utf8JsonWriter writer,
        Moment value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}