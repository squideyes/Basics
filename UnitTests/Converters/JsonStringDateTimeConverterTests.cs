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

public class JsonStringDateTimeConverterTests
{
    private class Data
    {
        public DateTime? DateTime { get; set; }
    }

    [Theory]
    [InlineData("{}", null)]
    [InlineData("{\"DateTime\":\"2021-01-02T03:04:05.006Z\"}", "01/01/2021 22:04:05.006")]
    [InlineData("{\"DateTime\":\"2021-01-02T03:04:05.006\"}", "01/02/2021 03:04:05.006")]
    [InlineData("{\"DateTime\":\"2021-01-02\"}", "01/02/2021")]
    public void GoodJsonDeserializesCorrectly(string json, string dateTimeString)
    {
        var options = new JsonSerializerOptions();

        options.Converters.Add(new JsonStringDateTimeConverter());

        var data = JsonSerializer.Deserialize<Data>(json, options);

        if (dateTimeString == null)
            data!.DateTime.Should().BeNull();
        else
            DateTime.Parse(dateTimeString!).Should().Be(data!.DateTime);
    }

    [Fact]
    public void GoodDateTimeSerializesCorrectly()
    {
        var options = new JsonSerializerOptions();

        options.Converters.Add(new JsonStringDateTimeConverter());

        var data = new Data()
        {
            DateTime = new DateTime(1, 2, 3, 4, 5, 6, 7)
        };

        var json = JsonSerializer.Serialize(data, options);

        json.Should().Be("{\"DateTime\":\"0001-02-03T04:05:06.0070000\"}");
    }
}
