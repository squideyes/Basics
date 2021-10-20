using FluentAssertions;
using SquidEyes.Basics;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace SquidEyes.UnitTests
{
    public class JsonElementExtendersTests
    {
        private class Data
        {
            public string? String { get; } = "ABC123";
            public int Int32 { get; } = 123;
            public float Float { get; } = 123.0f;
            public double Double { get; } = 123.0;
            public bool Boolean { get; } = true;
            public Uri Uri { get; } = new Uri("http://someco.com");
            public UriKind Enum { get; } = UriKind.Absolute;
            public DateTime DateTime { get; } = new DateTime(1, 2, 3, 4, 5, 6, 7);
            public TimeSpan TimeSpan { get; } = TimeSpan.FromMinutes(123);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetStringWorks(bool goodPropertyName)
        {
            Validate(nameof(Data.String), goodPropertyName,
                (e, n) => e.GetString(n), d => d.String);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetBooleanWorks(bool goodPropertyName)
        {
            Validate(nameof(Data.Boolean), goodPropertyName,
                (e, n) => e.GetBoolean(n), d => d.Boolean);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetInt32Works(bool goodPropertyName)
        {
            Validate(nameof(Data.Int32), goodPropertyName,
                (e, n) => e.GetInt32(n), d => d.Int32);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetFloatWorks(bool goodPropertyName)
        {
            Validate(nameof(Data.Float), goodPropertyName,
                (e, n) => e.GetFloat(n), d => d.Float);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetDoubleWorks(bool goodPropertyName)
        {
            Validate(nameof(Data.Double), goodPropertyName,
                (e, n) => e.GetDouble(n), d => d.Double);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetUriWorks(bool goodPropertyName)
        {
            Validate(nameof(Data.Uri), goodPropertyName,
                (e, n) => e.GetUri(n), d => d.Uri);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetEnumWorks(bool goodPropertyName)
        {
            Validate(nameof(Data.Enum), goodPropertyName,
                (e, n) => e.GetEnumValue<UriKind>(n), d => d.Enum);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetDateTimeWorks(bool goodPropertyName)
        {
            Validate(nameof(Data.DateTime), goodPropertyName,
                (e, n) => e.GetDateTime(n), d => d.DateTime);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetTimeSpanWorks(bool goodPropertyName)
        {
            Validate(nameof(Data.TimeSpan), goodPropertyName,
                (e, n) => e.GetTimeSpan(n), d => d.TimeSpan);
        }

        private static void Validate<T>(string propertyName, bool goodPropertyName,
            Func<JsonElement, string, T> convert, Expression<Func<Data, T>> getData)
        {
            var options = new JsonSerializerOptions();

            options.Converters.Add(new JsonStringTimeSpanConverter());
            options.Converters.Add(new JsonStringEnumConverter());
            options.Converters.Add(new JsonStringDateTimeConverter());

            var doc = JsonDocument.Parse(
                JsonSerializer.Serialize(new Data(), options));

            try
            {
                var value = convert(doc.RootElement,
                    propertyName + (goodPropertyName ? "" : "XXX"));

                var func = getData.Compile();

                value.Should().Be(func(new Data()));
            }
            catch (KeyNotFoundException)
            {
                if (goodPropertyName)
                    throw;
            }
        }
    }
}
