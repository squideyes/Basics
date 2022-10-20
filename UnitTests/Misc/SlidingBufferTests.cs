// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Basics;
using System.Linq;
using Xunit;

namespace SquidEyes.UnitTests;

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