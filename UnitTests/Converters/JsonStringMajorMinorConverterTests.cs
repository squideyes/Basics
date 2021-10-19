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
using System.Text.Json;
using Xunit;

namespace SquidEyes.UnitTests
{
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
}
