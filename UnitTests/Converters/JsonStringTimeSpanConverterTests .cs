// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Basics;
using System;
using System.Text.Json;
using Xunit;

namespace SquidEyes.UnitTests;

public class JsonStringTimeSpanConverterTests
{
    private class Data
    {
        public TimeSpan TimeSpan { get; set; }
    }

    [Theory]
    [InlineData("{}", null)]
    [InlineData("{\"TimeSpan\":\"1:02:03:05.005\"}", "1:02:03:05.005")]
    public void GoodJsonDeserializesCorrectly(string json, string dateTimeString)
    {
        var options = new JsonSerializerOptions();

        options.Converters.Add(new JsonStringTimeSpanConverter());

        var data = JsonSerializer.Deserialize<Data>(json, options);

        if (dateTimeString == null)
            data!.TimeSpan.Should().Be(default);
        else
            TimeSpan.Parse(dateTimeString!).Should().Be(data!.TimeSpan);
    }

    [Fact]
    public void GoodDataSerializesCorrectly()
    {
        var options = new JsonSerializerOptions();

        options.Converters.Add(new JsonStringDateTimeConverter());

        var data = new Data()
        {
            TimeSpan = new TimeSpan(1, 2, 3, 4, 5)
        };

        var json = JsonSerializer.Serialize(data, options);

        json.Should().Be("{\"TimeSpan\":\"1.02:03:04.0050000\"}");
    }
}