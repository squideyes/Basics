// Copyright © 2021 by SquidEyes, LLC
// 
// Permission is hereby granted, free of charge, to any person obtaining 
// a copy of this software and associated documentation files (the “Software”),
// to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, 
// and/or sell copies of the Software, and to permit persons to whom the Software
// is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS 
// IN THE SOFTWARE.

using FluentAssertions;
using SquidEyes.Basics;
using System;
using System.Text.Json;
using Xunit;

namespace SquidEyes.UnitTests
{
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
}
