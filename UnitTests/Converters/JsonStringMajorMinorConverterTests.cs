// ********************************************************
// Copyright (C) 2021 Louis S. Berman (louis@squideyes.com) 
// 
// This file is part of SquidEyes.Basics
// 
// The use of this source code is licensed under the terms 
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Basics;
using System.Text.Json;
using Xunit;

namespace SquidEyes.UnitTests;

public class JsonStringMajorMinorConverterTests
{
    private class Data
    {
        public MajorMinor? Version { get; set; }
    }

    [Theory]
    [InlineData("{}", null)]
    [InlineData("{\"Version\":\"1.2\"}", "1.2")]
    public void GoodJsonDeserializesCorrectly(string json, string? majorMinorString)
    {
        var options = new JsonSerializerOptions();

        options.Converters.Add(new JsonStringMajorMinorConverter());

        var data = JsonSerializer.Deserialize<Data>(json, options);

        if (majorMinorString == null)
            data!.Version.Should().BeNull();
        else
            MajorMinor.Parse(majorMinorString)!.Should().Be(new MajorMinor(1, 2));
    }

    [Fact]
    public void GoodDataSerializesCorrectly()
    {
        var options = new JsonSerializerOptions();

        options.Converters.Add(new JsonStringMajorMinorConverter());

        var data = new Data()
        {
            Version = new MajorMinor(1, 2)
        };

        var json = JsonSerializer.Serialize(data, options);

        json.Should().Be("{\"Version\":\"1.2\"}");
    }
}
