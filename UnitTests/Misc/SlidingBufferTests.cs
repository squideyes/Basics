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
using System.Linq;
using Xunit;

namespace SquidEyes.UnitTests
{
    public class SlidingBufferTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ShouldEnumerateReversedOrNot(bool reversed)
        {
            var buffer = GetSlidingBuffer(3, reversed);

            int index = 0;

            foreach (var number in buffer)
                number.Should().Be(buffer[index++]);
        }

        [Theory]
        [InlineData(1, false, 0)]
        [InlineData(1, true, 0)]
        [InlineData(2, false, 1)]
        [InlineData(2, true, 1)]
        public void ShouldAddReversedOrNot(int size, bool reversed, int expected)
        {
            var buffer = GetSlidingBuffer(size, reversed);

            buffer[reversed ? 0 : size - 1].Should().Be(expected);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ShouldUpdateReversedOrNot(bool reversed)
        {
            int SIZE = 3;

            var buffer = GetSlidingBuffer(SIZE, reversed);

            buffer[reversed ? 0 : SIZE - 1].Should().Be(2);

            buffer.Update(3);

            buffer[reversed ? 0 : SIZE - 1].Should().Be(3);
        }

        private static SlidingBuffer<int> GetSlidingBuffer(int size, bool reversed)
        {
            var buffer = new SlidingBuffer<int>(size, reversed);

            buffer.AddRange(Enumerable.Range(0, size));

            return buffer;
        }
    }
}
