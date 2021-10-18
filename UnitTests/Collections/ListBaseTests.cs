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
using Xunit;

namespace SquidEyes.UnitTests
{
    public class ListBaseTests
    {
        private class Codes : ListBase<string>
        {
            public void Add(string code)
            {
                Items.Add(code);
            }
        }

        [Fact]
        public void FullCoverageTestShouldWork()
        {
            var codes = new Codes();

            codes.Count.Should().Be(0);
            codes.FirstOrDefault().Should().Be(null);
            codes.LastOrDefault().Should().Be(null);

            codes.Add("AAA");
            codes.Add("BBB");
            codes.Add("CCC");

            codes.Count.Should().Be(3);

            codes.First().Should().Be("AAA");
            codes.FirstOrDefault().Should().Be("AAA");
            codes.Last().Should().Be("CCC");
            codes.LastOrDefault().Should().Be("CCC");

            int count = 0;

            codes.ForEach(c => count++);

            count.Should().Be(3);

            string.Join(",", codes).Should().Be("AAA,BBB,CCC");
        }
    }
}
