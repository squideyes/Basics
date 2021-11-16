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
using System;
using System.Text.Json;
using Xunit;

namespace SquidEyes.UnitTests;

public class JsonStringDateOnlyConverterTests
{
    private class Data
    {
        public DateOnly? DateOnly { get; set; }
    }

    [Theory]
    [InlineData("{}", null)]
    [InlineData("{\"DateOnly\":\"2021-01-02\"}", "01/02/2021")]
    public void GoodJsonDeserializesCorrectly(string json, string dateOnlyString)
    {
        var options = new JsonSerializerOptions();

        options.Converters.Add(new JsonStringDateOnlyConverter());

        var data = JsonSerializer.Deserialize<Data>(json, options);

        if (dateOnlyString == null)
            data!.DateOnly.Should().BeNull();
        else
            DateOnly.Parse(dateOnlyString!).Should().BeEquivalentTo(data!.DateOnly);
    }

    [Fact]
    public void GoodDateTimeSerializesCorrectly()
    {
        var options = new JsonSerializerOptions();

        options.Converters.Add(new JsonStringDateOnlyConverter());

        var data = new Data()
        {
            DateOnly = new DateOnly(1, 2, 3)
        };

        var json = JsonSerializer.Serialize(data, options);

        json.Should().Be("{\"DateOnly\":\"0001-02-03\"}");
    }
}
